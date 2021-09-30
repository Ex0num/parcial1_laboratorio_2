using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Empleado : Usuario
    {
        //VARIABLE ESTATICA QUE UTILIZO PARA AUTOINCREMENTARSE CADA VEZ QUE SETEO UN VALOR AL CAMPO ID
        static int numIdentificadorEmpleado;

        //ATRIBUTOS
        string estadoCivil;
        long sueldo;
        string dni;
        int edad;
        string direccionDomicilio;
        int idEmpleado;


        //PROPIEDADES
        /// <summary>
        /// Propiedad que retorna el valor del campo estado civil o recibe un valor 
        /// representativo a un estado civil y lo setea
        /// </summary>
        public string EstadoCivil
        {

            get
            {
                return this.estadoCivil;
            }

            set
            {
                this.estadoCivil = value;
            }

        }


        /// <summary>
        /// Propiedad que retorna el valor del campo sueldo o recibe un valor 
        /// representativo a un sueldo, lo valida y si es valido lo setea
        /// </summary>
        public long Sueldo
        {

            get
            {
                return this.sueldo;
            }

            set 
            {
                if (IsValidSueldo(value) == 0)
                {
                    this.sueldo = value;

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
        /// Propiedad que retorna el valor del campo edad o recibe un valor 
        /// representativo a una edad, la valida y si es valido la setea
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
        /// representativo a un domicilio y si lo setea
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
        /// Propiedad que retorna el valor del campo idEmpleado o recibe un valor 
        /// representativo a un idEmpleado y lo valida, si es valido setea el ultimo valor que posee 
        /// la variable numeIddentificadorEmpleado
        /// </summary>
        public int IdEmpleado
        {
            get 
            {
                return this.idEmpleado;
            }
            set 
            {
                if (IsValidEmpleadoId(value) == 0)
                { 
                    this.idEmpleado = numIdentificadorEmpleado;
                }
            }
        }


        /// <summary>
        /// Metodo que recibe un valor int representativo a un idEmpleado y valida que no sea menor a 0,
        /// si no es valido retorna -1, si no 0.
        /// </summary>
        /// <param name="IdEmpleadoAux"></param>
        /// <returns></returns>
        public int IsValidEmpleadoId(int IdEmpleadoAux)
        {
            int retorno = 0;

            if (IdEmpleadoAux < 0)
            {
                retorno = -1;
            }

            return retorno;
        }


        //CONSTRUCTOR DE LA CLASE DERIVADA BASADO EN LA CLASE PADRE
        /// <summary>
        /// Constructor de un empleado que recibe los valores representativos a todos sus 
        /// campos y llama a su constructor base de usuario, luego llamar a todos las propiedades
        /// de los campos y setea los valores, tambien aumenta el valor de la variable numIdentificadorEmpleado
        /// </summary>
        /// <param name="estadoCivilAux"></param>
        /// <param name="sueldoAux"></param>
        /// <param name="dniAux"></param>
        /// <param name="edadAux"></param>
        /// <param name="direccionDomicilioAux"></param>
        /// <param name="nombreAux"></param>
        /// <param name="passwordAux"></param>
        public Empleado(string estadoCivilAux, long sueldoAux, string dniAux, int edadAux, string direccionDomicilioAux, string nombreAux, string passwordAux) : base(nombreAux, passwordAux)
        { 
            //Setteo todos los valores de los parametros
            EstadoCivil = estadoCivilAux;
            Sueldo = sueldoAux;
            Dni = dniAux;
            Edad = edadAux;
            DireccionDomicilio = direccionDomicilioAux;
            Nombre = nombreAux;
            Password = passwordAux;

            //HARDCODEO EL 0 PARA QUE EL ID SE ME MANEJE DE FORMA INDEPENDIENTE    S I E M P R E
            IdEmpleado = 0;
            numIdentificadorEmpleado++;
        }


        /// <summary>
        /// Constructor que recibe valores null y solo es utilizado para crear Empleados auxiliares. Aun asi se llaman
        /// a las propiedades de todos los campos. Este Empleado no afecta ni llama a la variable que contiene el ultimo 
        /// valor del id.
        /// </summary>
        /// <param name="sueldoAux"></param>
        /// <param name="estadoCivilAux"></param>
        /// <param name="dniAux"></param>
        /// <param name="edadAux"></param>
        /// <param name="direccionDomicilioAux"></param>
        /// <param name="nombreAux"></param>
        /// <param name="passwordAux"></param>
        public Empleado(long sueldoAux, string estadoCivilAux, string dniAux, int edadAux, string direccionDomicilioAux, string nombreAux, string passwordAux) : base(nombreAux, passwordAux)
        {
            //Setteo todos los valores de los parametros
            EstadoCivil = estadoCivilAux;
            Sueldo = sueldoAux;
            Dni = dniAux;
            Edad = edadAux;
            DireccionDomicilio = direccionDomicilioAux;
            Nombre = nombreAux;
            Password = passwordAux;

        }


        /// <summary>
        /// VerificarUsuario recibe un valores representativos a un nombre, un sueldo y una edad, a todos los valida
        /// llamando a la funcion IsValid de cada uno de ellos y si todas lasvalidaciones salen bien, el retorno vale 0,
        /// si no -1
        /// </summary>
        /// <param name="nombreRecibido"></param>
        /// <param name="sueldoRecibido"></param>
        /// <param name="edadRecibida"></param>
        /// <returns></returns>
        public override int verificarUsuario(string nombreRecibido, long sueldoRecibido, int edadRecibida)
        {
            int retorno = -1;

            if (IsValidName(nombreRecibido) == 0 && IsValidSueldo(sueldoRecibido) == 0 && IsValidEdad(edadRecibida) == 0)
            {
                retorno = 0;
            }

            return retorno;
        }

    }
}
