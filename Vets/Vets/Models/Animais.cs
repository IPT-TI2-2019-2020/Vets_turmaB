using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vets.Models {
   public class Animais {

      public Animais() {
         ListaConsultas = new HashSet<Consultas>();
      }


      [Key]
      public int ID { get; set; }

      public string Nome { get; set; }

      public string Especie { get; set; }

      public string Raca { get; set; }

      public double Peso { get; set; }

      public string Foto { get; set; }


      // FK para a 'tabela' dos Donos
      [ForeignKey("Dono")]
      public int DonoFK { get; set; }
      public Donos Dono { get; set; }

      // lista de consultas a que o Animal está associado
      public ICollection<Consultas> ListaConsultas { get; set; }
  
   }
}
