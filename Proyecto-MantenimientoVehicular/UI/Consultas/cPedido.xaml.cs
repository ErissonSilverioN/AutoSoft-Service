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
    /// Interaction logic for cPedido.xaml
    /// </summary>
    public partial class cPedido : Window
    {
        public cPedido()
        {
            InitializeComponent();
        }

        private void consultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<PedidosProveedor>();

            if (criterioTextBox.Text.Trim().Length > 0)
            {
                switch (filtroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = PedidosProveedorBLL.GetList(p => true);
                        break;

                    case 1:
                        int ID = Convert.ToInt32(criterioTextBox.Text);
                        listado = PedidosProveedorBLL.GetList(p => p.PedidoId == ID);
                        break;

                    case 2:
                        int proveedorid = Convert.ToInt32(criterioTextBox.Text);
                        listado = PedidosProveedorBLL.GetList(p => p.ProveedoresId == proveedorid);
                        break;

                    

                }

                listado = listado.Where(c => c.Fecha.Date >= desdeDatePicker.SelectedDate.Value && c.Fecha.Date <= hastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                listado = PedidosProveedorBLL.GetList(p => true);
            }

            consultarDataGrid.ItemsSource = listado;
        }
    }
    
}
