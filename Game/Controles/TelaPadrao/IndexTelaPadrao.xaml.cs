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
using System.Windows.Threading;

namespace Game.Controles.TelaPadrao
{
    /// <summary>
    /// Interação lógica para IndexTelaPadrao.xam
    /// </summary>
    public partial class IndexTelaPadrao : Page
    {
        public IndexTelaPadrao()
        {
            InitializeComponent();
        }

        #region Antigo
        /*
        public DispatcherTimer contadorTempoCliente = new DispatcherTimer();
        public DispatcherTimer contadorNovoCliente = new DispatcherTimer();
        public DispatcherTimer contadorRelogio = new DispatcherTimer();

        public int tempo = 0;
        public IndexTelaPadrao()
        {
            InitializeComponent();

            inicializaTimer();
        }

        public void inicializaTimer()
        {
            contadorTempoCliente.Tick += new EventHandler(MudaClienteAtual);
            contadorNovoCliente.Tick += new EventHandler(chamaNovoCliente);
            contadorRelogio.Tick += new EventHandler(Relogio);

            contadorTempoCliente.Interval = new TimeSpan(0, 0, 10);
            contadorNovoCliente.Interval = new TimeSpan(0, 0, 5);
            contadorRelogio.Interval = new TimeSpan(0, 0, 1);

            Util.DispatcherUtil.Dispatcher(() =>
            contadorRelogio.Start()
            );

            Util.DispatcherUtil.Dispatcher(() =>
            contadorNovoCliente.Start()
            );

            Util.DispatcherUtil.Dispatcher(() =>
            contadorTempoCliente.Start()
            );
        }

        public void MudaClienteAtual(object sender, EventArgs e)
        {
            var clienteNaFila = FilaCliente.Text.Split('-');
            bool primeiroClient = true;
            foreach (var cliente in clienteNaFila)
            {
                if (cliente == "")
                {
                    continue;
                }
                if (primeiroClient)
                {
                    primeiroClient = false;
                    ClienteAtual.Text = cliente;
                    FilaCliente.Text = "";
                }
                else
                {
                    FilaCliente.Text += cliente + "-";
                }
            }
        }

        public void chamaNovoCliente(object sender, EventArgs e)
        {
            var n = new Random().Next(50, 100);
            FilaCliente.Text += n.ToString() + "-";
        }
        public void Relogio(object sender, EventArgs e)
        {
            tempo++;
            Tempo.Text = tempo.ToString();
        }
        */
        #endregion

        private void AtacarInimigo(object sender, RoutedEventArgs e)
        {

        }

        private void PosicaoDefesa(object sender, RoutedEventArgs e)
        {

        }
    }
}
