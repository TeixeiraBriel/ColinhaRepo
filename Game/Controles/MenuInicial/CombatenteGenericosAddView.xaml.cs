using Game.Controladores;
using Infraestrutura.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XpandoLibrary;

namespace Game.Controles.MenuInicial
{
    /// <summary>
    /// Interação lógica para CombatenteGenericosAddView.xam
    /// </summary>
    public partial class CombatenteGenericosAddView : Page
    {
        List<TextBox> txtBoxes = new List<TextBox>();

        public CombatenteGenericosAddView()
        {
            InitializeComponent();
            criarCampos();
        }

        void criarCampos()
        {
            Combatente comb = new Combatente();
            var combXnd = comb.ToExpando();
            Equipe combatentesGenericos = Controlador.buscarCombatentesGenericos();
            if (combatentesGenericos == null)
            {
                combatentesGenericos = new Equipe();
                combatentesGenericos.combatentes = new List<Combatente>();
            }

            foreach (var item in combXnd)
            {
                StackPanel painelLinha = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };

                Label lblTitulo = new Label()
                {
                    Content = item.Key
                };
                TextBox txtblc = new TextBox()
                {
                    Name = "txtblc" + item.Key,
                    Width = 300,
                };

                txtBoxes.Add(txtblc);
                painelLinha.Children.Add(lblTitulo);
                painelLinha.Children.Add(txtblc);
                PainelPrincipal.Children.Add(painelLinha);
            }

            foreach (var combatente in combatentesGenericos.combatentes)
            {
                Button btnCombatente = new Button() { Content = combatente.Nome, Width = 130, Height = 30, };
                btnCombatente.Click += (s, e) =>
                {
                    Combatente combProvisorio = combatentesGenericos.combatentes.Find(x => x == combatente);
                    foreach (var item in combXnd)
                    {
                        string texto = null;
                        try
                        {
                            if (item.Key == "Painel")
                                continue;

                            if (item.Key == "HabilidadesPermitidas")
                            {
                                List<int> habPermitidas = combProvisorio.ToExpando().FirstOrDefault(x => x.Key == item.Key).Value as List<int>;
                                foreach (var hab in habPermitidas)
                                {
                                    if (habPermitidas.Last() == hab)
                                        texto += hab;
                                    else
                                        texto += hab + ",";
                                }
                            }
                            else
                            {
                                texto = combProvisorio.ToExpando().FirstOrDefault(x => x.Key == item.Key).Value.ToString();
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        txtBoxes.Find(x => x.Name == "txtblc" + item.Key).Text = texto;
                    }
                };

                PainelPrincipalDireta.Children.Add(btnCombatente);
            }

            Button btnConfirmar = new Button() { Content = "Confirmar", Width = 150, Height = 30 };
            btnConfirmar.Click += (s, e) =>
            {
                combatentesGenericos = Controlador.buscarCombatentesGenericos();
                if (combatentesGenericos == null)
                {
                    combatentesGenericos = new Equipe();
                    combatentesGenericos.combatentes = new List<Combatente>();
                }

                TextBox txtBoxId = txtBoxes.FirstOrDefault(x => x.Name == "txtblcIdPersonagem");
                Combatente teste = combatentesGenericos.combatentes.FirstOrDefault(x => x.IdPersonagem == int.Parse(txtBoxId.Text));
                if (teste != null)
                {
                    combatentesGenericos.combatentes.Remove(teste);
                    combatentesGenericos.QuantidadeMembros--;
                }

                var newItem = new ExpandoObject() as IDictionary<string, Object>;
                foreach (var item in combXnd)
                {
                    if (item.Key == "HabilidadesPermitidas")
                    {
                        var opcoes = txtBoxes.FirstOrDefault(x => x.Name == "txtblc" + item.Key).Text.Split(',');
                        List<int> HabilidadesPermitidas = new List<int>();
                        foreach (var opcao in opcoes)
                        {
                            HabilidadesPermitidas.Add(int.Parse(opcao));
                        }
                        newItem.Add(item.Key, HabilidadesPermitidas);
                    }
                    else
                    {
                        newItem.Add(item.Key, txtBoxes.FirstOrDefault(x => x.Name == "txtblc" + item.Key).Text);
                    }
                }

                var novoCombatente = JsonConvert.DeserializeObject<Combatente>(JsonConvert.SerializeObject(newItem));
                combatentesGenericos.QuantidadeMembros++;
                combatentesGenericos.combatentes.Add(novoCombatente);
                Controlador.AdicionarCombatentesGenericos(combatentesGenericos);
                this.NavigationService.GoBack();
            };

            PainelPrincipal.Children.Add(btnConfirmar);
        }
    }
}
