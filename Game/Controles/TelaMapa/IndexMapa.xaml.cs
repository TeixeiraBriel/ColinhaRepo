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

namespace Game.Controles.TelaMapa
{
    /// <summary>
    /// Interação lógica para IndexMapa.xam
    /// </summary>
    public partial class IndexMapa : Page
    {
        public IndexMapa()
        {
            InitializeComponent();
            CriarTodosGrid();
        }

        public void CriarTodosGrid()
        {
            for (int linha = 0; linha < 11; linha++)
            {
                for (int coluna = 0; coluna < 11; coluna++)
                {
                    Grid myGrid = new Grid() 
                    {
                        Height = 40,
                        Width = 70,
                        Background = Brushes.AliceBlue
                    };
                    myGrid.MouseEnter += (s, e) => MudaCorVermelhoGrid(s, e);
                    myGrid.MouseLeave += (s, e) => MudaCorPadraoGrid(s, e);

                    Border borda = new Border()
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(2),
                        Child = myGrid
                    };

                    if (coluna == 0 || linha == 0)
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
