﻿using Game.Controles.MenuInicial;
using Game.Controles.TelaPadrao;
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

namespace Game
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class JanelaPrincipal : Window
    {
        public JanelaPrincipal()
        {
            InitializeComponent();
            txtVersao.Content = $"v{System.Windows.Forms.Application.ProductVersion}";
            FrameJanelaPrincipal.Navigate(new IndexMenuInicial());

            Util.DispatcherUtil.Dispatcher(() =>
            {
                janelaDadosHabilidade.Inicializa(this);
                janelaDadosHabilidade.Esconder();
            });
        }
    }
}
