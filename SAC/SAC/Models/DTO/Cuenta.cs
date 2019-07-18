using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.DTO
{
    public class Cuenta
    {
        public int id;
        public string numero;
        public string nombre;
        
        public DTO.Empresa empresa;
        public DTO.Rubro rubro;
        public DTO.Grupo grupo;
        public string descripcion;
        public bool insertUpdate;
    }
}