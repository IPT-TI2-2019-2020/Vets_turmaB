using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vets.Models {
   /// <summary>
   /// Descrição das Consultas executadas por um Veteriário a um Animal
   /// </summary>
   public class Consultas {

      [Key]
      public int ID { get; set; }

      public DateTime Data { get; set; }

      public string Observacoes { get; set; }


      // FK para Animais
      [ForeignKey(nameof(Animal))]
      public int AnimalFK { get; set; }   // Consulta ---> Animal
      public virtual Animais Animal { get; set; }

      // FK para Veterinários
      [ForeignKey(nameof(Veterinario))]
      public int VeterinarioFK { get; set; }  // Consulta ---> Veterinário
      public virtual Veterinarios Veterinario { get; set; }

   }
}
