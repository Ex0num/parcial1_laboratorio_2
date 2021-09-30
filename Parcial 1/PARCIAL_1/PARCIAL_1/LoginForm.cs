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
using System.Collections;
using UserUI;
using System.Media;

namespace PARCIAL_1
{
    public partial class LoginForm : Form
    {
        //Listas que son atributos del form main del login. (Se van a ir pasando por referencia a los constructores de casi todos los forms del programa)
        static List<Administrador> listaAdministradores = new List<Administrador>();
        static List<Cliente> listaClientes = new List<Cliente>();
        static List<Empleado> listaEmpleados = new List<Empleado>();
        static List<Producto> listaProductos = new List<Producto>();
        static List<Venta> listaVentas = new List<Venta>();
        static int idUsuarioLogeado;


        /// <summary>
        /// Constructor del LoginForm
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            pictureBox_Gris1.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema1.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris2.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema2.Image = UserUI.Properties.Resources.Tono4;

            this.Icon = UserUI.Properties.Resources.login;
         
        }


        /// <summary>
        /// Se ejecuta al cargar el form por primera vez, hardcodea 2 administradores, empleados, 
        /// clientes, productos y ventas y los añade a la lista de cada uno.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {

            //CREACION DE HARDCODEO DE DATOS (ADMIN-CLIENTES-VENTAS-PRODUCTOS Y EMPLEADOS)
            Administrador admin1 = new Administrador(18, "Avenida Juan XXIII", "Lucas", "123");
            Administrador admin2 = new Administrador(22, "Pavone 3000", "Alberto", "321");

            Empleado empleado1 = new Empleado("Casado", 80000, "44823221", 18, "Pavone 1300", "Carolina", "321");
            Empleado empleado2 = new Empleado("Soltero", 50000, "44890245", 22, "Juan Alberdi 300", "Alfonso", "cactus123");

            Cliente cliente1 = new Cliente("Gabriel", 1200, "44823212", "Horlando 231", "11 158274567");
            Cliente cliente2 = new Cliente("Martina", 12200, "44223212", "Kokara 131", "4282-4778");

            Producto producto1 = new Producto(TipoProductoEnum.Alimento.ToString(), 150, "Comida balanceada para cachorros","Comidetex 4000", "12/12/2023",10);
            Producto producto2 = new Producto(TipoProductoEnum.Higiene.ToString(),400,"Toallitas especiales para gatos","Toalix","No tiene",100);

            Venta venta1 = new Venta(cliente1,producto1,2,empleado1.IdEmpleado,false);
            Venta venta2 = new Venta(cliente2,producto2,10,empleado2.IdEmpleado,false);

            //LAS AÑADO AL LISTADO A CADA UNO
            listaAdministradores.Add(admin1);
            listaAdministradores.Add(admin2);

            listaEmpleados.Add(empleado1);
            listaEmpleados.Add(empleado2);

            listaClientes.Add(cliente1);
            listaClientes.Add(cliente2);

            listaProductos.Add(producto1);
            listaProductos.Add(producto2);

            listaVentas.Add(venta1);
            listaVentas.Add(venta2);

        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Ingresar" busca al usuario que se esta intentando logear 
        /// (su nombre y su contraseña) tanto en la lista de usuarios como en la de administradores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Ingresar_Click(object sender, EventArgs e)
        {
            //Creo un usuario auxiliar con la pass y el nombre que me dieron
            Usuario usuarioAux = new Usuario(textBox_NombreUsuario.Text, textBox_PasswordUsuario.Text);

            bool usuarioEncontrado = false;
            bool esAdmin = false;


            foreach (Empleado empleado in listaEmpleados)
            {
                if (usuarioAux.Nombre == empleado.Nombre && usuarioAux.Password == empleado.Password)
                {
                    usuarioEncontrado = true;
                    esAdmin = false;
                    idUsuarioLogeado = empleado.IdEmpleado;
                    break;
                }
            }

            if (usuarioEncontrado == false)
            {
                foreach (Administrador administrador in listaAdministradores)
                {
                    if (usuarioAux.Nombre == administrador.Nombre && usuarioAux.Password == administrador.Password)
                    {
                        usuarioEncontrado = true;
                        esAdmin = true;
                        idUsuarioLogeado = administrador.Id;
                        break;
                    }
                }
            }

            if (usuarioEncontrado == false)
            {
                MessageBox.Show("No se pudo encontrar ningun usuario con esos datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
           
                OptionsForm formOpciones = new OptionsForm(usuarioAux.Nombre, esAdmin, idUsuarioLogeado, listaAdministradores, listaProductos, listaVentas, listaClientes, listaEmpleados);

                this.Hide();
                
                if (esAdmin == false)
                {
                    formOpciones.BackColor = Color.Honeydew;
                }
                
                formOpciones.ShowDialog();
                this.Show();
                
            }


        }


        /// <summary>
        /// Se ejecuta al presionar el boton "A" y lo que hace es autocompletar los textbox de nombre 
        /// y contraseña con los datos de un admin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_AutoCompletarInfoAdmin_Click(object sender, EventArgs e)
        {
            textBox_NombreUsuario.Text = "Lucas";
            textBox_PasswordUsuario.Text = "123";
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "E" y lo que hace es autocompletar los textbox de nombre 
        /// y contraseña con los datos de un empleado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_AutocompletarInfoEmpleado_Click(object sender, EventArgs e)
        {
            textBox_NombreUsuario.Text = "Carolina";
            textBox_PasswordUsuario.Text = "321";
        }
    }
}
