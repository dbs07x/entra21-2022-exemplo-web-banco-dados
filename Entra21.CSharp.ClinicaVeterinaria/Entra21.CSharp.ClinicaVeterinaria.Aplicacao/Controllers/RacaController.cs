using Entra21.CSharp.ClinicaVeterinaria.Repositorio.BancoDados;
using Entra21.CSharp.ClinicaVeterinaria.Repositorio.Enums;
using Entra21.CSharp.ClinicaVeterinaria.Servico;
using Microsoft.AspNetCore.Mvc;

namespace Entra21.CSharp.ClinicaVeterinaria.Aplicacao.Controllers
{
    // Dois pontos herança (mais para frente)
    public class RacaController : Controller
    {
        private readonly RacaServico _racaServico;
        private readonly List<string> _especies;

        // Construtor: objetivo construit o objeto de RacaController,
        // com o mínimo necessário para o funcionamento correto
        public RacaController(ClinicaVeterinariaContexto contexto)
        {
            _racaServico = new RacaServico(contexto);
        }

        /// <summary>
        /// Endpoint que permite listar todas as raças
        /// </summary>
        /// <returns>Retorna a página html com as raças</returns>
        [Route("/raca")]
        [HttpGet]

        public IActionResult ObterTodos()
        {
            var racas = _racaServico.ObterTodos();

            // Passar informação do C# para o HTML
            ViewBag.Racas = racas;

            return View("Index");
        }

        [Route("/raca/cadastrar")]
        [HttpGet]

        public IActionResult Cadastrar()
        {
            var especies = ObterEspecies();

            ViewBag.Especies = especies;

            return View();
        }

        [Route("/raca/registrar")]
        [HttpGet]
        public IActionResult Registrar(
            [FromQuery()] string nome,
            [FromQuery()] string especie)
        {
            _racaServico.Cadastrar(nome, especie);

            return RedirectToAction("Index");
        }

        [Route("/raca/apagar")]
        [HttpGet]
        // http://localhost:porta/raca/apagar?id=4
        public IActionResult Apagar([FromQuery] int id)
        {
            _racaServico.Apagar(id);

            return RedirectToAction("Index");
        }

        [Route("/raca/editar")]
        [HttpGet]
        public IActionResult Editar([FromQuery] int id)
        {
            var raca = _racaServico.ObterPorId(id);

            var especies = ObterEspecies();

            ViewBag.Raca = raca;
            ViewBag.Especies = especies;

            return View("Editar");
        }

        [Route("/raca/alterar")]
        [HttpGet]
        public IActionResult Alterar(
            [FromQuery] int id,
            [FromQuery] string nome,
            [FromQuery] string especie)
        {
            _racaServico.Alterar(id, nome, especie);

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
