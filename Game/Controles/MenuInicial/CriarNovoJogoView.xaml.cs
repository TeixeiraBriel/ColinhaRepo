using Game.Controladores;
using Game.Controles.MenuInformacoesJogador;
using Game.Controles.TelaMapa;
using Game.Controles.TelaPadrao;
using Infraestrutura.Entidades;
using Infraestrutura.Entidades.EntCombate;
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

namespace Game.Controles.MenuInicial
{
    /// <summary>
    /// Interação lógica para CriarNovoJogoView.xam
    /// </summary>
    public partial class CriarNovoJogoView : Page
    {
        SaveGame _newSaveGame;
        Controlador _controlador;
        Progressao _newSave;

        public CriarNovoJogoView()
        {
            InitializeComponent();
            PrimeiraEtapa.Visibility = Visibility.Visible;
            _newSaveGame = new SaveGame();
            //Controlador.salvarAvanço(_newSaveGame);


            /*
            _controlador = new Controlador();
            _newSave = new Progressao();
            _newSaveGame = new SaveGame();

            _controlador.CarregaJsons();
            _newSave.IniciaDadosBase();

            CriarPerguntas();*/
        }

        #region oldCriarJogo
        void CriarPerguntas()
        {
            var Classes = _controlador.Classes;
            StackPanel PanelClasses = new StackPanel { Orientation = Orientation.Horizontal };
            ComboBox cmbClasses = new ComboBox() { Margin = new Thickness(5, 2, 5, 2), Width = 100, HorizontalAlignment = HorizontalAlignment.Left };
            TextBlock txtClasses = new TextBlock() { Text = "Selecione sua Classe:", Margin = new Thickness(2, 2, 2, 2), VerticalAlignment = VerticalAlignment.Center };
            cmbClasses.Margin = new Thickness(5, 5, 5, 5);
            cmbClasses.SelectionChanged += (s, e) => AtualizarDados(s, e);

            foreach (var Classe in Classes)
            {
                cmbClasses.Items.Add(Classe.Nome);
            }

            PanelClasses.Children.Add(txtClasses);
            PanelClasses.Children.Add(cmbClasses);

            //PainelPerguntas.Children.Add(PanelClasses);
        }

        void AtualizarDados(object s, EventArgs e)
        {
            //SubPainelStatus.Children.Clear();
            var sender = s as ComboBox;
            _newSave.Jogador = _controlador.Classes.Find(x => x.Classe == sender.SelectedItem.ToString());

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
                //SubPainelStatus.Children.Add(info);
            }

            CarregaHablidadesPersonagem();
            janelaDadosHabilidade.Instancia._save = _newSave;
        }

        public void CarregaHablidadesPersonagem()
        {
            var _personagem = _newSave.Jogador;
            var _Habilidades = _controlador.Habilidades;
            //SubPainelHabilidades.Children.Clear();

            foreach (var idHabilidade in _personagem.HabilidadesPermitidas)
            {
                Habilidade habilidadeEscolhida = _Habilidades.Find(x => x.IdHabilidade == idHabilidade);

                string caminhaoImg = $"Dados\\Imagens\\{habilidadeEscolhida.Icon}";
                var imgArquivo = new ImageSourceConverter().ConvertFromString(caminhaoImg) as ImageSource;

                Image imgAdd = new Image() { Source = imgArquivo, Margin = new Thickness(3, 2, 3, 2), Height = 40, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                imgAdd.MouseEnter += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Hand; mouseOver(true, habilidadeEscolhida); };
                imgAdd.MouseLeave += (s, e) => { var _sender = s as Image; _sender.Cursor = Cursors.Arrow; mouseOver(false, habilidadeEscolhida); };

                //SubPainelHabilidades.Children.Add(imgAdd);
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
                var inimigo = _controlador.Inimigos[0];
                inimigo.DefineNivel(1);
                this.NavigationService.Navigate(new IndexTelaCombate(_newSave.Jogador, inimigo, _newSave));
            }
        }
        #endregion

        private void funcProximoPasso(object sender, RoutedEventArgs e)
        {
            string nomeBtn = (sender as Button).Name;

            switch (nomeBtn)
            {
                case "btnPasso2":
                    finalizaPrimeiroPasso();
                    break;
                case "btnPasso3":
                    finalizaSegundoPasso();
                    break;
            }
        }

        private void finalizaPrimeiroPasso()
        {
            PrimeiraEtapa.Visibility = Visibility.Collapsed;
            _newSaveGame.NomeLider = inputNomeLider.Text;

            cmbBoxRaca.SelectedIndex = 0;
            SegundaEtapa.Visibility = Visibility.Visible;

            this.NavigationService.Navigate(new IndexMapa());
        }
        private void finalizaSegundoPasso()
        {
            SegundaEtapa.Visibility = Visibility.Collapsed;

            Controlador.salvarAvanço(_newSaveGame);
            TerceiraEtapa.Visibility = Visibility.Visible;
        }

        private void AlternarClasseFunc(object sender, SelectionChangedEventArgs e)
        {
            string raca = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
            if (string.IsNullOrEmpty(raca))
                return;

            switch (raca)
            {
                case "Humano":
                    txtEscolhaRaça.Text = "Legal, você é um humano, os humanos são neutros em seus aspectos, não são ruins em nada, porém não se destacam tambem.";
                    _newSaveGame.PersonagemAtivo = _newSaveGame.CriaPersonagemCombatente(_newSaveGame.NomeLider, 3, 3, 3, 3, 3);
                    modificaTabelaStatus(3, 3, 3, 3, 3);
                    break;
                case "Elfo":
                    txtEscolhaRaça.Text = "Legal, você é um Elfo, os elfos são muito inteligentes e ageis, porém não tem tanta força e vida.";
                    _newSaveGame.PersonagemAtivo = _newSaveGame.CriaPersonagemCombatente(_newSaveGame.NomeLider, 2, 2, 4, 4, 3);
                    modificaTabelaStatus(2, 2, 4, 4, 3);
                    break;
                case "Orc":
                    txtEscolhaRaça.Text = "Legal, você é um Orc, os Orcs são grandes e fortes, sua vida e força sobressai em muito aos outros, porem sua inteligencia, agilidade e carisma deixam a desejar.";
                    _newSaveGame.PersonagemAtivo = _newSaveGame.CriaPersonagemCombatente(_newSaveGame.NomeLider, 5, 4, 2, 2, 2);
                    modificaTabelaStatus(5, 4, 2, 2, 2);
                    break;
                case "Anão":
                    txtEscolhaRaça.Text = "Legal, você é um Anão, os Anõre são Fortes, não tanto quanto um Orc, porém são bons com a vida e força, sendo inteligentes como humanos, mas pouco ageis e sociaveis.";
                    _newSaveGame.PersonagemAtivo = _newSaveGame.CriaPersonagemCombatente(_newSaveGame.NomeLider, 4, 4, 3, 2, 2);
                    modificaTabelaStatus(4, 4, 3, 2, 2);
                    break;
                case "Gnomo":
                    txtEscolhaRaça.Text = "Legal, você é um Gnomo, os Gnomos, pequenos e frageis, porém muito ageis e carismaticos, se eles não ganham na conversa, com certeza não serão pegos.";
                    _newSaveGame.PersonagemAtivo = _newSaveGame.CriaPersonagemCombatente(_newSaveGame.NomeLider, 2, 1, 3, 4, 5);
                    modificaTabelaStatus(2, 1, 3, 4, 5);
                    break;
            }
        }

        private void modificaTabelaStatus(double vigor, double forca, double Inteligencia, double agilidade, double carisma)
        {
            txtValorForca.Text = forca.ToString();
            txtValorVigor.Text = vigor.ToString();
            txtValorInteligencia.Text = Inteligencia.ToString();
            txtValorAgilidade.Text = agilidade.ToString();
            txtValorCarisma.Text = carisma.ToString();
        }
    }
}
