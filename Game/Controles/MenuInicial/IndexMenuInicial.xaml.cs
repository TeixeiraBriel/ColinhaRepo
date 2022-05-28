using Game.Controles.MenuInformacoesJogador;
using Game.Controles.TelaPadrao;
using Game.Util;
using Infraestrutura.Entidades;
using System.Windows;
using System.Windows.Controls;

namespace Game.Controles.MenuInicial
{
    public partial class IndexMenuInicial : Page
    {
        public IndexMenuInicial()
        {
            InitializeComponent();
        }

        private void ComeçaJogo(object sender, RoutedEventArgs e)
        {
            Personagem personagem = new Personagem();
            personagem.Nome = !string.IsNullOrWhiteSpace(inputNomePersonagem.Text) ? inputNomePersonagem.Text : "null";
            personagem.Vida = !string.IsNullOrWhiteSpace(inputVidaPersonagem.Text) ? int.Parse(inputVidaPersonagem.Text) : 1;
            personagem.Força = 10;
            personagem.Defesa = 5;

            Inimigo inimigo = new Inimigo();
            inimigo.Nome = !string.IsNullOrWhiteSpace(inputNomeInimgo.Text) ? inputNomeInimgo.Text : "null";
            inimigo.Vida = !string.IsNullOrWhiteSpace(inputVidaInimgo.Text) ? int.Parse(inputVidaInimgo.Text) : 1;
            inimigo.Força = 15;
            inimigo.Defesa = 0;

            this.NavigationService.Navigate(new IndexTelaPadrao(personagem, inimigo));
        }

        private void NavegaInfoJogador(object sender, RoutedEventArgs e)
        {
            string nome = inputName.Text;
            string life = inputLife.Text;
            string mana = inputMana.Text;
            this.NavigationService.Navigate(new IndexMenuInformacoesJogador(nome,life,mana));
        }
    }
}
