using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Administrador : Usuario
    {
        //VARIABLE ESTATICA QUE UTILIZO PARA AUTOINCREMENTARSE CADA VEZ QUE SETEO UN VALOR AL CAMPO ID
        static int numIdentificadorAdministrador;

        //ATRIBUTOS
        int edad;
        string direccionDomicilio;
        int id;

        //PROPIEDADES
        /// <summary>
        /// Propiedad que retorna el valor del campo edad o recibe un valor representativo a una edad y lo valida, 
        /// si es valido lo setea.
        /// </summary>
        public int Edad
        {
            get
            {
                return this.edad;
            }

            set
            {
                if (IsValidEdad(value) == 0)
                {
                    this.edad = value;
                }
            }

        }


        /// <summary>
        /// Propiedad que retorna el valor del campo direccionDomicilio o recibe un valor 
        /// representativo a un domicilio y lo setea.
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
        /// Propiedad que retorna el valor del campo Id o recibe un valor representativo a un id y lo valida, 
        /// si es valido setea el ultimo valor que posee la variable numIdentificadorAdministrador.
        /// </summary>
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (IsValidAdminId(value) == 0)
                { 
                    this.id = numIdentificadorAdministrador;
                }

            }
        }


        /// <summary>
        /// Metodo que recibe un valor int representativo a un id y lo valida. Si es invalido retorna -1, si no 0.
        /// </summary>
        /// <param name="IdAdminAux"></param>
        /// <returns></returns>
        public int IsValidAdminId(int IdAdminAux)
        {
            int retorno = 0;

            if (IdAdminAux < 0)
            {
                retorno = -1;
            }

            return retorno;
        }

        //CONSTRUCTOR DE LA CLASE DERIVADA BASADO EN LA CLASE PADRE
        /// <summary>
        /// Constructor que recibe los valores de todos los campos, llama a todas las propiedades de los mismos 
        /// para setear los valores recibidos. Este constructor incrementa el valor de la variable estatica que 
        /// representa el ultimo valor id.
        /// </summary>
        /// <param name="edadAux"></param>
        /// <param name="direccionDomicilioAux"></param>
        /// <param name="nombreAux"></param>
        /// <param name="passwordAux"></param>
        public Administrador(int edadAux, string direccionDomicilioAux, string nombreAux, string passwordAux) : base(nombreAux, passwordAux)
        {
            //Setteo todos los valores de los parametros
            Edad = edadAux;
            DireccionDomicilio = direccionDomicilioAux;
            Nombre = nombreAux;
            Password = passwordAux;

            Id = 0; //HARDCODEO EL 0 PORQUE EL ID DEL ADMIN SE ME VA A MANEJAR DE MANERA INDEPENDIENTE.
            numIdentificadorAdministrador++;
        }


        /// <summary>
        /// Constructor que recibe valores null y solo es utilizado para crear administradores auxiliares. 
        /// Este constructor no afecta ni llama a la variable que contiene el ultimo valor del  id.
        /// </summary>
        /// <param name="direccionDomicilioAux"></param>
        /// <param name="edadAux"></param>
        /// <param name="nombreAux"></param>
        /// <param name="passwordAux"></param>
        public Administrador(string direccionDomicilioAux, int edadAux, string nombreAux, string passwordAux) : base(nombreAux, passwordAux) //ESTE CONSTRUCTOR SOLO ES USADO PARA ADMINISTRADORES NULLS, AUXILIARES.
        {
            //Setteo todos los valores de los parametros
            Edad = edadAux;
            DireccionDomicilio = direccionDomicilioAux;
            Nombre = nombreAux;
            Password = passwordAux;
        }


        /// <summary>
        /// Este metodo recibe una lista de administradores, un id para buscar en la lista, una referencia 
        /// a una variable para ser cargada con la posicion del item encontrada si es que se encuentra y una de este mismo tipo 
        /// para devolver el resultado de la busqueda 
        /// </summary>
        /// <param name="listaAdministradoresRecibida"></param>
        /// <param name="idParaBuscar"></param>
        /// <param name="posDelItem"></param>
        /// <param name="resultadoBusqueda"></param>
        /// <returns></returns>
        static public Administrador buscarPorId(List<Administrador> listaAdministradoresRecibida, int idParaBuscar, out int posDelItem, out int resultadoBusqueda)
        {
            Administrador adminAux = new Administrador("null", 0, "null", "null");
            Administrador retorno = adminAux;
            resultadoBusqueda = -1;
            posDelItem = -1;
            int contador = 0;

            foreach (Administrador item in listaAdministradoresRecibida)
            {
                if (idParaBuscar == item.Id) //Si la id que busco llega a coincidir con algun item que la posee
                {
                    retorno = item; //Devuelvo el item 
                    resultadoBusqueda = 0;
                    posDelItem = contador;
                    break;
                }

                contador++;
            }

            return retorno;
        }


        /// <summary>
        /// Este metodo recibe un nombre, un sueldo y una edad, valida los campos y devuelve un 
        /// valor que representa si los campos del usuario recibido son validos o no
        /// </summary>
        /// <param name="nombreRecibido"></param>
        /// <param name="sueldoRecibido"></param>
        /// <param name="edadRecibida"></param>
        /// <returns></returns>
        public override int verificarUsuario(string nombreRecibido, long sueldoRecibido, int edadRecibida)
        {
            int retorno = -1;

            if (IsValidName(nombreRecibido) == 0 && IsValidEdad(edadRecibida) == 0)
            {
                retorno = 0;
            }

            return retorno;
        }

    }
 
}

