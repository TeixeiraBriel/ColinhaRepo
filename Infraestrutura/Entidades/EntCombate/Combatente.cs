using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Infraestrutura.Entidades.EntCombate
{
    public class Combatente : fichaBase
    {
        public Combatente()
        {
            VidaAtual = Vida;
            ManaAtual = Mana;
            EnergiaAtual = Energia;
            TempoParaAtacar = intervaloAtaques;
        }

        public double VidaAtual { get; set; }
        public double ManaAtual { get; set; }
        public double EnergiaAtual { get; set; }
        public string Foco { get; set; }
        public string Posicao { get; set; }
        public int intervaloAtaques { get; set; }
        public bool AtaquePronto { get; set; }
        public bool StatusTimerAtaque { get; set; }


        private int TempoParaAtacar { get; set; }
        private DispatcherTimer contadorAtaque = new DispatcherTimer();

        public void IniciaTimerAtacar()
        {
            contadorAtaque.Tick += new EventHandler(CombateAtivo);
            contadorAtaque.Interval = new TimeSpan(0, 0, 0, intervaloAtaques, 0);
            contadorAtaque.Start();
            StatusTimerAtaque = true;
        }
        public void ParaTimerAtacar()
        {
            contadorAtaque.Stop();
            StatusTimerAtaque = false;
        } 

        public async void CombateAtivo(object sender, EventArgs e)
        {
            AtaquePronto = true;
        }
    }
}
