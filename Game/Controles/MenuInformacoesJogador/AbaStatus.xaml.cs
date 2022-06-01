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
        private IndexMenuInformacoesJogador FramePai;
        Personagem _Personagem;
        Controlador _controlador;

        public AbaStatus(IndexMenuInformacoesJogador framePai)
        {
            InitializeComponent();
            _controlador = new Controlador();
            _Personagem = new Personagem();
            FramePai = framePai;
            iniciaDados();
        }

        void iniciaDados()
        {
            _controlador.CarregaJsons();
            _Personagem = _controlador.Personagens.FirstOrDefault();

            var teste = XpandoLibrary.Xpando.ToExpando(_Personagem);
            foreach (var item in teste)
            {
                if (item.Key == "IdPersonagem" || item.Key == "Foto" || item.Key == "HabilidadesPermitidas")
                    continue;

                TextBlock info = new TextBlock() { Text = $"{item.Key}: {item.Value}", Margin = new Thickness(5, 5, 5, 5) };
                PainelDados.Children.Add(info);
            }
        }
    }
}
