using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GerenciamentoAniversarios.Models
{
    public class Pessoa
    {
        [Display(Name = "Id")]
        public int PessoaId { get; set; }
       
        [Required(ErrorMessage = "Informe o nome da pessoa!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome da pessoa!")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe a data de aniversário da pessoa!")]
        public DateTime Aniversario { get; set; }

    }
}