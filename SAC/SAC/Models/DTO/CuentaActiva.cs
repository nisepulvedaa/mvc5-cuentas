using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.DTO
{
    public class CuentaActiva : Cuenta
    {
        public int cuentaActivaId;
        public DateTime cuentaActivaFecha;
        public int cuentaActivaDiasPlazo;
        public int cuentaActivaTotal;
        public int cuentaActivaEstado;
        public string usuarioCreacion;
        public string fechaCreacion;

        public DTO.Usuario analista;
        public DTO.Usuario validador;
        public DTO.Usuario certificador;

        public DTO.Cuenta cuenta;
    }
}