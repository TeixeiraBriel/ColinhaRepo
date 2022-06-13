using Game.Controladores;
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
        Controlador _controlador;
        public Progressao _save;

        JanelaPrincipal _janelaPrincipal;
        DispatcherTimer contadorRelogio = new DispatcherTimer();

        public janelaDadosHabilidade(JanelaPrincipal janelaPrincipal, Progressao save)
        {
            InitializeComponent();
            _janelaPrincipal = janelaPrincipal;
            timer();
            _controlador = new Controlador();
            _controlador.CarregaJsons();
            _save = save;
        }

        public static janelaDadosHabilidade Instancia;
        public static void Fechar() => Instancia?.Close();
        public static void Esconder() => Instancia?.Hide();
        public static void Mostrar() => Instancia?.Show();
        public static void Focar() => Instancia?.Activate();
        public static void Inicializa(JanelaPrincipal janelaPrincipal, Progressao save = null)
        {
            Instancia = new janelaDadosHabilidade(janelaPrincipal, save)
            {
                WindowStartupLocation = WindowStartupLocation.Manual,
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
                case "Buff":
                    DanoHab.Text = $"Cura: {habilidadeEscolhida.DanoBase}";
                    TipoDano.Text = $"+ ({_controlador.Personagens.Find(x => x.Nome == _save.Jogador.Classe).Inteligencia})";
                    TipoDano.Foreground = new SolidColorBrush(Colors.Blue);
                    break;
                case "Magia":
                case "DeBuff":
                    TipoDano.Text = $"+ ({_controlador.Personagens.Find(x => x.Nome == _save.Jogador.Classe).Inteligencia})";
                    TipoDano.Foreground = new SolidColorBrush(Colors.Blue);
                    break;

                case "ArtesMarciais":
                case "Combate":
                    TipoDano.Text = $"+ ({_controlador.Personagens.Find(x => x.Nome == _save.Jogador.Classe).Forca})";
                    TipoDano.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;
                case "Fortificar":
                    DanoHab.Text = $"Aumenta Defesa em: {habilidadeEscolhida.DanoBase}";
                    TipoDano.Text = $"+ ({_controlador.Personagens.Find(x => x.Nome == _save.Jogador.Classe).Forca})";
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
                Point GetMousePos() => _janelaPrincipal.PointToScreen(Mouse.GetPosition(_janelaPrincipal));

                Instancia.Left = GetMousePos().X - 100;
                Instancia.Top = GetMousePos().Y - 250;
            }
        }
    }
}
