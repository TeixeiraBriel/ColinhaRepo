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
using XpandoLibrary;

namespace Game.Controles.TelaPadrao
{
    /// <summary>
    /// Interação lógica para EquipeView.xam
    /// </summary>
    public partial class EquipeView : Page
    {
        Equipe _equipe;
        public EquipeView(Equipe equipe)
        {
            InitializeComponent();
            _equipe = equipe;

            listaCombatentes();
        }

        private void listaCombatentes()
        {
            StackPanel painel = new StackPanel();
            foreach (var Combatente in _equipe.combatentes)
            {
                Button btnCombatente = new Button();
                btnCombatente.Content = Combatente.Nome;
                btnCombatente.Width = 150;
                btnCombatente.Height = 30;
                btnCombatente.VerticalAlignment = VerticalAlignment.Top;
                btnCombatente.Margin = new Thickness(10); 

                btnCombatente.Click += (s, e) =>
                {
                    Main.Children.Clear();
                    painel.Children.Clear();
                    foreach (var item in Combatente.ToExpando())
                    {
                        painel.Children.Add(new Label() { Content = $"{item.Key}:{item.Value}" });
                    }
                    Main.Children.Add(painel);
                };

                PainelBotoes.Children.Add(btnCombatente);
            }
        }

        private void VoltarMapa(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
