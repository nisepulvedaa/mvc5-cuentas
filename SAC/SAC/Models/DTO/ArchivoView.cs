using SAC.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAC.Models.DTO
{
    public class ArchivoView
    {
        public ViewDataUploadFilesResult[] Files { get; set; }
    }
}