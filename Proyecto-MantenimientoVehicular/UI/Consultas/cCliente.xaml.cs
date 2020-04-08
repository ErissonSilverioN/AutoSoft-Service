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
    /// Interaction logic for cCliente.xaml
    /// </summary>
    public partial class cCliente : Window
    {
        public cCliente()
        {
            InitializeComponent();
        }

        private void consultarButton_Click(object sender, RoutedEventArgs e)
        {

            var listado = new List<Clientes>();

            if (criterioTextBox.Text.Trim().Length > 0)
            {
                switch (filtroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = ClienteBLL.GetList(p => true);
                        break;

                    case 1:
                        int ID = Convert.ToInt32(criterioTextBox.Text);
                        listado = ClienteBLL.GetList(p => p.ClienteId == ID);
                        break;

                    case 2:
                        listado = ClienteBLL.GetList(p => p.Nombre.Contains(criterioTextBox.Text));
                        break;

                    case 3:
                        listado = ClienteBLL.GetList(p => p.Email.Contains(criterioTextBox.Text));
                        break;


                    case 4:
                        int cedula = Convert.ToInt32(criterioTextBox.Text);
                        listado = ClienteBLL.GetList(p => p.Cedula == Convert.ToString(cedula));
                        break;



                }

                listado = listado.Where(c => c.Fecha.Date >= desdeDatePicker.SelectedDate.Value && c.Fecha.Date <= hastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                listado = ClienteBLL.GetList(p => true);
            }

            consultarDataGrid.ItemsSource = listado;
        }

        
    }
    
}
