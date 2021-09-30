using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO; //ESTA ES LA LIBERIA QUE ME PERMITE EL USO DE ARCHIVOS

namespace Entidades
{
    public static class Facturacion //AL SER UNA CLASE ESTATICA NO PODRIA HEREDARLA DE UNA VENTA
    {

        /// <summary>
        /// Este metodo y unico miembro de la clase Facturacion que recibe una venta y 
        /// se ocupa de crear un objeto de tipo TextWriter (un objeto escritor) y un objeto StreamWriter, 
        /// abriendo el path del archivo.txt y escribiendo desde 0 el mismo con los datos del cliente y el 
        /// producto, generando entonces la factura como tal.
        /// </summary>
        /// <param name="ventaParaExportar"></param>
        /// <returns></returns>
        public static int ExportarFactura(Venta ventaParaExportar)
        {
            int retorno = -1;

            //TextWriter ES EL OBJETO QUE SE UTILIZA PARA LECTURA, ESCRITURA DE ARCHIVOS entre otras cosas
            TextWriter escritor = new StreamWriter("Factura.txt");

            escritor.WriteLine("Facturacion de la compra");
            escritor.WriteLine("");
            escritor.WriteLine($"El comprador: {ventaParaExportar.ClienteComprador.Nombre}");
            escritor.WriteLine($"Compro el producto: {ventaParaExportar.ProductoAdquirido.Nombre}");
            escritor.WriteLine($"Unidades adquiridas: {ventaParaExportar.CantidadesUnidadesAdquiridas}");
            escritor.WriteLine($"Precio por unidad del producto: {ventaParaExportar.ProductoAdquirido.Precio}");
            escritor.WriteLine("");
            escritor.WriteLine("Gracias por su compra en Patitas-Petshop.");

            //SIEMPRE CERRAR EL ARCHIVO QUE ESCRIBIMOS PARA EVITAR ERRORES
            escritor.Close();

            retorno = 0;

            return retorno;
        }
    }
}
