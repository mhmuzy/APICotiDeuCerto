using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações

namespace Projeto.Services.Models
{
    public class FuncionarioCadastroModel
    {
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
