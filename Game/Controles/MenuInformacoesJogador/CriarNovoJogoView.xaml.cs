using Game.Controladores;
using Game.Controles.TelaPadrao;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game.Controles.MenuInformacoesJogador
{
    /// <summary>
    /// Interação lógica para CriarNovoJogoView.xam
    /// </summary>
    public partial class CriarNovoJogoView : Page
    {
        Controlador _controlador;
        Progressao _newSave;

        public CriarNovoJogoView()
        {
            InitializeComponent();
            _controlador = new Controlador();
            _newSave = new Progressao();

            _controlador.CarregaJsons();
            _newSave.IniciaDadosBase();

            CriarPerguntas();
        }

        void CriarPerguntas()
        {
            var Classes = _controlador.Classes;
            StackPanel PanelClasses = new StackPanel { Orientation = Orientation.Horizontal };
            ComboBox cmbClasses = new ComboBox() { Margin = new Thickness(5,2,5,2) , Width= 100, HorizontalAlignment = HorizontalAlignment.Left};
            TextBlock txtClasses = new TextBlock() { Text = "Selecione sua Classe:", Margin = new Thickness(2,2,2,2), VerticalAlignment = VerticalAlignment.Center};
            cmbClasses.Margin = new Thickness(5, 5, 5, 5);
            cmbClasses.SelectionChanged += (s, e) => AtualizarDados(s, e);

            foreach (var Classe in Classes)
            {
                cmbClasses.Items.Add(Classe.Nome);
            }

            PanelClasses.Children.Add(txtClasses);
            PanelClasses.Children.Add(cmbClasses);

            PainelPerguntas.Children.Add(PanelClasses);
        }

        void AtualizarDados(object s, EventArgs e)
        {
            SubPainelStatus.Children.Clear();
            var sender = s as ComboBox;
            _newSave.Jogador = _controlador.Personagens.Find(x => x.Classe == sender.SelectedItem.ToString());

            var DadosJogador = XpandoLibrary.Xpando.ToExpando(_newSave.Jogador);
            foreach (var item in DadosJogador)
            {
                if (item.Key == "IdPersonagem" || 
                    item.Key == "Foto" || 
                    item.Key == "HabilidadesPermitidas" || 
                    item.Key == "Nome" || 
                    item.Key.Contains("Xp"))
                    continue;

                TextBlock info = new TextBlock() { Text = $"{item.Key}: {item.Value}", Margin = new Thickness(5, 5, 5, 5) };
                SubPainelStatus.Children.Add(info);
            }

            CarregaHablidadesPersonagem();
            janelaDadosHabilidade.Instancia._save = _newSave;
        }

        public void CarregaHablidadesPersonagem()
        {
            var _personagem = _newSave.Jogador;
            var _Habilidades = _controlador.Habilidades;
            SubPainelHabilidades.Children.Clear();

            foreach (var idHabilidade in _personagem.HabilidadesPermitidas)
            {
                Habilidade habilidadeEscolhida = _Habilidades.Find(x => x.IdHabilidade == idHabilidade);

                string caminhaoImg = $"Dados\\Imagens\\{habilidadeEscolhida.Icon}";
                var imgArquivo = new ImageSourceConverter().ConvertFromString(caminhaoImg) as ImageSource;

                Image imgAdd = new Image() { Source = imgArquivo, Margin = new Thickness(3, 2, 3, 2), Height = 40 , HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top};
                imgAdd.MouseEnter += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Hand; mouseOver(true, habilidadeEscolhida); };
                imgAdd.MouseLeave += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Arrow; mouseOver(false, habilidadeEscolhida); };

                SubPainelHabilidades.Children.Add(imgAdd);
            }
        }
        void mouseOver(bool over, Habilidade habilidadeEscolhida)
        {
            if (over)
            {
                janelaDadosHabilidade.Instancia.DefineDados(habilidadeEscolhida);
                janelaDadosHabilidade.Focar();
                janelaDadosHabilidade.Mostrar();
            }
            else
            {
                janelaDadosHabilidade.Esconder();
            }
        }

        private void IniciarJogo(object sender, RoutedEventArgs e)
        {
            if (_newSave.Jogador.Classe != null)
            {
                this.NavigationService.Navigate(new IndexTelaPadrao(_newSave.Jogador, _controlador.Inimigos[0], _newSave));
            }
        }
    }
}
