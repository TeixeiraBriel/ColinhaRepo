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
using XpandoLibrary;

namespace Game.Controles.AssentamentoViews
{
    /// <summary>
    /// Interação lógica para InvestigarAssentamentoView.xam
    /// </summary>
    public partial class InvestigarAssentamentoView : Page
    {
        Assentamento _DadosAssentamento;
        Combatente _combatente;
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
                bool sucessoPesquisa = validaSucessoPesquisa(15);
                if (!sucessoPesquisa)
                    return;
                _Conhecimento.Add(cmbInvestigar.Text);
                _SaveGame.Assentamentos.Find(x => x.Id == _DadosAssentamento.Id).Conhecimento = _Conhecimento;
                atualizaExpoemConhecimentos();
                Controlador.salvarAvanço(_SaveGame);
            }
            else
            {
                gravarLog("Voce ja tem esse conhecimento investigado.");
            }
        }

        private void CalcularProbabilidade(object sender, SelectionChangedEventArgs e)
        {
            string NomePersonagem = e.AddedItems[0].ToString();
            if (string.IsNullOrEmpty(NomePersonagem))
                return;

            _combatente = _SaveGame.Equipe.combatentes.FirstOrDefault(x => x.Nome == NomePersonagem);
            lblChanceSucesso.Text = $"[{_combatente.Agilidade} + D21] para Sucesso > 15";
        }

        private bool validaSucessoPesquisa(double Dificuldade)
        {
            if (_combatente == null)
                return false;

            int valorDado = new Random().Next(1,21);
            double valorTotal = _combatente.Agilidade + valorDado;
            if (valorTotal > Dificuldade)
            {
                int xpGanho = 5;
                gravarLog($"Investigação realizada, você tirou {valorDado} no dado, gerando um total de {valorTotal} [{_combatente.Agilidade} + {valorDado}]. {_combatente.Nome} ganhou {xpGanho} de experiencia.");
                recebeXpViewGenerico(xpGanho);
                Controlador.salvarAvanço(_SaveGame);
                return true;
            }
            else
            {
                int xpGanho = 2;
                gravarLog($"Investigação falhou, você tirou {valorDado} no dado, gerando um total de {valorTotal} [{_combatente.Agilidade} + {valorDado}]. {_combatente.Nome} ganhou {xpGanho} de experiencia.");
                recebeXpViewGenerico(xpGanho);
                Controlador.salvarAvanço(_SaveGame);
                return false;
            }
        }

        void gravarLog(string texto)
        {
            LogInvestigações.Children.Add(new TextBlock() { Text = $"[{DateTime.Now.ToString("hh:MM:ss")}] - {texto}", TextWrapping = TextWrapping.Wrap});
            ScrollLog.ScrollToEnd();
        }

        void recebeXpViewGenerico(int xp)
        {
            if (_combatente.recebeXp(xp))
            {
                gravarLog($"{_combatente.Nome} subiu de nivel, agora ele é nivel {_combatente.Nivel}.");
                lblChanceSucesso.Text = $"[{_combatente.Agilidade} + D21] para Sucesso > 15";
            }
        }
    }
}
