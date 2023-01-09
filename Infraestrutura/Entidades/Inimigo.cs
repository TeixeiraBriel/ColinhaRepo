using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Inimigo : fichaBase
    {
        public string Foto { get; set; }
        public double XpDropado { get; set; }
        public int IdInimigo { get; set; }

    }
}
