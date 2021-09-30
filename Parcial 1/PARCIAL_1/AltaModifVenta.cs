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
    public partial class AltaModifVenta : Form
    {
        List<Venta> listaVentas;
        List<Producto> listaProductos;
        List<Cliente> listaClientes;
        string accionARealizar;

        int idCliente;
        int idProducto;
        int cantUnidadesAdquiridas;
        int idUsuarioCreador;
        bool esAdmin;

        /// <summary>
        /// Constructor del form AltaModifVenta, recibe la lista de ventas, productos y clientes, 
        /// la orden, el id del usuario creador de la venta y su rango, ademas de la posicion de 
        /// la venta (En caso de que se quiera modificar, si no, este ultimo valor se ignorara)
        /// </summary>
        /// <param name="listaVentasRecibida"></param>
        /// <param name="listaProdutosRecibida"></param>
        /// <param name="listaClientesRecibida"></param>
        /// <param name="accionARealizarRecibida"></param>
        /// <param name="idUsuarioCreadorRecibido"></param>
        /// <param name="esAdminRecibido"></param>
        /// <param name="posicionItemRecibida"></param>
        public AltaModifVenta(List<Venta> listaVentasRecibida, List<Producto> listaProdutosRecibida, List<Cliente> listaClientesRecibida, string accionARealizarRecibida, int idUsuarioCreadorRecibido, bool esAdminRecibido)
        {
            InitializeComponent();

            pictureBox_Gris15.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema15.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris16.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema16.Image = UserUI.Properties.Resources.Tono4;

            listaVentas = listaVentasRecibida;
            listaProductos = listaProdutosRecibida;
            listaClientes = listaClientesRecibida;
            accionARealizar = accionARealizarRecibida;
            idUsuarioCreador = idUsuarioCreadorRecibido;
            esAdmin = esAdminRecibido;

        }

        /// <summary>
        /// Se ejecuta al presionar el boton "Ingresar datos", recibe la orden o accion a realizar 
        /// (solo "Agregar" por el momento), obtiene y valida los datos ingresados, crea la nueva venta 
        /// y la añade a la lista de ventas... restandole tambien el saldo al cliente y las unidades al producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IngresarDatosClienteyProductoABMVenta_Click(object sender, EventArgs e)
        {
            switch (accionARealizar)
            {
                case "Agregar":
                    {

                        Cliente clienteAux = new Cliente();
                        Producto productoAux = new Producto();
                       
                        int.TryParse(textBox_IdCompradorIngresadoABMVenta.Text,out idCliente);
                        int.TryParse(textBox_IDProductoIngresadoABMVenta.Text, out idProducto);
                        int.TryParse(textBox_CantUnidadesAdquiridasABMVenta.Text, out cantUnidadesAdquiridas);

                        int resultadoValidacionIdCliente = clienteAux.IsValidClienteId(idCliente);
                        int resultadoValidacionIdProducto = Producto.IsValidProductoId(idProducto);
                        int resultadoValidacionUnidadesAdquiridas = Venta.IsValidCantidadUnidadesAdquiridas(cantUnidadesAdquiridas);

                        if (resultadoValidacionIdCliente == 0 && resultadoValidacionIdProducto == 0 && resultadoValidacionUnidadesAdquiridas == 0)
                        {
                            int resultadoExistenciaIdProducto;
                            int posicionDelProducto;

                            int resultadoExistenciaIdCliente;
                            int posicionDelCliente;

                            Producto productoEncontrado;
                            Cliente clienteEncontrado;

                            //Me llamo al metodo estatico del Pedido ID-form porque preciso sus buscar por id de PRODUCTO y de CLIENTES para laburar aca
                            productoEncontrado = PedidoIDForm.buscarPorId(listaProductos, idProducto, out posicionDelProducto, out resultadoExistenciaIdProducto);
                            clienteEncontrado = PedidoIDForm.buscarPorId(listaClientes, idCliente, out posicionDelCliente, out resultadoExistenciaIdCliente);

                            //Si existen tanto el supuesto comprador como el supueto producto...
                            if (resultadoExistenciaIdProducto == 0 && resultadoExistenciaIdCliente == 0)
                            {

                                //Necesito saber si el producto tiene las unidades suficientes para la compra y si el cliente tiene el saldo suficiente...
                                bool tieneSaldoSuficiente;
                                bool productoTieneUnidadesSuficientes;

                                tieneSaldoSuficiente = Venta.TieneSaldoSuficiente(clienteEncontrado, productoEncontrado, cantUnidadesAdquiridas);
                                productoTieneUnidadesSuficientes = Venta.TieneUnidadesSuficientesProducto(cantUnidadesAdquiridas, productoEncontrado);

                                //Si existe un saldo suficientes y unas unidades suficientes
                                if (tieneSaldoSuficiente == true && productoTieneUnidadesSuficientes == true)
                                {
                                    //Creo la venta como tal
                                    Venta nuevaVenta = new Venta(clienteEncontrado, productoEncontrado, cantUnidadesAdquiridas,idUsuarioCreador,esAdmin);

                                    //Resto el saldo cliente y unidades producto
                                    nuevaVenta++;

                                    //Agrego la nueva venta a la lista
                                    listaVentas.Add(nuevaVenta);
                                    MessageBox.Show("Se ha agregado correctamente la venta.", "Creacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    if (tieneSaldoSuficiente == false && productoTieneUnidadesSuficientes == false)
                                    {
                                        MessageBox.Show("El comprador no tiene el saldo suficiente para adquirir el producto y el producto no tiene suficientes unidades disponibles para las unidades que se desean adquirir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else if (tieneSaldoSuficiente == false)
                                    {
                                        MessageBox.Show("El comprador no tiene el saldo suficiente para adquirir el producto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else if (productoTieneUnidadesSuficientes == false)
                                    {
                                        MessageBox.Show("El producto no tiene suficientes unidades disponibles para las unidades que se desean adquirir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }


            
                            }
                            else
                            {
                                if (resultadoExistenciaIdProducto != 0 && resultadoExistenciaIdCliente != 0) //Si dieron mal las 2 IDS
                                {
                                    MessageBox.Show("No existe ningun producto ni cliente con esas IDS.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (resultadoExistenciaIdProducto != 0) //Si dio mal solo el id del producto
                                {
                                    MessageBox.Show("No existe ningun producto con esa ID","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (resultadoExistenciaIdCliente != 0) //Si dio mal solo el id del cliente
                                {
                                    MessageBox.Show("No existe ningun cliente con esa ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Existen errores en el ingreso de los IDS","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
       
                        break;
                    } 
                    #region CODIGO SUSPENDIDO DE MODIFICACION DE VENTA
                    /*case "Modificar":
                    {
                        Cliente clienteAux = new Cliente();
                        Producto productoAux = new Producto();

                        int.TryParse(textBox_IdCompradorIngresadoABMVenta.Text, out idCliente);
                        int.TryParse(textBox_IDProductoIngresadoABMVenta.Text, out idProducto);
                        int.TryParse(textBox_CantUnidadesAdquiridasABMVenta.Text, out cantUnidadesAdquiridas);

                        int resultadoValidacionIdCliente = clienteAux.IsValidClienteId(idCliente);
                        int resultadoValidacionIdProducto = Producto.IsValidProductoId(idProducto);
                        int resultadoValidacionUnidadesAdquiridas = Venta.IsValidCantidadUnidadesAdquiridas(cantUnidadesAdquiridas);

                        if (resultadoValidacionIdCliente == 0 && resultadoValidacionIdProducto == 0 && resultadoValidacionUnidadesAdquiridas == 0)
                        {
                            int resultadoExistenciaIdProducto;
                            int posicionDelProducto;

                            int resultadoExistenciaIdCliente;
                            int posicionDelCliente;

                            Producto productoEncontrado;
                            Cliente clienteEncontrado;

                            //Me llamo al metodo estatico del Pedido ID-form porque preciso sus buscar por id de PRODUCTO y de CLIENTES para laburar aca
                            productoEncontrado = PedidoIDForm.buscarPorId(listaProductos, idProducto, out posicionDelProducto, out resultadoExistenciaIdProducto);
                            clienteEncontrado = PedidoIDForm.buscarPorId(listaClientes, idCliente, out posicionDelCliente, out resultadoExistenciaIdCliente);

                            //Si existen tanto el supuesto comprador como el supuesto producto...
                            if (resultadoExistenciaIdProducto == 0 && resultadoExistenciaIdCliente == 0)
                            {

                                listaVentas[posicionVenta].ClienteComprador = clienteEncontrado;
                                listaVentas[posicionVenta].ProductoAdquirido = productoEncontrado;
                                listaVentas[posicionVenta].CantidadesUnidadesAdquiridas = cantUnidadesAdquiridas;

                                MessageBox.Show("Se ha modificado correctamente la venta.", "Modificacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (resultadoExistenciaIdProducto != 0 && resultadoExistenciaIdCliente != 0) //Si dieron mal las 2 IDS
                                {
                                    MessageBox.Show("No se pudo modificar. No existe ningun producto ni cliente con esas IDS.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (resultadoExistenciaIdProducto != 0) //Si dio mal solo el id del producto
                                {
                                    MessageBox.Show("No se pudo modificar. No existe ningun producto con esa ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (resultadoExistenciaIdCliente != 0) //Si dio mal solo el id del cliente
                                {
                                    MessageBox.Show("No se pudo modificar. No existe ningun cliente con esa ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Existen errores en el ingreso de los IDS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        break;
                    }*/
                    #endregion
            }

            //Sea como sea que salga el alta, bajar o modificacion cierro este form 
            this.Close();

        }

    }
}
