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
            FrameAbaInformacoesJogador.Navigate(new AbaStatus(this));
            teste();
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

        private void teste()
        {
            var txtColar = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\Itens\\Necklace_02.png") as ImageSource;
            var txtEspada = new ImageSourceConverter().ConvertFromString("Dados\\Imagens\\Itens\\metal sword.png") as ImageSource;

            Image ImgColar = new Image() { Source = txtColar, Margin = new Thickness(0, 5, 0, 5), Height = 80 };
            Image imgEspada = new Image() { Source = txtEspada, Margin = new Thickness(0, 5, 0, 5), Height = 80 };

            SlotColar.Children.Add(ImgColar);
            SlotArmaEsquerda.Children.Add(imgEspada);
        }
    }
}
