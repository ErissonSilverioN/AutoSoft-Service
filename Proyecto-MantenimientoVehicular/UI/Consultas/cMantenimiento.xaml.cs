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
    /// Interaction logic for cMantenimiento.xaml
    /// </summary>
    public partial class cMantenimiento : Window
    {
        public cMantenimiento()
        {
            InitializeComponent();
        }

        private void consultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Mantenimiento>();

            if (criterioTextBox.Text.Trim().Length > 0)
            {
                switch (filtroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = MantenimientoBLL.GetList(p => true);
                        break;

                    case 1:
                        int ID = Convert.ToInt32(criterioTextBox.Text);
                        listado = MantenimientoBLL.GetList(p => p.MantenimientoId == ID);
                        break;

                    case 2:
                        int clienteid = Convert.ToInt32(criterioTextBox.Text);
                        listado = MantenimientoBLL.GetList(p => p.ClienteId == clienteid);
                        break;

                    case 3:
                        int vehiculoid = Convert.ToInt32(criterioTextBox.Text);
                        listado = MantenimientoBLL.GetList(p => p.VehiculoId == vehiculoid);
                        break;

                }

                listado = listado.Where(c => c.Fecha.Date >= desdeDatePicker.SelectedDate.Value && c.Fecha.Date <= hastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                listado = MantenimientoBLL.GetList(p => true);
            }

            consultarDataGrid.ItemsSource = listado;
        }
    }
    
}
