using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Habilidade
    {
        public int IdHabilidade { get; set; }
        public string Nome { get; set; }
        public double DanoBase { get; set; }
        public double CustoBase { get; set; }
        public string Tipo { get; set; }
        public string Icon { get; set; }
        public string Descricao { get; set; }

    }
}
