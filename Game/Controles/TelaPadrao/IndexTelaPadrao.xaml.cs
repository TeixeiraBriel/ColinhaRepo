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
        public DispatcherTimer contadorRelogio = new DispatcherTimer();
        public int tempo = 60;

        public IndexTelaPadrao()
        {
            InitializeComponent();
            inicializaTimer();
        }

        public void inicializaTimer()
        {
            contadorRelogio.Tick += new EventHandler(Relogio);
            contadorRelogio.Interval = new TimeSpan(0, 0, 1);
            contadorRelogio.Start();
        }
        public void Relogio(object sender, EventArgs e)
        {
            tempo--;
            Tempo.Text = tempo.ToString();
            if (tempo == 0)
            {
                contadorRelogio.Stop();
            }
        }

        private void AtacarInimigo(object sender, RoutedEventArgs e)
        {
            var vidaInimigoCalc = VidaInimigo.Text.Split('/');
            string qtdDano = "15";
            vidaInimigoCalc[0] = (int.Parse(vidaInimigoCalc[0]) - int.Parse(qtdDano)).ToString();
            VidaInimigo.Text = $"{vidaInimigoCalc[0]}/{vidaInimigoCalc[1]}";

            RegistraNovoEventoAtaque(NomePersonagem.Text, NomeInimigo.Text, qtdDano);
        }

        private void PosicaoDefesa(object sender, RoutedEventArgs e)
        {

        }

        public void RegistraNovoEventoAtaque(string atacante, string defensor, string qtdDano)
        {
            TextBlock novoEnvento = new TextBlock { Text = $"{atacante} desferiu um golpe de {qtdDano} de dano em {defensor}" };
            PainelDeEventos.Children.Add(novoEnvento);
        }
    }
}
