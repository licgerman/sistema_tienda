using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_Tienda.POJOS;
using Sistema_Tienda.DATOS;

namespace Sistema_Tienda.PRESENTACION
{
    public partial class frmAgregarProducto : Form
    {
        public frmAgregarProducto()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            clsProductos producto = new clsProductos();
            producto.Clave = Convert.ToInt32( txtClave.Text );
            producto.Nombre = txtNombre.Text;
            producto.Precio = Convert.ToDouble(txtPrecio.Text);

            clsDaoProductos objProducto = new clsDaoProductos();
            if (objProducto.AgregarProducto(producto))
            {
                MessageBox.Show("Producto Agregado", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmAgregarProducto_Load(object sender, EventArgs e)
        {

        }
    }
}
