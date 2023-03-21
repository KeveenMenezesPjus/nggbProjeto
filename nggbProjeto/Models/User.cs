using nggbProjeto.Models.Enum;

namespace nggbProjeto.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TipoUser TipoUser { get; set; }
        public DateTime DataAcesso { get; set; }
    }
}
