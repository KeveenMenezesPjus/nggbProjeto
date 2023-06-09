﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using nggbProjeto.Models;

namespace nggbProjeto.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult TesteConexao()
        {
            var canConnect = _context.Database.CanConnect();
            if (canConnect)
            {
                return Content("Conexão bem-sucedida!");
            }
            else
            {
                return Content("Não foi possível conectar ao banco de dados.");
            }
        }

    }
}