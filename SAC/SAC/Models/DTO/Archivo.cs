using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAC.Models.DTO
{
    public class Archivo
    {
        public int Id;
        public int Version;
        public string Nombre;
        public DateTime Fecha;
        public byte[] Data;
        public double Monto;
        public string Comentario;
        public string Extension;
        public int Estado;
        public int UsuarioCreacion;
        public int CuentaActivaId;
        public DateTime CuentaActivaFecha;
    }
}