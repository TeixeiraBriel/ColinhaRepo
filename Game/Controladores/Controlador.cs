using Infraestrutura.Entidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private List<Personagem> _Classes;
        private List<Inimigo> _Inimigos;
        private List<Habilidade> _Habilidades;
        private Progressao _Progressao;

        public List<Personagem> Classes
        {
            get { return _Classes; }
            set { _Classes = value; }
        }
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
            var fileClasses = @"Dados\PersonagensJson.json";
            var fileHabilidades = @"Dados\Habilidades.json";
            var fileSave = @"Dados\Save.json";

            _Progressao = JsonConvert.DeserializeObject<Progressao>(File.ReadAllText(fileSave, Encoding.UTF8));
            _Personagens = JsonConvert.DeserializeObject<List<Personagem>>(File.ReadAllText(filePersonagens, Encoding.UTF8));
            _Classes = JsonConvert.DeserializeObject<List<Personagem>>(File.ReadAllText(filePersonagens, Encoding.UTF8));
            _Inimigos = JsonConvert.DeserializeObject<List<Inimigo>>(File.ReadAllText(fileInimigos, Encoding.UTF8));
            _Habilidades = JsonConvert.DeserializeObject<List<Habilidade>>(File.ReadAllText(fileHabilidades, Encoding.UTF8));
        }

        public bool salvarAvanço(Progressao save)
        {
            try
            {
                Progressao novo = save;
                string output = JsonConvert.SerializeObject(novo);

                File.WriteAllText(@"Dados\Save.json", output.ToString());

                using (StreamWriter file2 = File.CreateText(@"Dados\Save.json"))
                using (JsonTextWriter writer = new JsonTextWriter(file2))
                {
                    JObject.Parse(output).WriteTo(writer);
                }

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
