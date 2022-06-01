using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Personagem : fichaBase
    {
        public string Classe { get; set; }
        public string Foto { get; set; }
        public int IdPersonagem { get; set; }
        public double XpAtual { get; set; }
        public double XpMaximo { get; set; }
        public List<int> HabilidadesPermitidas { get; set; }
    }
}
