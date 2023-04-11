using Academico.Data;
using Academico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Academico.Controllers
{

    public class InstituicaoController : Controller
    {

        public bool InstituicaoExists(long? id)
        {
            return _context.Instituicoes.Any(i => i.Id == id);
        }
        private readonly AcademicoContext _context;

        public InstituicaoController(AcademicoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Instituicoes.OrderBy(i => i.Nome).ToListAsync());
        }

        private static IList<Instituicao> instituicoes = new List<Instituicao>()
        {
            new Instituicao
            {
                Id = 1,
                Nome = "Hogwarts",
                Endereco = "Escócia"
            },
            new Instituicao
            {
                Id = 2,
                Nome = "Mansão X",
                Endereco = "Nova Iorque"
            }
        };
        public IActionResult Index()
        {
            return View(instituicoes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([Bind("Nome", "Endereço")]Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(instituicao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar a instituição");
            }
            return View(instituicao);
        }

        public async Task<IActionResult> Edit(long? id, ["Id", "Nome", "Endereco"] Instituicao instituicao)
        {
            if (id != instituicao.Id)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(long? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(i =>i.Id == id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }


        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(i => i.Id == id);
            if(instituicao == null)
            {
                return NotFound();
            }
            return View(instituicao);
        }



    }
}
