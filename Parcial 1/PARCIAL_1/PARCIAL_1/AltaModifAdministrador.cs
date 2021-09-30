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
    public partial class AltaModifAdministrador : Form
    {
        List<Administrador> listaAdministradores;
        Administrador adminAux;
        string ordenMuestra;


        /// <summary>
        /// Es el constructor del AltaModifAdministrador, recibe la lista de administradores y 
        /// la orden recibida, en este caso solo existe un Agregar pero es escalable a un modificar
        /// </summary>
        /// <param name="listaAdministradoresRecibida"></param>
        /// <param name="ordenMuestreadoRecibido"></param>
        public AltaModifAdministrador(List<Administrador> listaAdministradoresRecibida, string ordenMuestreadoRecibido)
        {
            InitializeComponent();

            pictureBox_Gris7.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema7.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris8.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema8.Image = UserUI.Properties.Resources.Tono4;

            this.Icon = UserUI.Properties.Resources.agregar;

            listaAdministradores = listaAdministradoresRecibida;
            ordenMuestra = ordenMuestreadoRecibido;
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Ingresar datos" y dependiendo de la orden 
        /// (solo agregar por el momento) valida los datos ingresados y agrega un nuevo administrador 
        /// a la lista de administradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IngresarDatosAdmin_Click(object sender, EventArgs e)
        {

            if (ordenMuestra == "Agregar")
            {

                bool usuarioRepetido = false;
                int edadAux = 0; //CREO UN EDAD AUX ANTES QUE USAR EL DE ADMIN AUX PORQUE NO PUEDO USAR UN OUT CON UNA PROPIEDAD -> int.TryParse(textBox_EdadIngresadaABMAdmin.Text, out adminAux.Edad); MAL
                int.TryParse(textBox_EdadIngresadaABMAdmin.Text, out edadAux);

                adminAux = new Administrador(edadAux, textBox_DireccionIngresadaABMAdmin.Text, textBox_NombreIngresadoABMAdmin.Text, textBox_PasswordIngresadaABMAdmin.Text);

                adminAux.Nombre = textBox_NombreIngresadoABMAdmin.Text;
                adminAux.Password = textBox_PasswordIngresadaABMAdmin.Text;
                adminAux.Edad = edadAux;
                adminAux.DireccionDomicilio = textBox_DireccionIngresadaABMAdmin.Text;

                int resultadoValidacionGral;
                resultadoValidacionGral = adminAux.verificarUsuario(adminAux.Nombre,0,adminAux.Edad);

                //Si parece estar todo bien. Uso el admin aux para agregarlo finalmente a la lista (habiendo cargado ya los datos validados)
                if (resultadoValidacionGral == 0)
                {
                   
                    //A ESTE PUNTO CARGAMOS YA TODOS LOS CAMPOS. SOLO QUEDA VERIFICAR SI EXISTE OTRO USUARIO ADMIN CON ESTE NOMBRE DE USUARIO.
                    foreach (Administrador administrador in listaAdministradores)
                    {
                        //Si existe una coincidencia en los nombres de usuarios. ERROR!!!!
                        if (administrador.Nombre == adminAux.Nombre)
                        {
                            usuarioRepetido = true;
                            break;
                        }
                    }

                    if (usuarioRepetido == true)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo administrador. Ya existe uno con ese nombre de usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Lo agrego a la lista de admins
                        listaAdministradores.Add(adminAux);
                        //Console.Beep(450, 250);
                        MessageBox.Show("Se ha agregado correctamente el administrador.", "Creacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo agregar un nuevo administrador. Existen datos invalidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            
          
        }
    }
}
