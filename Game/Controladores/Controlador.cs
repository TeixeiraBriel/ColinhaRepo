using Infraestrutura.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Controladores
{
    public class Controlador
    {
        private List<Personagem> _Personagens;
        private List<Inimigo> _Inimigos;
        private List<Habilidade> _Habilidades;
        private Progressao _Progressao;

        public List<Personagem> Personagens
        {
            get { return _Personagens; }
            set { _Personagens = value; }
        }
        public List<Inimigo> Inimigos
        {
            get { return _Inimigos; }
            set { _Inimigos = value; }
        }
        public List<Habilidade> Habilidades
        {
            get { return _Habilidades; }
            set { _Habilidades = value; }
        }
        public Progressao Progressao
        {
            get { return _Progressao; }
            set { _Progressao = value; }
        }


        public void CarregaJsons()
        {
            var fileInimigos = @"Dados\InimigosJson.json";
            var filePersonagens = @"Dados\PersonagensJson.json";
            var fileHabilidades = @"Dados\Habilidades.json";
            var fileSave = @"Dados\Save.json";

            _Progressao = JsonConvert.DeserializeObject<Progressao>(File.ReadAllText(fileSave, Encoding.UTF8));
            _Personagens = JsonConvert.DeserializeObject<List<Personagem>>(File.ReadAllText(filePersonagens, Encoding.UTF8));
            _Inimigos = JsonConvert.DeserializeObject<List<Inimigo>>(File.ReadAllText(fileInimigos, Encoding.UTF8));
            _Habilidades = JsonConvert.DeserializeObject<List<Habilidade>>(File.ReadAllText(fileInimigos, Encoding.UTF8));
        }
    }
}
