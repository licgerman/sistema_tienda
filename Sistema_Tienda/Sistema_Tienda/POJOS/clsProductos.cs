using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Tienda.POJOS
{
    class clsProductos
    {
        private int clave;
        private string nombre;
        private double precio;

        public int Clave
        {
            get
            {
                return clave;    
            }
            set
            {
                clave = value;
            }
        }
        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public double Precio
        {
            get
            {
                return precio;
            }
            set
            {
                precio = value;
            }
        }
    }
}
