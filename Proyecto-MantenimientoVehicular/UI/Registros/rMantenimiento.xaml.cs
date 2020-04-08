using Proyecto_MantenimientoVehicular.BLL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

namespace Proyecto_MantenimientoVehicular.UI.Registros
{
    /// <summary>
    /// Interaction logic for rMantenimiento.xaml
    /// </summary>
    public partial class rMantenimiento : Window
    {
        Mantenimiento mantenimiento = new Mantenimiento();
        

        public rMantenimiento()
        {
            InitializeComponent();
            LlenaComboBox();
            ListaCliente();
            ListaVehiculo();
            idTextBox1.Text = "0";

            List<string> servicios = new List<string>
            {
                "Limpieza y ajuste de frenos","Ajuste de freno de estacionamiento","Rotación y balanceo de ruedas","Alineamiento de ruedas",
                "Afinamiento de motor", "Lubricación y engrase general","Limpieza y ajuste de frenos","Ajuste de freno de estacionamiento",
                "Cambio de Aceite de Transmision CVT y Filtro","Cambio de Aceite de Transmision Automatica y Filtro","Cambio de Filtro de Gasolina",
                "Cambio de Bujías","Limpieza de Inyectores"
            };
            this.serviciosComboBox.ItemsSource = servicios;


            this.DataContext = mantenimiento;
        }

       
    
        private void LimpiarCampos()
        {
            clienteComboBox.Text = " ";
            descripcionTextBox.Text = string.Empty;
            vehiculoComboBox.Text = " ";
            articuloComboBox.Text = " ";
            serviciosComboBox.Text = " ";
            cantidadTextBox.Text = "0";
            disponibleTextBox.Text = "0";
            precioTextBox.Text = "0";
            importeTextBox1.Text = "0";
            totalTextBox.Text = "0";
            fechaDatePicker.SelectedDate = DateTime.Now;
            proxmantDatePicker.SelectedDate = DateTime.Now;
            detalleDataGrid.ItemsSource = string.Empty;
            subtotalTextBox01.Text =  "0";

        }


        private bool ExisteEnLaBaseDatos()
        {
            Mantenimiento mantenimiento = MantenimientoBLL.Buscar((int)Convert.ToInt32(idTextBox1.Text));
            return (mantenimiento != null);
        }

        private bool ValidarCampos()
        {
            bool paso = true;


            if (string.IsNullOrWhiteSpace(clienteComboBox.Text))
            {
                MessageBox.Show("Debe elegir un Cliente", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(vehiculoComboBox.Text))
            {
                MessageBox.Show("Este campo es Obligatorio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }

            

            return paso;
        }

            private void Llenar()
        {
            this.DataContext = null;
            this.DataContext = mantenimiento;
        }

        

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            Mantenimiento mantenimientolocal = MantenimientoBLL.Buscar(mantenimiento.MantenimientoId);

            if (mantenimientolocal != null)
            {
                mantenimiento = mantenimientolocal;
                Llenar();
            }
            else
            {
                LimpiarCampos();
                MessageBox.Show("No Encontrado", "Salir", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void eliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (MantenimientoBLL.Eliminar(mantenimiento.MantenimientoId))
            {
                LimpiarCampos();
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show("No Eliminado", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

       

        private void removerButton_Click(object sender, RoutedEventArgs e)
        {
            if (detalleDataGrid.Columns.Count > 0 && detalleDataGrid.SelectedCells != null)
            {
                mantenimiento.DMantenimiento.RemoveAt(detalleDataGrid.SelectedIndex);
                Llenar();
            }
        }

        private void agregarButton_Click(object sender, RoutedEventArgs e)
        {


            if (disponibleTextBox.Text == "0.0")
            {
                MessageBox.Show("¡¡Articulo Agotado!!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }


            if (cantidadTextBox.Text == "0")
            {
                MessageBox.Show("Debe elegir una Cantidad!!", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;

            }

            else
            {

                mantenimiento.DMantenimiento.Add(new DetalleMantenimiento(
                id: 0,
                mantenimientoId: Convert.ToInt32(idTextBox1.Text),
                articuloId: Convert.ToInt32(articuloComboBox.SelectedValue),
                descripcion: descripcionTextBox.Text,
                cantidad: Convert.ToInt32(cantidadTextBox.Text),
                precio: Convert.ToDecimal(precioTextBox.Text),
                importe: Convert.ToDecimal(importeTextBox1.Text)


                    ));
                Llenar();
                Calculos();


                articuloComboBox.Text = string.Empty;
                disponibleTextBox.Clear();
                cantidadTextBox.Clear();
                precioTextBox.Clear();
                importeTextBox1.Clear();
                

            }

        }


        private void Calculos()
        {
            List<DetalleMantenimiento> ListDetalle = (List<DetalleMantenimiento>)detalleDataGrid.ItemsSource;

            decimal Total = 0;
            decimal PorcientoItebis = (decimal)0.18;
            decimal Subtotal = 0;
            decimal Itebis = 0;
           

            foreach (var item in ListDetalle)
            {
                Subtotal +=  item.Importe;
                Itebis += Subtotal * PorcientoItebis;
                Total += item.Importe + Convert.ToDecimal(Itebis);
                
               

            }

            totalTextBox.Text = Convert.ToString(Total);
            subtotalTextBox01.Text = Convert.ToString(Subtotal);
            itebisTextBox.Text = Convert.ToString(Itebis);

            
        }


        private void LlenaComboBox()
        {
            ArticuloBLL articulos = new ArticuloBLL();
            ClienteBLL cliente = new ClienteBLL();
            VehiculoBLL vehiculos = new VehiculoBLL();

            articuloComboBox.ItemsSource = ArticuloBLL.GetList(a => true);
            articuloComboBox.DisplayMemberPath = "Articulo";
            articuloComboBox.SelectedValuePath = "ArticuloId";

            vehiculoComboBox.ItemsSource = VehiculoBLL.GetList(t => true);
            vehiculoComboBox.DisplayMemberPath = "Descripcion";
            vehiculoComboBox.SelectedValuePath = "VehiculoId";

            clienteComboBox.ItemsSource = ClienteBLL.GetList(v => true);
            clienteComboBox.DisplayMemberPath = "Nombre";
            clienteComboBox.SelectedValuePath = "ClienteId";


        }


        private void ListaCliente()       //Lista para llenar ComboBox Cliente
        {
            List<Clientes> listacliente = ClienteBLL.GetList(a => true);
            this.DataContext = listacliente;
        }

        private void ListaVehiculo()     //Lista para llenar ComboBox Vehiculo
        {
            List<Clientes> listavehiculo = ClienteBLL.GetList(a => true);
            this.DataContext = listavehiculo;
        }

        private void articuloComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Articulos buscarArt = articuloComboBox.SelectedItem as Articulos;

            if (buscarArt != null)
            {
                precioTextBox.Text = Convert.ToString(buscarArt.Precio);
                disponibleTextBox.Text = Convert.ToString(buscarArt.Existencia);
            }
        }

        private decimal ToDecimal(object valor)
        {
            decimal retorno = 0;
            decimal.TryParse(valor.ToString(), out retorno);

            return Convert.ToDecimal(retorno);
        }

        private void cantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cantidadTextBox.Text != string.Empty)
            {
                CalcularImporte();
                
                

            }
        }


        private void CalcularImporte()      //   Calculo de Importe
        {

            ArticuloBLL BLL = new ArticuloBLL();

            decimal cantidad, precio;
            cantidad = ToDecimal(cantidadTextBox.Text);
            precio = ToDecimal(precioTextBox.Text);
            importeTextBox1.Text = ArticuloBLL.CalcularImporte(cantidad, precio).ToString("0.##");


        }

       

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {

            bool paso = false;

            if (!ValidarCampos())
                return;

            


            if (idTextBox1.Text == "0")
                paso = MantenimientoBLL.Guardar(mantenimiento);

            else
            {
                if (!ExisteEnLaBaseDatos())
                {
                    MessageBox.Show("No se puede modificar, no existe!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                paso = MantenimientoBLL.Modificar(mantenimiento);
            }

            if (paso)
            {
                MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se Guardo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void agregararticuloButton_Click(object sender, RoutedEventArgs e)
        {
            rArticulo registro = new rArticulo();
            registro.ShowDialog(); 
        }

        private void idTextBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void descripcionTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }

        }

        private void cantidadTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
