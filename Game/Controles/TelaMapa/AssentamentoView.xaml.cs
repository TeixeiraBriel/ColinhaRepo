using Game.Controladores;
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
        public AssentamentoView(Assentamento DadosAssentamento)
        {
            InitializeComponent();

            Main.Children.Add(new Label() { Content = DadosAssentamento.Nome});
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
                case "Treinamento":
                    this.NavigationService.Navigate(new CombateIndividual(new Combatente(), new Combatente()));
                    break;
                case "Treinamento em Equipe":
                    this.NavigationService.Navigate(new CombateEquipe(new List<Combatente>(), new List<Combatente>()));
                    break;
                case "Hospital":
                    MainAssentoViewFrame.NavigationService.Navigate(new AbaBolsa());
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
                    save.Equipe.combatentes.Add(new Combatente(){Nome = nome });
                    Controlador.salvarAvanço(save);
                    MainAssentoViewFrame.NavigationService.Navigate(new AbaBolsa());
                    break;
                case "Sua Equipe":
                    this.NavigationService.Navigate(new EquipeView(Controlador.buscarEquipe()));
                    break;
            }
        }
    }
}
