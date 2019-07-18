using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.DTO
{
    public class Log
    {
        public int Id;
        public string Descripcion;
        public int EstadoId;
        public DateTime Fecha;
        public int CuentaActivaId;
        public DateTime CuentaActivaFecha;
    }
}