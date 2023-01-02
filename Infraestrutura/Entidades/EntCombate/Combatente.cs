using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
        public Combatente alvo { get; set; }
        public string Posicao { get; set; }
        public string Dialogo { get; set; }
        public RepresentacaoTelaCombate Painel { get; set; }
        public double intervaloAtaques { get; set; }
        private double TempoParaAtacar { get; set; }
        private DispatcherTimer contadorAtaque = new DispatcherTimer();

        public void IniciaTimerAtacar()
        {
            contadorAtaque.Tick += new EventHandler(CombateAtivo);
            contadorAtaque.Interval = new TimeSpan(0, 0, 0, 0, 500);
            contadorAtaque.Start();
        }
        public void ParaTimerAtacar()
        {
            contadorAtaque.Stop();
        }

        public async void CombateAtivo(object sender, EventArgs e)
        {
            TempoParaAtacar = TempoParaAtacar + 0.5;
            Painel.ProgressBarIntervaloAtaque.Value = TempoParaAtacar;
            if (TempoParaAtacar == intervaloAtaques)
            {
                if (VidaAtual <= 0 || alvo.VidaAtual <= 0)
                {
                    ParaTimerAtacar();
                }
                else
                {
                    Dialogo = alvo.ReceberDabo(Forca);
                }
                TempoParaAtacar = 0;
            }
        }

        public string ReceberDabo(double dano)
        {
            string msg = "";

            if (alvo.VidaAtual - dano <= 0)
            {
                dano = alvo.VidaAtual - dano < 0 ? dano + (alvo.VidaAtual - dano) : dano;
                VidaAtual = 0;
                ParaTimerAtacar();
                msg = "{ALVO} MORREU! Ultimo dano Recebido de {NOME}: " + dano;
            }
            else
            {
                VidaAtual -= dano;
                msg = "{NOME} causou " + dano + " de dano em {ALVO}";
            }

            Painel.ProgressBarVida.Value = VidaAtual;
            return msg;
        }
    }
}