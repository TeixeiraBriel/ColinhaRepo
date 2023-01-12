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

namespace Game.Controles.TelaMapa
{
    /// <summary>
    /// Interação lógica para IndexMapa.xam
    /// </summary>
    public partial class IndexMapa : Page
    {
        List<Assentamento> _mapeamento;
        SaveGame _newSaveGame;

        public IndexMapa(SaveGame newSaveGame)
        {
            InitializeComponent();
            _newSaveGame = newSaveGame;
            _mapeamento = _newSaveGame.Assentamentos;
            CriarTodosGrid();
        }

        public void CriarTodosGrid()
        {
            for (int linha = 0; linha < 9; linha++)
            {
                for (int coluna = 0; coluna < 9; coluna++)
                {
                    Assentamento _assentamento = _mapeamento.FirstOrDefault(x => x.Coordenada.Item1 == linha && x.Coordenada.Item2 == coluna);

                    Grid myGrid = new Grid()
                    {
                        Height = 60,
                        Width = 60,
                        Background = Brushes.AliceBlue
                    };


                    myGrid.MouseEnter += (s, e) => MudaCorVermelhoGrid(s, e);
                    myGrid.MouseLeave += (s, e) => MudaCorPadraoGrid(s, e);

                    if (_assentamento != null)
                    {
                        myGrid.MouseLeftButtonUp += (s2, e2) =>
                        {
                            this.NavigationService.Navigate(new AssentamentoView(_assentamento));
                        };

                        if (_assentamento.Tipo == "Desconhecido")
                            myGrid.Children.Add(new TextBlock() { Text = "Desconhecido", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap });
                        else
                            myGrid.Children.Add(new TextBlock() { Text = _assentamento.Nome, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap });

                        _assentamento.RepresentacaoMapa = myGrid;
                    }


                    Border borda = new Border()
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(2),
                        CornerRadius = new CornerRadius(100),
                        Margin = new Thickness(1),
                        Child = myGrid
                    };

                    if (coluna == 0 | linha == 0)
                    {

                    }
                    else
                    {
                        Grid.SetColumn(borda, coluna);
                        Grid.SetRow(borda, linha);
                        GridTelaMapa.Children.Add(borda);
                    }
                }
            }
        }

        private void MudaCorVermelhoGrid(object sender, MouseEventArgs e)
        {
            var _sender = sender as Grid;
            _sender.Background = Brushes.Red;
            _sender.Cursor = Cursors.Hand;
        }

        private void MudaCorPadraoGrid(object sender, MouseEventArgs e)
        {
            var _sender = sender as Grid;
            _sender.Background = Brushes.AliceBlue;
            _sender.Cursor = Cursors.Arrow;
        }
    }
}
