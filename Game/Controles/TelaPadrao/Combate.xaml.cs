using Infraestrutura.Entidades;
using Infraestrutura.Entidades.EntCombate;
using System;
using System.Collections.Generic;
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
        //IniciaListas
        List<IdentificadorPosicaoCombatente> _listPosicoesAliados;
        List<IdentificadorPosicaoCombatente> _listPosicoesInimigos;
        List<Combatente> _combatentes;
        //IniciaSegundaFase
        List<Combatente> Aliados = new List<Combatente>();
        List<Combatente> Inimigos = new List<Combatente>();

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

            combatentes.Add(new Combatente(){
                Nome = "Milton",
                Energia = 100,
                Vida = 100,
                Agilidade= 100,
                Defesa= 100,
                Forca= 10,
                Inteligencia= 10,
                Mana= 100,
                velAtaque= 10,
                Foco = "",                
                Posicao = "I1"
            });
            combatentes.Add(new Combatente()
            {
                Nome = "Sara",
                Energia = 100,
                Vida = 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                velAtaque = 10,
                Foco = "",
                Posicao = "I4"
            });
            combatentes.Add(new Combatente()
            {
                Nome = "Rebecca",
                Energia = 100,
                Vida = 100,
                Agilidade = 100,
                Defesa = 100,
                Forca = 10,
                Inteligencia = 10,
                Mana = 100,
                velAtaque = 10,
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
            foreach (var item in _combatentes)
            {
                PainelPersonagens.Children.Add(criarCardCombatente(item));
            }

            lblCabecalho.Content = "FASE DE PREPARO";

            iniciaInimigos();
        }

        void iniciaInimigos()
        {
            foreach (var item in _listPosicoesInimigos)
            {
                Combatente combatente = _combatentes.Find(x => x.Posicao == item.Posicao);
                if (combatente != null)
                    (item.Painel.Children[0] as Label).Content = combatente.Nome;
                else
                    item.Painel.Children.Clear();
            }
        }

        private void IniciaSegundaFase(object sender, RoutedEventArgs e)
        {
            FaseCombate = 1;
            BtnCabecalho.Visibility = Visibility.Collapsed;
            PainelPersonagens.Children.Clear();

            foreach (var item in _listPosicoesAliados)
            {
                Label label = item.Painel.Children[0] as Label;
                if (label.Content.ToString() == item.Posicao)
                {
                    item.Painel.Children.Clear();
                }
            }

            Func<List<Combatente>, double> SomarVidas = list => {
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

            InfoCombatente();
            iniciaTimer();
        }

        void iniciaTimer()
        {
            contadorRelogio.Tick += new EventHandler(Relogio);
            contadorRelogio.Interval = new TimeSpan(0, 0, 1);
            contadorRelogio.Start();
        }

        public void Relogio(object sender, EventArgs e)
        {
            contadorRelogio.Stop();

            numeradorRelogio++;
            lblCabecalho.Content = numeradorRelogio;

            contadorRelogio.Start();
        }

        private StackPanel criarCardCombatente(Combatente combatente)
        {
            StackPanel painel = new StackPanel()
            {
                Background = Brushes.AliceBlue,
                Width = 200,
                Height = 110,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin= new Thickness(3)
            };

            Label lblNome = new Label()
            {
                Content = $"Nome: {combatente.Nome}"
            };

            Label lblVida = new Label()
            {
                Content = $"Vida: {combatente.Vida}"
            };

            StackPanel comboboxPainel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
            };

            Label lblPosicao = new Label()
            {
                Content = $"Posicao:"
            };

            ComboBox cmbPosicoes = new ComboBox();
            foreach (var item in _listPosicoesAliados)
            {
                cmbPosicoes.Items.Add(item.Posicao);
            }

            comboboxPainel.Children.Add(lblPosicao);
            comboboxPainel.Children.Add(cmbPosicoes);

            Button btnConfirmar = new Button(){
                Content = "Confirmar"
            };

            btnConfirmar.Click += (s, e) => {
                StackPanel local = _listPosicoesAliados.Find(x => x.Posicao == cmbPosicoes.Text).Painel;
                Label text = local.Children[0] as Label;
                if (text.Content.ToString() == cmbPosicoes.Text)
                {
                    local.Children.Clear();
                    Label posicaoTexto = new Label() { Content = combatente.Nome };
                    posicaoTexto.MouseLeftButtonDown += (s2, e2) => {
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
                    local.Children.Add(posicaoTexto);
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
