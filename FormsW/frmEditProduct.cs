using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using static FormsW.frmProductos;

namespace FormsW
{
    public partial class frmEditProduct : Form
    {
        private readonly Producto _product;
        private readonly HttpClient _httpClient;

        public frmEditProduct(Producto product, HttpClient httpClient)
        {
            InitializeComponent();
            _product = product;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7189/api/");

            // Inicializar campos del formulario con datos del producto
            txtNombre.Text = product.nombre;
            txtDescripcion.Text = product.descripción;
            txtPrecio.Text = product.precio.ToString();
        }

        //boton para editar productos
        private async void btnEditar_Click(object sender, EventArgs e)
        {
            _product.nombre = txtNombre.Text;
            _product.descripción = txtDescripcion.Text;
            if (decimal.TryParse(txtPrecio.Text, out var precio))
            {
                _product.precio = precio;

                var json = JsonConvert.SerializeObject(_product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync($"Producto/{_product.id}", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Producto actualizado con éxito.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el producto.");
                }
            }
            else
            {
                MessageBox.Show("Precio no válido.");
            }
        }
    }
}
