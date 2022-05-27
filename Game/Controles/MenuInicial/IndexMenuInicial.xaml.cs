using Game.Controles.MenuInformacoesJogador;
using Game.Controles.TelaPadrao;
using Game.Util;
using System.Windows;
using System.Windows.Controls;

namespace Game.Controles.MenuInicial
{
    public partial class IndexMenuInicial : Page
    {
        public IndexMenuInicial()
        {
            InitializeComponent();
        }

        private void ComeçaJogo(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new IndexTelaPadrao());
        }

        private void NavegaInfoJogador(object sender, RoutedEventArgs e)
        {
            string nome = inputName.Text;
            string life = inputLife.Text;
            string mana = inputMana.Text;
            this.NavigationService.Navigate(new IndexMenuInformacoesJogador(nome,life,mana));
        }
    }
}
