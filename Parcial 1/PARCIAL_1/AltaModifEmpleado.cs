using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace UserUI
{
    public partial class AltaModifEmpleado : Form
    {
        List<Empleado> listaEmpleados;
        Empleado empleadoAux;
        string ordenMuestra;

        /// <summary>
        /// Constructor del AltaModifEmpleado, recibe la lista de empleados y la orden
        /// </summary>
        /// <param name="listaEmpleadosRecibida"></param>
        /// <param name="ordenMuestreadoRecibido"></param>
        public AltaModifEmpleado(List<Empleado> listaEmpleadosRecibida, string ordenMuestreadoRecibido)
        {
            InitializeComponent();

            pictureBox_Gris11.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema11.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris12.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema12.Image = UserUI.Properties.Resources.Tono4;

            listaEmpleados = listaEmpleadosRecibida;
            ordenMuestra = ordenMuestreadoRecibido;
        }

        /// <summary>
        /// Se ejecuta al presionar el boton "Ingresar datos", obtiene todos los datos ingresados, 
        /// los valida y si no existe un empleado con esos datos y estos son coherentes, 
        /// se agrega un nuevo empleado a la lista de empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IngresarDatosEmpleado_Click(object sender, EventArgs e)
        {

            if (ordenMuestra == "Agregar")
            {

                bool empleadoRepetido = false;
                
                long sueldoAux = 0; //CREO UN SUELDO AUX ANTES QUE USAR EL DE EMPLEADO AUX PORQUE NO PUEDO USAR UN OUT CON UNA PROPIEDAD -> int.TryParse(textBox_EdadIngresadaABMAdmin.Text, out adminAux.Edad); MAL
                long.TryParse(textBox_SueldoIngresadoABMEmpleado.Text, out sueldoAux);

                int edadAux = 0;
                int.TryParse(textBox_EdadIngresadaABMEmpleado.Text, out edadAux);

                empleadoAux = new Empleado(0,"null","null",0,"null","null","null");

                int resultadoValidacionGral = empleadoAux.verificarUsuario(textBox_NombreIngresadoABMEmpleado.Text,sueldoAux,edadAux);

                //Si parece estar todo bien. Uso el admin aux para modificarlo finalmente a la lista (habiendo cargado ya los datos validados)
                if (resultadoValidacionGral == 0)
                {
                    empleadoAux = new Empleado(textBox_EstadoCivilIngresadoABMEmpleado.Text,sueldoAux,textBox_DNIIngresadoABMEmpleado.Text,edadAux,textBox_DireccionDomIngresadaABMEmpleado.Text,textBox_NombreIngresadoABMEmpleado.Text,textBox_PasswordIngresadaABMEmpleado.Text);

                    //A ESTE PUNTO CARGAMOS YA TODOS LOS CAMPOS. SOLO QUEDA VERIFICAR SI EXISTE OTRO CLIENTE CON ESTE NOMBRE DE USUARIO.
                    foreach (Empleado empleado in listaEmpleados)
                    {
                        //Si existe una coincidencia en los nombres de usuarios. ERROR!!!!
                        if (empleado.Nombre == empleadoAux.Nombre && empleado.Password == empleadoAux.Password && empleado.EstadoCivil == empleadoAux.EstadoCivil && empleado.Sueldo == empleadoAux.Sueldo && empleado.Dni == empleadoAux.Dni && empleado.Edad == empleadoAux.Edad && empleado.DireccionDomicilio == empleadoAux.DireccionDomicilio)
                        {
                            empleadoRepetido = true;
                            break;
                        }
                    }

                    if (empleadoRepetido == true)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo empleado. Ya existe uno con esos mismos datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Lo agrego a la lista de clintes
                        listaEmpleados.Add(empleadoAux);
                        MessageBox.Show("Se ha agregado correctamente el empleado.", "Creacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo agregar un nuevo empleado. Existen datos invalidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
