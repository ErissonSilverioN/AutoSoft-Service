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
    /// Interaction logic for cEntradaArticulo.xaml
    /// </summary>
    public partial class cEntradaArticulo : Window
    {
        public cEntradaArticulo()
        {
            InitializeComponent();
        }

        private void consultarButton_Click(object sender, RoutedEventArgs e)
        {

            var listado = new List<EntradaArticulos>();

            if (criterioTextBox.Text.Trim().Length > 0)
            {
                switch (filtroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = EntradaArticuloBLL.GetList(p => true);
                        break;

                    case 1:
                        int ID = Convert.ToInt32(criterioTextBox.Text);
                        listado = EntradaArticuloBLL.GetList(p => p.EntradaArticuloId == ID);
                        break;

                    case 2:
                        int articuoid = Convert.ToInt32(criterioTextBox.Text);
                        listado = EntradaArticuloBLL.GetList(p => p.ArticuloId == articuoid);
                        break;

                    



                }

                listado = listado.Where(c => c.Fecha.Date >= desdeDatePicker.SelectedDate.Value && c.Fecha.Date <= hastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                listado = EntradaArticuloBLL.GetList(p => true);
            }

            consultarDataGrid.ItemsSource = listado;
        }
    }
}
