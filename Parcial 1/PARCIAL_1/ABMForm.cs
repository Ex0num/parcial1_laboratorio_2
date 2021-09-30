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
    public partial class ABMForm : Form
    {
        List<Administrador> listaAdministradores;
        List<Empleado> listaEmpleados;
        List<Producto> listaProductos;
        List<Venta> listaVentas;
        List<Cliente> listaClientes;
        string seccionElegida;
        bool esAdmin;
        int idUsuarioLogeado;

        /// <summary>
        /// Constructor del ABMForm, recibe la seccion elegida, el id y el rango del usuario logeado, y 
        /// todas las listas de todos los elementos, para llamar al metodo refresh y mostrar en la lista de 
        /// elementos (dependiendo de la seccion elegida) lo que corresponda. Ejemplo: Si seccion elegida es 
        /// empleados, se van a llamar al refresh pasandole la lista de empleados.
        /// </summary>
        /// <param name="SeccionElegidaRecibida"></param>
        /// <param name="listaAdministradoresRecibida"></param>
        /// <param name="listaEmpleadosRecibida"></param>
        /// <param name="listaVentasRecibida"></param>
        /// <param name="listaProductosRecibida"></param>
        /// <param name="listaClientesRecibida"></param>
        /// <param name="esAdminRecibido"></param>
        /// <param name="idUsuarioLogeadoRecibido"></param>
        public ABMForm(string SeccionElegidaRecibida, List<Administrador> listaAdministradoresRecibida, List<Empleado> listaEmpleadosRecibida, List<Venta> listaVentasRecibida, List<Producto> listaProductosRecibida, List<Cliente> listaClientesRecibida, bool esAdminRecibido, int idUsuarioLogeadoRecibido)
        {
            InitializeComponent();

            esAdmin = esAdminRecibido;
            seccionElegida = SeccionElegidaRecibida;
            listaAdministradores = listaAdministradoresRecibida;
            listaEmpleados = listaEmpleadosRecibida;
            listaProductos = listaProductosRecibida;
            listaVentas = listaVentasRecibida;
            listaClientes = listaClientesRecibida;
            idUsuarioLogeado = idUsuarioLogeadoRecibido;

            button_FacturarVentaABMVentas.Hide();
            agregarDatosComboBoxFiltrosABM(seccionElegida, esAdmin);

            //Mostrar por primera vez los Objetos
            switch (seccionElegida)
            {
                case "Clientes":
                    {
                        Refresh(listaClientes);
                        groupBox_ListaItemsABM.Text = "Listado de clientes";

                        break;
                    }
                case "Stock":
                    {
                        Refresh(listaProductos);
                        groupBox_ListaItemsABM.Text = "Listado de productos";

                        if (esAdmin == true)
                        {
                            groupBox_Opciones.Width = 167;
                            groupBox_Opciones.Height = 166;
                            button_InversionMasCaraABMProductos.Show();
                        }

                        break;
                    }
                case "Ventas":
                    {
                        //DISEÑO
                        button_Modificar.Hide();
                        button_FacturarVentaABMVentas.Location = button_Modificar.Location;
                        button_FacturarVentaABMVentas.Show();

                        Refresh(listaVentas, esAdmin);
                        groupBox_ListaItemsABM.Text = "Listado de ventas";

                        break;
                    }
                case "Empleados":
                    {
                        Refresh(listaEmpleados);
                        groupBox_ListaItemsABM.Text = "Listado de empleados";

                        //DISEÑO
                        button_Modificar.Hide();
                        groupBox_Opciones.Width = 167;
                        groupBox_Opciones.Height = 107;


                        break;
                    }

                case "Administradores":
                    {
                        Refresh(listaAdministradores);
                        groupBox_ListaItemsABM.Text = "Listado de administradores";

                        //DISEÑO
                        button_Modificar.Hide();
                        groupBox_Opciones.Width = 167;
                        groupBox_Opciones.Height = 107;

                        break;
                    }
            }

        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Volver" y se hace un Close del form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_VolverABMAdministradores_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Modificar" y se llama al form PedidoIDForm indicandole 
        /// la seccion elegida, y la orden, en este caso modificar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Modificar_Click(object sender, EventArgs e)
        {
            //OJO ESTOS SON VALORES DEL ENUMERADO PERTENECIENTE AL FORM DE OPCIONES. SI SE CAMBIA ALGO AHI
            //DE ESE ENUMERADO. SE DEBE CAMBIAR ACA TAMBIEN (SE OBVIA LA OPCION DE CERRAR SESION)
            switch (seccionElegida)
            {
                case "Clientes":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Modificar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaClientes);
                        break;
                    }
                case "Stock":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Modificar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaProductos);
                        break;
                    }

                case "Ventas":
                    {

                        #region CODIGO SUSPENDIDO DE MODIFICACION VENTA
                        /*
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Modificar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaVentas);
                        */
                        #endregion
                        break;
                    }

                case "Empleados":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Modificar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaEmpleados);
                        break;
                    }

                case "Administradores":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Modificar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaAdministradores);
                        break;
                    }
            }

        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Agregar" y se llama al form AltaModif perteneciente a la 
        /// seccion que se eligio en el OptionsForm. Por ejemplo AltaModifCliente si se eligio a la seccion Cliente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Agregar_Click(object sender, EventArgs e)
        {

            //OJO ESTOS SON VALORES DEL ENUMERADO PERTENECIENTE AL FORM DE OPCIONES. SI SE CAMBIA ALGO AHI
            //DE ESE ENUMERADO. SE DEBE CAMBIAR ACA TAMBIEN (SE OBVIA LA OPCION DE CERRAR SESION)
            switch (seccionElegida)
            {
                case "Clientes":
                    {
                        AltaModifCliente formAltaModCliente = new AltaModifCliente(listaClientes, "Agregar", idUsuarioLogeado);

                        //Lo muestro como form modal y sigo mostrando el panel de control
                        formAltaModCliente.ShowDialog();
                        Refresh(listaClientes);
                        break;
                    }
                case "Stock":
                    {
                        AltaModifProducto formAltaModProducto = new AltaModifProducto(listaProductos, "Agregar", idUsuarioLogeado);

                        //Lo muestro como form modal y sigo mostrando el panel de control
                        formAltaModProducto.ShowDialog();
                        Refresh(listaProductos);
                        break;
                    }
                case "Ventas":
                    {

                        AltaModifVenta formPedidoIdAltaCamposVenta = new AltaModifVenta(listaVentas, listaProductos, listaClientes, "Agregar", idUsuarioLogeado, esAdmin);

                        //Lo muestro como form modal y sigo mostrando el panel de control
                        formPedidoIdAltaCamposVenta.ShowDialog();
                        Refresh(listaVentas, esAdmin);
                        break;
                    }
                case "Empleados":
                    {
                        AltaModifEmpleado formAltaModEmpleado = new AltaModifEmpleado(listaEmpleados, "Agregar");

                        //Lo muestro como form modal y sigo mostrando el panel de control
                        formAltaModEmpleado.ShowDialog();
                        Refresh(listaEmpleados);
                        break;
                    }

                case "Administradores":
                    {

                        AltaModifAdministrador formAltaModfAdmin = new AltaModifAdministrador(listaAdministradores, "Agregar");

                        //Lo muestro como form modal y sigo mostrando el panel de control
                        formAltaModfAdmin.ShowDialog();
                        Refresh(listaAdministradores);
                        break;
                    }
            }
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Eliminar" y se llama al form PedidoIDForm pasandole 
        /// la seccion y la orden a realizar para posterior a eso hacer una busqueda por id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Eliminar_Click(object sender, EventArgs e)
        {
            //OJO ESTOS SON VALORES DEL ENUMERADO PERTENECIENTE AL FORM DE OPCIONES. SI SE CAMBIA ALGO AHI
            //DE ESE ENUMERADO. SE DEBE CAMBIAR ACA TAMBIEN (SE OBVIA LA OPCION DE CERRAR SESION)
            switch (seccionElegida)
            {
                case "Clientes":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Eliminar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaClientes);
                        break;
                    }
                case "Stock":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Eliminar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaProductos);
                        break;
                    }
                case "Ventas":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Eliminar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaVentas, esAdmin);
                        break;
                    }
                case "Empleados":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Eliminar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaEmpleados);
                        break;
                    }

                case "Administradores":
                    {
                        //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
                        PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Eliminar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
                        formPedidoID.ShowDialog();
                        Refresh(listaAdministradores);
                        break;
                    }
            }
        }


        /// <summary>
        ///  Se ejecuta al presionar el boton "Facturar venta" y se llama al form PedidoIDForm 
        ///  pasandole la seccion y la orden a realizar para posterior a eso hacer una busqueda por id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_FacturarVentaABMVentas_Click(object sender, EventArgs e)
        {
            //ACA TENGO QUE MOSTRAR EL FORM QUE PIDE ID
            PedidoIDForm formPedidoID = new PedidoIDForm(seccionElegida, "Facturar", listaAdministradores, listaEmpleados, listaVentas, listaProductos, listaClientes);
            formPedidoID.ShowDialog();

        }


        /// <summary>
        /// Recibe una lista de administradores, limpia la lista de objetos e imprime todo administrador 
        /// que exista en la lista recibida.
        /// </summary>
        /// <param name="listaAdministradoresRecibida"></param>
        private void Refresh(List<Administrador> listaAdministradoresRecibida)
        {
            listBox_ListaObjetos.Items.Clear();

            foreach (Administrador administrador in listaAdministradores)
            {
                //Muestro todos los campos (CON SENTIDO) de administrador.
                listBox_ListaObjetos.Items.Add($"Nombre: {administrador.Nombre}");
                listBox_ListaObjetos.Items.Add($"Direc: {administrador.DireccionDomicilio}");
                listBox_ListaObjetos.Items.Add($"Edad: {administrador.Edad}");
                listBox_ListaObjetos.Items.Add($"Id: {administrador.Id}");
                listBox_ListaObjetos.Items.Add(" ");
            }
        }


        /// <summary>
        /// Recibe un administrador y lo busca en la lista de administradores, si efectivamente 
        /// existe agrega cada campo de este administrador a la lista de objetos.
        /// </summary>
        /// <param name="adminRecibido"></param>
        private void printAdmin(Administrador adminRecibido)
        {

            foreach (Administrador administrador in listaAdministradores)
            {
                if (administrador == adminRecibido)
                {
                    //Muestro todos los campos (CON SENTIDO) de administrador.
                    listBox_ListaObjetos.Items.Add($"Nombre: {administrador.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Direc: {administrador.DireccionDomicilio}");
                    listBox_ListaObjetos.Items.Add($"Edad: {administrador.Edad}");
                    listBox_ListaObjetos.Items.Add($"Id: {administrador.Id}");
                    listBox_ListaObjetos.Items.Add(" ");
                    break;
                }
            }
        }


        /// <summary>
        ///  Recibe una lista de empleados, limpia la lista de objetos e imprime todo empleado que 
        ///  exista en la lista recibida.
        /// </summary>
        /// <param name="listaEmpleadosRecibida"></param>
        private void Refresh(List<Empleado> listaEmpleadosRecibida)
        {
            listBox_ListaObjetos.Items.Clear();

            foreach (Empleado empleado in listaEmpleadosRecibida)
            {
                //Muestro todos los campos (CON SENTIDO) de empleado.
                listBox_ListaObjetos.Items.Add($"Nombre: {empleado.Nombre}");
                listBox_ListaObjetos.Items.Add($"Direc: {empleado.DireccionDomicilio}");
                listBox_ListaObjetos.Items.Add($"Edad: {empleado.Edad}");
                listBox_ListaObjetos.Items.Add($"DNI: {empleado.Dni}");
                listBox_ListaObjetos.Items.Add($"Estado civil: {empleado.EstadoCivil}");
                listBox_ListaObjetos.Items.Add($"Sueldo: {empleado.Sueldo}");
                listBox_ListaObjetos.Items.Add($"Id: {empleado.IdEmpleado}");
                listBox_ListaObjetos.Items.Add(" ");
            }
        }


        /// <summary>
        /// Recibe un empleado y lo busca en la lista de empleados, si efectivamente existe agrega cada 
        /// campo de este empleado a la lista de objetos.
        /// </summary>
        /// <param name="empleadoRecibido"></param>
        private void printEmpleado(Empleado empleadoRecibido)
        {

            foreach (Empleado empleado in listaEmpleados)
            {

                if (empleado == empleadoRecibido)
                {
                    //Muestro todos los campos (CON SENTIDO) de empleado.
                    listBox_ListaObjetos.Items.Add($"Nombre: {empleado.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Direc: {empleado.DireccionDomicilio}");
                    listBox_ListaObjetos.Items.Add($"Edad: {empleado.Edad}");
                    listBox_ListaObjetos.Items.Add($"DNI: {empleado.Dni}");
                    listBox_ListaObjetos.Items.Add($"Estado civil: {empleado.EstadoCivil}");
                    listBox_ListaObjetos.Items.Add($"Sueldo: {empleado.Sueldo}");
                    listBox_ListaObjetos.Items.Add($"Id: {empleado.IdEmpleado}");
                    listBox_ListaObjetos.Items.Add(" ");
                    break;
                }

            }
        }


        /// <summary>
        /// Recibe una lista de ventas y el rango del usuario logeado, limpia la lista de objetos e 
        /// imprime todo venta que exista en la lista recibida (Los empleados no ven el campo Rango).
        /// </summary>
        /// <param name="listaVentasRecibida"></param>
        /// <param name="esAdmin"></param>
        private void Refresh(List<Venta> listaVentasRecibida, bool esAdmin)
        {
            listBox_ListaObjetos.Items.Clear();

            if (esAdmin == true)
            {
                foreach (Venta venta in listaVentasRecibida)
                {
                    //Administrador ve que empleado creo que venta
                    listBox_ListaObjetos.Items.Add($"Nombre del comprador: {venta.ClienteComprador.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Nombre del producto adquirido: {venta.ProductoAdquirido.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Precio del producto abonado: {venta.ProductoAdquirido.Precio}");
                    listBox_ListaObjetos.Items.Add($"Unidades adquiridas: {venta.CantidadesUnidadesAdquiridas}");
                    listBox_ListaObjetos.Items.Add($"Id de la venta: {venta.IdVenta}");
                    listBox_ListaObjetos.Items.Add($"Id del usuario vendedor: {venta.IdUsuarioCreador}");

                    if (venta.EsAdmin == true)
                    {
                        listBox_ListaObjetos.Items.Add($"Rango: Administrador");
                    }
                    else
                    {
                        listBox_ListaObjetos.Items.Add($"Rango: Empleado");
                    }

                    listBox_ListaObjetos.Items.Add(" ");
                }
            }
            else
            {
                foreach (Venta venta in listaVentasRecibida)
                {

                    if (venta.IdUsuarioCreador == idUsuarioLogeado && venta.EsAdmin == false)
                    {
                        //Empleado ve todas las ventas que él creo y solo el.
                        listBox_ListaObjetos.Items.Add($"Nombre del comprador: {venta.ClienteComprador.Nombre}");
                        listBox_ListaObjetos.Items.Add($"Nombre del producto adquirido: {venta.ProductoAdquirido.Nombre}");
                        listBox_ListaObjetos.Items.Add($"Precio del producto abonado: {venta.ProductoAdquirido.Precio}");
                        listBox_ListaObjetos.Items.Add($"Unidades adquiridas: {venta.CantidadesUnidadesAdquiridas}");
                        listBox_ListaObjetos.Items.Add($"Id de la venta: {venta.IdVenta}");
                        listBox_ListaObjetos.Items.Add(" ");
                    }

                }

            }
        }


        /// <summary>
        /// Recibe una venta y lo busca en la lista de ventas, si efectivamente existe agrega cada 
        /// campo de esta venta a la lista de objetos.
        /// </summary>
        /// <param name="ventaRecibida"></param>
        private void printVenta(Venta ventaRecibida)
        {

            foreach (Venta venta in listaVentas)
            {
                if (venta == ventaRecibida && esAdmin == true)
                {
                    //Administrador ve que empleado creo que venta
                    listBox_ListaObjetos.Items.Add($"Nombre del comprador: {venta.ClienteComprador.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Nombre del producto adquirido: {venta.ProductoAdquirido.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Precio del producto abonado: {venta.ProductoAdquirido.Precio}");
                    listBox_ListaObjetos.Items.Add($"Unidades adquiridas: {venta.CantidadesUnidadesAdquiridas}");
                    listBox_ListaObjetos.Items.Add($"Id de la venta: {venta.IdVenta}");
                    listBox_ListaObjetos.Items.Add($"Id del usuario vendedor: {venta.IdUsuarioCreador}");

                    if (venta.EsAdmin == true)
                    {
                        listBox_ListaObjetos.Items.Add($"Rango: Administrador");
                    }
                    else
                    {
                        listBox_ListaObjetos.Items.Add($"Rango: Empleado");
                    }

                    listBox_ListaObjetos.Items.Add(" ");
                }
                else if (venta == ventaRecibida && esAdmin == false)
                {
                    //Empleado ve todas las ventas que él creo y solo el.
                    listBox_ListaObjetos.Items.Add($"Nombre del comprador: {venta.ClienteComprador.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Nombre del producto adquirido: {venta.ProductoAdquirido.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Precio del producto abonado: {venta.ProductoAdquirido.Precio}");
                    listBox_ListaObjetos.Items.Add($"Unidades adquiridas: {venta.CantidadesUnidadesAdquiridas}");
                    listBox_ListaObjetos.Items.Add($"Id de la venta: {venta.IdVenta}");
                    listBox_ListaObjetos.Items.Add(" ");
                }

            }
        }


        /// <summary>
        /// Recibe una lista de productos, limpia la lista de objetos e imprime todo producto existente 
        /// en la lista recibida.
        /// </summary>
        /// <param name="listaProductosRecibida"></param>
        private void Refresh(List<Producto> listaProductosRecibida)
        {
            listBox_ListaObjetos.Items.Clear();

            foreach (Producto producto in listaProductosRecibida)
            {
                //Muestro todos los campos (CON SENTIDO) de producto.
                listBox_ListaObjetos.Items.Add($"Nombre: {producto.Nombre}");
                listBox_ListaObjetos.Items.Add($"Tipo: {producto.TipoProducto}");
                listBox_ListaObjetos.Items.Add($"Descripcion: {producto.Descripcion}");
                listBox_ListaObjetos.Items.Add($"Precio: {producto.Precio}");
                listBox_ListaObjetos.Items.Add($"Fecha vencimiento: {producto.FechaVencimiento}");
                listBox_ListaObjetos.Items.Add($"Unidades: {producto.CantidadUnidades}");
                listBox_ListaObjetos.Items.Add($"Id-Producto: {producto.IdProducto}");
                listBox_ListaObjetos.Items.Add(" ");

            }
        }


        /// <summary>
        /// Recibe un producto y lo busca en la lista de productos, si efectivamente existe agrega 
        /// cada campo de este producto a la lista de objetos.
        /// </summary>
        /// <param name="productoRecibido"></param>
        private void printProducto(Producto productoRecibido)
        {

            foreach (Producto producto in listaProductos)
            {

                if (producto == productoRecibido)
                {
                    //Muestro todos los campos (CON SENTIDO) de producto.
                    listBox_ListaObjetos.Items.Add($"Nombre: {producto.Nombre}");
                    listBox_ListaObjetos.Items.Add($"Tipo: {producto.TipoProducto}");
                    listBox_ListaObjetos.Items.Add($"Descripcion: {producto.Descripcion}");
                    listBox_ListaObjetos.Items.Add($"Precio: {producto.Precio}");
                    listBox_ListaObjetos.Items.Add($"Fecha vencimiento: {producto.FechaVencimiento}");
                    listBox_ListaObjetos.Items.Add($"Unidades: {producto.CantidadUnidades}");
                    listBox_ListaObjetos.Items.Add($"Id-Producto: {producto.IdProducto}");
                    listBox_ListaObjetos.Items.Add(" ");
                    break;
                }

            }
        }


        /// <summary>
        ///  Recibe una lista de clientes, limpia la lista de objetos e imprime todo cliente existente 
        ///  en la lista recibida.
        /// </summary>
        /// <param name="listaClientesRecibida"></param>
        private void Refresh(List<Cliente> listaClientesRecibida)
        {
            listBox_ListaObjetos.Items.Clear();

            foreach (Cliente cliente in listaClientesRecibida)
            {
                //Muestro todos los campos (CON SENTIDO) de Cliente.
                listBox_ListaObjetos.Items.Add($"Nombre: {cliente.Nombre}");
                listBox_ListaObjetos.Items.Add($"DNI: {cliente.Dni}");
                listBox_ListaObjetos.Items.Add($"Direc: {cliente.DireccionDomicilio}");
                listBox_ListaObjetos.Items.Add($"Saldo: {cliente.Saldo}");
                listBox_ListaObjetos.Items.Add($"Telefono: {cliente.Telefono}");
                listBox_ListaObjetos.Items.Add($"Id: {cliente.IdCliente}");
                listBox_ListaObjetos.Items.Add(" ");

            }
        }


        /// <summary>
        /// Recibe un cliente y lo busca en la lista de clientes, si efectivamente existe agrega 
        /// cada campo de este cliente a la lista de objetos.
        /// </summary>
        /// <param name="clienteRecibido"></param>
        private void printCliente(Cliente clienteRecibido)
        {

            foreach (Cliente cliente in listaClientes)
            {

                if (cliente == clienteRecibido)
                {
                    //Muestro todos los campos (CON SENTIDO) de Cliente.
                    listBox_ListaObjetos.Items.Add($"Nombre: {cliente.Nombre}");
                    listBox_ListaObjetos.Items.Add($"DNI: {cliente.Dni}");
                    listBox_ListaObjetos.Items.Add($"Direc: {cliente.DireccionDomicilio}");
                    listBox_ListaObjetos.Items.Add($"Saldo: {cliente.Saldo}");
                    listBox_ListaObjetos.Items.Add($"Telefono: {cliente.Telefono}");
                    listBox_ListaObjetos.Items.Add($"Id: {cliente.IdCliente}");
                    listBox_ListaObjetos.Items.Add(" ");
                    break;
                }

            }
        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Borrar filtros", lee la informacion ingresada, el campo y 
        /// dependiendo de la seccion filtra la lista de objetos por la informacion y campo ingresado 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_BorrarFiltrosABM_Click(object sender, EventArgs e)
        {
            textBox_FiltradoInformacionIngresadaABM.Text = string.Empty; //Vacio el textbox
            comboBox_FiltradoCampoElegidoABM.SelectedIndex = 0; //Selecciono el item vacio.

            //Imprimo la lista de items sin filtracion
            switch (seccionElegida)
            {
                case "Clientes":
                    {
                        Refresh(listaClientes);

                        break;
                    }
                case "Stock":
                    {
                        Refresh(listaProductos);

                        break;
                    }
                case "Ventas":
                    {
                        Refresh(listaVentas, esAdmin);

                        break;
                    }
                case "Empleados":
                    {
                        Refresh(listaEmpleados);

                        break;
                    }

                case "Administradores":
                    {
                        Refresh(listaAdministradores);

                        break;
                    }
            }
        }


        /// <summary>
        /// Agrega a la comboBox los campos disponibles para poder filtrar la informacion que ingresemos 
        /// dependiendo de la seccion elegida.
        /// </summary>
        /// <param name="seccionElegidaRecibida"></param>
        /// <param name="esAdminRecibido"></param>
        private void agregarDatosComboBoxFiltrosABM(string seccionElegidaRecibida, bool esAdminRecibido)
        {
            //Borro lo que sea que haya antes
            comboBox_FiltradoCampoElegidoABM.Items.Clear();

            switch (seccionElegidaRecibida)
            {
                case "Clientes":
                    {
                        comboBox_FiltradoCampoElegidoABM.Items.Add(string.Empty);
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Nombre");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Saldo");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("DNI");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Cliente");

                        break;
                    }
                case "Stock":
                    {

                        comboBox_FiltradoCampoElegidoABM.Items.Add(string.Empty);
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Nombre");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Precio");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Tipo");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Producto");

                        break;
                    }
                case "Ventas":
                    {
                        comboBox_FiltradoCampoElegidoABM.Items.Add(string.Empty);
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Nombre comprador");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Comprador");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Producto");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Usuario vendedor");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Venta");

                        break;
                    }
                case "Empleados":
                    {
                        comboBox_FiltradoCampoElegidoABM.Items.Add(string.Empty);
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Nombre");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Estado civil");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("DNI");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Sueldo");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Edad");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Empleado");

                        break;
                    }

                case "Administradores":
                    {
                        comboBox_FiltradoCampoElegidoABM.Items.Add(string.Empty);
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Nombre");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("Edad");
                        comboBox_FiltradoCampoElegidoABM.Items.Add("ID-Administrador");
                        break;
                    }
            }

            comboBox_FiltradoCampoElegidoABM.SelectedIndex = 0;

        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Filtrar", obtiene la informacion y el indice 
        /// seleccionado del comboBox de campos. Dependiendo de la seccion elegida y el campo seleccionado 
        /// de esa seccion, se va a filtrar por la informacion ingresada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_FiltrarABM_Click(object sender, EventArgs e)
        {
            string informacion;
            int indiceComboBox;

            informacion = textBox_FiltradoInformacionIngresadaABM.Text;
            indiceComboBox = comboBox_FiltradoCampoElegidoABM.SelectedIndex;

            //Le saco espacios de atras y delante
            informacion = informacion.Trim();

            if (indiceComboBox != 0) //Si no tengo vacio seleccionado en la combobox
            {
                //Si no tengo un textbox vacio
                if (string.IsNullOrWhiteSpace(informacion) == false)
                {
                    if (seccionElegida == "Ventas" && esAdmin == false && indiceComboBox == 4)
                    {
                        MessageBox.Show("No es posible filtrar por empleados vendedores. No tiene acceso a esta funcionalidad", "Error de permisos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        listBox_ListaObjetos.Items.Clear();

                        switch (seccionElegida)
                        {
                            case "Clientes":
                                {
                                    //ME FIJO POR QUE ESTOY FILTRANDO EN TODOS LOS CASOS
                                    switch (indiceComboBox)
                                    {
                                        case 1: //ELIGIO CAMPO NOMBRE
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Cliente cliente in listaClientes)
                                                {
                                                    if (cliente.Nombre.Contains(informacion) == true)
                                                    {
                                                        printCliente(cliente);
                                                    }

                                                }

                                                break;
                                            }
                                        case 2: //ELIGIO CAMPO SALDO
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Cliente cliente in listaClientes)
                                                {
                                                    //Si el campo numerico [SALDO] pasado a string contiene la informacion ingresada
                                                    if (cliente.Saldo.ToString().Contains(informacion) == true)
                                                    {
                                                        printCliente(cliente);
                                                    }

                                                }

                                                break;
                                            }
                                        case 3: //ELIGIO CAMPO DNI
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Cliente cliente in listaClientes)
                                                {
                                                    if (cliente.Dni.Contains(informacion) == true)
                                                    {
                                                        printCliente(cliente);
                                                    }
                                                }

                                                break;
                                            }
                                        case 4: //ELIGIO ID CLIENTE
                                            {
                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Cliente cliente in listaClientes)
                                                {
                                                    //Si el campo numerico [ID-CLIENTE] pasado a string contiene la informacion ingresada
                                                    if (cliente.IdCliente.ToString().Contains(informacion) == true)
                                                    {
                                                        printCliente(cliente);
                                                    }

                                                }

                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "Stock":
                                {
                                    switch (indiceComboBox)
                                    {
                                        case 1: //ELIGIO CAMPO NOMBRE
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Producto producto in listaProductos)
                                                {
                                                    if (producto.Nombre.Contains(informacion) == true)
                                                    {
                                                        printProducto(producto);
                                                    }
                                                }

                                                break;
                                            }

                                        case 2: //ELIGIO CAMPO PRECIO
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Producto producto in listaProductos)
                                                {
                                                    //Si el campo numerico [SALDO] pasado a string contiene la informacion ingresada
                                                    if (producto.Precio.ToString().Contains(informacion) == true)
                                                    {
                                                        printProducto(producto);
                                                    }
                                                }

                                                break;
                                            }
                                        case 3: //ELIGIO CAMPO TIPO
                                            {


                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Producto producto in listaProductos)
                                                {
                                                    if (producto.TipoProducto.Contains(informacion) == true)
                                                    {
                                                        printProducto(producto);
                                                    }
                                                }

                                                break;
                                            }
                                        case 4: //ELIGIO CAMPO ID-PRODUCTO
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Producto producto in listaProductos)
                                                {
                                                    //Si el campo numerico [ID] pasado a string contiene la informacion ingresada
                                                    if (producto.IdProducto.ToString().Contains(informacion) == true)
                                                    {
                                                        printProducto(producto);
                                                    }
                                                }

                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "Ventas":
                                {
                                    switch (indiceComboBox)
                                    {
                                        case 1://ELIGIO CAMPO NOMBRE COMPRADOR
                                            {
                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Venta venta in listaVentas)
                                                {
                                                    if (venta.ClienteComprador.Nombre.Contains(informacion) == true)
                                                    {
                                                        printVenta(venta);
                                                    }
                                                }

                                                break;

                                            }
                                        case 2: //ELIGIO CAMPO ID COMPRADOR
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Venta venta in listaVentas)
                                                {
                                                    //Si el campo numerico [ID-CLIENTECOMPRADOR] pasado a string contiene la informacion ingresada
                                                    if (venta.ClienteComprador.IdCliente.ToString().Contains(informacion) == true)
                                                    {
                                                        printVenta(venta);
                                                    }
                                                }

                                                break;

                                            }
                                        case 3: //ELIGIO CAMPO ID DEL PRODUCTO
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Venta venta in listaVentas)
                                                {
                                                    //Si el campo numerico [ID-PRODUCTO] pasado a string contiene la informacion ingresada
                                                    if (venta.ProductoAdquirido.IdProducto.ToString().Contains(informacion) == true)
                                                    {
                                                        printVenta(venta);
                                                    }
                                                }

                                                break;

                                            }
                                        case 4: //ELIGIO CAMPO ID DE EMPLEADO VENDEDOR
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Venta venta in listaVentas)
                                                {
                                                    //Si el campo numerico [ID-EMPLEADOVENDEDOR] pasado a string contiene la informacion ingresada
                                                    if (venta.IdUsuarioCreador.ToString().Contains(informacion) == true)
                                                    {
                                                        printVenta(venta);
                                                    }
                                                }

                                                break;
                                            }
                                        case 5: //ELIGIO CAMPO ID DE VENTA
                                            {
                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Venta venta in listaVentas)
                                                {
                                                    //Si el campo numerico [ID-VENTA] pasado a string contiene la informacion ingresada
                                                    if (venta.IdVenta.ToString().Contains(informacion) == true)
                                                    {
                                                        printVenta(venta);
                                                    }
                                                }

                                                break;
                                            }
                                    }
                                    break;
                                }
                            case "Empleados":
                                {
                                    switch (indiceComboBox)
                                    {
                                        case 1: //ELIGIO EL CAMPO NOMBRE
                                            {
                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Empleado empleado in listaEmpleados)
                                                {
                                                    if (empleado.Nombre.Contains(informacion) == true)
                                                    {
                                                        printEmpleado(empleado);
                                                    }
                                                }

                                                break;
                                            }
                                        case 2: //ELIGIO EL CAMPO ESTADO CIVIL
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Empleado empleado in listaEmpleados)
                                                {
                                                    if (empleado.EstadoCivil.Contains(informacion) == true)
                                                    {
                                                        printEmpleado(empleado);
                                                    }
                                                }

                                                break;

                                            }
                                        case 3: //ELIGIO EL CAMPO DNI
                                            {
                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Empleado empleado in listaEmpleados)
                                                {
                                                    if (empleado.Dni.Contains(informacion) == true)
                                                    {
                                                        printEmpleado(empleado);
                                                    }
                                                }

                                                break;

                                            }
                                        case 4: //ELIGIO EL CAMPO SUELDO
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Empleado empleado in listaEmpleados)
                                                {
                                                    //Si el campo numerico [SUELDO] pasado a string contiene la informacion ingresada
                                                    if (empleado.Sueldo.ToString().Contains(informacion) == true)
                                                    {
                                                        printEmpleado(empleado);
                                                    }
                                                }

                                                break;

                                            }
                                        case 5: //ELIGIO EL CAMPO EDAD
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Empleado empleado in listaEmpleados)
                                                {
                                                    //Si el campo numerico [EDAD] pasado a string contiene la informacion ingresada
                                                    if (empleado.Edad.ToString().Contains(informacion) == true)
                                                    {
                                                        printEmpleado(empleado);
                                                    }
                                                }

                                                break;
                                            }
                                        case 6: //ELIGIO EL CAMPO ID EMPLEADO
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Empleado empleado in listaEmpleados)
                                                {
                                                    //Si el campo numerico [SUELDO] pasado a string contiene la informacion ingresada
                                                    if (empleado.IdEmpleado.ToString().Contains(informacion) == true)
                                                    {
                                                        printEmpleado(empleado);
                                                    }
                                                }

                                                break;
                                            }
                                    }
                                    break;
                                }

                            case "Administradores":
                                {
                                    switch (indiceComboBox)
                                    {
                                        case 1: //ELIGIO EL CAMPO NOMBRE
                                            {
                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Administrador administrador in listaAdministradores)
                                                {
                                                    if (administrador.Nombre.Contains(informacion) == true)
                                                    {
                                                        printAdmin(administrador);
                                                    }
                                                }

                                                break;
                                            }
                                        case 2: //ELIGIO EL CAMPO EDAD
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Administrador administrador in listaAdministradores)
                                                {
                                                    //Si el campo numerico [SALDO] pasado a string contiene la informacion ingresada
                                                    if (administrador.Edad.ToString().Contains(informacion) == true)
                                                    {
                                                        printAdmin(administrador);
                                                    }
                                                }

                                                break;
                                            }
                                        case 3: //ELIGIO EL CAMPO ID-ADMINISTRADOR
                                            {

                                                //IMPRIMO TODO LO QUE CONTENGA LA INFORMACION INGRESADA
                                                foreach (Administrador administrador in listaAdministradores)
                                                {
                                                    //Si el campo numerico [SALDO] pasado a string contiene la informacion ingresada
                                                    if (administrador.Id.ToString().Contains(informacion) == true)
                                                    {
                                                        printAdmin(administrador);
                                                    }
                                                }

                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                    }


                }
                else
                {
                    MessageBox.Show("No es posible buscar con informacion vacia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("No es posible filtrar informacion si no hay un campo seleccionado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// Se ejecuta al presionar el boton "Inversion mas cara" que solo es mostrado a un administrador
        /// y se llama al metodo productoConMasInversion, imprime el producto devuelto y agrega a la lista
        /// de objetos el valor de inversion mayor
        /// inversion mayor que se recibio
        /// Precio del producto por Unidades disponibles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_InversionMasCaraABMProductos_Click(object sender, EventArgs e)
        {
            //Borro la lista
            listBox_ListaObjetos.Items.Clear();

            int inversionMayor = 0;

            //Imprimo el producto que me devuelva la funcion de productoConMasInversion
            printProducto( productoConMasInversion(out inversionMayor) );

            //Si hay como minimo un producto encontrado ya que el valor de inversion mayor es mayor a 0
            if (inversionMayor > 0) 
            {
                //Ahi imprimo el campo de inversion mayor. si no, no.
                listBox_ListaObjetos.Items.Add($"Valor de inversion total: {inversionMayor}$");
            }
           
        }


        /// <summary>
        /// Este metodo recibe una referencia a una variable int para que pueda ser cargada con el 
        /// valor de la inversion mayor de un producto. Busca en la lista de productos el producto
        /// que tenga el precio por unidades mas caro y lo devuelve junto al resultado de la cuenta
        /// </summary>
        /// <param name="inversionMayor"></param>
        /// <returns></returns>
        private Producto productoConMasInversion(out int inversionMayor)
        {
            inversionMayor = 0;

            //CREO UN NUEVO PRODUCTO QUE SERA AUXILIAR
            Producto productoMayor = new Producto();

            int contador = 0;

            foreach (Producto producto in listaProductos)
            {
                if (contador == 0) //SI ESTAMOS HABLANDO DEL PRIMER PRODUCTO
                {
                    //LO TOMO COMO EL MAYOR
                    productoMayor = producto;
                    inversionMayor = (producto.CantidadUnidades * (int)producto.Precio);   
                }
                else if ((producto.CantidadUnidades * (int)producto.Precio) > inversionMayor) //SI NO ES EL PRIMER PRODUCTO Y ES MAYOR AL VALOR DE INVERSION MAYOR
                {
                    //LA NUEVA INVERSION MAYOR ES LA DE ESTE PRODUCTO
                    productoMayor = producto; //Me guardo el producto
                    inversionMayor = (producto.CantidadUnidades * (int)producto.Precio); //Y el valor de inversion

                }
                contador++;
            }

            return productoMayor;
        }
    }
}

