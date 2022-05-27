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
    }
}
