using Game.Controladores;
using Game.Controles.TelaMapa;
using Infraestrutura.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Game.Controles.AssentamentoViews
{
    /// <summary>
    /// Interação lógica para InvestigarAssentamentoView.xam
    /// </summary>
    public partial class InvestigarAssentamentoView : Page
    {
        Assentamento _DadosAssentamento;
        List<string> _Conhecimento;
        SaveGame _SaveGame;

        public InvestigarAssentamentoView(Assentamento DadosAssentamento)
        {
            InitializeComponent();
            _SaveGame = Controlador.buscarSave();
            _DadosAssentamento = _SaveGame.Assentamentos.Find(x => x.Id == DadosAssentamento.Id);
            _Conhecimento = _DadosAssentamento.Conhecimento != null ? _DadosAssentamento.Conhecimento : new List<string>();
            atribuiPersonagensLivres();
            atualizaExpoemConhecimentos();
        }

        void atribuiPersonagensLivres() 
        {
            foreach (var combatente in _SaveGame.Equipe.combatentes)
            {
                cmbEnviar.Items.Add(combatente.Nome);
            }
        }

        void atualizaExpoemConhecimentos()
        {
            foreach (string item in _Conhecimento)
            {
                switch (item)
                {
                    case "Nome":
                        lblNome.Text = _DadosAssentamento.Nome; 
                        break;
                    case "Raça Predominante":
                        lblRacaPredominante.Text = _DadosAssentamento.RacaPredominante;
                        break;
                    case "Comida":
                        lblComida.Text = _DadosAssentamento.Comida.ToString();
                        break;
                    case "Madeira":
                        lblMadeira.Text = _DadosAssentamento.Madeira.ToString();
                        break;
                    case "Moedas":
                        lblMoedas.Text = _DadosAssentamento.Moedas.ToString();
                        break;
                    case "Exercito":
                        lblExercito.Text = _DadosAssentamento.Exercito.ToString();
                        break;
                }
            }
        }

        private void RealizarInvestigacao(object sender, RoutedEventArgs e)
        {
            if (_Conhecimento.FirstOrDefault(x => x == cmbInvestigar.Text) == null)
            {
                _Conhecimento.Add(cmbInvestigar.Text);
                _SaveGame.Assentamentos.Find(x => x.Id == _DadosAssentamento.Id).Conhecimento = _Conhecimento;
                atualizaExpoemConhecimentos();
                Controlador.salvarAvanço(_SaveGame);
            }
        }
    }
}
