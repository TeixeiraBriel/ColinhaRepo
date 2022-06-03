using Infraestrutura.Entidades;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game.Controles.TelaPadrao
{
    /// <summary>
    /// Lógica interna para janelaDadosHabilidade.xaml
    /// </summary>
    public partial class janelaDadosHabilidade : Window
    {
        JanelaPrincipal _janelaPrincipal;
        DispatcherTimer contadorRelogio = new DispatcherTimer();

        public janelaDadosHabilidade(JanelaPrincipal janelaPrincipal)
        {
            InitializeComponent();
            _janelaPrincipal = janelaPrincipal;
            timer();
        }

        public static janelaDadosHabilidade Instancia;
        public static void Fechar() => Instancia?.Close();
        public static void Esconder() => Instancia?.Hide();
        public static void Mostrar() => Instancia?.Show();
        public static void Focar() => Instancia?.Activate();
        public static void Inicializa(JanelaPrincipal janelaPrincipal)
        {
            Instancia = new janelaDadosHabilidade(janelaPrincipal)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowState = WindowState.Normal,
            };
        }
        public void DefineDados(Habilidade habilidadeEscolhida) 
        {
            string tipoCusto = habilidadeEscolhida.Tipo == "Magia" || habilidadeEscolhida.Tipo == "Buff" || habilidadeEscolhida.Tipo == "DeBuff" ? "Mana" : "Energia";
            CustoHab.Text = $"Custo: {habilidadeEscolhida.CustoBase} de {tipoCusto}";

            NomeHab.Text = $"{habilidadeEscolhida.Nome}";
            DanoHab.Text = $"Dano: {habilidadeEscolhida.DanoBase}";
            Tipohab.Text = $"{habilidadeEscolhida.Tipo}";
            DescricaoHab.Text = $"Descrição: {habilidadeEscolhida.Descricao}";
            
            switch (habilidadeEscolhida.Tipo)
            {
                case "Magia":
                case "Buff":
                case "DeBuff":
                    TipoDano.Foreground = new SolidColorBrush(Colors.Blue);
                    break;

                case "ArtesMarciais":
                case "Combate":
                case "Fortificar":
                    TipoDano.Text = $"+ ()";
                    TipoDano.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;
            }

            var imgArquivo = new ImageSourceConverter().ConvertFromString($"Dados\\Imagens\\{habilidadeEscolhida.Icon}") as ImageSource;
            IconHab.Source = imgArquivo;
        }

        void timer()
        {
            contadorRelogio.Tick += new EventHandler(AdjustPosition);
            contadorRelogio.Interval = new TimeSpan(0, 0, 0);
            contadorRelogio.Start();
        }

        public void AdjustPosition(object sender, EventArgs e)
        {
            if (Instancia != null)
            {
              /*Instancia.Left = _janelaPrincipal.Left + _janelaPrincipal.ActualWidth;
                Instancia.Top = _janelaPrincipal.Top;*/
                
                Point GetMousePos() => _janelaPrincipal.PointToScreen(Mouse.GetPosition(_janelaPrincipal));

                Instancia.Left = GetMousePos().X - 100;
                Instancia.Top = GetMousePos().Y - 250;
            }
        }
    }
}
