using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Producto
    {
        //VARIABLE ESTATICA QUE UTILIZO PARA AUTOINCREMENTARSE CADA VEZ QUE SETEO UN VALOR AL CAMPO ID-PRODUCTO
        static int numIdentificadorProducto;

        //ATRIBUTOS
        string tipoProducto;
        double precio;
        string descripcion;
        string nombre;
        int idProducto;
        string fechaVencimiento;
        int cantidadUnidades;


        //PROPIEDADES
        /// <summary>
        /// Propiedad que retorna un el campo tipo producto y recibe un valor representativo del tipo
        /// producto y lo valida, si es valido lo setea
        /// </summary>
        public string TipoProducto
        {
            get
            {
                return tipoProducto;
            }

            set
            {
                if (IsValidTipoProducto(value) == 0)
                {
                    this.tipoProducto = value;
                }   
            }

        }


        /// <summary>
        /// Propiedad que retorna el campo precio y recibe un valor representativo al precio, valida si es valido
        /// y si lo es lo setea
        /// </summary>
        public double Precio
        {
            get 
            {
                return this.precio;
            }

            set 
            {

                if (IsValidPrecio(value) == 0)
                {
                    this.precio = value;
                }
                
            }
        }


        /// <summary>
        /// Propiedad que retorna el campo descripcion y recibe un valor representativo de una descripcion
        /// y lo setea
        /// </summary>
        public string Descripcion
        {
            get 
            {
                return this.descripcion;
            }
            set 
            {

                if (IsValidDescripcion(value) == 0)
                {
                    this.descripcion = value;
                }
               
            }
        }


        /// <summary>
        /// Propiedad que retorna el campo nombre y recibe un valor representativo a un nombre
        /// el cual es validado y si es valido se setea
        /// </summary>
        public string Nombre
        {
            get 
            {
                return this.nombre;

            }
            set 
            {
                if (IsValidNombre(value) == 0)
                {
                    this.nombre = value;
                }
            }

        }


        /// <summary>
        /// Propiedad que retorna el campo idProducto y recibe un valor representativo a un 
        /// id de un producto, el cual es validado y si es valido se setea el ultimo valor que tiene
        /// la variable numIdentificadorProducto
        /// </summary>
        public int IdProducto
        {
            get 
            {
                return this.idProducto;
            }
            set 
            {

                if (IsValidProductoId(value) == 0)
                {
                    this.idProducto = numIdentificadorProducto;
                }
                
            }
        }


        /// <summary>
        /// Propiedad que retona el campo fecha de vencimiento y recibe un valor representativo
        /// a una fecha de vencimiento y se setea
        /// </summary>
        public string FechaVencimiento
        {
            get 
            {
                return this.fechaVencimiento;
            }
            set 
            {
                this.fechaVencimiento = value;
            }

        }


        /// <summary>
        /// Propiedad que retorna el campo cantidad unidades y recibe un valor representativo a 
        /// una cantidad de unidades, el cual es validado y si es valido se setea
        /// </summary>
        public int CantidadUnidades
        {
            get
            {
                return this.cantidadUnidades;
            }
            set 
            {
                if (IsValidCantidadUnidades(value) == 0)
                {
                    this.cantidadUnidades = value;
                }
            }
        }


        //VALIDACIONES
        /// <summary>
        /// Metodo que recibe un valor representativo al tipo de un producto y lo valida comparandolo 
        /// con todos los campos del enumerado TipoProducto, si ningun campo coincide con el valor, 
        /// devuelve -1, sino 0
        /// </summary>
        /// <param name="tipoAux"></param>
        /// <returns></returns>
        public int IsValidTipoProducto(string tipoAux)
        {
            int retorno = 0;

            if (tipoAux != (TipoProductoEnum.Alimento.ToString()) && tipoAux != (TipoProductoEnum.Cama.ToString()) && tipoAux != (TipoProductoEnum.Juguete.ToString()) && tipoAux != (TipoProductoEnum.Higiene.ToString()))
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un valor representativo a un precio, valida que no sea menor o igual a 0 ni
        /// mayor a 9999999, si lo es devuelve -1, sino 0
        /// </summary>
        /// <param name="precioAux"></param>
        /// <returns></returns>
        public int IsValidPrecio(double precioAux)
        {
            int retorno = 0;

            if (precioAux <= 0 ||precioAux > 999999)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        ///Metodo que recibe un valor representativo a una descripcion, valida que su largo no sea
        /// mayor a 62 caracteres, si lo es devuelve -1, sino 0
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public int IsValidDescripcion(string descripcion)
        {
            int retorno = 0;

            //Si es mayor a 62 caracteres
            if (descripcion.Length > 62)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un valor representativo a un nombre, valida que no tenga un largo mayor a 32
        /// caracteres, si lo tiene devuelve -1, sino 0
        /// </summary>
        /// <param name="nombreAux"></param>
        /// <returns></returns>
        public int IsValidNombre(string nombreAux)
        {
            int retorno = 0;

            if (nombreAux.Length > 32)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un valor representativo a un idProducto, valida que no sea menor a 0. Si es invalido 
        /// retorna -1, si no retorna 0
        /// </summary>
        /// <param name="idProductoAux"></param>
        /// <returns></returns>
        static public int IsValidProductoId(int idProductoAux)
        {
            int retorno = 0;

            if (idProductoAux < 0)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un valor representativo a una cantidad de unidades, valida que no sea menor a 0 o
        /// mayor a 9999999. Si alguna de las dos se cumple es invalido por lo que devuelve -1, si no 0 
        /// retorna -1, si no retorna 0
        /// </summary>
        /// <param name="cantidadUnidadesAux"></param>
        /// <returns></returns>
        public int IsValidCantidadUnidades(int cantidadUnidadesAux)
        {
            int retorno = 0;

            if (cantidadUnidadesAux < 0 || cantidadUnidadesAux > 999999)
            {
                retorno = -1;
            }

            return retorno;
        }


        //CONSTRUCTOR
        /// <summary>
        /// Constructor de un producto, recibe todos los campos de un producto, los setea y valida llamando a las propiedades
        /// de cada uno de ellos y setea el id del producto con un 0 para que la propiedad setee el valor id 
        /// correspondiente
        /// </summary>
        /// <param name="tipoProductoAux"></param>
        /// <param name="precioAux"></param>
        /// <param name="descripcionAux"></param>
        /// <param name="nombreAux"></param>
        /// <param name="fechaVencimientoAux"></param>
        /// <param name="cantidadUnidadesAux"></param>
        public Producto(string tipoProductoAux, double precioAux, string descripcionAux, string nombreAux, string fechaVencimientoAux, int cantidadUnidadesAux)
        {
            TipoProducto = tipoProductoAux;
            Precio = precioAux;
            Descripcion = descripcionAux;
            Nombre = nombreAux;
            FechaVencimiento = fechaVencimientoAux;
            cantidadUnidades = cantidadUnidadesAux;
            IdProducto = 0; //HARDCODEO EL 0 PORQUE EL ID DEL PRODUCTO SE ME VA A MANEJAR DE MANERA INDEPENDIENTE. 
            numIdentificadorProducto++;
        }


        /// <summary>
        /// Constructor de un producto vacio, solo es utilizado si se desea crear un producto auxiliar
        /// </summary>
        public Producto()
        {
                
        }


        /// <summary>
        /// Sobrecarga de un operador de conversor explicito, al realizar un casteo de un producto
        /// a double, devuelve el valor total invertido en el producto contando sus unidades y 
        /// multiplicandolo por el precio
        /// </summary>
        /// <param name="p"></param>
        public static explicit operator double(Producto producto)
        {
            return (producto.Precio * producto.CantidadUnidades);
        }
    }

    //ENUMERADOS
    /// <summary>
    /// Enumerado que contiene todos los posibles valores de un tipo de producto
    /// </summary>
    public enum TipoProductoEnum
    {
        Cama = 0,
        Alimento = 1,
        Juguete = 2,
        Higiene = 3
    }

}


