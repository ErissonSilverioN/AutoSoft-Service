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
    /// Interaction logic for cProveedores.xaml
    /// </summary>
    public partial class cProveedores : Window
    {
        public cProveedores()
        {
            InitializeComponent();
        }

        private void consultarButton_Click(object sender, RoutedEventArgs e)
        {

            var listado = new List<Proveedores>();

            if (criterioTextBox.Text.Trim().Length > 0)
            {
                switch (filtroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = ProveedoresBLL.GetList(p => true);
                        break;

                    case 1:
                        int ID = Convert.ToInt32(criterioTextBox.Text);
                        listado = ProveedoresBLL.GetList(p => p.ProveedorId == ID);
                        break;

                    case 2:
                        int rnc = Convert.ToInt32(criterioTextBox.Text);
                        listado = ProveedoresBLL.GetList(p => p.RNC == Convert.ToString(rnc));
                        break;

                    case 3:
                        listado = ProveedoresBLL.GetList(p => p.Nombre.Contains(criterioTextBox.Text));
                        break;


                    case 4:
                        listado = ProveedoresBLL.GetList(p => p.Telefono.Contains(criterioTextBox.Text));
                        break;



                }

                listado = listado.Where(c => c.Fecha.Date >= desdeDatePicker.SelectedDate.Value && c.Fecha.Date <= hastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                listado = ProveedoresBLL.GetList(p => true);
            }

            consultarDataGrid.ItemsSource = listado;
        }
    }
}
