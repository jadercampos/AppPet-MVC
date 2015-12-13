using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPet.MVC.Models
{
    [Table("Servicos")]
    public class Servico : Entidade
    {
        [ScaffoldColumn(false)]
        public int ServicoId { get; set; }
        [Display(Name = "Tipo de Serviço")]
        public int TipoServicoId { get; set; }
        public virtual TipoServico TipoServico { get; set; }
        [Display(Name = "Raça")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int RacaId { get; set; }
        public virtual Raca Raca { get; set; }
        [Display(Name = "Serviço"), Required(ErrorMessage = "Campo obrigatório"), MinLength(4, ErrorMessage = "O nome não pode ter menos de 4 caracteres"), MaxLength(100)]
        public string Nome { get; set; }
        [Display(Name = "Descrição"), DataType(DataType.MultilineText), MaxLength(500)]
        public string Descricao { get; set; }
        [Display(Name = "Preço"), Required(ErrorMessage = "Campo obrigatório"), DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}", ApplyFormatInEditMode = true)]
        public decimal Preco { get; set; }
    }
}