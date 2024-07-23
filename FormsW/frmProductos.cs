using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsW
{
    public partial class frmProductos : Form
    {
        private readonly HttpClient _httpClient;

        public frmProductos()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7295/api/");
        }

        private async void frmProductos_Load(object sender, EventArgs e)
        {
            await CargarProductos();
        }

        private async Task CargarProductos()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("Producto");
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var products = JsonConvert.DeserializeObject<List<Producto>>(responseContent);

                    dataGridView1.DataSource = products;

                    // Agregar columnas para Editar y Eliminar
                    AddActionButtons();
                }
                else
                {
                    MessageBox.Show("Error al cargar productos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos: {ex.Message}");
            }
        }

        private void AddActionButtons()
        {
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            btnEdit.Name = "BotonEditar";
            btnEdit.HeaderText = "Editar";
            btnEdit.Text = "Editar";
            btnEdit.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnEdit);

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            btnDelete.Name = "BotonEliminar";
            btnDelete.HeaderText = "Eliminar";
            btnDelete.Text = "Eliminar";
            btnDelete.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btnDelete);
        }

        private async void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica que el índice de la fila sea valido
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                if (e.ColumnIndex >= 0 && e.ColumnIndex < dataGridView1.Columns.Count)
                {
                    // Obtén el nombre de la columna
                    var columnName = dataGridView1.Columns[e.ColumnIndex].Name;

                    // Verifica si el clic se realizo en la columna de edicion
                    if (columnName == "BotonEditar")
                    {
                        var productoSeleccionado = dataGridView1.Rows[e.RowIndex].DataBoundItem as Producto;
                        if (productoSeleccionado != null)
                        {
                            var editForm = new frmEditProduct(productoSeleccionado, _httpClient);
                            editForm.ShowDialog();
                            await CargarProductos();
                        }
                    }
                    // Verifica si el clic se realizo en la columna de eliminación
                    else if (columnName == "BotonEliminar")
                    {
                        var productoSeleccionado = dataGridView1.Rows[e.RowIndex].DataBoundItem as Producto;
                        if (productoSeleccionado != null)
                        {
                            var result = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmación", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                await DeleteProductAsync(productoSeleccionado.id);
                                await CargarProductos();
                            }
                        }
                    }
                }
            }
        }


        private async Task DeleteProductAsync(int productId)
        {

            try
            {
                // Crea una solicitud DELETE a la API
                HttpResponseMessage response = await _httpClient.DeleteAsync($"Producto/{productId}");

                // Verifica si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Producto eliminado correctamente.");
                }
                else
                {
                    MessageBox.Show("Error al eliminar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el producto: {ex.Message}");
            }
        }

        public class Producto
        {
            public int id { get; set; }
            public string nombre { get; set; }
            public string descripción { get; set; }
            public decimal precio { get; set; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            AddProducto producto = new AddProducto();
            producto.ShowDialog();
            CargarProductos();
        }
    }
}
