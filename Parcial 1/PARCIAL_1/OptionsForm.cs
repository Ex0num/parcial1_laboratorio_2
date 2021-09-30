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
    public partial class OptionsForm : Form
    {

        List<Administrador> listaAdministradores;
        List<Empleado> listaEmpleados;
        List<Producto> listaProductos;
        List<Venta> listaVentas;
        List<Cliente> listaClientes;
        bool esAdmin;
        int idUsuarioLogeado;
        

        /// <summary>
        /// Enumerado de las secciones del form del menu
        /// </summary>
        enum SeccionesMenu
        { 
            CerrarSesion = 0,
            Clientes = 1,
            Stock = 2,
            Ventas = 3,
            Empleados = 4,
            Administradores = 5
        }


        /// <summary>
        /// Constructor del OptionsForm, recibe el nombre, id y rango del usuario logeado y todas las listas 
        /// de todos los elementos, setea un mensaje de bienvenida personalizado si es un administrador 
        /// o un empleado y muestra el nombre del usuario logeado
        /// </summary>
        /// <param name="nombreUser"></param>
        /// <param name="esAdminRecibido"></param>
        /// <param name="idUsuarioLogeadoRecibido"></param>
        /// <param name="listaAdministradoresRecibida"></param>
        /// <param name="listaProductosRecibida"></param>
        /// <param name="listaVentasRecibida"></param>
        /// <param name="listaClientesRecibida"></param>
        /// <param name="listaEmpleadosRecibida"></param>
        public OptionsForm(string nombreUser, bool esAdminRecibido, int idUsuarioLogeadoRecibido, List<Administrador> listaAdministradoresRecibida, List<Producto> listaProductosRecibida, List<Venta> listaVentasRecibida, List<Cliente> listaClientesRecibida, List<Empleado> listaEmpleadosRecibida)
        {
            InitializeComponent();

            pictureBox_Gris3.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema3.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris4.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema4.Image = UserUI.Properties.Resources.Tono4;

            esAdmin = esAdminRecibido;
            listaAdministradores = listaAdministradoresRecibida;
            listaEmpleados = listaEmpleadosRecibida;
            listaProductos = listaProductosRecibida;
            listaVentas = listaVentasRecibida;
            listaClientes = listaClientesRecibida;
            idUsuarioLogeado = idUsuarioLogeadoRecibido;

            switch (esAdmin)
            {
                case true:
                {
                    label_BienvenidoRangoUsuario.Text = $"Bienvenido/a {nombreUser}";
                    label_InfoRangoUsuario.Text = "Su rango es el de Administrador";
                    break;
                }
                case false:
                {
                    label_BienvenidoRangoUsuario.Text = $"Bienvenido/a {nombreUser}";
                    label_BienvenidoRangoUsuario.BackColor = Color.Honeydew;

                    label_InfoRangoUsuario.Text = "Su rango es el de Empleado.";
                    label_BienvenidoRangoUsuario.BackColor = Color.Honeydew;

                    groupBox_Calendario.BackColor = Color.Honeydew;
                    groupBox_Horario.BackColor = Color.Honeydew;
                    groupBox_Menu.BackColor = Color.Honeydew;

                    break;
                }
            }
           
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Clientes" y se llama al form ABM indicandole la seccion elegida, 
        /// en este caso clientes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_JoinClientesABM_Click(object sender, EventArgs e)
        {
            //Le paso la seccion elegida (Clientes) y las listas de todos.
            ABMForm ABMForm = new ABMForm(SeccionesMenu.Clientes.ToString(), listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes, esAdmin,idUsuarioLogeado);
            ABMForm.Icon = UserUI.Properties.Resources.abm;
            this.Hide();
            ABMForm.ShowDialog();
            this.Show();
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Stock" y se llama al form ABM indicandole la seccion elegida, 
        /// en este caso Stock.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_JoinStockABM_Click(object sender, EventArgs e)
        {
            //Le paso la seccion elegida (Stock) y las listas de todos.
            ABMForm ABMForm = new ABMForm(SeccionesMenu.Stock.ToString(), listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes, esAdmin,idUsuarioLogeado);
            ABMForm.Icon = UserUI.Properties.Resources.abm;
            this.Hide();
            ABMForm.ShowDialog();
            this.Show();
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Ventas" y se llama al form ABM indicandole la seccion elegida, 
        /// en este caso ventas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_JoinVentasABM_Click(object sender, EventArgs e)
        {
            //Le paso la seccion elegida (Ventas) y las listas de todos.
            ABMForm ABMForm = new ABMForm(SeccionesMenu.Ventas.ToString(), listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes, esAdmin, idUsuarioLogeado);
            ABMForm.Icon = UserUI.Properties.Resources.abm;
            this.Hide();
            ABMForm.ShowDialog();
            this.Show();

        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Empleados" y se llama al form ABM indicandole la seccion elegida, 
        /// en este caso empleados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_JoinEmpleadosABM_Click(object sender, EventArgs e)
        {
            if (esAdmin == false)
            {
                MessageBox.Show("No tiene acceso a este panel de control. No posee el rango necesario para utilizar estas funcionalidades", "Error de permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                //Le paso la seccion elegida (Empleados) y las listas de todos.
                ABMForm ABMForm = new ABMForm(SeccionesMenu.Empleados.ToString(), listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes, esAdmin, idUsuarioLogeado);
                ABMForm.Icon = UserUI.Properties.Resources.abm;
                this.Hide();
                ABMForm.ShowDialog();
                this.Show();
            }
            
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Administradores" y se llama al form ABM indicandole la seccion 
        /// elegida, en este caso administradores.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_AdministradoresABM_Click(object sender, EventArgs e)
        {
            if (esAdmin == false)
            {
                MessageBox.Show("No tiene acceso a este panel de control. No posee el rango necesario para utilizar estas funcionalidades", "Error de permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                //Le paso la seccion elegida (Administradores) y las listas de todos.
                ABMForm ABMForm = new ABMForm(SeccionesMenu.Administradores.ToString(), listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes, esAdmin, idUsuarioLogeado);
                ABMForm.Icon = UserUI.Properties.Resources.abm;

                this.Hide();
                ABMForm.ShowDialog();
                this.Show();
            }
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Cerrar sesion" y hace un Close del form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_CerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Se ejecuta durante el proceso de cierre del formulario, pide una confirmacion para cerrar sesion. 
        /// Si se confirma cierra, si no, es cancelado el cierre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult confirmacion;
            confirmacion = MessageBox.Show("¿Seguro de querer salir y cerrar sesion?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacion == DialogResult.No)
            {
                //Cancela el evento de "CLOSING". (EVENT CANCEL). Si el usuario toca "NO"
                e.Cancel = true;
            }
        }

    }
}
