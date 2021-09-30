using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Venta
    {
        //VARIABLE ESTATICA QUE UTILIZO PARA AUTOINCREMENTARSE CADA VEZ QUE SETEO UN VALOR AL CAMPO ID-VENTA
        static int numIdentificadorVenta;

        //ATRIBUTOS
        Cliente clienteComprador;
        Producto productoAdquirido;
        int cantidadUnidadesAdquiridas;
        int idUsuarioCreador;
        bool esAdmin;
        int idVenta;


        //PROPIEDADES
        /// <summary>
        /// Propiedad que que retona el campo clienteComprador y recibe un valor representativo al 
        /// cliente comprador y lo setea
        /// </summary>
        public Cliente ClienteComprador
        {
            get
            {
                return this.clienteComprador;
            }
            set
            {
                this.clienteComprador = value;
            }

        }


        /// <summary>
        /// Propiedad que retona el campo productoAdquirido y recibe un valor representativo
        /// a un producto adquirido y lo setea
        /// </summary>
        public Producto ProductoAdquirido
        {
            get
            {
                return this.productoAdquirido;
            }
            set
            {
                this.productoAdquirido = value;
            }
        }


        /// <summary>
        /// Propiedad que retorna el campo cantidadUnidadesAdquiridas y recibe un valor representativo
        /// a la cantidad de unidades recibidas y es validado con su propio is valid. Si esto es valido
        /// setea el valor recibido
        /// </summary>
        public int CantidadesUnidadesAdquiridas
        {
            get
            {
                return this.cantidadUnidadesAdquiridas;
            }
            set
            {
                if (IsValidCantidadUnidadesAdquiridas(value) == 0)
                {
                    this.cantidadUnidadesAdquiridas = value;
                }
            }
        }


        /// <summary>
        /// Propiedad que retorna el campo idVenta y recibe un valor representativo a un id 
        /// de una venta, es validado con su propio IsValid y si es valido se setea el ultimo valor
        /// que contiene la variable numIdentificadorVenta
        /// </summary>
        public int IdVenta
        {
            get
            {
                return this.idVenta;
            }
            set
            {

                if (IsValidVentaId(value) == 0)
                {

                    this.idVenta = numIdentificadorVenta;
                }

            }
        }


        /// <summary>
        /// Propiedad que retorna el campo idUsuarioCreador y recibe un valor representativo a un id 
        /// de un creador, es validado con su propio IsValid y si es valido se setea el valor recibido
        /// </summary>
        public int IdUsuarioCreador
        {
            get 
            {
                return this.idUsuarioCreador;
            }
            set 
            {
                if (IsValidEmpleadoCreadorId(value) == 0)
                {
                    this.idUsuarioCreador = value;
                }              
            }
        } 


        /// <summary>
        /// Propiedad que retorna el campo esAdmin y recibe un valor boleano representativo 
        /// a si es o no un administrador y lo setea
        /// </summary>
        public bool EsAdmin
        {
            get 
            {
                return this.esAdmin;
            }
            set 
            {
                this.esAdmin = value;
            }
        }


        //VALIDACIONES
        /// <summary>
        /// Metodo que recibe un valor representativo a un id de una venta y valida que no sea menor a 0
        /// en caso de serlo devuelve -1, sino 0
        /// </summary>
        /// <param name="IdVentaAux"></param>
        /// <returns></returns>
        public int IsValidVentaId(int IdVentaAux)
        {
            int retorno = 0;

            if (IdVentaAux < 0)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un valor representativo a un id de un empleado y valida que no sea menor a 0
        /// en caso de serlo devuelve -1, sino 0
        /// </summary>
        /// <param name="IdEmpleadoAux"></param>
        /// <returns></returns>
        public int IsValidEmpleadoCreadorId(int IdEmpleadoAux)
        {
            int retorno = 0;

            if (IdEmpleadoAux < 0)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        ///  Metodo que recibe un valor representativo a una cantidad de unidades y valida que sea mayor a 0
        ///  en caso no de serlo devuelve -1, sino 0
        /// </summary>
        /// <param name="cantUnidadesRecibidas"></param>
        /// <returns></returns>
        static public int IsValidCantidadUnidadesAdquiridas(int cantUnidadesRecibidas)
        {
            int retorno = -1;

            if (cantUnidadesRecibidas > 0)
            {
                retorno = 0;
            }

            return retorno;
        }


        //CONSTRUCTOR
        /// <summary>
        /// Constructor de una venta, recibe todos los campos de la misma y setea los valores recibidos con las propiedades
        /// de cada campo, tambien aumenta el valor de la variable numIdentificadorVenta
        /// </summary>
        /// <param name="clienteCompradorAux"></param>
        /// <param name="productoAdquiridoAux"></param>
        /// <param name="cantidadUnidadesAdquiridasAux"></param>
        /// <param name="idUsuarioCreadorAux"></param>
        /// <param name="esAdminRecibido"></param>
        public Venta(Cliente clienteCompradorAux, Producto productoAdquiridoAux, int cantidadUnidadesAdquiridasAux, int idUsuarioCreadorAux, bool esAdminRecibido)
        {
            ClienteComprador = clienteCompradorAux;
            ProductoAdquirido = productoAdquiridoAux;
            cantidadUnidadesAdquiridas = cantidadUnidadesAdquiridasAux;
            IdVenta = 0; //HARDCODEO EL 0 PORQUE EL ID DEL PRODUCTO SE ME VA A MANEJAR DE MANERA INDEPENDIENTE.
            IdUsuarioCreador = idUsuarioCreadorAux;
            EsAdmin = esAdminRecibido;
            numIdentificadorVenta++;
        }


        /// <summary>
        /// Constructor vacio de una venta, se utiliza para crear una venta auxiliar y no afecta ni modifica a la variable numIdentificadorVenta
        /// </summary>
        public Venta()
        {
                
        }


        /// <summary>
        /// Metodo que recibe un cliente, un producto y un valor representativo a unidades adquiridas, este valida 
        /// que el saldo del cliente recibido sea suficiente para adquirir el precio del producto recibido por 
        /// la cantidad de unidades adquiridas, retorna false si el saldo del cliente no es suficiente y true si lo es
        /// </summary>
        /// <param name="clienteComprador"></param>
        /// <param name="productoAdquirido"></param>
        /// <param name="unidadesAdquiridas"></param>
        /// <returns></returns>
        static public bool TieneSaldoSuficiente(Cliente clienteComprador, Producto productoAdquirido, int unidadesAdquiridas)
        {
            bool retorno = true;

            double saldoCliente = clienteComprador.Saldo;
            double precioProducto = productoAdquirido.Precio;


            if (saldoCliente < (precioProducto * unidadesAdquiridas))
            {
                retorno = false;
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un producto y un valor representativo a unidades adquiridas, este valida 
        /// que el la cantidad de unidades adquiridas que se desean comprar sean menor o igual a la cantidad
        /// de unidades disponible (stock) del producto. Si la cantidad de unidades que se desean adquirir
        /// es mayor que la disponible retorna false, si no true.
        /// </summary>
        /// <param name="cantidadUnidadesAdquiridas"></param>
        /// <param name="productoAdquirido"></param>
        /// <returns></returns>
        static public bool TieneUnidadesSuficientesProducto(int cantidadUnidadesAdquiridas, Producto productoAdquirido)
        {
            bool retorno = true;

            if (productoAdquirido.CantidadUnidades < cantidadUnidadesAdquiridas)
            {
                retorno = false;
            }

            return retorno;
        }


        //DESHACER VENTA (AUMENTA SALDO Y AUMENTA UNIDADES)
        /// <summary>
        /// Sobrecarga del operador -- de una venta. Deshace una venta, ingresando al cliente comprador y aumentandole
        /// en su campo saldo el valor de lo que le salio el precio del producto por las unidades adquiridas. Ademas,
        /// las unidades del producto comprado se ven aumentadas por las unidades adquiridas de la venta.
        /// </summary>
        /// <param name="venta1"></param>
        /// <returns></returns>
        public static Venta operator --(Venta venta1)
        {
            Cliente clienteComprador;
            clienteComprador = venta1.ClienteComprador;

            Producto productoComprado;
            productoComprado = venta1.ProductoAdquirido;

            int unidadesAdquiridas = venta1.CantidadesUnidadesAdquiridas;
            double precioProducto = productoComprado.Precio;


            double saldoAAumentar = precioProducto * unidadesAdquiridas;

            clienteComprador.Saldo = clienteComprador.Saldo + saldoAAumentar;
            productoComprado.CantidadUnidades = productoComprado.CantidadUnidades + unidadesAdquiridas;

            return venta1;
        }


        //CREAR VENTA (RESTA SALDO Y RESTA UNIDADES)
        /// <summary>
        /// Sobrecarga del operador ++ de una venta. Efectua una venta, ingresando al cliente comprador y restandole
        /// en su campo saldo el valor de lo que le salio el precio del producto por las unidades adquiridas. Ademas,
        /// las unidades del producto comprado se ven restadas por las unidades adquiridas de la venta.
        /// </summary>
        /// <param name="venta1"></param>
        /// <returns></returns>
        public static Venta operator ++(Venta venta1)
        {
            Cliente clienteComprador;
            clienteComprador = venta1.ClienteComprador;

            Producto productoComprado;
            productoComprado = venta1.ProductoAdquirido;

            int unidadesAdquiridas = venta1.CantidadesUnidadesAdquiridas;
            double precioProducto = productoComprado.Precio;


            double saldoARestar = precioProducto * unidadesAdquiridas;

            clienteComprador.Saldo = clienteComprador.Saldo - saldoARestar;
            productoComprado.CantidadUnidades = productoComprado.CantidadUnidades - unidadesAdquiridas;

            return venta1;
        }

    }
}
