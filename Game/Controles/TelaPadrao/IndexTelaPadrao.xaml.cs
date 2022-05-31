using Game.Controles.MenuInicial;
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
        public DispatcherTimer contadorInimigo = new DispatcherTimer();
        public DispatcherTimer contadorFimDeJogo = new DispatcherTimer();
        bool vezInimigo = false;
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

            contadorInimigo.Tick += new EventHandler(InimigoAtaca);
            contadorInimigo.Interval = new TimeSpan(0, 0, 3);
            contadorInimigo.Start();

            contadorFimDeJogo.Tick += new EventHandler(VerificaFimJogo);
            contadorFimDeJogo.Interval = new TimeSpan(0, 0, 1);
            contadorFimDeJogo.Start();
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

        public void InimigoAtaca(object sender, EventArgs e)
        {
            if (vezInimigo)
            {
                var vidaPersonagemCalc = VidaPersonagem.Text.Split('/');
                string qtdDano = "15";
                vidaPersonagemCalc[0] = (int.Parse(vidaPersonagemCalc[0]) - int.Parse(qtdDano)).ToString();
                VidaPersonagem.Text = $"{vidaPersonagemCalc[0]}/{vidaPersonagemCalc[1]}";
                BarraDeVidaPersonagem.Value = (int.Parse(vidaPersonagemCalc[0]));

                vezInimigo = false;
                RegistraNovoEventoAtaque(NomeInimigo.Text, NomePersonagem.Text, qtdDano);
            }
        }

        public void VerificaFimJogo(object sender, EventArgs e)
        {
            var vidaPersonagem = int.Parse(VidaPersonagem.Text.Split('/')[0]);
            var vidaInimigo = int.Parse(VidaInimigo.Text.Split('/')[0]);

            if (vidaPersonagem <= 0 || vidaInimigo <= 0)
            {
                this.NavigationService.Navigate(new IndexMenuInicial());
            }
        }

        private void AtacarInimigo(object sender, RoutedEventArgs e)
        {
            if (!vezInimigo)
            {
                var vidaInimigoCalc = VidaInimigo.Text.Split('/');
                string qtdDano = "15";
                vidaInimigoCalc[0] = (int.Parse(vidaInimigoCalc[0]) - int.Parse(qtdDano)).ToString();
                VidaInimigo.Text = $"{vidaInimigoCalc[0]}/{vidaInimigoCalc[1]}";
                BarraDeVidaInimigo.Value = (int.Parse(vidaInimigoCalc[0]));

                vezInimigo = true;
                RegistraNovoEventoAtaque(NomePersonagem.Text, NomeInimigo.Text, qtdDano);
            }
        }

        private void PosicaoDefesa(object sender, RoutedEventArgs e)
        {

        }

        int ContadorEventos = 0;
        public void RegistraNovoEventoAtaque(string atacante, string defensor, string qtdDano)
        {
            TextBlock novoEnvento = new TextBlock { Text = $"{ContadorEventos} - {atacante} desferiu um golpe de {qtdDano} de dano em {defensor}" };
            PainelDeEventos.Children.Add(novoEnvento);
            ContadorEventos++;
            ScrollEventos.PageDown();
        }
    }
}
