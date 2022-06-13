using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Entidades
{
    public class Progressao
    {
        private int _Lutas;
        private int _Vitorias;
        private int _Derrotas;
        private int _Moedas;
        private int _Nivel;
        private Personagem _Jogador;
        private double _VidaAtual;
        private double _ManaAtual;
        private double _EnergiaAtual;

        public int Lutas { get { return _Lutas; } set { _Lutas = value; } }
        public int Vitorias { get { return _Vitorias; } set { _Vitorias = value; } }
        public int Derrotas { get { return _Derrotas; } set { _Derrotas = value; } }
        public int Moedas { get { return _Moedas; } set { _Moedas = value; } }
        public int Nivel { get { return _Nivel; } set { _Nivel = value; } }
        public Personagem Jogador { get { return _Jogador; } set { _Jogador = value; } }
        public double VidaAtual { get { return _VidaAtual; } set { _VidaAtual = value; } }
        public double ManaAtual { get { return _ManaAtual; } set { _ManaAtual = value; } }
        public double EnergiaAtual { get { return _EnergiaAtual; } set { _EnergiaAtual = value; } }

        public void IniciaDadosBase()
        {
            _Lutas = 0;
            _Vitorias = 0;
            _Derrotas = 0;
            _Moedas = 0;
            _Nivel = 1;
            _Jogador = new Personagem();
            _VidaAtual = _Jogador.Vida;
            _ManaAtual = _Jogador.Mana;
            _EnergiaAtual = _Jogador.Energia;
        }
    }
}
