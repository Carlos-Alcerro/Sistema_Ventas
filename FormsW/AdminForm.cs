using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsW
{
    public partial class AdminForm : Form
    {
        private Form1 _loginForm;

        public AdminForm(Form1 loginForm)
        {
            InitializeComponent();
            _loginForm = loginForm;
        }

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            _loginForm.Show();
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            frmProductos productos = new frmProductos();
            productos.FormClosed += (s, args) => this.Show(this);
            productos.Show();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.FormClosed += (s, args) => this.Show(this);
            usuarios.Show();
        }
    }
}
