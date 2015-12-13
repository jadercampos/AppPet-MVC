using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPet.MVC.Models
{
    public class Raca : Entidade
    {
        [ScaffoldColumn(false)]
        public int RacaId { get; set; }
        [Display(Name = "Raça"), Required(ErrorMessage = "Campo obrigatório"), MinLength(4, ErrorMessage = "O nome não pode ter menos de 4 caracteres"), MaxLength(50)]
        public string Nome { get; set; }
        [Display(Name = "Descrição"), DataType(DataType.MultilineText), MaxLength(500)]
        public string Descricao { get; set; }
        [Display(Name = "Espécie")]
        public int EspecieId { get; set; }
        public virtual Especie Especie { get; set; }
    }
}