using Game.Controladores;
using Game.Controles.AssentamentoViews;
using Game.Controles.MenuInformacoesJogador;
using Game.Controles.TelaPadrao;
using Infraestrutura.Entidades;
using Infraestrutura.Entidades.EntCombate;
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

namespace Game.Controles.TelaMapa
{
    /// <summary>
    /// Interação lógica para Assentamento.xam
    /// </summary>
    public partial class AssentamentoView : Page
    {
        Assentamento _DadosAssentamento;
        SaveGame _saveGame;
        public AssentamentoView(Assentamento DadosAssentamento)
        {
            InitializeComponent();
            _DadosAssentamento = DadosAssentamento;
            _saveGame = Controlador.buscarSave();

            AtualizaAssentamento();
        }

        private void VoltarMapa(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void NavegaLocal(object sender, RoutedEventArgs e)
        {
            string nomeBtn = (sender as Button).Content.ToString();

            switch (nomeBtn)
            {
                //CIDADE
                case "Treinamento":
                    Equipe possiveisInimigos = Controlador.buscarCombatentesGenericos();
                    int numAleatorio = new Random().Next(0, (possiveisInimigos.QuantidadeMembros - 1));
                    Combatente inimigo = possiveisInimigos.combatentes[numAleatorio];
                    this.NavigationService.Navigate(new CombateIndividual(_saveGame.PersonagemAtivo, inimigo));
                    break;
                case "Treinamento em Equipe":
                    this.NavigationService.Navigate(new CombateEquipe(Controlador.buscarEquipe().combatentes, Controlador.buscarCombatentesGenericos().combatentes));
                    break;
                case "Hospital":
                    var personagem = _saveGame.PersonagemAtivo;
                    personagem.InicializaCombatente(personagem.Nivel);
                    break;
                case "Loja":
                    MainAssentoViewFrame.NavigationService.Navigate(new AbaBolsa());
                    break;
                case "Guilda":
                    MainAssentoViewFrame.NavigationService.Navigate(new AbaBolsa());
                    break;
                case "Prisão":
                    var save = Controlador.buscarSave();
                    var nome = save.Equipe.combatentes.Last().Nome;
                    nome = nome.Contains(":1") ? nome.Split(':')[0] + (int.Parse(nome.Split(':')[1]) + 1) : "Prisioneiro:1";
                    save.Equipe.combatentes.Add(new Combatente() { Nome = nome, Nivel = 1, XpMaximo = 10, intervaloAtaques = 3 });
                    Controlador.salvarAvanço(save);
                    MainAssentoViewFrame.NavigationService.Navigate(new AbaBolsa());
                    break;
                //Assentamento
                case "Investigar":
                    MainAssentoViewFrame.NavigationService.Navigate(new InvestigarAssentamentoView(_DadosAssentamento));
                    break;
                case "Sua Equipe":
                    this.NavigationService.Navigate(new EquipeView(Controlador.buscarEquipe()));
                    break;
            }
        }

        private void AtualizaAssentamento()
        {
            if (_DadosAssentamento.Tipo == "Desconhecido")
            {
                AssentamentoCidade.Visibility = Visibility.Collapsed;
                AssentamentoDesconhecido.Visibility = Visibility.Visible;
                AssentamentoConhecido.Visibility = Visibility.Collapsed;
            }
            else if (_DadosAssentamento.Tipo == "Cidade")
            {
                AssentamentoCidade.Visibility = Visibility.Visible;
                AssentamentoDesconhecido.Visibility = Visibility.Collapsed;
                AssentamentoConhecido.Visibility = Visibility.Collapsed;
            }
            else
            {
                AssentamentoCidade.Visibility = Visibility.Collapsed;
                AssentamentoDesconhecido.Visibility = Visibility.Collapsed;
                AssentamentoConhecido.Visibility = Visibility.Visible;
            }
        }
    }
}
