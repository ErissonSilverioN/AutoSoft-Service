using Proyecto_MantenimientoVehicular.BLL;
using Proyecto_MantenimientoVehicular.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_MantenimientoVehicular.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cUsuario.xaml
    /// </summary>
    public partial class cUsuario : Window
    {
        public cUsuario()
        {
            InitializeComponent();
        }

        private void consultarButton_Click(object sender, RoutedEventArgs e)
        {

            var listado = new List<Usuarios>();

            if (criterioTextBox.Text.Trim().Length > 0)
            {
                switch (filtroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = UsuarioBLL.GetList(p => true);
                        break;

                    case 1:
                        int ID = Convert.ToInt32(criterioTextBox.Text);
                        listado = UsuarioBLL.GetList(p => p.UsuarioId == ID);
                        break;

                    case 2:
                        listado = UsuarioBLL.GetList(p => p.Nombre.Contains(criterioTextBox.Text));
                        break;

                    case 3:
                        listado = UsuarioBLL.GetList(p => p.Usuario.Contains(criterioTextBox.Text));
                        break;


                    



                }

                listado = listado.Where(c => c.Fecha.Date >= desdeDatePicker.SelectedDate.Value && c.Fecha.Date <= hastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                listado = UsuarioBLL.GetList(p => true);
            }

            consultarDataGrid.ItemsSource = listado;
        }
    }
}
