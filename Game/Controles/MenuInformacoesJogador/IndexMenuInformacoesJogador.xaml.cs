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

namespace Game.Controles.MenuInformacoesJogador
{
    /// <summary>
    /// Interação lógica para IndexMenuInformacoesJogador.xam
    /// </summary>
    public partial class IndexMenuInformacoesJogador : Page
    {
        public IndexMenuInformacoesJogador(string nome, string life, string mana)
        {
            InitializeComponent();
        }

        private void MudaAbaBolsa(object sender, RoutedEventArgs e)
        {
            FrameAbaInformacoesJogador.Navigate(new AbaBolsa(this));
        }

        private void MudaAbaStatus(object sender, RoutedEventArgs e)
        {
            FrameAbaInformacoesJogador.Navigate(new AbaStatus(this));
        }

        private void AtacarInimigo(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
