using Entra21.CSharp.ClinicaVeterinaria.Repositorio.Enums;
using Entra21.CSharp.ClinicaVeterinaria.Servico;
using Entra21.CSharp.ClinicaVeterinaria.Servico.ViewModels.Racas;
using Microsoft.AspNetCore.Mvc;

namespace Entra21.CSharp.ClinicaVeterinaria.Aplicacao.Controllers
{
    // Dois pontos herança (mais para frente)
    public class RacaController : Controller
    {
        private readonly IRacaServico _racaServico;

        // Construtor: objetivo construit o objeto de RacaController,
        // com o mínimo necessário para o funcionamento correto
        public RacaController(IRacaServico racaServico)
        {
            _racaServico = racaServico;
        }

        /// <summary>
        /// Endpoint que permite listar todas as raças
        /// </summary>
        /// <returns>Retorna a página html com as raças</returns>
        [HttpGet("/raca")]

        public IActionResult ObterTodos()
        {
            var racas = _racaServico.ObterTodos();

            return View("Index", racas);
        }

        [HttpGet("/raca/cadastrar")]

        public IActionResult Cadastrar()
        {
            var especies = ObterEspecies();

            ViewBag.Especies = especies;

            var racaCadastrarViewModel = new RacaCadastrarViewModel();

            return View(racaCadastrarViewModel);
        }

        [HttpPost("/raca/cadastrar")]
        public IActionResult Cadastrar(
            [FromForm()] RacaCadastrarViewModel racaCadastrarViewModel)
        {
            // Valida o parâmetro recebido na Action se é inválido
            if(!ModelState.IsValid)
            {
                ViewBag.Especies = ObterEspecies();

                return View(racaCadastrarViewModel);
            }

            _racaServico.Cadastrar(racaCadastrarViewModel);

            return RedirectToAction("Index");
        }

        [HttpGet("/raca/apagar")]
        public IActionResult Apagar([FromQuery] int id)
        {
            _racaServico.Apagar(id);

            return RedirectToAction("Index");
        }

        [HttpGet("/raca/editar")]
        public IActionResult Editar([FromQuery] int id)
        {
            var raca = _racaServico.ObterPorId(id);

            var especies = ObterEspecies();

            var racaEditarViewModel = new RacaEditarViewModel
            {
                Id = raca.Id,
                Nome = raca.Nome,
                Especie = raca.Especie,
            };

            ViewBag.Especies = especies;

            return View(racaEditarViewModel);
        }

        [HttpPost("/raca/editar")]
        public IActionResult Editar(
            [FromForm] RacaEditarViewModel racaEditarViewModel)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Especies = ObterEspecies();

                return View(racaEditarViewModel);
            }

            _racaServico.Editar(racaEditarViewModel);

            return RedirectToAction("Index");
        }

        private List<string> ObterEspecies()
        {
            return Enum.GetNames<Especie>()
                            .OrderBy(x => x)
                            .ToList();
        }
    }
}
