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
            string nomePersonagem = !string.IsNullOrWhiteSpace(inputNomePersonagem.Text) ? inputNomePersonagem.Text : null;
            string nomeInimigo = !string.IsNullOrWhiteSpace(inputNomeInimgo.Text) ? inputNomeInimgo.Text : null;
            string VidaPersonagem = !string.IsNullOrWhiteSpace(inputVidaPersonagem.Text) ? inputVidaPersonagem.Text : null;
            string VidaInimigo = !string.IsNullOrWhiteSpace(inputVidaInimgo.Text) ? inputVidaInimgo.Text : null;

            if (nomePersonagem == null || nomeInimigo == null || VidaPersonagem == null || VidaInimigo == null)
            {
                this.NavigationService.Navigate(new IndexTelaPadrao());
            }
            else
            {
                this.NavigationService.Navigate(new IndexTelaPadrao(nomePersonagem, nomeInimigo, VidaPersonagem, VidaInimigo));
            }
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
