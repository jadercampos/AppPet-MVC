using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPet.MVC.Models
{
    [Table("Clientes")]
    public class Cliente : Entidade
    {
        [ScaffoldColumn(false)]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Campo obrigatório"), MinLength(4, ErrorMessage = "O nome não pode ter menos de 4 caracteres"), MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(20)]
        public string Telefone { get; set; }
        [MaxLength(20)]
        public string Celular { get; set; }
        [DataType(DataType.EmailAddress), MaxLength(100)]
        public string Facebook { get; set; }
        [Display(Name = "Data de Nascimento"), DataType(DataType.Date)]
        public DateTime? DtNascimento { get; set; }
        [MaxLength(9)]
        public string Cep { get; set; }
        [Display(Name = "Endereço"), MaxLength(200)]
        public string Endereco { get; set; }
        [Display(Name = "Número"), MaxLength(10)]
        public string Numero { get; set; }
        [MaxLength(100)]
        public string Complemento { get; set; }
        [MaxLength(100)]
        public string Bairro { get; set; }
        [MaxLength(100)]
        public string Cidade { get; set; }
        [Display(Name = "Estado"), MaxLength(2)]
        public string Uf { get; set; }
        [Display(Name = "Observação"), DataType(DataType.MultilineText), MaxLength(500)]
        public string Descricao { get; set; }
    }
}