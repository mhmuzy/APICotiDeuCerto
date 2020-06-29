using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Services.Models
{
    public class SetorEdicaoModel
    {
        [Required(ErrorMessage = "Informe o id do setor")]
        public int IdSetor { get; set; }

        [Required(ErrorMessage = "Informe o nome do setor")]
        public string Nome { get; set; }
    }
}
