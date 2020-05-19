using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vets.Data;
using Vets.Models;

namespace Vets.Controllers {
   public class VeterinariosController : Controller {

      /// <summary>
      /// variável que identifica a BD do nosso projeto
      /// </summary>
      private readonly VetsDB _context;

      /// <summary>
      /// variável que contém os dados do 'ambiente' do servidor. 
      /// Em particular, onde estão os ficheiros guardados, no disco rígido do servidor
      /// </summary>
      private readonly IWebHostEnvironment _caminho;


      public VeterinariosController(VetsDB context, IWebHostEnvironment caminho) {
         _context = context;
         _caminho = caminho;
      }

      // GET: Veterinarios
      public async Task<IActionResult> Index() {
         return View(await _context.Veterinarios.ToListAsync());
      }




      // GET: Veterinarios/Details/5
      /// <summary>
      /// Mostra os detalhes de um Veterinário.
      /// Se houverem, mostra os detalhes das consultas associadas a ele
      /// Pesquisa feita em modo 'Eager Loading'
      /// </summary>
      /// <param name="id">Identificador do Veterinário</param>
      /// <returns></returns>
      public async Task<IActionResult> Details(int? id) {
         if (id == null) {
            return NotFound();
         }

         // em SQL, a pesquisa seria esta:
         // SELECT *
         // FROM Veterinarios v, Animais a, Donos d, Consultas c
         // WHERE c.AnimalFK = a.ID AND
         //       c.VeterinarioFK = v.ID AND
         //       a.DonoFK = d.ID AND
         //       v.ID = id

         // acesso aos dados em modo 'Eager Loading'
         var veterinario = await _context.Veterinarios
                                         .Include(v => v.Consultas)
                                         .ThenInclude(a => a.Animal)
                                         .ThenInclude(d => d.Dono)
                                         .FirstOrDefaultAsync(v => v.ID == id);

         if (veterinario == null) {
            return NotFound();
         }

         return View(veterinario);
      }


      // GET: Veterinarios/Details2/5
      /// <summary>
      /// Mostra os detalhes de um Veterinário.
      /// Se houverem, mostra os detalhes das consultas associadas a ele
      /// Pesquisa feita em modo 'Lazy Loading'
      /// </summary>
      /// <param name="id">Identificador do Veterinário</param>
      /// <returns></returns>
      public async Task<IActionResult> Details2(int? id) {
         if (id == null) {
            return NotFound();
         }

         // em SQL, a pesquisa seria esta:
         // SELECT *
         // FROM Veterinarios v
         // WHERE v.ID = id

         // acesso aos dados em modo 'Lazy Loading'
         var veterinario = await _context.Veterinarios.FirstOrDefaultAsync(v => v.ID == id);
         // necessário adicionar o termo 'virtual' aos relacionamentos
         //
         // necessário adicionar um novo pacote (package)
         // Install-Package Microsoft.EntityFrameworkCore.Proxies
         //
         // dar ordem ao programa para usar este serviço
         // no ficheiro 'startup.cs' adicionar esta funcionalidade à BD
         //      services.AddDbContext<VetsDB>(options => options
         //                                                   .UseSqlServer(Configuration.GetConnectionString("ConnectionDB"))
         //                                                   .UseLazyLoadingProxies()  // ativamos a opção do Lazy Loading
         //      );
         // https://docs.microsoft.com/en-us/ef/core/querying/related-data



         if (veterinario == null) {
            return NotFound();
         }

         return View(veterinario);
      }




      // GET: Veterinarios/Create
      public IActionResult Create() {
         return View();
      }



      // POST: Veterinarios/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("ID,Nome,NumCedulaProf,Fotografia")] Veterinarios veterinario, IFormFile fotoVet) {
         // **************************************
         // processar a fotografia
         // **************************************
         // vars. auxiliares
         string caminhoCompleto = "";
         bool haImagem = false;

         // será q há fotografia?
         //    - uma hipótese possível, seria reenviar os dados para a View e solicitar a adição da imagem
         //    - outra hipótese, será associar ao veterinário uma fotografia 'por defeito'
         if (fotoVet == null) { veterinario.Fotografia = "noVet.png"; }
         else {
            // há ficheiro
            // será o ficheiro uma imagem?
            if (fotoVet.ContentType == "image/jpeg" ||
                fotoVet.ContentType == "image/png") {
               // o ficheiro é uma imagem válida
               // preparar a imagem para ser guardada no disco rígido
               // e o seu nome associado ao Veterinario
               Guid g;
               g = Guid.NewGuid();
               string extensao = Path.GetExtension(fotoVet.FileName).ToLower();
               string nome = g.ToString() + extensao;
               // onde guardar o ficheiro
               caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens\\Vets", nome);
               // associar o nome do ficheiro ao Veterinário
               veterinario.Fotografia = nome;
               // assinalar que existe imagem e é preciso guardá-la no disco rígido
               haImagem = true;
            }
            else {
               // há imagem, mas não é do tipo correto
               veterinario.Fotografia = "noVet.png";
            }
         }

         if (ModelState.IsValid) {
            try {
               _context.Add(veterinario);
               await _context.SaveChangesAsync();
               // se há imagem, vou guardá-la no disco rígido
               if (haImagem) {
                  using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                  await fotoVet.CopyToAsync(stream);
               }
               return RedirectToAction(nameof(Index));
            }
            catch (Exception) {
               // se chegar aqui, é pq alguma coisa correu mesmo mal...
               // o que fazer?
               // opções a realizar (todas, ou apenas uma...):
               //   - escrever, no disco do servidor, um log com o erro
               //   - escrever numa tabela de Erros, na BD, o log do erro
               //   - enviar o modelo de volta para a View
               //   - se o erro for corrigível, corrigir o erro
            }
         }
         return View(veterinario);
      }

      // GET: Veterinarios/Edit/5
      public async Task<IActionResult> Edit(int? id) {
         if (id == null) {
            return NotFound();
         }

         var veterinarios = await _context.Veterinarios.FindAsync(id);
         if (veterinarios == null) {
            return NotFound();
         }
         return View(veterinarios);
      }

      // POST: Veterinarios/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,NumCedulaProf,Fotografia")] Veterinarios veterinarios) {
         if (id != veterinarios.ID) {
            return NotFound();
         }

         if (ModelState.IsValid) {
            try {
               _context.Update(veterinarios);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
               if (!VeterinariosExists(veterinarios.ID)) {
                  return NotFound();
               }
               else {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(veterinarios);
      }

      // GET: Veterinarios/Delete/5
      public async Task<IActionResult> Delete(int? id) {
         if (id == null) {
            return NotFound();
         }

         var veterinarios = await _context.Veterinarios
             .FirstOrDefaultAsync(m => m.ID == id);
         if (veterinarios == null) {
            return NotFound();
         }

         return View(veterinarios);
      }

      // POST: Veterinarios/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id) {
         var veterinarios = await _context.Veterinarios.FindAsync(id);
         _context.Veterinarios.Remove(veterinarios);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool VeterinariosExists(int id) {
         return _context.Veterinarios.Any(e => e.ID == id);
      }
   }
}
