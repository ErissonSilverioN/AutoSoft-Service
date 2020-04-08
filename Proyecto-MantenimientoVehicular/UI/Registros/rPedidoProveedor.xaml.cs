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

namespace Proyecto_MantenimientoVehicular.UI.Registros
{
    /// <summary>
    /// Interaction logic for DetallePedidosProveedor.xaml
    /// </summary>
    public partial class rPedidoProveedor : Window
    {
        PedidosProveedor Pedidos = new PedidosProveedor();

        public rPedidoProveedor()
        {
            InitializeComponent();
            ListaProveedores();
            LlenaComboProveedor();
            IdTextbox.Text = "0";
            List<string>  categorias = new List<string>
            {
                "A-Piezas Desgatables","B-Consumibles","C-Elementos de Regulacion","D-Piezas Moviles",
                "E-Componentes Electronicos", "F-Piezas Estructurales"
            };
            this.categoriaComboBox.ItemsSource = categorias;
            this.DataContext = Pedidos;
        }

        private void Limpiar()
        {
            IdTextbox.Text = "0";
            proveedorComboBox.Text = string.Empty;
            articuloTextBox.Text = string.Empty;
            categoriaComboBox.Text = string.Empty;
            notaTextBox.Text = "****                        ****";
            detalleDataGrid.ItemsSource = string.Empty;
            unidadTextBox.Text = "0";


        }

        private void ListaProveedores()     
        {
            List<Proveedores> listaproveedor = ProveedoresBLL.GetList(a => true);
            this.proveedorComboBox.ItemsSource = listaproveedor;
          
        }

        private void LlenaComboProveedor()
        {
            proveedorComboBox.ItemsSource = ProveedoresBLL.GetList(a => true);
            proveedorComboBox.DisplayMemberPath = "Nombre";
            proveedorComboBox.SelectedValuePath = "ProveedorId";
        }

        private bool ExisteEnBaseDatos()
        {
            PedidosProveedor pedidosProveedores = PedidosProveedorBLL.Buscar(Convert.ToInt32(IdTextbox.Text));
            return (pedidosProveedores != null);
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(categoriaComboBox.Text))
            {
                MessageBox.Show("Debe Elegir un Proveedor!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }



            if (string.IsNullOrWhiteSpace(categoriaComboBox.Text))
            {
                MessageBox.Show("Debe Elegir una Categoria!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }



           return paso;

        }

        private void Llenar()
        {
            this.DataContext = null;
            this.DataContext = Pedidos;

        }


        private void eliiminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (PedidosProveedorBLL.Eliminar(Pedidos.PedidoId))
            {
               
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();

            }
            else
            {
                MessageBox.Show("No Eliminado!!");
            }
        }

        private void removerButton_Click(object sender, RoutedEventArgs e)
        {
            if (detalleDataGrid.Columns.Count > 0 && detalleDataGrid.SelectedCells != null)
            {
                Pedidos.DPedidos.RemoveAt(detalleDataGrid.SelectedIndex);
                Llenar();
            }
        }

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            PedidosProveedor pedidolocal = PedidosProveedorBLL.Buscar(Pedidos.PedidoId);

            if (pedidolocal != null)
            {
                Pedidos = pedidolocal;
                Llenar();

            }
            else
            {
                Limpiar();
                MessageBox.Show("Llamada no Encontrada!!");


            }
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!Validar())
                return;

            if (IdTextbox.Text == "0")
            {
                paso = PedidosProveedorBLL.Guardar(Pedidos);
            }
            else
            {
                if (!ExisteEnBaseDatos())
                {
                    MessageBox.Show("Pedido Proveedor No Existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                paso = PedidosProveedorBLL.Modificar(Pedidos);
            }

            if (paso)
            {
                MessageBox.Show("¡¡Guardado!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }
            else
            {
                MessageBox.Show("¡¡No se pudo Guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void agregarButton_Click(object sender, RoutedEventArgs e)
        {
            Pedidos.DPedidos.Add(new DetallePedidos(
                id: 0,
                pedidoId: Convert.ToInt32(IdTextbox.Text),
                proveedor: Convert.ToInt32(proveedorComboBox.SelectedValue),
                categoria: Convert.ToString(categoriaComboBox.Text),
                articulo: articuloTextBox.Text,
                unidad: Convert.ToInt32(unidadTextBox.Text),
                precio: Convert.ToDecimal(precioTextBox.Text)
                
               ));

            Llenar();

           
            articuloTextBox.Clear();
            unidadTextBox.Clear();
            

           


        }

        private void IdTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void articuloTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }

        }

        private void unidadTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void agregarproveedor_Click(object sender, RoutedEventArgs e)
        {
            rProveedores registro = new rProveedores();
            registro.ShowDialog();
        }
    }
}
