using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsW
{
    public partial class AddProducto : Form
    {
        private readonly HttpClient _httpClient;

        public AddProducto()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7295/api/");
        }

        private async void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // Recolectar datos del formulario
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            decimal precio;

            // Validar precio
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Por favor, ingrese un precio válido.");
                return;
            }

            // Crear objeto producto
            var nuevoProducto = new Producto
            {
                nombre = nombre,
                descripción = descripcion,
                precio = precio
            };

            try
            {
                // Convertir el objeto a JSON
                string json = JsonConvert.SerializeObject(nuevoProducto);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                // Enviar solicitud POST
                HttpResponseMessage response = await _httpClient.PostAsync("Producto", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Producto agregado exitosamente.");
                    this.Close(); // Cerrar el formulario
                }
                else
                {
                    MessageBox.Show("Error al agregar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}");
            }
        }

        // Clase Producto para el envío de datos
        public class Producto
        {
            public string nombre { get; set; }
            public string descripción { get; set; }
            public decimal precio { get; set; }
        }

    }
}
