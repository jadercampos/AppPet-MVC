using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPet.MVC.Models
{
    [Table("TiposServicos")]
    public class TipoServico
    {
        [ScaffoldColumn(false)]
        public int TipoServicoId { get; set; }
        [Display(Name = "Tipo de Serviço"), Required(ErrorMessage = "Campo obrigatório"), MinLength(4, ErrorMessage = "O nome não pode ter menos de 4 caracteres"), MaxLength(50)]
        public string Nome { get; set; }
        [Display(Name = "Descrição"), DataType(DataType.MultilineText), MaxLength(500)]
        public string Descricao { get; set; }
    }
}