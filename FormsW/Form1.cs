using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IdentityModel.Tokens.Jwt;


namespace FormsW
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7295/api/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                var loginRequest = new
                {
                    Email = txtUser.Text.Trim(),
                    Contrasena = txtPassword.Text.Trim()
                };

                string json = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("Usuario/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var authResponse = JsonConvert.DeserializeObject<AutenticacionResponse>(responseContent);

                    if (authResponse.Token != null && !IsTokenExpired(authResponse.Token))
                    {

                        if (authResponse.Usuario.RolId == 1)
                        {
                            OpenForm(new AdminForm(this));
                        }
                        else if (authResponse.Usuario.RolId == 2)
                        {
                            OpenForm(new ClientForm());
                        }
                        else
                        {
                            MessageBox.Show("Rol de usuario no reconocido.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El token ha expirado.");
                    }
                }
                else
                {
                    MessageBox.Show("Credenciales Incorrectas");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al autenticar: {ex.Message}");
            }
        }
        private void OpenForm(Form form)
        {
            this.Hide();
            form.FormClosed += (s, args) => this.Show();
            form.Show();
        }

        private bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiry = jwtToken.Payload.Exp;

            if (expiry.HasValue)
            {
                var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expiry.Value).UtcDateTime;
                return DateTime.UtcNow > expirationDate;
            }
            return true;
        }

        public class AutenticacionResponse
        {
            public Usuario Usuario { get; set; }
            public string Token { get; set; }
        }

        public class Usuario
        {
            public int Id { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public int RolId { get; set; }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frmRegister register = new frmRegister(this);
            register.FormClosed += (s, args) => this.Show();
            register.Show();
        }
    }
}
