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

      public string Nome { get; set; }

      public string NumCedulaProf { get; set; }

      public string Fotografia { get; set; }

      // lista das Consulta a q um Veterinário está associado
      public ICollection<Consultas> Consultas { get; set; }

   }
}
