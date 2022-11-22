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
            MySqlCommand comando = new MySqlCommand();
            try
            {
                cn.ConnectionString = "server=localhost; database=sistema_tienda; user=contador; pwd=abc123";
                cn.Open();

                /// ELIMINAR EL REGISTRO MEDIANTE UN COMANDO
                string strSQL = "delete from productos where Clave = " + producto.Clave;
                comando = new MySqlCommand(strSQL, cn);
                comando.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                /// FINALIZAMOS LA CONEXION CERRAMOS TODO
                comando.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return true;
        }

        public bool AgregarProducto(clsProductos producto)
        {

            /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
            MySqlConnection cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; database=sistema_tienda; user=root; pwd=root";
            cn.Open();

            /// AGREGAR EL REGISTRO A LA BASE DE DATOS
            string strSQL = "insert into productos values (@Clave, @Nombre, @precio, @Foto)";
            MySqlCommand comando = new MySqlCommand(strSQL, cn);
            comando.Parameters.AddWithValue("Clave", producto.Clave);
            comando.Parameters.AddWithValue("Nombre", producto.Nombre);
            comando.Parameters.AddWithValue("Precio", producto.Precio);
            comando.Parameters.AddWithValue("Foto", producto.Foto);
            comando.ExecuteNonQuery();

            /// FINALIZAMOS LA CONEXION CERRAMOS TODO
            comando.Dispose();
            cn.Close();
            cn.Dispose();

            return true;
        }

        /// <summary>
        /// Agregar un producto utilizando un STORE PROCEDURE
        /// </summary>
        /// <param name="producto">Objeto que contiene todos los datos a guardar</param>
        /// <returns>true: guardó sin problemas, false: no pudo guardar</returns>
        public bool AgregarProductoSP(clsProductos producto)
        {
            /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
            MySqlConnection cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; database=sistema_tienda; user=root; pwd=root";
            cn.Open();

            /// AGREGAR EL REGISTRO A LA BASE DE DATOS
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = "insertaProducto";
            comando.Connection = cn;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("Clave", producto.Clave);
            comando.Parameters.AddWithValue("Nombre", producto.Nombre);
            comando.Parameters.AddWithValue("Precio", producto.Precio);
            MySqlDataReader r = comando.ExecuteReader();
            
            while(r.Read())
            {
                clsProductos p = new clsProductos();
                p.Nombre = r.GetString(0);
            }

            /// FINALIZAMOS LA CONEXION CERRAMOS TODO
            comando.Dispose();
            cn.Close();
            cn.Dispose();

            return true;
        }

        /// <summary>
        /// Metodo para eliminar mediante procedimientos almacenados
        /// </summary>
        /// <param name="clave">Clave del producto a eliminar</param>
        /// <returns>Regresa TRUE si pudo elimnar</returns>
        public bool EliminarProductoSP(int  clave)
        {
            /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
            MySqlConnection cn = new MySqlConnection();
            cn.ConnectionString = "server=localhost; database=sistema_tienda; user=root; pwd=root";
            cn.Open();

            /// AGREGAR EL REGISTRO A LA BASE DE DATOS
            MySqlCommand comando = new MySqlCommand();
            comando.CommandText = "eliminarProducto";
            comando.Connection = cn;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("id", clave);
            comando.ExecuteNonQuery();

            /// FINALIZAMOS LA CONEXION CERRAMOS TODO
            comando.Dispose();
            cn.Close();
            cn.Dispose();

            return true;
        }

        /// <summary>
        /// Método que permite almacenar varios productos en uno solo procedimiento
        /// Utiliza transacciones
        /// </summary>
        /// <param name="ListaProductos">Lista de productos a guardar</param>
        /// <returns></returns>
        public bool AgregarVariosProductos(List<clsProductos> ListaProductos)
        {
            MySqlConnection cn = new MySqlConnection();
            /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
            cn.ConnectionString = "server=localhost; database=sistema_tienda; user=root; pwd=root";
            cn.Open();
            MySqlTransaction trans = cn.BeginTransaction();
            try
            {                           
                foreach (clsProductos producto in ListaProductos)
                {
                    /// AGREGAR PRODUCTO POR PRODUCTO A LA BASE DE DATOS
                    string strSQL = "insert into productos values (@Clave, @Nombre, @precio, @Foto)";
                    MySqlCommand comando = new MySqlCommand(strSQL, cn);
                    comando.Parameters.AddWithValue("Clave", producto.Clave);
                    comando.Parameters.AddWithValue("Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("Precio", producto.Precio);
                    comando.Parameters.AddWithValue("Foto", producto.Foto);
                    comando.ExecuteNonQuery();
                    comando.Dispose();
                }
                trans.Commit();
                return true;
            }
            catch
            {
                // SE DESHACE LA TRANSACCIÓN SI OCURRE UN ERROR
                trans.Rollback();                
                return false;
            }
            finally
            {
                /// FINALIZAMOS LA CONEXION CERRAMOS TODO
                cn.Close();
                cn.Dispose();
            }
        }



        public List<clsProductos> ObtenerProductos()
        {
            MySqlConnection cn = new MySqlConnection();
            /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
            cn.ConnectionString = "server=localhost; database=sistema_tienda; user=root; pwd=root";
            cn.Open();

                List<clsProductos> productos=new List<clsProductos>();                
                string strSQL = "select * from productos";                                               
                MySqlCommand comando = new MySqlCommand(strSQL, cn);
                MySqlDataReader dr = comando.ExecuteReader();
                while(dr.Read())
                {
                    clsProductos x = new clsProductos();
                    x.Clave = dr.GetInt32("clave");
                    x.Nombre = dr.GetString("nombre");
                    x.Precio = dr.GetDouble("precio");
                    productos.Add(x);
                }
                comando.Dispose();                
                
                /// FINALIZAMOS LA CONEXION CERRAMOS TODO
            cn.Close();
            cn.Dispose();
            return productos;
        }





        //public List<clsReportePaises> PaisesCiudades( string Continente )
        //{
        //    MySqlConnection cn = new MySqlConnection();
        //    /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
        //    cn.ConnectionString = "server=localhost; database=world; user=root; pwd=root";
        //    cn.Open();

        //    List<clsReportePaises> productos = new List<clsReportePaises>();
        //    string strSQL = "select * from country_city where continent like '" + Continente + "'";
        //    MySqlCommand comando = new MySqlCommand(strSQL, cn);
        //    MySqlDataReader dr = comando.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        clsReportePaises x = new clsReportePaises();
        //        x.code = dr.GetString("CODE");
        //        x.name = dr.GetString("NAME");
        //        x.continent = dr.GetString("CONTINENT");
        //        x.population = dr.GetInt32("POPULATION");
        //        x.ciudades = dr.GetInt32("CIUDADES");
        //        productos.Add(x);
        //    }
        //    comando.Dispose();

        //    /// FINALIZAMOS LA CONEXION CERRAMOS TODO
        //    cn.Close();
        //    cn.Dispose();
        //    return productos;
        //}

        public List<clsProductos> ObtenerProductosPorPrecio(double Precio)
        {
            MySqlConnection cn = new MySqlConnection();
            /// CREAR LA CONEXIÓN, CONFIGURAR Y ABRIRLA
            cn.ConnectionString = "server=localhost; database=sistema_tienda; user=root; pwd=root";
            cn.Open();

            List<clsProductos> productos = new List<clsProductos>();
            string strSQL = "select * from productos where precio>=@precio";
            MySqlCommand comando = new MySqlCommand(strSQL, cn);
            comando.Parameters.AddWithValue("@precio", Precio);
            MySqlDataReader dr = comando.ExecuteReader();
            while (dr.Read())
            {
                clsProductos x = new clsProductos();
                x.Clave = dr.GetInt32(0);
                x.Nombre = dr.GetString(1);
                x.Precio = dr.GetDouble(2);
                productos.Add(x);
            }
            comando.Dispose();

            /// FINALIZAMOS LA CONEXION CERRAMOS TODO
            cn.Close();
            cn.Dispose();
            return productos;
        }
    }
}
