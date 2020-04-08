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
    /// Interaction logic for rArticulo.xaml
    /// </summary>
    public partial class rArticulo : Window
    {
        Articulos articulos = new Articulos();
        public rArticulo()
        {
            InitializeComponent();

            List<string> categorias = new List<string>
            {
                "A-Piezas Desgatables","B-Consumibles","C-Elementos de Regulacion","D-Piezas Moviles",
                "E-Componentes Electronicos", "F-Piezas Estructurales"
            };
            this.categoriaComboBox.ItemsSource = categorias;

            ListaEntrada();
            
            
            idTextBox.Text = "0";


            this.DataContext = articulos;
        }


        private void ListaEntrada()
        {
            List<EntradaArticulos> entradaArticulos = EntradaArticuloBLL.GetList(a => true);
            this.DataContext = entradaArticulos;
        }

        
        private void LimpiarCampos()
        {
            idTextBox.Text = "0";
            articuloTextBox.Text = string.Empty;
            categoriaComboBox.Text = string.Empty;
            precioTextBox.Text = "0.0";
            existenciaTextBox.Text = "0";
            fechaDatePicker.SelectedDate = DateTime.Now;
        }

        private bool ExisteEnLaBaseDatos()
        {
            Articulos articulos = ArticuloBLL.Buscar((int)Convert.ToInt32(idTextBox.Text));
            return (articulos != null);
        }


        private bool ValidarCampos()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(articuloTextBox.Text))
            {
                MessageBox.Show("Ingrese un Articulo", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }


            if (string.IsNullOrWhiteSpace(precioTextBox.Text))
            {
                MessageBox.Show("Ingrese el Precio", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }


            if (string.IsNullOrWhiteSpace(categoriaComboBox.Text))
            {
                MessageBox.Show("Ingrese una Categoria", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                paso = false;
            }


            return paso;
        }

        private void Llenar()
        {
            this.DataContext = null;
            this.DataContext = articulos;
        }

        private void guardrButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!ValidarCampos())
                return;

            if (idTextBox.Text == "0")
                paso = ArticuloBLL.Guardar(articulos);

            else
            {
                if (!ExisteEnLaBaseDatos())
                {
                    MessageBox.Show("No se puede modificar, no existe!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                paso = ArticuloBLL.Modificar(articulos);
            }

            if (paso)
            {
                MessageBox.Show("Articulo Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al Guardar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            Articulos articulolocal = ArticuloBLL.Buscar(articulos.ArticuloId);

            if (articulolocal != null)
            {
                articulos = articulolocal;
                Llenar();
            }
            else
            {
                LimpiarCampos();
                MessageBox.Show("Articulo No Existe", "Salir", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void eliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ArticuloBLL.Eliminar(articulos.ArticuloId))
            {
                LimpiarCampos();
                MessageBox.Show("Articulo Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Exclamation);
               
            }
            else
            {
                MessageBox.Show("Error al Eliminar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }


        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void precioTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        
    }
}
