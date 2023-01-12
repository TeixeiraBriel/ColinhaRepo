using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Infraestrutura.Entidades
{
    public class Assentamento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public Tuple<int,int> Coordenada { get; set; }
        public int Reputacao { get; set; }
        public string RacaPredominante { get; set; }
        public int Comida { get; set; }
        public int Madeira { get; set; }
        public int Moedas { get; set; }
        public int Exercito { get; set; }
        public List<string> Conhecimento { get; set; }
        public Grid RepresentacaoMapa { get; set; }
    }
}
