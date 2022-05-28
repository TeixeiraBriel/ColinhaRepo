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
            contadorInimigo.Interval = new TimeSpan(0, 0, 4);
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
                txtTurno = $"Turno Inimigo!";
            else
                txtTurno = $"Seu Turno!";
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
                string qtdDano = (_personagem.Força - _inimigo.Defesa).ToString();
                vidaPersonagemCalc[0] = (int.Parse(vidaPersonagemCalc[0]) - int.Parse(qtdDano)).ToString();
                VidaPersonagem.Text = $"{vidaPersonagemCalc[0]}/{vidaPersonagemCalc[1]}";

                vezInimigo = false;
                RegistraNovoEventoAtaque(_inimigo.Nome, _personagem.Nome, _inimigo.Força, _personagem.Defesa);
            }
        }

        public void VerificaFimJogo(object sender, EventArgs e)
        {
            var vidaPersonagem = int.Parse(VidaPersonagem.Text.Split('/')[0]);
            var vidaInimigo = int.Parse(VidaInimigo.Text.Split('/')[0]);

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
                string qtdDano = (_personagem.Força - _inimigo.Defesa).ToString();
                vidaInimigoCalc[0] = (int.Parse(vidaInimigoCalc[0]) - int.Parse(qtdDano)).ToString();
                VidaInimigo.Text = $"{vidaInimigoCalc[0]}/{vidaInimigoCalc[1]}";

                vezInimigo = true;
                RegistraNovoEventoAtaque(_personagem.Nome, _inimigo.Nome, _personagem.Força, _inimigo.Defesa);
            }
        }

        private void PosicaoDefesa(object sender, RoutedEventArgs e)
        {

        }

        int ContadorEventos = 0;
        public void RegistraNovoEventoAtaque(string atacante, string defensor, int qtdDano, int defesa)
        {
            TextBlock novoEnvento = new TextBlock { Text = $"{ContadorEventos} - {atacante} desferiu um golpe de {qtdDano}(-{defesa}) de dano em {defensor}" };
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
