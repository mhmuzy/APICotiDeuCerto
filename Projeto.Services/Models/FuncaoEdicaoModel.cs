using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Services.Models
{
    public class FuncaoEdicaoModel
    {
        [Required(ErrorMessage = "Informe o id da função")]
        public int IdFuncao { get; set; }

        [Required(ErrorMessage = "Informe a descrição da função")]
        public string Descricao { get; set; }
    }
}
