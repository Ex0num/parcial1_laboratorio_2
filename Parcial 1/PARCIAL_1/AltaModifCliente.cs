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
    public partial class AltaModifCliente : Form
    {
        List<Cliente> listaClientes;
        Cliente clienteAux;
        string ordenMuestra;
        int posicionCliente;

        /// <summary>
        /// Constructor del AltaModifCliente, recibe la lista de clientes, la orden a realizar, la posicion 
        /// del cliente (si se trata de una modificacion, en su defecto no se tiene en cuenta este valor)
        /// </summary>
        /// <param name="listaClientesRecibida"></param>
        /// <param name="ordenMuestreadoRecibido"></param>
        /// <param name="posicionClienteParaAccionarRecibida"></param>
        public AltaModifCliente(List<Cliente> listaClientesRecibida, string ordenMuestreadoRecibido, int posicionClienteParaAccionarRecibida)
        {
            InitializeComponent();

            pictureBox_Gris9.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema9.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris10.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema10.Image = UserUI.Properties.Resources.Tono4;

            listaClientes = listaClientesRecibida;
            ordenMuestra = ordenMuestreadoRecibido;
            posicionCliente = posicionClienteParaAccionarRecibida;

            if (ordenMuestra == "Modificar")
            {
                label_InfoCreacionCliente.Text = "Bienvenido al menu de modificacion de un Cliente";
                label_IngreseNombreABMCliente.Text = "Ingrese el nuevo nombre";
                label_IngreseSaldoInicialABMCliente.Text = "Ingrese el nuevo saldo";
                label_IngreseDNIABMCliente.Text = "Ingrese el nuevo dni";
                label_IngreseDireccionABMCliente.Text = "Ingrese la nueva direccion";
                label_IngreseTelefonoABMCliente.Text = "Ingrese el nuevo telefono";
            }
        }

        /// <summary>
        /// Se ejecuta al presionar el boton "Ingresar datos", dependiendo de la orden modifica 
        /// o agrega un cliente. Ambas obtienen y validan los datos ingresados, y en el caso de modificar 
        /// reemplaza los datos por los nuevos datos ingresados, en el caso de agregar agrega un nuevo 
        /// cliente a la lista de clientes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IngresarDatosCliente_Click(object sender, EventArgs e)
        {

            if (ordenMuestra == "Modificar")
            {

                bool clienteRepetido = false;
                double saldoAux = 0; //CREO UN SALDO AUX ANTES QUE USAR EL DE CLIENTE AUX PORQUE NO PUEDO USAR UN OUT CON UNA PROPIEDAD -> int.TryParse(textBox_EdadIngresadaABMAdmin.Text, out adminAux.Edad); MAL
                double.TryParse(textBox_SaldoInicIngresadoABMCliente.Text, out saldoAux);

                clienteAux = new Cliente();

                int resultadoValidacionNombre = clienteAux.IsValidName(textBox_NombreIngresadoABMCliente.Text);
                int resultadoValidacionSaldo = clienteAux.IsValidSaldo(saldoAux);
               
                //Si parece estar todo bien. Uso el admin aux para modificarlo finalmente a la lista (habiendo cargado ya los datos validados)
                if (resultadoValidacionSaldo == 0 && resultadoValidacionNombre == 0)
                {
                    clienteAux.Nombre = textBox_NombreIngresadoABMCliente.Text;
                    clienteAux.Saldo = saldoAux;
                    clienteAux.Dni = textBox_DNIIngresadoABMCliente.Text;
                    clienteAux.DireccionDomicilio = textBox_DireccionIngresadaABMCliente.Text;
                    clienteAux.Telefono = textBox_TelefonoIngresadoABMCliente.Text;

                    //A ESTE PUNTO CARGAMOS YA TODOS LOS CAMPOS. SOLO QUEDA VERIFICAR SI EXISTE OTRO USUARIO CLIENTE CON ESTE NOMBRE DE USUARIO.
                    foreach (Cliente cliente in listaClientes)
                    {
                        //Si existe una coincidencia en los nombres de clientes. ERROR!!!!
                        if (cliente.Nombre == clienteAux.Nombre && cliente.Dni == clienteAux.Dni && cliente.Telefono == clienteAux.Telefono && cliente.Dni == clienteAux.Dni && cliente.DireccionDomicilio == clienteAux.DireccionDomicilio && cliente.Saldo == clienteAux.Saldo)
                        {
                            clienteRepetido = true;
                            break;
                        }
                    }

                    if (clienteRepetido == true)
                    {
                        MessageBox.Show("No se pudo modificar el cliente. Se esta intentando modificar con los mismo datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        listaClientes[posicionCliente].Nombre = clienteAux.Nombre;
                        listaClientes[posicionCliente].Saldo = saldoAux;
                        listaClientes[posicionCliente].Dni = clienteAux.Dni;
                        listaClientes[posicionCliente].DireccionDomicilio = clienteAux.DireccionDomicilio;
                        listaClientes[posicionCliente].Telefono = clienteAux.Telefono;

                        MessageBox.Show("El cliente se ha modificado correctamente.", "Modificacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }

                }
                else
                {
                    MessageBox.Show("No se pudo modificar el cliente. Existen datos invalidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else if (ordenMuestra == "Agregar")
            {
                
                bool clienteRepetido = false;
                double saldoAux = 0; //CREO UN EDAD AUX ANTES QUE USAR EL DE ADMIN AUX PORQUE NO PUEDO USAR UN OUT CON UNA PROPIEDAD -> int.TryParse(textBox_EdadIngresadaABMAdmin.Text, out adminAux.Edad); MAL
                double.TryParse(textBox_SaldoInicIngresadoABMCliente.Text, out saldoAux);

                clienteAux = new Cliente();

                int resultadoValidacionNombre = clienteAux.IsValidName(textBox_NombreIngresadoABMCliente.Text);
                int resultadoValidacionSaldo = clienteAux.IsValidSaldo(saldoAux);

                //Si parece estar todo bien. Uso el admin aux para agregarlo finalmente a la lista (habiendo cargado ya los datos validados)
                if (resultadoValidacionSaldo == 0 && resultadoValidacionNombre == 0)
                {
                    clienteAux = new Cliente(textBox_NombreIngresadoABMCliente.Text,saldoAux,textBox_DNIIngresadoABMCliente.Text,textBox_DireccionIngresadaABMCliente.Text,textBox_TelefonoIngresadoABMCliente.Text);

                    //A ESTE PUNTO CARGAMOS YA TODOS LOS CAMPOS. SOLO QUEDA VERIFICAR SI EXISTE OTRO CLIENTE CON ESTE NOMBRE DE USUARIO.
                    foreach (Cliente cliente in listaClientes)
                    {
                        //Si existe una coincidencia en los nombres de usuarios. ERROR!!!!
                        if (cliente.Nombre == clienteAux.Nombre && cliente.Dni == clienteAux.Dni && cliente.Telefono == clienteAux.Telefono && cliente.Dni == clienteAux.Dni && cliente.DireccionDomicilio == clienteAux.DireccionDomicilio && cliente.Saldo == clienteAux.Saldo)
                        {
                            clienteRepetido = true;
                            break;
                        }
                    }

                    if (clienteRepetido == true)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo cliente. Ya existe uno con esos mismos datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Lo agrego a la lista de clintes
                        listaClientes.Add(clienteAux);
                        MessageBox.Show("Se ha agregado correctamente el cliente.", "Creacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo agregar un nuevo cliente. Existen datos invalidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
