using GestaoDeEquipamentos.Dominio.ModuloEquipamento;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;
using GestaoDeEquipamentos.WebApp.Controllers;

namespace GestaoDeEquipamentos.WebApp.Models
{
    public class EquipamentoViewModels
    {

        
    }
    public class VisualizarEquipamentoViewModel
    {
        public List<DetalhesEquipamentoViewModel> Registros { get; set; }
        public VisualizarEquipamentoViewModel(List<Equipamento> equipamentos)
        {
            Registros = new List<DetalhesEquipamentoViewModel>();

            foreach (Equipamento e in equipamentos)
            {
                DetalhesEquipamentoViewModel detalhesVM = new DetalhesEquipamentoViewModel(
                    e.Id,
                    e.Nome,
                    e.PrecoAquisicao,
                    e.DataFabricacao,
                    e.Fabricante.Nome
                    );
                Registros.Add(detalhesVM);
            }
        }
    }
    public class DetalhesEquipamentoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoAquisicao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public string NomeFabricante { get; set; }

        public DetalhesEquipamentoViewModel(int id, string nome, decimal precoAquisicao, DateTime dataFabricacao, string nomeFabricante)
        {
            Id = id;
            Nome = nome;
            PrecoAquisicao = precoAquisicao;
            DataFabricacao = dataFabricacao;
            NomeFabricante = nomeFabricante;
        }
        public override string ToString()
        {
            return $"Id: {Id} - Nome: {Nome} - Fabricante: {NomeFabricante} - Preço de Aquisição:`{PrecoAquisicao} - Data de Fabricação: {DataFabricacao}";
        }
    }
    public class SelecionarFabricanteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public SelecionarFabricanteViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
    public class CadastrarEquipamentoViewModel
    {
        public string Nome { get; set; }
        public decimal PrecoAquisicao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public int FabricanteId { get; set; }
        public List<SelecionarFabricanteViewModel> FabricantesDisponiveis { get; set; }
        public CadastrarEquipamentoViewModel() { }
        public CadastrarEquipamentoViewModel(List<Fabricante> fabricantes) 
        {
            FabricantesDisponiveis = new List<SelecionarFabricanteViewModel>();

            foreach (Fabricante fabricante in fabricantes)
            {
                SelecionarFabricanteViewModel selecionarVM =
                    new SelecionarFabricanteViewModel(fabricante.Id, fabricante.Nome);

                FabricantesDisponiveis.Add(selecionarVM);
            }
        }
    }
}
