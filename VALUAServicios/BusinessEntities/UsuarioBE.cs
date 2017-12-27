using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class UsuarioBE :BEBase
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string CuentaUsuario { get; set; }
        public string Email { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int IdVista { get; set; }
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public int Confidencial { get; set; }
    }
}
