using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using Vets.Data;

namespace Vets.Areas.Identity.Pages.Account {
   [AllowAnonymous]
   public class RegisterModel : PageModel {
      private readonly SignInManager<ApplicationUser> _signInManager;
      private readonly UserManager<ApplicationUser> _userManager;
      private readonly ILogger<RegisterModel> _logger;
      private readonly IEmailSender _emailSender;

      /// <summary>
      /// variável que contém os dados do 'ambiente' do servidor. 
      /// Em particular, onde estão os ficheiros guardados, no disco rígido do servidor
      /// </summary>
      private readonly IWebHostEnvironment _caminho;

      public RegisterModel(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ILogger<RegisterModel> logger,
          IEmailSender emailSender,
          IWebHostEnvironment caminho) {
         _userManager = userManager;
         _signInManager = signInManager;
         _logger = logger;
         _emailSender = emailSender;
         _caminho = caminho;
      }

      [BindProperty]
      public InputModel Input { get; set; }

      public string ReturnUrl { get; set; }

      public IList<AuthenticationScheme> ExternalLogins { get; set; }


      /// <summary>
      /// esta classe identifica os dados que estão a ser recolhidos na interface de 'Registo'
      /// </summary>
      public class InputModel {
         [Required(ErrorMessage = "O {0} é de preeenchimento obrigatório")]
         [EmailAddress(ErrorMessage = "O {0} está mal escrito...")]
         [Display(Name = "Email")]
         public string Email { get; set; }

         [Required]
         [StringLength(100, ErrorMessage = "A {0} deve ter entre {2} e {1} carateres de tamanho.", MinimumLength = 6)]
         [DataType(DataType.Password)]
         [Display(Name = "Password")]
         public string Password { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Confirm password")]
         [Compare("Password", ErrorMessage = "A password e a sua confirmação não correspondem.")]
         public string ConfirmPassword { get; set; }

         //************************************************************************************************

         /// <summary>
         /// recolhe o NOME do utilizador que se regista
         /// </summary>
         [Required(ErrorMessage = "O Nome é de preenchimento obrigatório")]
         [StringLength(40, ErrorMessage = "O {0} não pode ter mais de {1} carateres.")]
         [RegularExpression("[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+(( | d[ao](s)? | e |-|'| d')[A-ZÓÂÍ][a-zçáéíóúàèìòùãõäëïöüâêîôûñ]+){1,3}",
                         ErrorMessage = "Deve escrever entre 2 e 4 nomes, começados por uma Maiúscula, seguidos de minúsculas.")]
         public string Nome { get; set; }

      }





      public async Task OnGetAsync(string returnUrl = null) {
         ReturnUrl = returnUrl;
         ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
      }




      /// <summary>
      /// este método reage a uma interação por HTTP POST
      /// </summary>
      /// <param name="fotoUser">Fotografia do novo Utilizador</param>
      /// <param name="returnUrl">caso exista, define para onde se reencaminha a ação do programa, após o Registo </param>
      /// <returns></returns>
      public async Task<IActionResult> OnPostAsync(IFormFile fotoUser, string returnUrl = null) {

         returnUrl = returnUrl ?? Url.Content("~/");

         //   ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


         if (ModelState.IsValid) {
            // se o InputModel é válido...

            // neste sítio, temos de adicionar o código
            // construído na criação de um novo veterinário
            // para a adição de uma fotografia
            // **************************************
            // processar a fotografia
            // **************************************
            // vars. auxiliares
            string caminhoCompleto = "";
            string nomeFoto = "";
            bool haImagem = false;

            // será q há fotografia?
            //    - uma hipótese possível, seria reenviar os dados para a View e solicitar a adição da imagem
            //    - outra hipótese, será associar ao veterinário uma fotografia 'por defeito'
            if (fotoUser == null) { nomeFoto = "noVet.png"; }
            else {
               // há ficheiro
               // será o ficheiro uma imagem?
               if (fotoUser.ContentType == "image/jpeg" ||
                   fotoUser.ContentType == "image/png") {
                  // o ficheiro é uma imagem válida
                  // preparar a imagem para ser guardada no disco rígido
                  // e o seu nome associado ao Veterinario
                  Guid g;
                  g = Guid.NewGuid();
                  string extensao = Path.GetExtension(fotoUser.FileName).ToLower();
                  string nome = g.ToString() + extensao;
                  // onde guardar o ficheiro
                  caminhoCompleto = Path.Combine(_caminho.WebRootPath, "Imagens\\Users", nome);
                  // associar o nome do ficheiro ao Veterinário
                  nomeFoto = nome;
                  // assinalar que existe imagem e é preciso guardá-la no disco rígido
                  haImagem = true;
               }
               else {
                  // há imagem, mas não é do tipo correto
                  nomeFoto = "noVet.png";
               }
            }

            // criação de um novo utilizador
            var user = new ApplicationUser {
               UserName = Input.Email,
               Email = Input.Email,
               Nome = Input.Nome,
               Fotografia = nomeFoto,
               Timestamp = DateTime.Now
            };

            // vou escrever esses dados na BD
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded) {
               // se cheguei aqui, é pq houve sucesso na escrita na BD

               // vou, agora, guardar a imagem no disco rígido do Servidor
               // se há imagem, vou guardá-la no disco rígido
               if (haImagem) {
                  using var stream = new FileStream(caminhoCompleto, FileMode.Create);
                  await fotoUser.CopyToAsync(stream);
               }



               _logger.LogInformation("User created a new account with password.");

               var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
               var callbackUrl = Url.Page(
                   "/Account/ConfirmEmail",
                   pageHandler: null,
                   values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                   protocol: Request.Scheme);

               await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                   $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

               if (_userManager.Options.SignIn.RequireConfirmedAccount) {
                  return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
               }
               else {
                  await _signInManager.SignInAsync(user, isPersistent: false);
                  return LocalRedirect(returnUrl);
               }
            }
            foreach (var error in result.Errors) {
               ModelState.AddModelError(string.Empty, error.Description);
            }
         }

         // If we got this far, something failed, redisplay form
         return Page();
      }
   }
}
