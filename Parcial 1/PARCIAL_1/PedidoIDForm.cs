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
    public partial class PedidoIDForm : Form
    {
        List<Administrador> listaAdministradores;
        List<Empleado> listaEmpleados;
        List<Producto> listaProductos;
        List<Venta> listaVentas;
        List<Cliente> listaClientes;
        int valorNumeroIdIngresado = -1;
        string seccion;
        string accionARealizar;


        /// <summary>
        /// Constructor del PedidoIDForm recibe la seccion elegida, la accion a realizar y todas las listas 
        /// de todos los elementos
        /// </summary>
        /// <param name="seccionRecibida"></param>
        /// <param name="accionARealizarRecibida"></param>
        /// <param name="listaAdministradoresRecibida"></param>
        /// <param name="listaEmpleadosRecibida"></param>
        /// <param name="listaVentasRecibida"></param>
        /// <param name="listaProductosRecibida"></param>
        /// <param name="listaClientesRecibida"></param>
        public PedidoIDForm(string seccionRecibida,string accionARealizarRecibida,List<Administrador> listaAdministradoresRecibida, List<Empleado> listaEmpleadosRecibida, List<Venta> listaVentasRecibida, List<Producto> listaProductosRecibida, List<Cliente> listaClientesRecibida)
        {
            InitializeComponent();

            pictureBox_Gris17.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema17.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris18.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema18.Image = UserUI.Properties.Resources.Tono4;

            listaAdministradores = listaAdministradoresRecibida;
            listaEmpleados = listaEmpleadosRecibida;
            listaProductos = listaProductosRecibida;
            listaVentas = listaVentasRecibida;
            listaClientes = listaClientesRecibida;
            seccion = seccionRecibida;
            accionARealizar = accionARealizarRecibida;
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Ingresar ID", guarda el valor del id ingresado, valida si es 
        /// algo coherente, y busca el elemento en la lista correspondiente dependiendo de la seccion elegida, 
        /// si existe el item, se realizara la accion recibida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IngresarId_Click(object sender, EventArgs e)
        {
            //Me guardo el valor que me ingreso el usuario
            string valorIdNumeroIngresadoString = textBox_ValorIdIngresado.Text;

            if (isValidId(valorIdNumeroIngresadoString) == 0)
            {
                int.TryParse(textBox_ValorIdIngresado.Text, out valorNumeroIdIngresado);

                int resultadoBusqueda;
                int posicionItem;

                switch (seccion)
                {
                    case "Clientes":
                        {
                            Cliente itemEncontrado;
                            itemEncontrado = buscarPorId(listaClientes, valorNumeroIdIngresado, out posicionItem, out resultadoBusqueda);

                            if (resultadoBusqueda < 0) //NO EXISTE EL ITEM
                            {
                                MessageBox.Show("No se encontro ningun cliente con esa ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (resultadoBusqueda == 0) //EXISTE EL ITEM
                            {
                                if (accionARealizar == "Modificar")
                                {
                                    AltaModifCliente formAltaModCliente = new AltaModifCliente(listaClientes, accionARealizar, posicionItem);
                                    formAltaModCliente.Text = "Modificar cliente";
                                    formAltaModCliente.ShowDialog();

                                }
                                else if (accionARealizar == "Eliminar")
                                {
                                    DialogResult confirmacion;
                                    confirmacion = MessageBox.Show($"¿Seguro desea eliminar el cliente con el id: {valorNumeroIdIngresado}? Las ventas que le pertenezcan no seran afectadas, sin embargo referenciaran a un cliente inexistente. Esta accion es irreversible.", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (confirmacion == DialogResult.Yes)
                                    {
                                        listaClientes.Remove(itemEncontrado);
                                        MessageBox.Show($"La eliminacion fue satisfactoria", "Eliminacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                    }
                                    else if (confirmacion == DialogResult.No)
                                    {
                                        MessageBox.Show($"La eliminacion fue cancelada", "Eliminacion cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                               
                            }

                            break;
                        }
                    case "Stock":
                        {
                            Producto itemEncontrado;
                            itemEncontrado = buscarPorId(listaProductos, valorNumeroIdIngresado, out posicionItem, out resultadoBusqueda);

                            if (resultadoBusqueda < 0) //NO EXISTE EL ITEM
                            {
                                MessageBox.Show("No se encontro ningun producto con esa ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (resultadoBusqueda == 0) //EXISTE EL ITEM
                            {
                                if (accionARealizar == "Modificar")
                                {
                                    AltaModifProducto formAltaModfProducto = new AltaModifProducto(listaProductos, accionARealizar, posicionItem);
                                    formAltaModfProducto.Text = "Modificar producto";
                                    formAltaModfProducto.ShowDialog();

                                }
                                else if (accionARealizar == "Eliminar")
                                {

                                    DialogResult confirmacion;
                                    confirmacion = MessageBox.Show($"¿Seguro desea eliminar el producto con el id: {valorNumeroIdIngresado}? Esta accion es irreversible.", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (confirmacion == DialogResult.Yes)
                                    {
                                        listaProductos.Remove(itemEncontrado);
                                        MessageBox.Show($"La eliminacion fue satisfactoria", "Eliminacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                    }
                                    else if (confirmacion == DialogResult.No)
                                    {
                                        MessageBox.Show($"La eliminacion fue cancelada", "Eliminacion cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }

                              
                            }

                            break;
                        }
                    case "Ventas":
                        {

                            switch (accionARealizar)
                            {
                                case "Facturar":
                                    {
                                        Venta ventaEncontrada;
                                        ventaEncontrada = buscarPorId(listaVentas, valorNumeroIdIngresado, out posicionItem, out resultadoBusqueda);

                                        if (resultadoBusqueda < 0) //NO EXISTE EL ITEM
                                        {
                                            MessageBox.Show("No se encontro ninguna venta con esa ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else
                                        {
                                            Facturacion.ExportarFactura(ventaEncontrada);
                                             MessageBox.Show("Factura exportada a txt correctamente", "Factura lista", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        break;
                                    }     
                                case "Modificar":
                                    {
                                        #region CODIGO SUSPENDIDO DE MODIFICACION VENTA
                                        /*
                                        Venta itemEncontrado;
                                        itemEncontrado = buscarPorId(listaVentas, valorNumeroIdIngresado, out posicionItem, out resultadoBusqueda);

                                        if (resultadoBusqueda < 0) //NO EXISTE EL ITEM
                                        {
                                            MessageBox.Show("No se encontro ninguna venta con esa ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else if (resultadoBusqueda == 0) //EXISTE EL ITEM
                                        {

                                            AltaModifVenta formAltaModfVenta = new AltaModifVenta(listaVentas, listaProductos, listaClientes, accionARealizar,posicionItem);
                                            formAltaModfVenta.Text = "Modificar venta";
                                            //this.Hide();
                                            formAltaModfVenta.ShowDialog();

                                        }
                                        */
                                        #endregion
                                        break;
                                         
                                    }
                               
                                case "Eliminar":
                                    {
                                        Venta ventaEncontrada;
                                        ventaEncontrada = buscarPorId(listaVentas, valorNumeroIdIngresado, out posicionItem, out resultadoBusqueda);

                                        if (resultadoBusqueda < 0) //NO EXISTE EL ITEM
                                        {
                                            MessageBox.Show("No se encontro ninguna venta con esa ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else
                                        {
                                            DialogResult confirmacion;
                                            confirmacion = MessageBox.Show($"¿Seguro desea eliminar la venta con el id: {valorNumeroIdIngresado}? Esto causara que se devuelva el saldo de la venta al cliente y las unidades adquiridas al producto. Esta accion es irreversible.", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                            if (confirmacion == DialogResult.Yes)
                                            {
                                                //DESHACER VENTA
                                                ventaEncontrada--;
                                                listaVentas.Remove(ventaEncontrada);
                                                MessageBox.Show($"La eliminacion fue satisfactoria", "Eliminacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                            }
                                            else if (confirmacion == DialogResult.No)
                                            {
                                                MessageBox.Show($"La eliminacion fue cancelada", "Eliminacion cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }

                                        break;
                                    }
                            }

                            
                            break;
                        }
                    case "Empleados":
                        {

                            Empleado itemEncontrado;
                            itemEncontrado = buscarPorId(listaEmpleados, valorNumeroIdIngresado, out posicionItem, out resultadoBusqueda);

                            if (resultadoBusqueda < 0) //NO EXISTE EL ITEM
                            {
                                MessageBox.Show("No se encontro ningun empleado con esa ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (resultadoBusqueda == 0) //EXISTE EL ITEM
                            {
                                #region CODIGO SUSPENDIDO MODIFICACION EMPLEADO
                                /*if (accionARealizar == "Modificar")
                                {
                                    AltaModifEmpleado formAltaModfEmpleado = new AltaModifEmpleado(listaEmpleados, accionARealizar, posicionItem);
                                    formAltaModfEmpleado.Text = "Modificar empleado";
                                    formAltaModfEmpleado.ShowDialog();

                                }*/
                                #endregion

                                if (accionARealizar == "Eliminar") 
                                {
                                    bool esElUnicoEmpleado = false;

                                    if (listaEmpleados.Count == 1)
                                    {
                                        esElUnicoEmpleado = true;
                                    }

                                    if (esElUnicoEmpleado == false)
                                    {
                                        DialogResult confirmacion;
                                        confirmacion = MessageBox.Show($"¿Seguro desea eliminar al empleado con el id: {valorNumeroIdIngresado}? Esta accion es irreversible.", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (confirmacion == DialogResult.Yes)
                                        {
                                            listaEmpleados.Remove(itemEncontrado);
                                            MessageBox.Show($"La eliminacion fue satisfactoria", "Eliminacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                        }
                                        else if (confirmacion == DialogResult.No)
                                        {
                                            MessageBox.Show($"La eliminacion fue cancelada", "Eliminacion cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"La eliminacion fue cancelada debido a que no se puede eliminar el unico empleado existente", "Eliminacion cancelada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                              
                            }

                            break;
                        }
                    case "Administradores":
                        {

                            Administrador itemEncontrado;
                            itemEncontrado = buscarPorId(listaAdministradores, valorNumeroIdIngresado, out posicionItem, out resultadoBusqueda);

                            if (resultadoBusqueda < 0) //NO EXISTE EL ITEM
                            {
                                MessageBox.Show("No se encontro ningun administrador con esa ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else if (resultadoBusqueda == 0) //EXISTE EL ITEM
                            {
                                #region CODIGO SUSPENDIDO MODIFICACION ADMINISTRADOR
                                /*if (accionARealizar == "Modificar")
                                {
                                    AltaModifAdministrador formAltaModfAdmin = new AltaModifAdministrador(listaAdministradores, accionARealizar, posicionItem);
                                    formAltaModfAdmin.Text = "Modificar administrador";
                                    formAltaModfAdmin.ShowDialog();

                                }*/
                                #endregion

                                if (accionARealizar == "Eliminar")
                                {
                                    bool esElUnicoAdmin = false;

                                    if (listaAdministradores.Count == 1)
                                    {
                                        esElUnicoAdmin = true;
                                    }

                                    if (esElUnicoAdmin == false)
                                    {
                                        DialogResult confirmacion;
                                        confirmacion = MessageBox.Show($"¿Seguro desea eliminar al administrador con el id: {valorNumeroIdIngresado}? Esta accion es irreversible.", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (confirmacion == DialogResult.Yes)
                                        {
                                            listaAdministradores.Remove(itemEncontrado);
                                            MessageBox.Show($"La eliminacion fue satisfactoria", "Eliminacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        }
                                        else if (confirmacion == DialogResult.No)
                                        {
                                            MessageBox.Show($"La eliminacion fue cancelada", "Eliminacion cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"La eliminacion fue cancelada debido a que no se puede eliminar el unico admin existente", "Eliminacion cancelada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                                
                            }

                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show($"Hay caracteres invalidos", "Ingreso del id cancelado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();

        }


        /// <summary>
        /// Recibe una lista de administradores, un id para buscar, una variable para ser cargada con la 
        /// posicion del administrador si es que se encuentra, y una variable para ser cargada con el resultado
        /// de la busqueda del administrador.
        /// </summary>
        /// <param name="listaAdministradoresRecibida"></param>
        /// <param name="idParaBuscar"></param>
        /// <param name="posDelItem"></param>
        /// <param name="resultadoBusqueda"></param>
        /// <returns></returns>
        static public Administrador buscarPorId(List<Administrador> listaAdministradoresRecibida, int idParaBuscar,out int posDelItem, out int resultadoBusqueda)
        {
            Administrador adminAux = new Administrador("null",0,"null","null");
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
        ///  Recibe una lista de clientes, un id para buscar, una variable para ser cargada con la posicion 
        ///  del cliente si es que se encuentra, y una variable para ser cargada con el resultado de la 
        ///  busqueda del cliente.
        /// </summary>
        /// <param name="listaClientesRecibida"></param>
        /// <param name="idParaBuscar"></param>
        /// <param name="posDelItem"></param>
        /// <param name="resultadoBusqueda"></param>
        /// <returns></returns>
        static public Cliente buscarPorId(List<Cliente> listaClientesRecibida, int idParaBuscar, out int posDelItem, out int resultadoBusqueda)
        {
            Cliente clienteAux = new Cliente();
            Cliente retorno = clienteAux;
            resultadoBusqueda = -1;
            posDelItem = -1;
            int contador = 0;

            foreach (Cliente item in listaClientesRecibida)
            {
                if (idParaBuscar == item.IdCliente) //Si la id que busco llega a coincidir con algun item que la posee
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
        ///  Recibe una lista de empleados, un id para buscar, una variable para ser cargada con la posicion 
        ///  del empleado si es que se encuentra, y una variable para ser cargada con el resultado de la
        ///  busqueda del empleado.
        /// </summary>
        /// <param name="listaEmpleadosRecibida"></param>
        /// <param name="idParaBuscar"></param>
        /// <param name="posDelItem"></param>
        /// <param name="resultadoBusqueda"></param>
        /// <returns></returns>
        static public Empleado buscarPorId(List<Empleado> listaEmpleadosRecibida, int idParaBuscar, out int posDelItem, out int resultadoBusqueda)
        {
            Empleado empleadoAux = new Empleado(0,"null","null",0,"null","null","null");
            Empleado retorno = empleadoAux;
            resultadoBusqueda = -1;
            posDelItem = -1;
            int contador = 0;

            foreach (Empleado item in listaEmpleadosRecibida)
            {
                if (idParaBuscar == item.IdEmpleado) //Si la id que busco llega a coincidir con algun item que la posee
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
        ///  Recibe una lista de productos, un id para buscar, una variable para ser cargada con la posicion 
        ///  del producto si es que se encuentra, y una variable para ser cargada con el resultado de la
        ///  busqueda del producto.
        /// </summary>
        /// <param name="listaProductosRecibida"></param>
        /// <param name="idParaBuscar"></param>
        /// <param name="posDelItem"></param>
        /// <param name="resultadoBusqueda"></param>
        /// <returns></returns>
        static public Producto buscarPorId(List<Producto> listaProductosRecibida, int idParaBuscar, out int posDelItem, out int resultadoBusqueda)
        {
            Producto productoAux = new Producto();
            Producto retorno = productoAux;
            resultadoBusqueda = -1;
            posDelItem = -1;
            int contador = 0;

            foreach (Producto item in listaProductosRecibida)
            {
                if (idParaBuscar == item.IdProducto) //Si la id que busco llega a coincidir con algun item que la posee
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
        ///  Recibe una lista de ventas, un id para buscar, una variable para ser cargada con la posicion 
        ///  de la venta si es que se encuentra, y una variable para ser cargada con el resultado de la 
        ///  busqueda de la venta.
        /// </summary>
        /// <param name="listaVentasRecibida"></param>
        /// <param name="idParaBuscar"></param>
        /// <param name="posDelItem"></param>
        /// <param name="resultadoBusqueda"></param>
        /// <returns></returns>
        static public Venta buscarPorId(List<Venta> listaVentasRecibida, int idParaBuscar, out int posDelItem, out int resultadoBusqueda)
        {
            Venta ventaAux = new Venta();
            Venta retorno = ventaAux;
            resultadoBusqueda = -1;
            posDelItem = -1;
            int contador = 0;

            foreach (Venta item in listaVentasRecibida)
            {
                if (idParaBuscar == item.IdVenta) //Si la id que busco llega a coincidir con algun item que la posee
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
        /// Recibe un string representativo a un id y valida si es o no coherente.
        /// </summary>
        /// <param name="idRecibido"></param>
        /// <returns></returns>
        private int isValidId(string idRecibido)
        {
            int retorno = 0;

            if (idRecibido.Contains(" ") == false && idRecibido != "")
            {

                for (int i = 0; i < idRecibido.Length; i++)
                {
                    if (idRecibido[i] < '0' || idRecibido[i] > '9')
                    {
                        retorno = -1;
                        break;
                    }

                }
            }
            else
            {
                retorno = -1;
            }

  
            return retorno;
        }
    }
}
