using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Medico
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set;}

        public Medico()
        {
            this.Id = 0;
            this.Nome = "";
            this.CRM = "";
            this.Especialidade = "";
        }
    }
}