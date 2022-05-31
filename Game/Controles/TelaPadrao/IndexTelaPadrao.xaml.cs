﻿using Game.Controles.MenuInicial;
using Infraestrutura.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        List<Habilidade> _Habilidades = new List<Habilidade>();

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
            CarregaJsons();
            CarregaHablidadesPersonagem();
        }

        public void ReiniciaDadosCombate(Personagem personagem, Inimigo inimigo)
        {
            NomePersonagem.Text = personagem.Nome;
            NomeInimigo.Text = inimigo.Nome;
            VidaPersonagem.Text = $"{personagem.Vida}/{personagem.Vida}";
            ManaPersonagem.Text = $"{personagem.Mana}/{personagem.Mana}";
            EnergiaPersonagem.Text = $"{personagem.Energia}/{personagem.Energia}";

            VidaInimigo.Text = $"{inimigo.Vida}/{inimigo.Vida}";
            ManaInimigo.Text = $"{inimigo.Mana}/{inimigo.Mana}";
            EnergiaInimigo.Text = $"{inimigo.Energia}/{inimigo.Energia}";

            ImageFotoPersonagem.Source = new ImageSourceConverter().ConvertFromString($"Dados\\Imagens\\Personagens\\{personagem.Foto}") as ImageSource;
            ImageFotoInimigo.Source = new ImageSourceConverter().ConvertFromString($"Dados\\Imagens\\Inimigos\\{inimigo.Foto}") as ImageSource;
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
                    //_personagem.Energia
                    BarraDeVidaPersonagem.Value = porcentagemVida;
                }
                else
                {
                    qtdDano = 0;
                }

                vezInimigo = false;
                RegistraNovoEventoAtaque(_inimigo.Nome, _personagem.Nome, _inimigo.Forca, qtdDano, _personagem.Defesa, "Soco", Brushes.Red);
            }
        }

        private void AtacarInimigo(object sender, RoutedEventArgs e, Habilidade habilidadeEscolhida)
        {
            if (!vezInimigo)
            {
                double[] resultado = RealizarAtaque(habilidadeEscolhida);

                double qtdDano = resultado[0];
                double qtdDanoCausado = resultado[0] - _inimigo.Defesa;
                double qtdGasto = resultado[1];
                double tipoGasto = resultado[2];

                if (tipoGasto == 0)
                {
                    ModificaBarraInfo(ManaPersonagem, BarraDeManaPersonagem, qtdGasto);
                }
                else
                {
                    ModificaBarraInfo(EnergiaPersonagem, BarraDeStaminaPersonagem, qtdGasto);
                }

                if (qtdDanoCausado > 0)
                {
                    ModificaBarraInfo(VidaInimigo, BarraDeVidaInimigo, qtdDanoCausado);
                }
                else
                {
                    qtdDanoCausado = 0;
                }

                vezInimigo = true;
                RegistraNovoEventoAtaque(_personagem.Nome, _inimigo.Nome, qtdDano, qtdDanoCausado, _inimigo.Defesa, habilidadeEscolhida.Nome, Brushes.Blue);
            }
        }

        private double[] RealizarAtaque(Habilidade habilidade)
        {
            double qtdDano = habilidade.DanoBase;
            double qtdGasto = habilidade.CustoBase;
            double tipoGasto = 0;


            switch (habilidade.Tipo)
            {
                case "Magia":
                case "Buff":
                case "DeBuff":
                    tipoGasto = 0;
                    break;

                case "ArtesMarciais":
                case "Combate":
                case "Fortificar":
                    tipoGasto = 1;
                    qtdDano = qtdDano * _personagem.Forca;
                    break;
            }

            return new double[] { qtdDano, qtdGasto, tipoGasto };
        }

        private void PosicaoDefesa(object sender, RoutedEventArgs e)
        {

        }

        int ContadorEventos = 0;
        public void RegistraNovoEventoAtaque(string atacante, string defensor, double qtdDano, double danoCausado, double defesa, string nomeAtaque, SolidColorBrush cor)
        {
            TextBlock novoEnvento = 
                new TextBlock 
                { 
                    Text = $"{ContadorEventos} - {atacante} desferiu {nomeAtaque} de {danoCausado}(atk:{qtdDano} || def:{defesa}) de dano em {defensor}.", 
                    Foreground = cor, 
                    TextWrapping = TextWrapping.Wrap 
                };

            PainelDeEventos.Children.Add(novoEnvento);
            ContadorEventos++;
            ScrollEventos.PageDown();
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

        public void CarregaJsons()
        {
            var fileHablidades = @"Dados\Habilidades.json";
            _Habilidades = JsonConvert.DeserializeObject<List<Habilidade>>(File.ReadAllText(fileHablidades, Encoding.UTF8));
        }

        public void CarregaHablidadesPersonagem()
        {
            foreach (var idHabilidade in _personagem.HabilidadesPermitidas)
            {
                Habilidade habilidadeEscolhida = _Habilidades.Find(x => x.IdHabilidade == idHabilidade);

                string caminhaoImg = $"Dados\\Imagens\\{habilidadeEscolhida.Icon}";
                var imgArquivo = new ImageSourceConverter().ConvertFromString(caminhaoImg) as ImageSource;

                Image imgAdd = new Image() { Source = imgArquivo, Margin = new Thickness(0, 5, 0, 5), Height = 40 };
                imgAdd.MouseLeftButtonDown += (s, e) => AtacarInimigo(s, e, habilidadeEscolhida);

                PainelHabilidades.Children.Add(imgAdd);
            }

            var imgFuga = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\ImgFuga.png") as ImageSource;

            Image imgFUGA = new Image() { Source = imgFuga, Margin = new Thickness(0, 5, 0, 5), Height = 40 };
            imgFUGA.MouseLeftButtonDown += (s, e) => VoltarFunc(s, e);

            PainelHabilidades.Children.Add(imgFUGA);
        }

        public void ModificaBarraInfo(TextBlock TextBlockTexto, ProgressBar BarraDoStatus, double qtdDano)
        {
            var textoGeral = TextBlockTexto.Text.Split('/');
            textoGeral[0] = (double.Parse(textoGeral[0]) - qtdDano).ToString();
            TextBlockTexto.Text = $"{textoGeral[0]}/{textoGeral[1]}";
            double porcentagemResultante = (double.Parse(textoGeral[0]) * 100) / double.Parse(textoGeral[1]);

            BarraDoStatus.Value = porcentagemResultante;
        }
    }
}
