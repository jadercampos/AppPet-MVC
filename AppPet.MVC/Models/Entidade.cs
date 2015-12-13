using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPet.MVC.Models
{
    [ScaffoldTable(false)]
    public class Entidade
    {
        [ScaffoldColumn(false)]
        public DateTime? DtInc { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? DtAlt { get; set; }
        [ScaffoldColumn(false)]
        public string UserInc { get; set; }
        [ScaffoldColumn(false)]
        public string UserAlt { get; set; }
    }
}