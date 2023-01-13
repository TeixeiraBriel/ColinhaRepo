using Infraestrutura.Entidades;
using Infraestrutura.Entidades.EntCombate;
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
        //teste
        private List<Combatente> _Personagens;
        private List<Combatente> _Classes;
        private List<Combatente> _Inimigos;
        private List<Habilidade> _Habilidades;
        private Progressao _Progressao;

        public List<Combatente> Classes
        {
            get { return _Classes; }
            set { _Classes = value; }
        }
        public List<Combatente> Inimigos
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

        public static List<Combatente> buscaClasses()
        {
            var fileClasses = @"Dados\ClassesJson.json";
            return JsonConvert.DeserializeObject<List<Combatente>>(File.ReadAllText(fileClasses, Encoding.UTF8));
        }

        public static List<Habilidade> buscaHabilidades()
        {
            var fileHablidades = @"Dados\Habilidades.json";
            return JsonConvert.DeserializeObject<List<Habilidade>>(File.ReadAllText(fileHablidades, Encoding.UTF8));
        }

        public void CarregaJsons()
        {

            /*
            var fileInimigos = @"Dados\InimigosJson.json";
            var fileHabilidades = @"Dados\Habilidades.json";
            var fileSave = @"Dados\Save.json";

            //_Progressao = JsonConvert.DeserializeObject<Progressao>(File.ReadAllText(fileSave, Encoding.UTF8));
            _Inimigos = JsonConvert.DeserializeObject<List<Combatente>>(File.ReadAllText(fileInimigos, Encoding.UTF8));
            _Habilidades = JsonConvert.DeserializeObject<List<Habilidade>>(File.ReadAllText(fileHabilidades, Encoding.UTF8));
            */
        }

        public static SaveGame buscarSave()
        {
            var fileSave = @"Dados\Save.json";
            try
            {
                SaveGame save = JsonConvert.DeserializeObject<SaveGame>(File.ReadAllText(fileSave, Encoding.UTF8));
                return save;
            }
            catch
            {
                return null;
            }
        }

        public static bool salvarAvanço(SaveGame save)
        {
            try
            {
                SaveGame novo = save;
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

        public static Equipe buscarCombatentesGenericos()
        {
            var fileCombatentesGenericos = @"Dados\CombatentesGenericos.json";
            try
            {
                Equipe CombatentesGenericos = JsonConvert.DeserializeObject<Equipe>(File.ReadAllText(fileCombatentesGenericos, Encoding.UTF8));
                return CombatentesGenericos;
            }
            catch
            {
                return null;
            }
        }

        public static bool AdicionarCombatentesGenericos(Equipe equipe)
        {
            try
            {
                string output = JsonConvert.SerializeObject(equipe);

                File.WriteAllText(@"Dados\CombatentesGenericos.json", output.ToString());

                using (StreamWriter file3 = File.CreateText(@"Dados\CombatentesGenericos.json"))
                using (JsonTextWriter writer = new JsonTextWriter(file3))
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

        public static Equipe buscarEquipe()
        {
            Equipe equipe = new Equipe();
            equipe = buscarSave().Equipe;

            return equipe;
        }
    }
}
