using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Infraestrutura.Entidades.EntCombate
{
    public class RepresentacaoTelaCombate
    {
        public StackPanel StackPanel { get; set; }
        public Label LabelNome { get; set; }
        public ProgressBar ProgressBarVida { get; set; }
        public ProgressBar ProgressBarIntervaloAtaque { get; set; }
        public string Foco { get; set; }
        public Combatente alvo { get; set; }
        public string Posicao { get; set; }
        public string Dialogo { get; set; }

        public RepresentacaoTelaCombate(string nome, double valorVidaTotal, double valorTotalTempo)
        {
            StackPanel = new StackPanel();
            LabelNome = new Label() { Content = nome };

            ProgressBarVida = new ProgressBar() { Margin = new System.Windows.Thickness(0,3,0,3)};
            ProgressBarVida.Width = 100;
            ProgressBarVida.Maximum = valorVidaTotal;
            ProgressBarVida.Value = valorVidaTotal;

            ProgressBarIntervaloAtaque = new ProgressBar() { Foreground = Brushes.Brown};
            ProgressBarIntervaloAtaque.Width = 100;
            ProgressBarIntervaloAtaque.Maximum = valorTotalTempo;
            ProgressBarIntervaloAtaque.Value = 0;

            StackPanel.Children.Add(LabelNome);
            StackPanel.Children.Add(ProgressBarVida);
            StackPanel.Children.Add(ProgressBarIntervaloAtaque);
        }
    }
}
