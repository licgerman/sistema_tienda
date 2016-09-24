using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Tienda.DATOS;
using Sistema_Tienda.POJOS;


namespace Sistema_Tienda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PRESENTACION.frmAgregarProducto X = new PRESENTACION.frmAgregarProducto();
            X.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            clsProductos p = new clsProductos();
            p.Clave = int.Parse(txtClave.Text);

            clsDaoProductos objetoProducto= new clsDaoProductos();
            bool x = objetoProducto.EliminarProducto(p);
            MessageBox.Show(x.ToString());
        }
    }
}
