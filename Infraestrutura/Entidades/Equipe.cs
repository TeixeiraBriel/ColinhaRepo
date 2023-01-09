using Infraestrutura.Entidades.EntCombate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Equipe
    {
        public int QuantidadeMembros { get; set; }
        public List<Combatente> combatentes { get; set; }
    }
}
