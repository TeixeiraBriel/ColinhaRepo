using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Infraestrutura.Entidades.EntCombate
{
    public class IdentificadorPosicaoCombatente
    {
        public string Posicao { get; set; }
        public StackPanel Painel { get; set; }
        public Combatente Combatente { get; set; }
    }
}
