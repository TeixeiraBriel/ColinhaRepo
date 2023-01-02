using Game.Controladores;
using Game.Controles.MenuInformacoesJogador;
using Game.Controles.TelaPadrao;
using Game.Util;
using Infraestrutura.Entidades;
using Infraestrutura.Entidades.EntCombate;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Game.Controles.MenuInicial
{
    public partial class IndexMenuInicial : Page
    {
        List<Personagem> Personagens = new List<Personagem>();
        List<Inimigo> Inimigos = new List<Inimigo>();
        Progressao Save = new Progressao();
        Controlador _controlador;

        public IndexMenuInicial(Progressao save = null)
        {
            InitializeComponent();
            _controlador = new Controlador();

            CarregaComponentes();

            if (save != null && save.Jogador != null)
            {
                string textoResumoParte2 = $"XP:{save.Jogador.XpAtual}/{save.Jogador.XpMaximo} Moedas:{save.Moedas}";
                if (save.VidaAtual < 0)
                {
                    btnContinuar.IsEnabled = false;
                    btnAtributos.IsEnabled = false;
                    textoResumoParte2 += " Morto!";
                }
                else
                {
                    Save = save;
                    btnContinuar.IsEnabled = true;
                    btnAtributos.IsEnabled = true;
                }
                textResumoSaveParte1.Text = $"Jogos:{save.Lutas} Vitorias:{save.Vitorias} Derrota:{save.Derrotas}";
                textResumoSaveParte2.Text = textoResumoParte2;
            }
        }

        private void ComeçaJogo(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbPersonagens.Text) || string.IsNullOrEmpty(cmbInimigos.Text) || string.IsNullOrEmpty(cmbNivelInimigo.Text))
            {
                //Exibir msg que n da certo
            }
            else
            {
                Personagem perso = Personagens.Find(x => x.Nome == cmbPersonagens.Text);
                Inimigo inimi = Inimigos.Find(x => x.Nome == cmbInimigos.Text);

                inimi.DefineNivel(int.Parse(cmbNivelInimigo.Text));

                this.NavigationService.Navigate(new IndexTelaCombate(perso, inimi));
            }
        }

        private void NavegaInfoJogador(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new IndexMenuInformacoesJogador(Save));
        }

        public void CarregaComponentes()
        {
            _controlador.CarregaJsons();
            Personagens = _controlador.Classes;
            Inimigos = _controlador.Inimigos;

            foreach (var personagem in Personagens)
            {
                cmbPersonagens.Items.Add(personagem.Nome);
            }

            foreach (var inimigo in Inimigos)
            {
                cmbInimigos.Items.Add(inimigo.Nome);
            }

            for (int i = 0; i < 20; i++)
            {
                cmbNivelInimigo.Items.Add(i.ToString());
            }
        }

        private void ContinuarJogo(object sender, RoutedEventArgs e)
        {
            Inimigo inimi = Inimigos.Find(x => x.IdInimigo == 0);
            inimi.DefineNivel(Save.Nivel);

            this.NavigationService.Navigate(new IndexTelaCombate(Save.Jogador, inimi, Save));
        }

        private void NovoJogo(object sender, RoutedEventArgs e)
        {
            Save = new Progressao();
            Save.Jogador = Personagens.Find(x => x.Classe == "Mago");
            Save.Nivel = 1;
            Save.VidaAtual = Save.Jogador.Vida;
            Save.ManaAtual = Save.Jogador.Mana;
            Save.EnergiaAtual = Save.Jogador.Energia;

            Inimigo inimi = Inimigos.Find(x => x.IdInimigo == 0);
            inimi.DefineNivel(Save.Nivel);

            this.NavigationService.Navigate(new CriarNovoJogoView());
        }

        private void Teste(object sender, RoutedEventArgs e)
        {
            List<Combatente> combatentesAliados = new List<Combatente>();
            combatentesAliados.Add(new Combatente()
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
            combatentesAliados.Add(new Combatente()
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
            combatentesAliados.Add(new Combatente()
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

            List<Combatente> combatentesInimigos = new List<Combatente>();
            combatentesInimigos.Add(new Combatente()
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
            combatentesInimigos.Add(new Combatente()
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
            combatentesInimigos.Add(new Combatente()
            {
                Nome = "Marcos",
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

            this.NavigationService.Navigate(new Combate(combatentesAliados,combatentesInimigos));
        }
    }
}
