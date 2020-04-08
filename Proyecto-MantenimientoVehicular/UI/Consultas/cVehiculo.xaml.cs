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
    /// Interaction logic for cVehiculo.xaml
    /// </summary>
    public partial class cVehiculo : Window
    {
        public cVehiculo()
        {
            InitializeComponent();
        }

        private void consultar1Button_Click(object sender, RoutedEventArgs e)
        {

            var listado = new List<Vehiculos>();

            if (criterioTextBox.Text.Trim().Length > 0)
            {
                switch (filtroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = VehiculoBLL.GetList(p => true);
                        break;

                    case 1:
                        int ID = Convert.ToInt32(criterioTextBox.Text);
                        listado = VehiculoBLL.GetList(p => p.VehiculoId == ID);
                        break;

                    case 2:
                        int Placa = Convert.ToInt32(criterioTextBox.Text);
                        listado = VehiculoBLL.GetList(p => p.VehiculoId == Placa);
                        break;

                    case 3:
                        int Año = Convert.ToInt32(criterioTextBox.Text);
                        listado = VehiculoBLL.GetList(p => p.VehiculoId == Año);
                        break;

                }

                listado = listado.Where(c => c.Fecha.Date >= desdeDatePicker.SelectedDate.Value && c.Fecha.Date <= hastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                listado = VehiculoBLL.GetList(p => true);
            }

            consultarDataGrid.ItemsSource = listado;
        }
    }
}
