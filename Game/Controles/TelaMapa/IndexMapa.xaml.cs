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
        List<Tuple<string, Grid>> _mapeamento;
        public IndexMapa()
        {
            InitializeComponent();
            _mapeamento = new List<Tuple<string, Grid>>();
            CriarTodosGrid();
        }

        public void CriarTodosGrid()
        {
            for (int linha = 0; linha < 9; linha++)
            {
                for (int coluna = 0; coluna < 9; coluna++)
                {
                    Grid myGrid = new Grid()
                    {
                        Height = 60,
                        Width = 60,
                        Background = Brushes.AliceBlue
                    };


                    myGrid.MouseEnter += (s, e) => MudaCorVermelhoGrid(s, e);
                    myGrid.MouseLeave += (s, e) => MudaCorPadraoGrid(s, e);

                    myGrid.MouseLeftButtonUp += (s2, e2) => { 
                        var _sender = s2 as Grid;
                        var item = _mapeamento.FirstOrDefault(x => x.Item2 == _sender);

                        if (item.Item1 == "5|5")
                        {
                            this.NavigationService.Navigate(new AssentamentoView(new Assentamento() { Nome = "Cidade Principal", Coordenada = new Tuple<int, int>(linha,coluna), Tipo = "Cidade"}));
                        }
                        else
                        {
                            this.NavigationService.Navigate(new AssentamentoView(new Assentamento() { Nome = $"Mapa at {item.Item1}", Coordenada = new Tuple<int, int>(linha, coluna), Tipo = "Desconhecida" }));
                        }
                    };

                    if (coluna == 5 && linha == 5)
                    {
                        myGrid.Children.Add(new Label() { Content = "Cidade", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center });
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

                        _mapeamento.Add(new Tuple<string, Grid>($"{linha}|{coluna}", myGrid));
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
