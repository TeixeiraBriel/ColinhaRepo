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
        }
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
        }
        private void finalizaSegundoPasso()
        {
            SegundaEtapa.Visibility = Visibility.Collapsed;

            _newSaveGame.Equipe = new Equipe() { combatentes = new List<Combatente>() { _newSaveGame.PersonagemAtivo}, QuantidadeMembros = 1 };
            Controlador.salvarAvanço(_newSaveGame);
            TerceiraEtapa.Visibility = Visibility.Visible;

            this.NavigationService.Navigate(new IndexMapa());
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
                    _newSaveGame.PersonagemAtivo = Combatente.CriaPersonagemCombatente(_newSaveGame.NomeLider, 3, 3, 3, 3, 3);
                    modificaTabelaStatus(3, 3, 3, 3, 3);
                    break;
                case "Elfo":
                    txtEscolhaRaça.Text = "Legal, você é um Elfo, os elfos são muito inteligentes e ageis, porém não tem tanta força e vida.";
                    _newSaveGame.PersonagemAtivo = Combatente.CriaPersonagemCombatente(_newSaveGame.NomeLider, 2, 2, 4, 4, 3);
                    modificaTabelaStatus(2, 2, 4, 4, 3);
                    break;
                case "Orc":
                    txtEscolhaRaça.Text = "Legal, você é um Orc, os Orcs são grandes e fortes, sua vida e força sobressai em muito aos outros, porem sua inteligencia, agilidade e carisma deixam a desejar.";
                    _newSaveGame.PersonagemAtivo = Combatente.CriaPersonagemCombatente(_newSaveGame.NomeLider, 5, 4, 2, 2, 2);
                    modificaTabelaStatus(5, 4, 2, 2, 2);
                    break;
                case "Anão":
                    txtEscolhaRaça.Text = "Legal, você é um Anão, os Anõre são Fortes, não tanto quanto um Orc, porém são bons com a vida e força, sendo inteligentes como humanos, mas pouco ageis e sociaveis.";
                    _newSaveGame.PersonagemAtivo = Combatente.CriaPersonagemCombatente(_newSaveGame.NomeLider, 4, 4, 3, 2, 2);
                    modificaTabelaStatus(4, 4, 3, 2, 2);
                    break;
                case "Gnomo":
                    txtEscolhaRaça.Text = "Legal, você é um Gnomo, os Gnomos, pequenos e frageis, porém muito ageis e carismaticos, se eles não ganham na conversa, com certeza não serão pegos.";
                    _newSaveGame.PersonagemAtivo = Combatente.CriaPersonagemCombatente(_newSaveGame.NomeLider, 2, 1, 3, 4, 5);
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
