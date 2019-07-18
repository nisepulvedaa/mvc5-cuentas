using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.DTO
{
    public class Usuario
    {
        public int usuarioId;
        public string rut;
        public string nombre;
        public string email;
        public DTO.Empresa  empresa;
        public DTO.Perfil perfil;
        public bool pwdEstado;

        public int perfilId { get; internal set; }
    }
}