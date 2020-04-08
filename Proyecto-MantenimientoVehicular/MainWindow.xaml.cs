using Proyecto_MantenimientoVehicular.BLL;
using Proyecto_MantenimientoVehicular.Entidades;
using Proyecto_MantenimientoVehicular.UI.Menu;
using Proyecto_MantenimientoVehicular.UI.Registros;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Proyecto_MantenimientoVehicular
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            usuarioTextBox1.Text = string.Empty;
            contraseñaPasswordBox1.Password = string.Empty;
        }
       

        public void IniciarSesion()
        {

            Expression<Func<Usuarios, bool>> filtro = x => true;
            List<Usuarios> usuario = new List<Usuarios>();

            var username = usuarioTextBox1.Text;
            var password = contraseñaPasswordBox1.Password;
            filtro = x => x.Usuario.Equals(username);
            usuario = UsuarioBLL.GetList(filtro);



            if (usuario.Exists(x => x.Usuario.Equals(username)))
            {
                if (usuario.Exists(x => x.Contraseña.Equals((password))))
                {
                    List<Usuarios> id = UsuarioBLL.GetList(U => U.Usuario == usuarioTextBox1.Text);
                    wMenu menuprincipal = new wMenu();
                    menuprincipal.Show();
                    this.Hide();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("Nombre de usuarios o contraseñas incorrectos");
                    return;
                }
            }
            else
            {
                if (usuarioTextBox1.Text == string.Empty || contraseñaPasswordBox1.Password == string.Empty)
                    MessageBox.Show("LLene todos los campos");
                else if (!usuario.Exists(x => x.Usuario.Equals(username)))
                    MessageBox.Show("Nombre de usuarios o contraseñas incorrectos");

            }
        }

       

        private void iniciarsesionButton_Click(object sender, RoutedEventArgs e)
        {
            if (usuarioTextBox1.Text == "Admin" && contraseñaPasswordBox1.Password == "Admin")
            {
                LimpiarCampos();

                wMenu menuprimcipal = new wMenu();
                menuprimcipal.ShowDialog();
                this.Close();
                
            }
            else
            {
                IniciarSesion();
                
            }

        }

        private void salirButton1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}     
       


