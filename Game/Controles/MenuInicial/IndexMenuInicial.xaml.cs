using Game.Controladores;
using Game.Controles.MenuInformacoesJogador;
using Game.Controles.TelaMapa;
using Game.Controles.TelaPadrao;
using Game.Util;
using Infraestrutura.Entidades;
using Infraestrutura.Entidades.EntCombate;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Game.Controles.MenuInicial
{
    public partial class IndexMenuInicial : Page
    {
        SaveGame Save = new SaveGame();

        public IndexMenuInicial(Progressao save = null)
        {
            InitializeComponent();
            SaveGame _save = Controlador.buscarSave();

            if (_save!= null )
            {
                Save= _save;
                btnContinuar.IsEnabled = true;
            }
        }

        private void NovoJogo(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CriarNovoJogoView());
        }

        private void ContinuarJogo(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new IndexMapa(Save));
        }

        private void CriaCombatente(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CombatenteGenericosAddView());
        }
    }
}
