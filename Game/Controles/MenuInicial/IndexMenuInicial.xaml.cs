﻿using Game.Controles.MenuInformacoesJogador;
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

        public IndexMenuInicial()
        {
            InitializeComponent();
            CarregaComboBoxes();
        }

        private void ComeçaJogo(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(cmbPersonagens.Text) || string.IsNullOrEmpty(cmbInimigos.Text))
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
            CarregaJsons();

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
        public void CarregaJsons()
        {
            var fileInimigos = @"Dados\InimigosJson.json";
            var filePersonagens = @"Dados\PersonagensJson.json";

            Personagens = JsonConvert.DeserializeObject<List<Personagem>>(File.ReadAllText(filePersonagens, Encoding.UTF8));
            Inimigos = JsonConvert.DeserializeObject<List<Inimigo>>(File.ReadAllText(fileInimigos, Encoding.UTF8));
        }

    }
}