using Microsoft.AspNetCore.Mvc;
using NiverAmigos.Application;
using NiverAmigos.Entidade;

namespace NiverAmigos.Controllers
{
    public class AmigoController : Controller
    {
        private AmigoManager AmigoManager { get; set; }

        public AmigoController() 
        {
            AmigoManager= new AmigoManager();
        }

        public IActionResult Index()
        {
            ViewBag.Todos = AmigoManager.ObterTodos();
            ViewBag.Aniversariantes = AmigoManager.ObterAniversariantes();

            return View();
        }

        public IActionResult Detail(int amigoId)
        {
            var result = AmigoManager.ObterPorId(amigoId);
            return View(result);
        }

        public IActionResult Search(string query)
        {
            var result = AmigoManager.Search(query);
            ViewBag.Search = $"Exibindo resultados para: {query}";
            return View(result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(Amigo model)
        {
            if (ModelState.IsValid == false)
                return View("Add");

            AmigoManager.Salvar(model);

            return RedirectToAction("Index");
        }

        public IActionResult Update(int amigoId)
        {
            var result = AmigoManager.ObterPorId(amigoId);
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(Amigo model)
        {
            if (ModelState.IsValid == false)
                return View("Update", model);

            AmigoManager.Atualizar(model);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int amigoId)
        {
            AmigoManager.Excluir(amigoId);

            return RedirectToAction("Index");
        }

    }
}
