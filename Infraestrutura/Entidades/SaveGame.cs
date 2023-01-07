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

        public Combatente CriaPersonagemCombatente(string nome,double vigor, double forca, double Inteligencia, double agilidade, double carisma)
        {
            Combatente combatente= new Combatente();

            combatente.Nome = nome;
            combatente.Vigor = vigor;
            combatente.Forca = vigor;
            combatente.Inteligencia = vigor;
            combatente.Agilidade = vigor;
            combatente.Carisma = vigor;

            combatente.InicializaCombatente();

            return combatente;
        }
    }
}
