using Game.Controles.TelaMapa;
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
    /// Interação lógica para InformacaoPersonagemView.xam
    /// </summary>
    public partial class InformacaoPersonagemView : Page
    {
        public InformacaoPersonagemView()
        {
            InitializeComponent();
        }

        private void MudaAbaBolsa(object sender, RoutedEventArgs e)
        {
            //FrameAbaInformacoesJogador.Navigate(new AbaBolsa(this));
        }

        private void NavegaInfoPersonagemFrame(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            string nomeBtn = btn.Name;
            switch (nomeBtn)
            {
                case "btnStatus":
                    infoPersonagemFrame.Navigate(new InfoStatusPersonagem());
                    break;
                case "btnInventario":
                    infoPersonagemFrame.Navigate(new AbaBolsa());
                    break;
                case "btnHabilidades":
                    infoPersonagemFrame.Navigate(new IndexMapa());
                    break;
            }
        }
    }
}
