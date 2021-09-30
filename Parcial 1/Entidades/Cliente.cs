using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Cliente
    {
        //VARIABLE ESTATICA QUE UTILIZO PARA AUTOINCREMENTARSE CADA VEZ QUE SETEO UN VALOR AL CAMPO ID-CLIENTE
        static int numIdentificadorCliente;

        //ATRIBUTOS
        string nombre;
        double saldo;
        string dni;
        string direccionDomicilio;
        string telefono;
        int idCliente;

        //PROPIEDADES
        /// <summary>
        /// Propiedad que retorna el valor del campo nombre o recibe un valor 
        /// representativo a un nombre, lo valida y si es valido lo setea, si no devuelve un string
        /// "NombreInvalido".
        /// </summary>
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                if (IsValidName(value) == 0)
                {
                    this.nombre = value;
                }
                else
                {
                    this.nombre = "NombreInvalido";
                }
            }
        }


        /// <summary>
        /// Propiedad que retorna el valor del campo saldo o recibe un valor 
        /// representativo al saldo y si es valido, lo setea
        /// </summary>
        public double Saldo
        {
            get 
            {
                return this.saldo;
            }
            set 
            {
                if (IsValidSaldo(value) == 0)
                {
                    this.saldo = value;
                }
            }

        }


        /// <summary>
        /// Propiedad que retorna el valor del campo DNI o recibe un valor 
        /// representativo a un DNI y lo setea
        /// </summary>
        public string Dni
        {
            get 
            {
                return this.dni;
            }
            set 
            {
                this.dni = value;
            }
        }


        /// <summary>
        /// Propiedad que retorna el valor del campo direccionDomicilio o recibe un valor 
        /// representativo a un domicilio y lo setea
        /// </summary>
        public string DireccionDomicilio
        {
            get 
            {
                return this.direccionDomicilio;
            }
            set 
            {
                this.direccionDomicilio = value;
            }
        }


        /// <summary>
        /// Propiedad que retorna el valor del campo telefono o recibe un valor 
        /// representativo a un telefono y lo setea
        /// </summary>
        public string Telefono
        {
            get 
            {
                return this.telefono;
            }
            set 
            {
                this.telefono = value;
            }
        }


        /// <summary>
        /// Propiedad que retorna el valor del campo idCliente o recibe un valor 
        /// representativo a un id cliente y lo valida, si es valido setea el ultimo valor que posee la variable numeroDdentificadorCliente
        /// </summary>
        public int IdCliente
        {
            get
            {
                return this.idCliente;
            }
            set
            {

                if (IsValidClienteId(value) == 0)
                {
                    this.idCliente = numIdentificadorCliente;
                }

            }
        }


        //VALIDACIONES
        /// <summary>
        /// Metodo que recibe un valor representativo a un nombre, valida que no tenga mas de 4 mayusculas
        /// ni mas de 3 espacios, ademas de que solo contenga caracteres de letras sea mayusculas y o minisculas
        /// ademas de los espacios.
        /// </summary>
        /// <param name="nombreAux"></param>
        /// <returns></returns>
        public int IsValidName(string nombreAux)
        {
            int retorno = 0;
            int contadorEspacios = 0;
            int contadorMayusculas = 0;

            if (nombreAux.Length > 32 || nombreAux.Length < 2)
            {
                retorno = -1;
            }
            else
            {
                //Recorre el string de nombre verificando que solo sea valido A-z Mayusculas y minusculas y espacios.
                for (int i = 0; i < nombreAux.Length; i++)
                {
                    if (nombreAux[i] < 'A')
                    {
                        if (nombreAux[i] == ' ')
                        {
                            contadorEspacios++;
                        }
                        else
                        {
                            retorno = -1;
                            break;
                        }
                    }
                    else if (nombreAux[i] > 'Z')
                    {
                        if (nombreAux[i] < 'a' || nombreAux[i] > 'z')
                        {
                            retorno = -1;
                            break;
                        }
                    }
                    else
                    {
                        contadorMayusculas++;
                    }
                }

                if (contadorEspacios > 3 || contadorMayusculas > 4)
                {
                    retorno = -1;
                }
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un valor representativo a un saldo, valida que no sea menor a 0 ni mayor a 900000
        /// si es invalido retorna -1, si no 0
        /// </summary>
        /// <param name="saldoAux"></param>
        /// <returns></returns>
        public int IsValidSaldo(double saldoAux)
        {
            int retorno = 0;

            if (saldoAux < 0 || saldoAux > 900000)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        /// Metodo que recibe un valor representativo a un idCliente, valida que no sea menor a 0. Si es invalido 
        /// retorna -1, si no retorna 0
        /// </summary>
        /// <param name="IdClienteAux"></param>
        /// <returns></returns>
        public int IsValidClienteId(int IdClienteAux)
        {
            int retorno = 0;

            if (IdClienteAux < 0)
            {
                retorno = -1;
            }

            return retorno;
        }


        //CONSTRUCTOR QUE AFECTA ID
        /// <summary>
        /// Constructor que recibe los valores de todos los campos, llama a todas las propiedades de los mismos 
        /// para setear los valores recibidos. Este constructor incrementa el valor de la variable estatica que 
        /// representa el ultimo valor id.
        /// </summary>
        /// <param name="nombreAux"></param>
        /// <param name="saldoAux"></param>
        /// <param name="dniAux"></param>
        /// <param name="direccionDomicilioAux"></param>
        /// <param name="telefonoAux"></param>
        public Cliente(string nombreAux, double saldoAux, string dniAux, string direccionDomicilioAux, string telefonoAux)
        {
            Nombre = nombreAux;
            Saldo = saldoAux;
            Dni = dniAux;
            DireccionDomicilio = direccionDomicilioAux;
            Telefono = telefonoAux;
            IdCliente = 0; //HARDCODEO EL 0 PORQUE EL ID DEL PRODUCTO SE ME VA A MANEJAR DE MANERA INDEPENDIENTE. 
            numIdentificadorCliente++;
        }


        //CONSTRUCTOR QUE NO AFECTA ID
        /// <summary>
        ///  Constructor que recibe valores null y solo es utilizado para crear clientes auxiliares. 
        ///  Este cliente no afecta ni llama a la variable que contiene el ultimo valor del id.
        /// </summary>
        /// <param name="saldoAux"></param>
        /// <param name="nombreAux"></param>
        /// <param name="dniAux"></param>
        /// <param name="direccionDomicilioAux"></param>
        /// <param name="telefonoAux"></param>
        public Cliente(double saldoAux,string nombreAux, string dniAux, string direccionDomicilioAux, string telefonoAux)
        {
            Nombre = nombreAux;
            Saldo = saldoAux;
            Dni = dniAux;
            DireccionDomicilio = direccionDomicilioAux;
            Telefono = telefonoAux;
            
        }


        /// <summary>
        /// Constructor de un cliente, no recibe ningun valor ni tampoco afecta ni 
        /// llama a la variable que contiene el ultimo valor del id.
        /// </summary>
        public Cliente()
        {
                
        }

    }
}
