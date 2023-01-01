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
    public partial class Combate : Page
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
        int numeradorRelogio = 0;

        public Combate()
        {
            InitializeComponent();
            iniciaListas();
            iniciaCampos();
        }

        void iniciaListas()
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

            List<Combatente> combatentes = new List<Combatente>();

            combatentes.Add(new Combatente()
            {
                Nome = "Milton",
                Energia = 100,
                Vida = 100,
                VidaAtual = 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                intervaloAtaques = 15,
                Foco = "",
                Posicao = "I1"
            });
            combatentes.Add(new Combatente()
            {
                Nome = "Sara",
                Energia = 100,
                Vida = 100,
                VidaAtual = 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                intervaloAtaques = 10,
                Foco = "",
                Posicao = "I4"
            });
            combatentes.Add(new Combatente()
            {
                Nome = "Rebecca",
                Energia = 100,
                Vida = 100,
                VidaAtual = 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                intervaloAtaques = 5,
                Foco = "",
                Posicao = "I7"
            });
            combatentes.Add(new Combatente()
            {
                Nome = "Julia",
                Energia = 100,
                Vida = 100,
                VidaAtual = 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                intervaloAtaques = 15,
                Foco = "",
                Posicao = "I1"
            });
            combatentes.Add(new Combatente()
            {
                Nome = "Benedita",
                Energia = 100,
                Vida = 100,
                VidaAtual = 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                intervaloAtaques = 10,
                Foco = "",
                Posicao = "I4"
            });
            combatentes.Add(new Combatente()
            {
                Nome = "Marcos",
                Energia = 100,
                Vida = 100,
                VidaAtual= 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                intervaloAtaques = 5,
                Foco = "",
                Posicao = "I7"
            });

            _listPosicoesAliados = listPosicoesAliados;
            _listPosicoesInimigos = listPosicoesInimigos;
            _combatentes = combatentes;
        }

        void iniciaCampos()
        {
            PainelPersonagens.Children.Clear();
            iniciaInimigos();
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

        void iniciaInimigos()
        {
            foreach (var item in _listPosicoesInimigos)
            {
                Combatente combatente = _combatentes.Find(x => x.Posicao == item.Posicao);
                if (combatente != null)
                {
                    (item.Painel.Children[0] as Label).Content = combatente.Nome;
                    item.Combatente = combatente;
                    Inimigos.Add(combatente);

                    ProgressBar vidaCombatente = new ProgressBar();
                    vidaCombatente.Width = 100;
                    vidaCombatente.Maximum = combatente.Vida;
                    vidaCombatente.Value = combatente.Vida;

                    item.Painel.Children.Add(vidaCombatente);
                }
                else
                    item.Painel.Children.Clear();
            }
        }

        private void IniciaSegundaFase(object sender, RoutedEventArgs e)
        {
            PainelCabecalho.Children.RemoveAt(1);
            lblCabecalho.Foreground = Brushes.Black;
            lblCabecalho.Content = "Seleção de Foco";
            FaseCombate = 1;

            PainelPersonagens.Children.Clear();

            foreach (var item in _listPosicoesAliados)
            {
                Label label = item.Painel.Children[0] as Label;
                if (label.Content.ToString() == item.Posicao)
                {
                    item.Painel.Children.Clear();
                }
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

            Action<List<IdentificadorPosicaoCombatente>> EncherParticipantes = list =>
            {
                foreach (var item in list)
                {
                    if (item.Combatente != null)
                    {
                        item.Combatente.Posicao = item.Posicao;
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
                item.Foco = Aliados.FirstOrDefault(x => x.VidaAtual > 0).Nome;
            }

            PainelPersonagens.Children.Clear();
            PainelPersonagens.Orientation = Orientation.Vertical;
            iniciaTimer();
        }

        void iniciaTimer()
        {
            contadorRelogio.Tick += new EventHandler(CombateAtivo);
            contadorRelogio.Interval = new TimeSpan(0, 0, 0, 1, 0);
            contadorRelogio.Start();
        }

        public async void CombateAtivo(object sender, EventArgs e)
        {
            numeradorRelogio++;
            lblCabecalho.Content = numeradorRelogio;

            List<Combatente> combatatensParticipantesProntos = ValidaAtaquesProntos();
            causarDano(combatatensParticipantesProntos);
        }

        List<Combatente> ValidaAtaquesProntos()
        {
            List<Combatente> combatatensParticipantesProntos = new List<Combatente>();

            foreach (var combatente in CombatentesParticipantes)
            {
                if (combatente.AtaquePronto)
                {
                    combatatensParticipantesProntos.Add(combatente);
                }
            }

            return combatatensParticipantesProntos;
        }

        void causarDano(List<Combatente> combatatensParticipantesProntos)
        {
            foreach (var combatente in combatatensParticipantesProntos)
            {
                bool inimigo = true;
                combatente.AtaquePronto = false;
                Combatente alvo = CombatentesParticipantes.Find(x => x.Nome == combatente.Foco);

                if (combatente.VidaAtual <= 0 || alvo.VidaAtual <= 0)
                {
                    continue;
                }

                double dano = combatente.Forca;
                var posicaoProgresBar = _listPosicoesAliados.FirstOrDefault(x => x.Combatente != null && x.Combatente.Nome == alvo.Nome);
                if (posicaoProgresBar == null)
                {
                    posicaoProgresBar = _listPosicoesInimigos.FirstOrDefault(x => x.Combatente != null && x.Combatente.Nome == alvo.Nome);
                    inimigo= false;
                }

                ProgressBar barraVidaAlvo = posicaoProgresBar.Painel.Children[1] as ProgressBar;

                if (alvo.VidaAtual - dano <= 0)
                {
                    dano = alvo.VidaAtual - dano < 0 ? dano + (alvo.VidaAtual - dano) : dano;
                    alvo.VidaAtual = 0;
                    PainelPersonagens.Children.Add(new Label() { Foreground = Brushes.Purple, Content = $"[00:{numeradorRelogio}] = {alvo.Nome} MORREU! Ultimo dano Recebido de {combatente.Nome}: {dano}." });
                }
                else
                {
                    alvo.VidaAtual -= dano;
                    if (inimigo)
                        PainelPersonagens.Children.Add(new Label() { Foreground = Brushes.Red, Content = $"[00:{numeradorRelogio}] = {combatente.Nome} causou {dano} de dano em {alvo.Nome}" });
                    else
                        PainelPersonagens.Children.Add(new Label() { Foreground = Brushes.Blue, Content = $"[00:{numeradorRelogio}] = {combatente.Nome} causou {dano} de dano em {alvo.Nome}" });
                }

                barraVidaAlvo.Maximum = alvo.Vida;
                barraVidaAlvo.Value = alvo.VidaAtual;
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
                StackPanel local = _listPosicoesAliados.Find(x => x.Posicao == cmbPosicoes.Text).Painel;
                Label text = local.Children[0] as Label;
                if (text.Content.ToString() == cmbPosicoes.Text)
                {
                    local.Children.Clear();
                    Label posicaoTexto = new Label() { Content = combatente.Nome };

                    posicaoTexto.MouseLeftButtonDown += (s2, e2) =>
                    {
                        if (FaseCombate != 0)
                        {
                            return;
                        }
                        local.Children.Clear();
                        local.Children.Add(new Label() { Content = cmbPosicoes.Text });
                        PainelPersonagens.Children.Add(criarCardCombatente(combatente));
                        _listPosicoesAliados.Find(x => x.Posicao == cmbPosicoes.Text.ToString()).Combatente = null;
                        Aliados.Remove(combatente);
                    };

                    ProgressBar vidaCombatente = new ProgressBar();
                    vidaCombatente.Width = 100;
                    vidaCombatente.Maximum = combatente.Vida;
                    vidaCombatente.Value = combatente.Vida;

                    local.Children.Add(posicaoTexto);
                    local.Children.Add(vidaCombatente);
                    combatente.Posicao = cmbPosicoes.Text;
                    _listPosicoesAliados.Find(x => x.Posicao == cmbPosicoes.Text.ToString()).Combatente = combatente;
                    Aliados.Add(combatente);
                    PainelPersonagens.Children.Remove(painel);
                }
            };

            painel.Children.Add(lblNome);
            painel.Children.Add(lblVida);
            painel.Children.Add(comboboxPainel);
            painel.Children.Add(btnConfirmar);

            return painel;
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
                    combatente.Foco = foco.Text.ToString();
                    foco.IsEnabled = false;
                    btnConfirmaFoco.IsEnabled = false;
                    FaseDoisCheck--;
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
