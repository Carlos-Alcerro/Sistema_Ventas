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

namespace FormsW
{
    public partial class frmRegister : Form
    {
        private readonly HttpClient _httpClient;
        private readonly Form1 _loginForm;
        public frmRegister(Form1 loginForm)
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7295/api/");
            _loginForm = loginForm;
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Crea un objeto para enviar los datos del registro
                var registerRequest = new
                {
                    nombres = txtNombres.Text.Trim(),
                    apellidos = txtApellidos.Text.Trim(),
                    sexo = txtSexo.Text.Trim(),
                    email = txtCorreo.Text.Trim(),
                    contrasena = txtPassword.Text.Trim()
                };

                // Serializa el objeto a JSON
                string json = JsonConvert.SerializeObject(registerRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Envía la solicitud POST a la API
                HttpResponseMessage response = await _httpClient.PostAsync("Usuario/register", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Registro exitoso.");
                    this.Close(); // Cierra el formulario después de un registro exitoso
                    _loginForm.Show(); // Muestra el formulario de inicio de sesión
                }
                else
                {
                    MessageBox.Show("Error al registrar: " + await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public class RegisterRequest
        {
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Sexo { get; set; }
            public string Email { get; set; }
            public string Contrasena { get; set; }
        }
    }
}
