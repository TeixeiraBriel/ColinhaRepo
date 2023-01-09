using Infraestrutura.Entidades.EntCombate;
using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Infraestrutura.Entidades
{
    public class Combatente : fichaBase
    {
        public Combatente()
        {
        }

        public string Classe { get; set; }
        public string Foto { get; set; }
        public int IdPersonagem { get; set; }
        public double XpAtual { get; set; }
        public double XpDropado { get; set; }
        public double XpMaximo { get; set; }
        public int Nivel { get; set; }
        public List<int> HabilidadesPermitidas { get; set; }
        public double VidaAtual { get; set; }
        public double ManaAtual { get; set; }
        public double EnergiaAtual { get; set; }
        public RepresentacaoTelaCombate Painel { get; set; }
        public double intervaloAtaques { get; set; }
        private double TempoParaAtacar { get; set; }
        private DispatcherTimer contadorAtaque = new DispatcherTimer();

        public void InicializaCombatente(int Nivel)
        {
            Nivel = 1;
            DefineNivel(Nivel);

            VidaAtual = Vida;
            ManaAtual = Mana;
            EnergiaAtual = Energia;

            Classe = "";
            Foto = "";
            XpAtual = 0;
            XpMaximo = 10;
            XpDropado = 3;
            HabilidadesPermitidas = new List<int>();
        }
        public static Combatente CriaPersonagemCombatente(string nome, double vigor, double forca, double Inteligencia, double agilidade, double carisma)
        {
            Combatente combatente = new Combatente();

            combatente.Nome = nome;
            combatente.Vigor = vigor;
            combatente.Forca = vigor;
            combatente.Inteligencia = vigor;
            combatente.Agilidade = vigor;
            combatente.Carisma = vigor;

            combatente.InicializaCombatente(1);

            return combatente;
        }
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
                if (VidaAtual <= 0 || Painel.alvo.VidaAtual <= 0)
                {
                    ParaTimerAtacar();
                }
                else
                {
                    Painel.Dialogo = Painel.alvo.ReceberDabo(Forca);
                }
                TempoParaAtacar = 0;
            }
        }
        public string ReceberDabo(double dano)
        {
            string msg = "";

            if (Painel.alvo.VidaAtual - dano <= 0)
            {
                dano = Painel.alvo.VidaAtual - dano < 0 ? dano + (Painel.alvo.VidaAtual - dano) : dano;
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
        public void DefineNivel(int nivel)
        {
            Vida = Vigor * (0.5 * nivel);
            Mana = Inteligencia + (1 * nivel);
            Energia = Agilidade + (1 * nivel);
            Forca = Forca + (0.7 * nivel);
            Defesa = Defesa + (0.6 * nivel);
            Agilidade = Agilidade + (0.7 * nivel);
            XpDropado = XpDropado * nivel;
        }
    }
}