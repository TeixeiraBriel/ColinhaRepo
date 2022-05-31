using Game.Controles.MenuInicial;
using Infraestrutura.Entidades;
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
        Personagem _personagem;
        Inimigo _inimigo;

        bool vezInimigo = false;
        public int tempo = 60;

        public IndexTelaPadrao(Personagem personagem, Inimigo inimigo)
        {
            InitializeComponent();
            inicializaTimer();

            _personagem = personagem;
            _inimigo = inimigo;

            ReiniciaDadosCombate(personagem, inimigo);
        }

        public void ReiniciaDadosCombate(Personagem personagem, Inimigo inimigo)
        {
            NomePersonagem.Text = personagem.Nome;
            NomeInimigo.Text = inimigo.Nome;
            VidaPersonagem.Text = $"{personagem.Vida}/{personagem.Vida}";
            VidaInimigo.Text = $"{inimigo.Vida}/{inimigo.Vida}";
        }

        public void inicializaTimer()
        {
            contadorRelogio.Tick += new EventHandler(Relogio);
            contadorRelogio.Interval = new TimeSpan(0, 0, 1);
            contadorRelogio.Start();

            contadorInimigo.Tick += new EventHandler(InimigoAtaca);
            contadorInimigo.Interval = new TimeSpan(0, 0, 2);
            contadorInimigo.Start();

            contadorFimDeJogo.Tick += new EventHandler(VerificaFimJogo);
            contadorFimDeJogo.Interval = new TimeSpan(0, 0, 1);
            contadorFimDeJogo.Start();
        }

        public void Relogio(object sender, EventArgs e)
        {
            if (!this.IsVisible)
            {
                return;
            }
            tempo--;
            Tempo.Text = tempo.ToString();

            string txtTurno = "";
            if (vezInimigo)
            {
                Turno.Foreground = Brushes.Red;
                txtTurno = $"Turno Inimigo!";
            }
            else
            {
                Turno.Foreground = Brushes.Blue;
                txtTurno = $"Seu Turno!";
            }
            Turno.Text = txtTurno;

            if (tempo == 0)
            {
                FimDeJogo();
            }
        }

        public void InimigoAtaca(object sender, EventArgs e)
        {
            if (vezInimigo)
            {
                var vidaPersonagemCalc = VidaPersonagem.Text.Split('/');
                double qtdDano = (_inimigo.Forca - _personagem.Defesa);
                if (qtdDano > 0)
                {
                    vidaPersonagemCalc[0] = (double.Parse(vidaPersonagemCalc[0]) - qtdDano).ToString();
                    VidaPersonagem.Text = $"{vidaPersonagemCalc[0]}/{vidaPersonagemCalc[1]}";
                    double porcentagemVida = (double.Parse(vidaPersonagemCalc[0]) * 100) / double.Parse(vidaPersonagemCalc[1]);
                    BarraDeVidaPersonagem.Value = porcentagemVida;
                }
                else
                {
                    qtdDano = 0;
                }

                vezInimigo = false;
                RegistraNovoEventoAtaque(_inimigo.Nome, _personagem.Nome, qtdDano, _inimigo.Forca, _personagem.Defesa, Brushes.Red);
            }
        }

        public void VerificaFimJogo(object sender, EventArgs e)
        {
            var vidaPersonagem = double.Parse(VidaPersonagem.Text.Split('/')[0]);
            var vidaInimigo = double.Parse(VidaInimigo.Text.Split('/')[0]);

            if (vidaPersonagem <= 0 || vidaInimigo <= 0)
            {
                FimDeJogo();
            }
        }

        private void AtacarInimigo(object sender, RoutedEventArgs e)
        {
            if (!vezInimigo)
            {
                var vidaInimigoCalc = VidaInimigo.Text.Split('/');
                double qtdDano = (_personagem.Forca - _inimigo.Defesa);
                if (qtdDano > 0)
                {
                    vidaInimigoCalc[0] = (double.Parse(vidaInimigoCalc[0]) - qtdDano).ToString();
                    VidaInimigo.Text = $"{vidaInimigoCalc[0]}/{vidaInimigoCalc[1]}";
                    double porcentagemVida = (double.Parse(vidaInimigoCalc[0]) * 100) / double.Parse(vidaInimigoCalc[1]);

                    BarraDeVidaInimigo.Value = porcentagemVida;

                }
                else
                {
                    qtdDano = 0;
                }

                vezInimigo = true;
                RegistraNovoEventoAtaque(_personagem.Nome, _inimigo.Nome, qtdDano, _personagem.Forca, _inimigo.Defesa, Brushes.Blue);
            }
        }

        private void PosicaoDefesa(object sender, RoutedEventArgs e)
        {

        }

        int ContadorEventos = 0;
        public void RegistraNovoEventoAtaque(string atacante, string defensor, double qtdDano, double forca, double defesa, SolidColorBrush cor)
        {
            TextBlock novoEnvento = 
                new TextBlock 
                { 
                    Text = $"{ContadorEventos} - {atacante} desferiu um golpe de {qtdDano}(atk:{forca} || def:{defesa}) de dano em {defensor}.", 
                    Foreground = cor, 
                    TextWrapping = TextWrapping.Wrap 
                };

            PainelDeEventos.Children.Add(novoEnvento);
            ContadorEventos++;
            ScrollEventos.PageDown();
        }

        public void FimDeJogo()
        {
            contadorRelogio.Stop();
            contadorInimigo.Stop();
            contadorFimDeJogo.Stop();
            this.NavigationService.Navigate(new IndexMenuInicial());
        }

        private void VoltarFunc(object sender, RoutedEventArgs e)
        {
            FimDeJogo();
        }
    }
}
