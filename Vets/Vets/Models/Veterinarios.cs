using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vets.Models {
   public class Veterinarios {

      public Veterinarios() {
         // estou a colocar dados na lista
         // na prática é como se fizesse
         // Consultas = SELECT * 
         //             FROM Consultas c, Veterinarios v 
         //             WHERE c.VeterinarioFK = v.ID AND
         //                   v.ID = ?   ;
         Consultas = new HashSet<Consultas>();
      }

      [Key]
      public int ID { get; set; }

      [Required]
      public string Nome { get; set; }

      /// <summary>
      /// Número da Cédula Profissional
      /// </summary>
      [RegularExpression("vet-[0-9]{5}")]  // "vet-34589"
      [StringLength (9)]
      [Display(Name ="Nº Cédula Profissional")]
      [Required]
      public string NumCedulaProf { get; set; }

      public string Fotografia { get; set; }

      /// <summary>
      /// lista das Consulta a q um Veterinário está associado
      /// </summary>
      public virtual ICollection<Consultas> Consultas { get; set; }

   }
}
