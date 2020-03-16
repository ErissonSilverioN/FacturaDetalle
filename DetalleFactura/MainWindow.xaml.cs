using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DetalleFactura.BLL;
using DetalleFactura.Entidades;
using DetalleFactura.NewFolder;

namespace DetalleFactura
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Factura factura = new Factura();
        public List<FacturaDetalle> Detalles { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //List<string> Categoria = new List<string>()
            //{
            //    ""
            //};

            this.DataContext = factura;
        }

        private void Limpiar()
        {
            idTextBox.Text = "0";
            estudianteTextBox.Text = string.Empty;
            cantidadTextBox.Text = "0";
            precioTextBox.Text = "0";
            importeTextBox.Text = "0";
            fechaDatePicker.SelectedDate = DateTime.Now;

            Factura factura = new Factura();
            factura.FacturasDetalle = new List<FacturaDetalle>();
            
            
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(estudianteTextBox.Text))
            {
                MessageBox.Show("Campo Obligatorio");
                paso = false;
            }
            if (string.IsNullOrWhiteSpace(cantidadTextBox.Text))
            {
                MessageBox.Show("Campo Obligatorio");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(precioTextBox.Text))
            {
                MessageBox.Show("Campo Obligatorio");
                paso = false;
            }

            return paso;


        }


        private bool ExisteEnLaBaseDatos()
        {
            Factura factura = FacturaBLL.Buscar((int)Convert.ToInt32(idTextBox.Text));
                return (factura != null);
        }

        private void Llenar()
        {
            this.DataContext = null;
            this.DataContext = factura;
        }

        private void guardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;
            if (!Validar())
                return;

            if (idTextBox.Text == "0")
                paso = FacturaBLL.Guardar(factura);
            else
            {
                if (!ExisteEnLaBaseDatos())
                {
                    MessageBox.Show("No se puede Modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = FacturaBLL.Modificar(factura);
            }
            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo");
        }

        private void eliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (FacturaBLL.Eliminar(factura.FacturaId))
            {
                Limpiar();

                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Erro Al eliminar una persona");
        }

        private void buscarButton_Click(object sender, RoutedEventArgs e)
        {
            Factura facturalocal = FacturaBLL.Buscar(factura.FacturaId);

            Limpiar();
            if (facturalocal != null)
            {
                factura = facturalocal;
                Llenar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Cliente no Existe...");
            }
        }

        private void agregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (detalleDateGrid.SelectedItem != null)
                this.Detalles = (List<FacturaDetalle>)detalleDateGrid.ItemsSource;

            this.Detalles.Add(
                new FacturaDetalle(
                    id: 0,
                    facturaId: Convert.ToInt32(idTextBox.Text),
                    estudianteId: 0,
                    cantidad: Convert.ToDecimal(cantidadTextBox.Text),
                    precio: Convert.ToDecimal(precioTextBox.Text),
                    importe: Convert.ToDecimal(importeTextBox.Text)

                    ));

            Llenar();

            estudianteTextBox.Clear();
            cantidadTextBox.Clear();
            precioTextBox.Clear();
           
        }

        private void removerButton_Click(object sender, RoutedEventArgs e)
        {
            if (detalleDateGrid.Columns.Count > 0  && detalleDateGrid.SelectedCells != null)
            {
                factura.FacturasDetalle.RemoveAt(detalleDateGrid.SelectedIndex);
                Llenar();
            }
        }

        private void cantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal monto = 0;
            decimal pago = 0;

            if (!string.IsNullOrWhiteSpace(cantidadTextBox.Text) && cantidadTextBox.Text != "-")
            {
                monto = decimal.Parse(cantidadTextBox.Text);
            }
            if (!string.IsNullOrWhiteSpace(precioTextBox.Text) && precioTextBox.Text != "-")
            {
                pago = decimal.Parse(precioTextBox.Text);
            }

            decimal resultado = monto * pago;

            importeTextBox.Text = resultado.ToString();
        }

        private void precioTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal monto = 0;
            decimal pago = 0;

            if (!string.IsNullOrWhiteSpace(cantidadTextBox.Text) && cantidadTextBox.Text != "-")
            {
                monto = decimal.Parse(cantidadTextBox.Text);
            }
            if (!string.IsNullOrWhiteSpace(precioTextBox.Text) && precioTextBox.Text != "-")
            {
                pago = decimal.Parse(precioTextBox.Text);
            }

            decimal resultado = monto * pago;

            importeTextBox.Text = resultado.ToString();
        }
    }
}
