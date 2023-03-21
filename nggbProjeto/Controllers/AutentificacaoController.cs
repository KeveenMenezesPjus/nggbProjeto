using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

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
        public async Task<IActionResult> Cadastro(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Cria um hash da senha usando o algoritmo PBKDF2
                var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: model.Password,
                    salt: Encoding.UTF8.GetBytes(model.Email), // Utilize um salt único para cada usuário, como o nome de usuário
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = passwordHash, // Salva o hash da senha no banco de dados
                    TipoUser = Models.Enum.TipoUser.Usuario,
                    DataAcesso = DateTime.Now,
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
                // Obter o usuário com o e-mail fornecido
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    // Comparar a senha fornecida com o hash de senha do usuário
                    var passwordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: model.Password,
                        salt: Encoding.UTF8.GetBytes(user.Email),
                        prf: KeyDerivationPrf.HMACSHA512,
                        iterationCount: 10000,
                        numBytesRequested: 256 / 8));

                    if (user.Password == passwordHash)
                    {
                        user.DataAcesso = DateTime.Now;
                        _context.Entry(user).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        // Autenticação bem sucedida
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View("Index");
        }


    }
}
