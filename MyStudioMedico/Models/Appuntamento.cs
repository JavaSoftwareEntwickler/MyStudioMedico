using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace MyStudioMedico.Models
{
    [Table("Appuntamenti")]
    public class Appuntamento
    {
        public int AppuntamentoID { get; set; }
       
      
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")] 
        public DateTime? Data { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

        [ForeignKey("DottoreID")]
        public int DottoreID { get; set; }

        public Dottore Dottore { get; set; }

    }
    [Authorize]
    [Table("Dottori")]
    public class Dottore
    {
        public int DottoreID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        [NotMapped]
        public string NomeDott { get; set; }

    }
}
