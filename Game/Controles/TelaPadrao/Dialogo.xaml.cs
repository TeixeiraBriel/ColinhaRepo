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
    /// Interação lógica para Dialogo.xam
    /// </summary>
    public partial class Dialogo : Page
    {
        List<string> _dialogs;
        bool alternador = true;
        object _telasaida = null;

        public Dialogo(List<string> dialogs, object TelaSaida)
        {
            InitializeComponent();
            _dialogs = dialogs;
            _telasaida = TelaSaida;
            Esquerda.Content = _dialogs[0];
        }

        private void ProximoDialogo(object sender, RoutedEventArgs e)
        {
            _dialogs.RemoveAt(0);
            if (_dialogs.Count > 0)
            {
                if (alternador)
                {
                    Direita.Content = _dialogs[0];
                }
                else
                {
                    Esquerda.Content = _dialogs[0];
                }
                alternador = !alternador;
            }
            else
            {
                this.NavigationService.Navigate(_telasaida);
            }
        }
    }
}
