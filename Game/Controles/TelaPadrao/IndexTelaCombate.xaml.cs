﻿using Game.Controladores;
using Game.Controles.MenuInicial;
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
    public partial class IndexTelaCombate : Page
    {
        List<Habilidade> _Habilidades;
        Progressao _save;
        Controlador _controlador;

        public DispatcherTimer contadorRelogio = new DispatcherTimer();
        public DispatcherTimer contadorInimigo = new DispatcherTimer();
        public DispatcherTimer contadorFortificar = new DispatcherTimer();

        Personagem _personagem;
        Inimigo _inimigo;

        bool vezInimigo = false;
        public int tempo = 60;

        public IndexTelaCombate(Personagem personagem, Inimigo inimigo, Progressao save = null)
        {
            InitializeComponent();
            inicializaTimer();

            _controlador = new Controlador();
            _personagem = personagem;
            _inimigo = inimigo;
            _Habilidades = new List<Habilidade>();

            if (save != null)
            {
                _save = save;
            }
            else
            {
                _save = new Progressao();
                _controlador.CarregaJsons();
                _save.Jogador = _controlador.Classes.Find(x => x.Classe == personagem.Classe);
                _save.VidaAtual = -10;
            }

            ReiniciaDadosCombate(personagem, inimigo);
            CarregaJsons();
            CarregaHablidadesPersonagem();
            janelaDadosHabilidade.Instancia._save = _save;

            if (personagem.Agilidade < inimigo.Agilidade)
            {
                vezInimigo = true;
            }
        }

        public void ReiniciaDadosCombate(Personagem personagem, Inimigo inimigo)
        {
            NomePersonagem.Text = personagem.Nome;
            NomeInimigo.Text = inimigo.Nome;

            VidaPersonagem.Text = $"{personagem.Vida}/{personagem.Vida}";
            ManaPersonagem.Text = $"{personagem.Mana}/{personagem.Mana}";
            EnergiaPersonagem.Text = $"{personagem.Energia}/{personagem.Energia}";

            if (_save.VidaAtual > 0)
            {
                var vidaPerdida = personagem.Vida - _save.VidaAtual;
                var manaPerdida = personagem.Mana - _save.ManaAtual;
                var energiaPerdida = personagem.Energia - _save.EnergiaAtual;

                ModificaBarraInfo(VidaPersonagem, BarraDeVidaPersonagem, vidaPerdida);
                ModificaBarraInfo(ManaPersonagem, BarraDeManaPersonagem, manaPerdida);
                ModificaBarraInfo(EnergiaPersonagem, BarraDeStaminaPersonagem, energiaPerdida);
            }

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

            contadorFortificar.Tick += new EventHandler(CooldownFortificar);
            contadorFortificar.Interval = new TimeSpan(0, 0, 1);
            contadorFortificar.Start();
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
                string tipoHabilidade = habilidadeEscolhida.Tipo;

                double qtdDano = resultado[0];
                double qtdDanoCausado = resultado[0] - _inimigo.Defesa;
                double qtdGasto = resultado[1];
                double tipoGasto = resultado[2];
                AtualizaDadosSave();

                if ((_save.ManaAtual - habilidadeEscolhida.CustoBase < 0 && tipoGasto == 0) || (_save.EnergiaAtual - habilidadeEscolhida.CustoBase < 0 && tipoGasto == 1))
                {                  
                    return;
                }

                if (tipoGasto == 0)
                {
                    ModificaBarraInfo(ManaPersonagem, BarraDeManaPersonagem, qtdGasto);
                }
                else
                {
                    ModificaBarraInfo(EnergiaPersonagem, BarraDeStaminaPersonagem, qtdGasto);
                }

                if (habilidadeEscolhida.Tipo == "Fortificar")
                {
                    Image btn = sender as Image;
                    PosicaoDefesa(qtdDano, btn);
                }

                if (habilidadeEscolhida.Tipo == "Buff")
                {
                    ModificaBarraInfo(VidaPersonagem, BarraDeVidaPersonagem, (-1 * qtdDano));
                }
                else if (qtdDanoCausado > 0)
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
                    qtdDano = qtdDano * (0.5 * _personagem.Inteligencia);
                    break;

                case "ArtesMarciais":
                case "Combate":
                case "Fortificar":
                    tipoGasto = 1;
                    qtdDano = qtdDano * (0.5 * _personagem.Forca);
                    break;
            }

            return new double[] { qtdDano, qtdGasto, tipoGasto };
        }

        List<Tuple<Image, int, double, int>> BtnsDesativos = new List<Tuple<Image, int, double, int>>();
        private void PosicaoDefesa(double qtdDano, Image btn)
        {
            _personagem.Defesa += qtdDano;
            btn.IsEnabled = false;
            btn.Visibility = Visibility.Collapsed;
            BtnsDesativos.Add(new Tuple<Image, int , double, int>(btn, ContadorEventos, qtdDano, 6));

            contadorFortificar.Start();  
        }

        void CooldownFortificar(object sender, EventArgs e) 
        {
            List<Tuple<Image, int, double, int>> RetirarDaLista = new List<Tuple<Image, int, double, int>>();

            foreach (Tuple<Image, int, double, int> tuple in BtnsDesativos)
            {
                if (ContadorEventos == tuple.Item2 + tuple.Item4)
                {
                    tuple.Item1.IsEnabled = true;
                    tuple.Item1.Visibility = Visibility.Visible;
                    _personagem.Defesa -= tuple.Item3;
                    RetirarDaLista.Add(tuple);
                }
            }

            foreach (var item in RetirarDaLista)
            {
                BtnsDesativos.Remove(item);
            }
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
            VerificaFimJogo();
        }

        public void VerificaFimJogo()
        {
            var vidaPersonagem = double.Parse(VidaPersonagem.Text.Split('/')[0]);
            var vidaInimigo = double.Parse(VidaInimigo.Text.Split('/')[0]);

            if (vidaInimigo <= 0)
            {
                AtualizaDadosSave();
                FimDeJogo();
            }
            else if (vidaPersonagem <= 0)
            {
                AtualizaDadosSave();
                FimDeJogo();
            }
        }

        public void FimDeJogo(bool fuga = false)
        {
            contadorRelogio.Stop();
            contadorInimigo.Stop();
            //contadorFimDeJogo.Stop();

            _save.Lutas++;
            if (_save.VidaAtual <= 0 || fuga)
            {
                _save.Derrotas++;
            }
            else
            {
                _save.Vitorias++;
                _save.Moedas += 100;
                _save.Jogador.XpAtual += _inimigo.XpDropado;
            }

            _controlador.salvarAvanço(_save);
            this.NavigationService.Navigate(new IndexMenuInicial(_save));
        }

        private void VoltarFunc(object sender, RoutedEventArgs e)
        {
            AtualizaDadosSave();
            FimDeJogo(true);
        }

        private void AtualizaDadosSave()
        {
            _save.VidaAtual = double.Parse(VidaPersonagem.Text.Split('/')[0]);
            _save.ManaAtual = double.Parse(ManaPersonagem.Text.Split('/')[0]);
            _save.EnergiaAtual = double.Parse(EnergiaPersonagem.Text.Split('/')[0]);
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
                imgAdd.MouseEnter += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Hand; mouseOver(true, habilidadeEscolhida); };
                imgAdd.MouseLeave += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Arrow; mouseOver(false, habilidadeEscolhida); };

                PainelHabilidades.Children.Add(imgAdd);
            }

            var imgFuga = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\ImgFuga.png") as ImageSource;

            Image imgFUGA = new Image() { Source = imgFuga, Margin = new Thickness(0, 5, 0, 5), Height = 40 };
            imgFUGA.MouseLeftButtonDown += (s, e) => VoltarFunc(s, e);
            imgFUGA.MouseEnter += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Hand; TextoCusto.Text = "Sair"; TextoCusto.Visibility = Visibility.Visible; };
            imgFUGA.MouseLeave += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Arrow; TextoCusto.Visibility = Visibility.Collapsed; };

            PainelHabilidades.Children.Add(imgFUGA);
        }

        void mouseOver(bool over, Habilidade habilidadeEscolhida)
        {
            if (over)
            {
                janelaDadosHabilidade.Instancia.DefineDados(habilidadeEscolhida);
                janelaDadosHabilidade.Focar();
                janelaDadosHabilidade.Mostrar();
            }
            else
            {
                janelaDadosHabilidade.Esconder();
            }
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
