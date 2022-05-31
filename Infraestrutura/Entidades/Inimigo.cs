using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Inimigo : fichaBase
    {
        public string Nome { get; set; }
        public string Foto { get; set; }
        public double XpDropado { get; set; }
        public int IdInimigo { get; set; }

        public void DefineNivel(int nivel)
        {
            Vida = Vida * (0.5 * nivel);
            Mana = Mana + (1.5 * nivel);
            Forca = Forca + (2.5 * nivel);
            Defesa = Defesa + (1.5 * nivel);
            Agilidade = Agilidade + (1.5 * nivel);
            XpDropado = XpDropado * nivel;
        }
    }
}
