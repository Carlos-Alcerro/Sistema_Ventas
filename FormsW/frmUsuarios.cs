using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsW
{
    public partial class frmUsuarios : Form
    {
        private readonly HttpClient _httpClient;

        public frmUsuarios()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7295/api/");
        }

        private async void frmUsuarios_Load(object sender, EventArgs e)
        {
            await CargarUsuarios();
        }

        private async Task CargarUsuarios()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("Usuario/users");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(responseContent);

                    // Configura el DataGridView
                    dataGridView1.DataSource = usuarios;
                }
                else
                {
                    MessageBox.Show("Error al cargar usuarios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}");
            }
        }

        public class Usuario
        {
            public int Id { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Sexo { get; set; }
            public string email { get; set; }
        }
    }
}
