using Infraestrutura.Entidades;
using Infraestrutura.Entidades.EntCombate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using XpandoLibrary;

namespace Game.Controles.TelaPadrao
{
    /// <summary>
    /// Interação lógica para Combate.xam
    /// </summary>
    public partial class CombateEquipe : Page
    {
        //VariaveisAutoIniciadas
        int FaseCombate = 0;
        int FaseDoisCheck = 0;
        //IniciaListas
        List<IdentificadorPosicaoCombatente> _listPosicoesAliados;
        List<IdentificadorPosicaoCombatente> _listPosicoesInimigos;
        List<Combatente> _combatentes;
        //IniciaSegundaFase
        List<Combatente> Aliados = new List<Combatente>();
        List<Combatente> Inimigos = new List<Combatente>();
        //TerceiraFase
        List<Combatente> CombatentesParticipantes = new List<Combatente>();


        DispatcherTimer contadorRelogio = new DispatcherTimer();
        TimeSpan numeradorRelogio = new TimeSpan();

        public CombateEquipe(List<Combatente> aliados, List<Combatente> inimigos)
        {
            InitializeComponent();
            iniciaListas(aliados, inimigos);
            iniciaCampos();
        }

        void iniciaListas(List<Combatente> aliados, List<Combatente> inimigos)
        {
            List<IdentificadorPosicaoCombatente> listPosicoesAliados = new List<IdentificadorPosicaoCombatente>() {
                new IdentificadorPosicaoCombatente(){Posicao="A1", Painel=A1},
                new IdentificadorPosicaoCombatente(){Posicao="A2", Painel=A2},
                new IdentificadorPosicaoCombatente(){Posicao="A3", Painel=A3},
                new IdentificadorPosicaoCombatente(){Posicao="A4", Painel=A4},
                new IdentificadorPosicaoCombatente(){Posicao="A5", Painel=A5},
                new IdentificadorPosicaoCombatente(){Posicao="A6", Painel=A6},
                new IdentificadorPosicaoCombatente(){Posicao="A7", Painel=A7},
                new IdentificadorPosicaoCombatente(){Posicao="A8", Painel=A8},
                new IdentificadorPosicaoCombatente(){Posicao="A9", Painel=A9},
            };
            List<IdentificadorPosicaoCombatente> listPosicoesInimigos = new List<IdentificadorPosicaoCombatente>() {
                new IdentificadorPosicaoCombatente(){Posicao="I1", Painel=I1},
                new IdentificadorPosicaoCombatente(){Posicao="I2", Painel=I2},
                new IdentificadorPosicaoCombatente(){Posicao="I3", Painel=I3},
                new IdentificadorPosicaoCombatente(){Posicao="I4", Painel=I4},
                new IdentificadorPosicaoCombatente(){Posicao="I5", Painel=I5},
                new IdentificadorPosicaoCombatente(){Posicao="I6", Painel=I6},
                new IdentificadorPosicaoCombatente(){Posicao="I7", Painel=I7},
                new IdentificadorPosicaoCombatente(){Posicao="I8", Painel=I8},
                new IdentificadorPosicaoCombatente(){Posicao="I9", Painel=I9},
            };

            _listPosicoesAliados = listPosicoesAliados;
            _listPosicoesInimigos = listPosicoesInimigos;
            iniciaInimigos(inimigos);
            _combatentes = aliados;
        }

        void iniciaCampos()
        {
            PainelPersonagens.Children.Clear();
            foreach (var item in _combatentes)
            {
                var teste = _listPosicoesInimigos.FirstOrDefault(x => x.Combatente != null && x.Combatente.Nome == item.Nome);
                if (_listPosicoesInimigos.FirstOrDefault(x => x.Combatente != null && x.Combatente.Nome == item.Nome) == null)
                {
                    PainelPersonagens.Children.Add(criarCardCombatente(item));
                }
            }

            lblCabecalho.Foreground = Brushes.Black;
            lblCabecalho.Content = "Seleção de Combatentes";
            Button BtnCabecalho = new Button() { Name = "btnCabecalho", Width = 100, Height = 25, Content = "Proxima Fase" };
            BtnCabecalho.Click += (s1, e1) =>
            {
                if (Aliados.Count > 0)
                {
                    IniciaSegundaFase(s1, e1);
                }
                else
                {
                    lblCabecalho.Foreground = Brushes.Red;
                    lblCabecalho.Content = "Favor Selecionar ao menos 1 combatente.";
                }
            };
            PainelCabecalho.Children.Add(BtnCabecalho);
        }

        void iniciaInimigos(List<Combatente> inimigosParam)
        {
            foreach (var inimigo in inimigosParam)
            {
                if (inimigo.Painel == null)
                {
                    inimigo.Painel = new RepresentacaoTelaCombate(inimigo.Nome, inimigo.Vida, inimigo.intervaloAtaques);
                }
                PosicionaCombatenteSelecionado(inimigo, _listPosicoesInimigos, Inimigos);
            }
        }

        private void IniciaSegundaFase(object sender, RoutedEventArgs e)
        {
            PainelCabecalho.Children.RemoveAt(1);
            lblCabecalho.Foreground = Brushes.Black;
            lblCabecalho.Content = "Seleção de Foco";
            FaseCombate = 1;

            PainelPersonagens.Children.Clear();

            Action<List<IdentificadorPosicaoCombatente>> LimpaCampo = (_listPos) =>
            {
                foreach (var item in _listPos)
                {
                    if (item.Combatente == null)
                        item.Painel.Children.Clear();
                }
            };

            LimpaCampo(_listPosicoesAliados);
            LimpaCampo(_listPosicoesInimigos);

            foreach (var atacante in Inimigos)
            {
                atacante.Painel.alvo = Aliados.FirstOrDefault();
            }

            Func<List<Combatente>, double> SomarVidas = list =>
            {
                double valorTotal = 0;
                foreach (Combatente item in list)
                {
                    if (item != null)
                    {
                        valorTotal += item.Vida;
                    }
                }
                return valorTotal;
            };

            VidaAliados.Maximum = SomarVidas(Aliados);
            VidaAliados.Value = VidaAliados.Maximum;
            VidaInigos.Maximum = SomarVidas(Inimigos);
            VidaInigos.Value = VidaInigos.Maximum;
            FaseDoisCheck = Aliados.Count;

            Button BtnCabecalho = new Button() { Name = "btnCabecalho", Width = 100, Height = 25, Content = "Proxima Fase" };
            BtnCabecalho.Click += (s1, e1) =>
            {
                if (FaseDoisCheck == 0)
                {
                    IniciaTerceiraFase(s1, e1);
                }
                else
                {
                    lblCabecalho.Foreground = Brushes.Red;
                    lblCabecalho.Content = "Favor confirmar seus alvos";
                }
            };
            PainelCabecalho.Children.Add(BtnCabecalho);

            InfoCombatente();
        }

        private void IniciaTerceiraFase(object sender, RoutedEventArgs e)
        {
            PainelCabecalho.Children.RemoveAt(1);
            lblCabecalho.Foreground = Brushes.Black;
            lblCabecalho.Content = "PANCADARIA";

            foreach (var atacante in Aliados)
            {
                atacante.Painel.alvo = Inimigos.FirstOrDefault(x => x.Nome == atacante.Painel.Foco);
            }

            Action<List<IdentificadorPosicaoCombatente>> EncherParticipantes = list =>
            {
                foreach (var item in list)
                {
                    if (item.Combatente != null)
                    {
                        item.Combatente.Painel.Posicao = item.Posicao;
                        CombatentesParticipantes.Add(item.Combatente);
                    }
                }
            };

            EncherParticipantes(_listPosicoesAliados);
            EncherParticipantes(_listPosicoesInimigos);

            foreach (var item in CombatentesParticipantes)
            {
                item.IniciaTimerAtacar();
            }

            foreach (var item in Inimigos)
            {
                item.Painel.Foco = Aliados.FirstOrDefault(x => x.VidaAtual > 0).Nome;
            }

            PainelPersonagens.Children.Clear();
            PainelPersonagens.Orientation = Orientation.Vertical;
            iniciaTimer();
        }

        void iniciaTimer()
        {
            contadorRelogio.Tick += new EventHandler(CombateAtivo);
            contadorRelogio.Interval = new TimeSpan(0, 0, 0, 0,300);
            contadorRelogio.Start();
        }

        public async void CombateAtivo(object sender, EventArgs e)
        {
            numeradorRelogio += TimeSpan.FromMilliseconds(300);
            lblCabecalho.Content = numeradorRelogio.ToString(@"mm\:ss");


            var combatente = Aliados.FirstOrDefault(x => !string.IsNullOrEmpty(x.Painel.Dialogo));
            if (combatente != null)
            {
                var newMsg = combatente.Painel.Dialogo.Replace("{ALVO}", combatente.Painel.alvo.Nome).Replace("{NOME}", combatente.Nome);
                var msg = $"[{numeradorRelogio.ToString(@"mm\:ss")}] = {newMsg}";
                PainelPersonagens.Children.Add(new Label() { Foreground = Brushes.Blue, Content = msg });
                combatente.Painel.Dialogo = "";
            }
            else
            {
                combatente = Inimigos.FirstOrDefault(x => !string.IsNullOrEmpty(x.Painel.Dialogo));
                if (combatente == null)
                    return;
                var newMsg = combatente.Painel.Dialogo.Replace("{ALVO}", combatente.Painel.alvo.Nome).Replace("{NOME}", combatente.Nome);
                var msg = $"[{numeradorRelogio.ToString(@"mm\:ss")}] = {newMsg}";
                PainelPersonagens.Children.Add(new Label() { Foreground = Brushes.Red, Content = msg });
                combatente.Painel.Dialogo = "";
            }
        }

        private StackPanel criarCardCombatente(Combatente combatente)
        {
            StackPanel painel = new StackPanel()
            {
                Background = Brushes.AliceBlue,
                Width = 200,
                Height = 110,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(3)
            };
            Label lblNome = new Label() { Content = $"Nome: {combatente.Nome}" };
            Label lblVida = new Label() { Content = $"Vida: {combatente.Vida}" };
            StackPanel comboboxPainel = new StackPanel() { Orientation = Orientation.Horizontal };
            Label lblPosicao = new Label() { Content = $"Posicao:" };
            ComboBox cmbPosicoes = new ComboBox();

            foreach (var item in _listPosicoesAliados)
            {
                cmbPosicoes.Items.Add(item.Posicao);
            }

            comboboxPainel.Children.Add(lblPosicao);
            comboboxPainel.Children.Add(cmbPosicoes);

            Button btnConfirmar = new Button() { Content = "Confirmar" };
            btnConfirmar.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(cmbPosicoes.Text.ToString()))
                    return;
                if (combatente.Painel == null)
                    combatente.Painel = new RepresentacaoTelaCombate(combatente.Nome, combatente.Vida, combatente.intervaloAtaques);

                combatente.Painel.Posicao = cmbPosicoes.Text;
                PosicionaCombatenteSelecionado(combatente, _listPosicoesAliados, Aliados);

                combatente.Painel.LabelNome.MouseLeftButtonDown += (s2, e2) =>
                {
                    if (FaseCombate != 0)
                    {
                        return;
                    }
                    combatente.Painel.StackPanel.Children.Clear();
                    combatente.Painel.StackPanel.Children.Add(new Label() { Content = cmbPosicoes.Text });
                    PainelPersonagens.Children.Add(criarCardCombatente(combatente));
                    _listPosicoesAliados.Find(x => x.Posicao == cmbPosicoes.Text.ToString()).Combatente = null;
                    Aliados.Remove(combatente);
                };

                PainelPersonagens.Children.Remove(painel);
            };

            painel.Children.Add(lblNome);
            painel.Children.Add(lblVida);
            painel.Children.Add(comboboxPainel);
            painel.Children.Add(btnConfirmar);

            return painel;
        }

        void PosicionaCombatenteSelecionado(Combatente personagem, List<IdentificadorPosicaoCombatente> listaPosicoes, List<Combatente> listaAtivos)
        {
            if (personagem.Painel.Posicao == null)
            {
                int numAleatorio = new Random().Next(0, _listPosicoesInimigos.Count -1);
                personagem.Painel.Posicao = _listPosicoesInimigos[numAleatorio].Posicao;
            }

            var vaga = listaPosicoes.FirstOrDefault(x => x.Posicao == personagem.Painel.Posicao && x.Combatente == null);
            if (vaga != null)
            {
                personagem.Painel = new RepresentacaoTelaCombate(personagem.Nome, personagem.Vida, personagem.intervaloAtaques);
                vaga.Painel.Children.Clear();
                vaga.Painel.Children.Add(personagem.Painel.StackPanel);
                listaAtivos.Add(personagem);
                vaga.Combatente = personagem;
            }
        }

        void InfoCombatente()
        {
            PainelPersonagens.Children.Clear();
            foreach (var combatente in Aliados)
            {
                StackPanel painel = new StackPanel()
                {
                    Width = 200,
                    Background = Brushes.AliceBlue,
                    HorizontalAlignment = HorizontalAlignment.Left,
                };

                StackPanel comboboxPainel = new StackPanel() { Orientation = Orientation.Horizontal };
                Label lblAtacar = new Label() { Content = $"Atacar:" };
                ComboBox foco = new ComboBox() { Width = 120 };

                var inimigos = _listPosicoesInimigos.FindAll(x => x.Combatente != null).ToList();
                foreach (var inimigo in inimigos)
                {
                    foco.Items.Add(inimigo.Combatente.Nome);
                }

                Button btnConfirmaFoco = new Button() { Content = "Ok", Width = 20, Margin = new Thickness(3) };
                btnConfirmaFoco.Click += (s, e) =>
                {
                    if (string.IsNullOrEmpty(foco.Text.ToString()))
                    {
                        return;
                    }
                    combatente.Painel.Foco = foco.Text.ToString();
                    foco.IsEnabled = false;
                    btnConfirmaFoco.IsEnabled = false;
                    FaseDoisCheck--;

                    if (FaseDoisCheck == 0)
                    {
                        IniciaTerceiraFase(s, e);
                    }
                };

                comboboxPainel.Children.Add(lblAtacar);
                comboboxPainel.Children.Add(foco);
                comboboxPainel.Children.Add(btnConfirmaFoco);
                painel.Children.Add(comboboxPainel);

                foreach (var dados in combatente.ToExpando())
                {
                    Label lbl = new Label()
                    {
                        Content = $"{dados.Key}: {dados.Value}"
                    };
                    painel.Children.Add(lbl);
                }

                ScrollViewer sclView = new ScrollViewer()
                {
                    Margin = new Thickness(3),
                    Content = painel
                };

                PainelPersonagens.Children.Add(sclView);
            }
        }
    }
}
