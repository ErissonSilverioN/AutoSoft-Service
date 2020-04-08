using Proyecto_MantenimientoVehicular.Entidades;
using Proyecto_MantenimientoVehicular.UI.Consultas;
using Proyecto_MantenimientoVehicular.UI.Registros;
using System.Windows;

namespace Proyecto_MantenimientoVehicular.UI.Menu
{
    /// <summary>
    /// Interaction logic for wMenu.xaml
    /// </summary>
    public partial class wMenu : Window
    {
        public wMenu()
        {
            InitializeComponent();
        }

       

        private void registrousuario_Click(object sender, RoutedEventArgs e)
        {
            rUsuarios registro = new rUsuarios();
            registro.ShowDialog();
        }

        private void registrovehiculo_Click(object sender, RoutedEventArgs e)
        {
            rVehiculo registro = new rVehiculo();
            registro.ShowDialog();
        }

        private void registroarticulo_Click(object sender, RoutedEventArgs e)
        {
            rArticulo registro = new rArticulo();
            registro.ShowDialog();
        }

        private void registrarcliente_Click(object sender, RoutedEventArgs e)
        {
            rCliente registro = new rCliente();
            registro.ShowDialog();
        }

        private void registroproveedor_Click(object sender, RoutedEventArgs e)
        {
            rProveedores registro = new rProveedores();
            registro.ShowDialog();
        }

        private void registrarentradaart_Click(object sender, RoutedEventArgs e)
        {
            rEntradaArticulo registro = new rEntradaArticulo();
            registro.ShowDialog();
        }

        private void registrarmantenimiento_Click(object sender, RoutedEventArgs e)
        {
            rMantenimiento registro = new rMantenimiento();
            registro.ShowDialog();
        }

        private void registrarpedidosproveedor_Click(object sender, RoutedEventArgs e)
        {
            rPedidoProveedor registo = new rPedidoProveedor();
            registo.ShowDialog();
        }




        private void consultaCliente_Click(object sender, RoutedEventArgs e)
        {
            cCliente consulta = new cCliente();
            consulta.ShowDialog();
        }

        private void consultaVehiculo_Click(object sender, RoutedEventArgs e)
        {
            cVehiculo consulta = new cVehiculo();
            consulta.ShowDialog();
        }

        private void consultaentradaarticulo_Click(object sender, RoutedEventArgs e)
        {
            cEntradaArticulo consulta = new cEntradaArticulo();
            consulta.ShowDialog();
        }

        private void consultararticulo_Click(object sender, RoutedEventArgs e)
        {
            cArticulo consulta = new cArticulo();
            consulta.ShowDialog();
        }

        private void consultaProveedores_Click(object sender, RoutedEventArgs e)
        {
            cProveedores consulta = new cProveedores();
            consulta.ShowDialog();
        }

        private void consultaUsuario_Click(object sender, RoutedEventArgs e)
        {
            cUsuario consulta = new cUsuario();
            consulta.ShowDialog();
        }

        private void consultarmantenimiento_Click(object sender, RoutedEventArgs e)
        {
            cMantenimiento consulta = new cMantenimiento();
            consulta.ShowDialog();

        }

        private void consultarpedido_Click(object sender, RoutedEventArgs e)
        {
            cPedido consulta = new cPedido();
            consulta.ShowDialog();
        }
    }
}
