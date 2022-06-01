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

        public void DefineNivel(int nivel)
        {
            Vida = Vida * (0.5 * nivel);
            Mana = Mana + (1 * nivel);
            Forca = Forca + (0.7 * nivel);
            Defesa = Defesa + (0.6 * nivel);
            Agilidade = Agilidade + (0.7 * nivel);
            XpDropado = XpDropado * nivel;
        }
    }
}
