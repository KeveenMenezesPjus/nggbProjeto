using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using nggbProjeto.Models;
using nggbProjeto.ViewModels;

namespace nggbProjeto.Controllers
{
    public class AutentificacaoController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public AutentificacaoController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CadastroPartial()
        {
            return PartialView("_Cadastro");
        }
        [HttpPost]
        public async Task<IActionResult> Cadastro(UserViewModel model) { 
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View("Index");
        }


        public IActionResult LoginPartial()
        {
            return PartialView("_Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Autenticação bem sucedida
                    return RedirectToAction(nameof(Index));
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Index");
        }

    }
}
