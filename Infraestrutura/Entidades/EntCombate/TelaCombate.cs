using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades.EntCombate
{
    public class TelaCombate
    {
        public TelaCombate() 
        {
            
        }

        public int VidaAliados { get; set; }
        public int VidaInimigos { get; set; }

        public List<Combatente> Aliados { get; set; }
        public List<Combatente> Inimigos { get; set; }
    }
}
