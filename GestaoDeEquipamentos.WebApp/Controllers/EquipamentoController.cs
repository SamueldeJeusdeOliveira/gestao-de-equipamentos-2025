using GestaoDeEquipamentos.Dominio.ModuloEquipamento;
using GestaoDeEquipamentos.Dominio.ModuloFabricante;
using GestaoDeEquipamentos.Infraestrutura.Arquivos.Compartilhado;
using GestaoDeEquipamentos.Infraestrutura.Arquivos.ModuloFabricante;
using GestaoDeEquipamentos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace GestaoDeEquipamentos.WebApp.Controllers
{
    public class EquipamentoController : Controller
    {
        private RepositorioFabricanteEmArquivo repositorioFabricante;
        private RepositorioEquipamentoEmArquivo repositorioEquipamento;
        public EquipamentoController()
        {
            ContextoDados contexto = new ContextoDados(true);
            repositorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
        }

        public IActionResult Index()
        {
            List<Equipamento> equipamentos = repositorioEquipamento.SelecionarRegistros();

            VisualizarEquipamentosViewModel visualizarVM = new VisualizarEquipamentosViewModel(equipamentos);

            return View(visualizarVM);
        }
        public IActionResult Cadastrar()
        {
            List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();

            CadastrarEquipamentoViewModel cadastros = new CadastrarEquipamentoViewModel(fabricantes);

            return View(cadastros);
        }
        [HttpPost]
        public IActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVM)
        {
            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(cadastrarVM.FabricanteId);

            if (fabricanteSelecionado == null)
                return RedirectToAction(nameof(Index));

            Equipamento novoEquipamento = new Equipamento(
                cadastrarVM.Nome,
                cadastrarVM.PrecoAquisicao,
                cadastrarVM.DataFabricacao,
                fabricanteSelecionado
                );
            repositorioEquipamento.CadastrarRegistro(novoEquipamento);


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Editar(int id)
        {
            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(id);

            List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();

            EditarEquipamentoViewModel editarVm = new EditarEquipamentoViewModel(
                equipamentoSelecionado.Id,
                equipamentoSelecionado.Nome,
                equipamentoSelecionado.PrecoAquisicao,
                equipamentoSelecionado.DataFabricacao,
                equipamentoSelecionado.Fabricante.Id,
                fabricantes
            );

            return View(editarVm);
        }

        [HttpPost]
        public IActionResult Editar(int id, EditarEquipamentoViewModel editarVm)
        {
            Fabricante fabricanteSelecionado = repositorioFabricante.SelecionarRegistroPorId(editarVm.FabricanteId);

            Equipamento equipamentoEditado = new Equipamento(
                editarVm.Nome,
                editarVm.PrecoAquisicao,
                editarVm.DataFabricacao,
                fabricanteSelecionado
            );

            repositorioEquipamento.EditarRegistro(id, equipamentoEditado);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Excluir(int id)
        {
            Equipamento equipamentoSelecionado = repositorioEquipamento.SelecionarRegistroPorId(id);

            List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();

            ExcluirEquipamentoViewModel editarVm = new ExcluirEquipamentoViewModel(
                equipamentoSelecionado.Id,
                equipamentoSelecionado.Nome
            );

            return View(editarVm);
        }
        [HttpPost]
        public IActionResult ExcluirConfirmado(int id)
        {
            repositorioEquipamento.ExcluirRegistro(id);

            return RedirectToAction(nameof(Index));
        }
    }  
}
