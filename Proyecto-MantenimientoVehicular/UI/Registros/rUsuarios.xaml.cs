using Proyecto_MantenimientoVehicular.BLL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Proyecto_MantenimientoVehicular.UI.Registros
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
    {
        Usuarios usuarios = new Usuarios();
        public rUsuarios()
        {

            InitializeComponent();

            List<string> tipousuario = new List<string>
            {
                "Usuario","Administrador"
            };
            this.usuariotipoComboBox1.ItemsSource = tipousuario;

            idTextBox1.Text = "0";

            this.DataContext = usuarios;
        }

        private void LimpiarCampos()
        {
            nombreTextBox1.Text = string.Empty;
            usuarioTextBox1.Text = string.Empty;
            contraseñaPasswordBox1.Password = string.Empty;
            confirmarPasswordBox1.Password = string.Empty;
            usuariotipoComboBox1.Text = string.Empty;
            fechaDatePicker1.SelectedDate = DateTime.Now;
        }

        private bool ExisteEnLaBaseDatos()
        {
            Usuarios usuarios = UsuarioBLL.Buscar((int)Convert.ToInt32(idTextBox1.Text));
            return (usuarios != null);
        }

        private bool ValidarCampos()
        {
            bool paso = true;

            if (contraseñaPasswordBox1.Password != confirmarPasswordBox1.Password)
            {
                MessageBox.Show("La Contraseña no Coinciden","Error",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(nombreTextBox1.Text))
            {
                MessageBox.Show("Este Campo es Obligatorio");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(usuarioTextBox1.Text))
            {
                MessageBox.Show("Este Campo es Obligatorio");
                paso = false;
            }

            

            if (string.IsNullOrWhiteSpace(contraseñaPasswordBox1.Password))
            {
                MessageBox.Show("Este Campo es Obligatorio");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(confirmarPasswordBox1.Password))
            {
                MessageBox.Show("Este Campo es Obligatorio");
                paso = false;
            }

            return paso;
        }


        private void Llenar()
        {
            this.DataContext = null;
            this.DataContext = usuarios;
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (!ValidarCampos())
                return;

            if (idTextBox1.Text == "0")
                paso = UsuarioBLL.Guardar(usuarios);

            else
            {
                if (!ExisteEnLaBaseDatos())
                {
                    MessageBox.Show("No se puede modificar, no existe!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                paso = UsuarioBLL.Modificar(usuarios);
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

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            Usuarios usuariolocal = UsuarioBLL.Buscar(usuarios.UsuarioId);

            if (usuariolocal != null)
            {
                usuarios = usuariolocal;
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
            if (UsuarioBLL.Eliminar(usuarios.UsuarioId))
            {
                LimpiarCampos();
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show("No Eliminado", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void nuevoButton_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void idTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

       

        private void nombreTextBox1_PreviewTextInput_1(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[a-zA-Z]"))
            {
                e.Handled = true;
            }
        }
    }
}
