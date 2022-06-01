using Game.Controladores;
using Game.Controles.MenuInformacoesJogador;
using Game.Controles.TelaPadrao;
using Game.Util;
using Infraestrutura.Entidades;
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
        List<Personagem> Personagens = new List<Personagem>();
        List<Inimigo> Inimigos = new List<Inimigo>();
        Controlador _controlador;
        Progressao Save = new Progressao();

        public IndexMenuInicial(Progressao save = null)
        {
            InitializeComponent();
            _controlador = new Controlador();

            CarregaComboBoxes();

            if (save != null)
            {
                Save = save;
                btnContinuar.IsEnabled = true;
            }
        }

        private void ComeçaJogo(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbPersonagens.Text) || string.IsNullOrEmpty(cmbInimigos.Text) || string.IsNullOrEmpty(cmbNivelInimigo.Text))
            {
                //Exibir msg que n da certo
            }
            else
            {
                Personagem perso = Personagens.Find(x => x.Nome == cmbPersonagens.Text);
                Inimigo inimi = Inimigos.Find(x => x.Nome == cmbInimigos.Text);

                inimi.DefineNivel(int.Parse(cmbNivelInimigo.Text));

                this.NavigationService.Navigate(new IndexTelaPadrao(perso, inimi));
            }
        }

        private void NavegaInfoJogador(object sender, RoutedEventArgs e)
        {
            string nome = inputName.Text;
            string life = inputLife.Text;
            string mana = inputMana.Text;
            this.NavigationService.Navigate(new IndexMenuInformacoesJogador(nome, life, mana));
        }

        public void CarregaComboBoxes()
        {
            _controlador.CarregaJsons();
            Personagens = _controlador.Personagens;
            Inimigos = _controlador.Inimigos;

            foreach (var personagem in Personagens)
            {
                cmbPersonagens.Items.Add(personagem.Nome);
            }

            foreach (var inimigo in Inimigos)
            {
                cmbInimigos.Items.Add(inimigo.Nome);
            }

            for (int i = 0; i < 20; i++)
            {
                cmbNivelInimigo.Items.Add(i.ToString());
            }
        }

        private void ContinuarJogo(object sender, RoutedEventArgs e)
        {
            Personagem perso = Personagens.Find(x => x.IdPersonagem == Save.Classe);
            Inimigo inimi = Inimigos.Find(x => x.IdInimigo == 0);

            if (Save.Nivel == 0)
                Save.Nivel = 1;

            inimi.DefineNivel(Save.Nivel);

            this.NavigationService.Navigate(new IndexTelaPadrao(perso, inimi, Save));
        }

        private void NovoJogo(object sender, RoutedEventArgs e)
        {
            var fileSave = @"Dados\Save.json";
            Save = JsonConvert.DeserializeObject<Progressao>(File.ReadAllText(fileSave, Encoding.UTF8));

            Personagem perso = Personagens.Find(x => x.IdPersonagem == Save.Classe);
            Inimigo inimi = Inimigos.Find(x => x.IdInimigo == 0);

            inimi.DefineNivel(Save.Nivel);

            this.NavigationService.Navigate(new IndexTelaPadrao(perso, inimi, Save));
        }
    }
}
