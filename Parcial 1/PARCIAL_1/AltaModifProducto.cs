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
    public partial class AltaModifProducto : Form
    {
        List<Producto> listaProductos;
        Producto productoAux;
        string ordenMuestra;
        int posicionProducto;

        /// <summary>
        /// Constructor del AltaModifProducto, recibe la lista de productos, la orden a realizar, 
        /// y la posicion del producto para modificarlo, (Si la orden es un alta, simplemente se ignora 
        /// el valor recibido de la posicion del producto)
        /// </summary>
        /// <param name="listaProductosRecibida"></param>
        /// <param name="ordenMuestreadoRecibido"></param>
        /// <param name="posicionProductoParaAccionarRecibida"></param>
        public AltaModifProducto(List<Producto> listaProductosRecibida, string ordenMuestreadoRecibido, int posicionProductoParaAccionarRecibida)
        {
            InitializeComponent();

            pictureBox_Gris13.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema13.Image = UserUI.Properties.Resources.Tono4;

            pictureBox_Gris14.Image = UserUI.Properties.Resources.Tono5;
            pictureBox_Crema14.Image = UserUI.Properties.Resources.Tono4;

            listaProductos = listaProductosRecibida;
            ordenMuestra = ordenMuestreadoRecibido;
            posicionProducto = posicionProductoParaAccionarRecibida;

            if (ordenMuestra == "Modificar")
            {
                label_InfoCreacionProducto.Text = "Bienvenido al menu de modificacion de un Producto";
                label_IngreseNombreABMProducto.Text = "Ingrese el nuevo nombre";
                label_IngreseTipoProductoABMProducto.Text = "Ingrese el nuevo tipo";
                label_IngresePrecioProductoABMProducto.Text = "Ingrese el nuevo precio";
                label_IngreseFechaVencimientoABMProducto.Text = "Ingrese la nueva fecha de vencimiento";
                label_IngreseDescripcionABMProducto.Text = "Ingrese una nueva y breve descripcion";
                label_IngreseCantidadUnidadesABMProducto.Text = "Ingrese una nueva cantidad de unidades";
            }
        }

        /// <summary>
        /// Se ejecuta al presionar el boton "Ingresar datos", dependiendo de lo que valga la orden 
        /// agrega o modifica un producto. Modificar valida los datos al igual que alta, pero reemplaza 
        /// datos del producto con los datos validados y Agregar añade a la lista de productos 
        /// los datos validados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_IngresarDatosABMProducto_Click(object sender, EventArgs e)
        {


            if (ordenMuestra == "Modificar")
            {

                double precioAux = 0; //CREO UN PRECIO AUX ANTES QUE USAR EL DE PRODUCTO AUX PORQUE NO PUEDO USAR UN OUT CON UNA PROPIEDAD -> int.TryParse(textBox_EdadIngresadaABMAdmin.Text, out adminAux.Edad); MAL
                double.TryParse(textBox_PrecioIngresadoABMProducto.Text, out precioAux);

                productoAux = new Producto();

                int cantidadUnidadesAux = 0;
                int.TryParse(textBox_CantidadUnidadesIngresadaABMProducto.Text, out cantidadUnidadesAux);

                int resultadoValidacionNombre = productoAux.IsValidNombre(textBox_NombreIngresadoABMProducto.Text);
                int resultadoValidacionDescripcion = productoAux.IsValidDescripcion(textBox_DescripcionIngresadaABMProducto.Text);
                int resultadoValidacionPrecio = productoAux.IsValidPrecio(precioAux);
                int resultadoValidacionUnidades = productoAux.IsValidCantidadUnidades(cantidadUnidadesAux);
                int resultadoValidacionTipo = comboBox_TipoProductoIngresadoABMProducto.SelectedIndex;

                //Si parece estar todo bien. Uso el producto aux para modificarlo finalmente a la lista (habiendo cargado ya los datos validados)
                if (resultadoValidacionNombre == 0 && resultadoValidacionDescripcion == 0 && resultadoValidacionPrecio == 0 && resultadoValidacionUnidades == 0 && resultadoValidacionTipo >= 0)
                {
                    productoAux.TipoProducto = comboBox_TipoProductoIngresadoABMProducto.SelectedItem.ToString();
                    productoAux.Precio = precioAux;
                    productoAux.Descripcion = textBox_DescripcionIngresadaABMProducto.Text;
                    productoAux.Nombre = textBox_NombreIngresadoABMProducto.Text;
                    productoAux.FechaVencimiento = textBox_FechaVencimientoIngresadaABMProducto.Text;
                    productoAux.CantidadUnidades = cantidadUnidadesAux;

                        listaProductos[posicionProducto].Nombre = productoAux.Nombre;
                        listaProductos[posicionProducto].Precio = productoAux.Precio;
                        listaProductos[posicionProducto].TipoProducto = productoAux.TipoProducto;
                        listaProductos[posicionProducto].FechaVencimiento = productoAux.FechaVencimiento;
                        listaProductos[posicionProducto].Descripcion = productoAux.Descripcion;
                        listaProductos[posicionProducto].CantidadUnidades = productoAux.CantidadUnidades;

                        MessageBox.Show("El producto se ha modificado correctamente.", "Modificacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                  

                }
                else
                {
                    MessageBox.Show("No se pudo modificar el producto. Existen datos invalidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else if (ordenMuestra == "Agregar")
            {

                bool productoRepetido = false;

                double precioAux = 0; //CREO UN PRECIO AUX ANTES QUE USAR EL DE PRODUCTO AUX PORQUE NO PUEDO USAR UN OUT CON UNA PROPIEDAD -> int.TryParse(textBox_EdadIngresadaABMAdmin.Text, out adminAux.Edad); MAL
                double.TryParse(textBox_PrecioIngresadoABMProducto.Text, out precioAux);

                productoAux = new Producto();

                int cantidadUnidadesAux = 0;
                int.TryParse(textBox_CantidadUnidadesIngresadaABMProducto.Text, out cantidadUnidadesAux);

                int resultadoValidacionNombre = productoAux.IsValidNombre(textBox_NombreIngresadoABMProducto.Text);
                int resultadoValidacionDescripcion = productoAux.IsValidDescripcion(textBox_DescripcionIngresadaABMProducto.Text);
                int resultadoValidacionPrecio = productoAux.IsValidPrecio(precioAux);
                int resultadoValidacionUnidades = productoAux.IsValidCantidadUnidades(cantidadUnidadesAux);
                int resultadoValidacionTipo = comboBox_TipoProductoIngresadoABMProducto.SelectedIndex;

                //Si parece estar todo bien. Uso el producto aux para modificarlo finalmente a la lista (habiendo cargado ya los datos validados)
                if (resultadoValidacionNombre == 0 && resultadoValidacionDescripcion == 0 && resultadoValidacionPrecio == 0 && resultadoValidacionUnidades == 0 && resultadoValidacionTipo >= 0)
                { 
                    productoAux.TipoProducto = comboBox_TipoProductoIngresadoABMProducto.SelectedItem.ToString();
                    
                    productoAux = new Producto(productoAux.TipoProducto,precioAux,textBox_DescripcionIngresadaABMProducto.Text,textBox_NombreIngresadoABMProducto.Text,textBox_FechaVencimientoIngresadaABMProducto.Text,cantidadUnidadesAux);

                    //A ESTE PUNTO CARGAMOS YA TODOS LOS CAMPOS. SOLO QUEDA VERIFICAR SI EXISTE OTRO CLIENTE CON ESTE NOMBRE DE USUARIO.
                    foreach (Producto producto in listaProductos)
                    {
                        //Si existe una coincidencia en los nombres de usuarios. ERROR!!!!
                        if (producto.Nombre == productoAux.Nombre && producto.Precio == productoAux.Precio && producto.Descripcion == productoAux.Descripcion && producto.TipoProducto == productoAux.TipoProducto)
                        {
                            productoRepetido = true;
                            break;
                        }
                    }

                    if (productoRepetido == true)
                    {
                        MessageBox.Show("No se pudo agregar un nuevo producto. Ya existe uno con esos mismos datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //Lo agrego a la lista de clientes
                        listaProductos.Add(productoAux);
                        MessageBox.Show("Se ha agregado correctamente el producto.", "Creacion realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo agregar un nuevo producto. Existen datos invalidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
