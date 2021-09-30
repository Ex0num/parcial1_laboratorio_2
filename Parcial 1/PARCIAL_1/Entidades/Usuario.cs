using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Usuario
    {
        //ATRIBUTOS
        string nombre;
        string password;


        //PROPIEDADES
        /// <summary>
        /// Propiedad que retorna el campo nombre y recibe un valor representativo a un nombre
        /// este mismo es validado con su IsValid y si es valido se setea, si no... el valor seteado
        /// es "NombreInvalido"
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
        /// Propiedad que retorna el campo password y recibe un valor representativo a una password y la setea
        /// </summary>
        public string Password
        {
            get 
            {
                return this.password;
            }

            set 
            {
                this.password = value;
            }
        
        }


        //VALIDACIONES
        /// <summary>
        ///  Metodo que recibe un valor representativo a un nombre, valida que no tenga un largo mayor a 32
        ///  caracteres ni menor a 2, ademas de que sea que contenga solo mayusculas o minusculas,  
        ///  y que no hayan mas de 4 mayusculas ni mas de 3 espacios si algo de lo mencionado
        ///  se incumple devuelve -1, sino 0
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

                if (nombreAux == "NombreInvalido")
                {
                    retorno = -1;
                }
            }

            

            return retorno;
        }


        /// <summary>
        ///  Metodo que recibe un valor representativo a un sueldo, valida que no sea menor a 3000
        ///  y si lo es devuelve -1, si no 0
        /// </summary>
        /// <param name="sueldoAux"></param>
        /// <returns></returns>
        public int IsValidSueldo(long sueldoAux)
        {
            int retorno = 0;

            if (sueldoAux < 3000)
            {
                retorno = -1;
            }

            return retorno;
        }


        /// <summary>
        ///  Metodo que recibe un valor representativo a un edad, valida que no sea menor a 17 ni mayor a 110
        ///  si es invalido devuelve -1 si no 0
        /// </summary>
        /// <param name="edadAux"></param>
        /// <returns></returns>
        public int IsValidEdad(int edadAux)
        {
            int retorno = 0;

            if (edadAux < 17 || edadAux > 110)
            {
                retorno = -1;
            }

            return retorno;
        }


        //CONSTRUCTOR
        /// <summary>
        /// Constructor de un usuario, recibe un nombre y una password y llama a las propiedades de ambos
        /// para setearles los valores recibidos
        /// </summary>
        /// <param name="nombreAux"></param>
        /// <param name="passwordAux"></param>
        public Usuario(string nombreAux, string passwordAux)
        {
           this.Nombre = nombreAux;
           this.Password = passwordAux; 
        }


        /// <summary>
        /// Metodo que recibe un nombre, un sueldo y una edad, valida cada uno de sus metodos
        /// IsValid, retorna 0 si la informacion recibida es valida y -1 si no lo es.
        /// </summary>
        /// <param name="nombreRecibido"></param>
        /// <param name="sueldoRecibido"></param>
        /// <param name="edadRecibida"></param>
        /// <returns></returns>
        public virtual int verificarUsuario(string nombreRecibido, long sueldoRecibido, int edadRecibida)
        {
            int retorno = -1;

            if (IsValidName(nombreRecibido) == 0 && IsValidName(nombreRecibido) == 0 && IsValidEdad(edadRecibida) == 0)
            {
                retorno = 0;
            }

            return retorno;
        }
    }


}
