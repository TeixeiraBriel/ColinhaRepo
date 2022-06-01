using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Progressao
    {
        public int Lutas { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Moedas { get; set; }
        public int Nivel { get; set; }
        public Personagem Jogador { get; set; }
        public double VidaAtual { get; set; }
        public double ManaAtual { get; set; }
        public double EnergiaAtual { get; set; }    
    }
}
