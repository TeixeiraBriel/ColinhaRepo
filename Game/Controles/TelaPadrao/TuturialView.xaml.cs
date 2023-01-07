using Game.Controles.MenuInformacoesJogador;
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

namespace Game.Controles.TelaPadrao
{
    /// <summary>
    /// Interação lógica para TuturialView.xam
    /// </summary>
    public partial class TuturialView : Page
    {
        public TuturialView()
        {
            InitializeComponent();
            TutorialFrame.Navigate(new IndexMapa());
        }

        private void navegaPasso(object sender, RoutedEventArgs e)
        {
            string nomeBtn = (sender as Button).Name;

            switch(nomeBtn) 
            {
                case "btnPasso2":
                    TutorialFrame.Navigate(new AbaBolsa());
                    PrimeiroPasso.Visibility= Visibility.Collapsed;
                    break;
            }
        }
    }
}
