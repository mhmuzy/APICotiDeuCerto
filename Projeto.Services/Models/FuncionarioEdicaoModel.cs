using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Services.Models
{
    public class FuncionarioEdicaoModel
    {
        [Required(ErrorMessage = "Informe o id do funcionário")]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Informe o nome do funcionário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o salário do funcionário")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "Informe a data de admissão do funcionário")]
        public string DataAdmissao { get; set; }

        [Required(ErrorMessage = "Informe o setor do funcionário")]
        public int IdSetor { get; set; }

        [Required(ErrorMessage = "Informe a função do funcionário")]
        public int IdFuncao { get; set; }
    }
}
