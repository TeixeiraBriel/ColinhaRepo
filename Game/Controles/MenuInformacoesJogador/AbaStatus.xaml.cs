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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game.Controles.MenuInformacoesJogador
{
    /// <summary>
    /// Interação lógica para AbaStatus.xam
    /// </summary>
    public partial class AbaStatus : Page
    {
        private Progressao _save = new Progressao();
        Personagem _Personagem;
        Controlador _controlador;

        public AbaStatus(Progressao save)
        {
            InitializeComponent();

            _controlador = new Controlador();
            _Personagem = new Personagem();
            _save = save;

            iniciaDados();
        }

        void iniciaDados()
        {
            Progressao save = _save;

            infoNome.Text = "Nome: " + _save.Jogador.Nome;
            infoVida.Text = "Vida Maxima: " + _save.Jogador.Vida.ToString();
            infoMana.Text = "Mana Maxima: " + _save.Jogador.Mana.ToString();
            infoEnergia.Text = "Energia Maxima: " + _save.Jogador.Energia.ToString();
            infoDefesa.Text = "Defesa: " + _save.Jogador.Defesa.ToString();
            infoForca.Text = "Força: " + _save.Jogador.Forca.ToString();
            infoInteligencia.Text = "Inteligencia: " + _save.Jogador.Inteligencia.ToString();
            infoAgilidade.Text = "Agilidade: " + _save.Jogador.Agilidade.ToString();
            infoClasse.Text = "Classe: " + _save.Jogador.Classe;
            infoFoto.Text = "Foto: " + _save.Jogador.Foto;
            infoXPMaximo.Text = "XP Maximo: " + _save.Jogador.XpMaximo.ToString();
            infoXPAtual.Text = "XP Atual: " + _save.Jogador.XpAtual.ToString();
            infoLutas.Text = "Lutas: " + _save.Lutas.ToString();
            infoVitorias.Text = "Vitorias: " + _save.Vitorias.ToString();
            infoDerrotas.Text = "Derrotas: " + _save.Derrotas.ToString();
            infoMoedas.Text = "Moedas: " + _save.Moedas.ToString();
            infoNivel.Text = "Nivel: " + _save.Nivel.ToString();
            infoVidaAtual.Text = "Vida Atual: " + _save.VidaAtual.ToString();
            infoManaAtual.Text = "Mana Atual: " + _save.ManaAtual.ToString();
            infoEnergiaAtual.Text = "Energial Atual: " + _save.EnergiaAtual.ToString();

            string habPermitidas = "Habilidades: | ";
            _controlador.CarregaJsons();
            foreach (var hab in _controlador.Habilidades)
            {
                if (_save.Jogador.HabilidadesPermitidas.Contains(hab.IdHabilidade))
                {
                    habPermitidas += $"{hab.Nome} | ";
                }
            }
            infoHabilidadesPermitidas.Text = habPermitidas;
        }
    }
}
