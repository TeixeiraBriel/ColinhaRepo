using Infraestrutura.Entidades.EntCombate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class SaveGame
    {
        public TimeSpan TempoJogado { get; set; }
        public string NomeLider { get; set; }
        public Combatente PersonagemAtivo { get; set; }
        public Equipe Equipe { get; set; }
        public List<Assentamento> Assentamentos { get; set; }
    }
}
