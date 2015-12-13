using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPet.MVC.Models
{
    [Table("Unidades")]
    public class Unidade : Entidade
    {
        [ScaffoldColumn(false)]
        public int UnidadeId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório"), MinLength(4, ErrorMessage = "O nome não pode ter menos de 4 caracteres"), MaxLength(100)]
        public string Nome { get; set; }
        [Display(Name = "Observação"), DataType(DataType.MultilineText), MaxLength(500)]
        public string Descricao { get; set; }
    }
}