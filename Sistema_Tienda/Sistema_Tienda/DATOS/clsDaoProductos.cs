using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_Tienda.POJOS;
using MySql.Data.MySqlClient;

namespace Sistema_Tienda.DATOS
{
    class clsDaoProductos
    {
        public bool EliminarProducto(clsProductos producto)
        {
            /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
            MySqlConnection cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; database=sistema_tienda; user=root; pwd=root";
            cn.Open();

            /// ELIMINAR EL REGISTRO MEDIANTE UN COMANDO
            string strSQL = "delete from productos where Clave = " + producto.Clave;
            MySqlCommand comando = new MySqlCommand(strSQL, cn);
            comando.ExecuteNonQuery();

            /// FINALIZAMOS LA CONEXION CERRAMOS TODO
            comando.Dispose();
            cn.Close();
            cn.Dispose();

            return true;
        }

        public bool AgregarProducto(clsProductos producto)
        {
            
            return true;
        }

    }
}
