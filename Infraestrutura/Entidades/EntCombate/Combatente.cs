using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades.EntCombate
{
    public class Combatente : fichaBase
    {
        public Combatente() 
        {
            velAtaque = Agilidade + Energia;
            VidaAtual = Vida;
            ManaAtual= Mana;
            EnergiaAtual= Energia;
        }

        public double VidaAtual { get; set; }
        public double ManaAtual { get; set; }
        public double EnergiaAtual { get; set; }
        public string Foco { get; set; }
        public string Posicao { get; set; }
        public double velAtaque { get; set; }
    }
}
