using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vets.Models;

namespace Vets.Data {

   /// <summary>
   /// cria a base de dados do projeto
   /// </summary>
   public class VetsDB : DbContext {


      /// <summary>
      /// Construtor da classe
      /// serve para ligar esta classe à BD
      /// </summary>
      /// <param name="options"></param>
      public VetsDB(DbContextOptions<VetsDB> options) : base(options) { }



      protected override void OnModelCreating(ModelBuilder modelBuilder) {

         base.OnModelCreating(modelBuilder);


         // insert DB seed
         modelBuilder.Entity<Donos>().HasData(
            new Donos { ID = 1, Nome = "Luís Freitas", Sexo = "M", NIF = "813635582" },
            new Donos { ID = 2, Nome = "Andreia Gomes", Sexo = "F", NIF = "854613462" },
            new Donos { ID = 3, Nome = "Cristina Sousa", Sexo = "F", NIF = "265368715" },
            new Donos { ID = 4, Nome = "Sónia Rosa", Sexo = "F", NIF = "835623190" },
            new Donos { ID = 5, Nome = "António Santos", Sexo = "M", NIF = "751512205" },
            new Donos { ID = 6, Nome = "Gustavo Alves", Sexo = "M", NIF = "728663835" },
            new Donos { ID = 7, Nome = "Rosa Vieira", Sexo = "F", NIF = "644388118" },
            new Donos { ID = 8, Nome = "Daniel Dias", Sexo = "M", NIF = "262618487" },
            new Donos { ID = 9, Nome = "Tânia Gomes", Sexo = "F", NIF = "842615197" },
            new Donos { ID = 10, Nome = "Andreia Correia", Sexo = "F", NIF = "635139506" }
             );

         modelBuilder.Entity<Veterinarios>().HasData(
            new Veterinarios { ID = 1, Nome = "Maria Pinto", NumCedulaProf = "vet-34589", Fotografia = "Maria.jpg" },
            new Veterinarios { ID = 2, Nome = "Ricardo Ribeiro", NumCedulaProf = "vet-34590", Fotografia = "Ricardo.jpg" },
            new Veterinarios { ID = 3, Nome = "José Soares", NumCedulaProf = "vet-56732", Fotografia = "Jose.jpg" }
            );

         modelBuilder.Entity<Animais>().HasData(
            new Animais { ID = 1, Nome = "Bubi", Especie = "Cão", Raca = "Pastor Alemão", Peso = 24, Foto = "animal1.jpg", DonoFK = 1 },
            new Animais { ID = 10, Nome = "Saltitão", Especie = "Gato", Raca = "Persa", Peso = 2, Foto = "animal10.jpg", DonoFK = 1 },
            new Animais { ID = 3, Nome = "Tripé", Especie = "Cão", Raca = "Serra Estrela", Peso = 4, Foto = "animal3.jpg", DonoFK = 2 },
            new Animais { ID = 2, Nome = "Pastor", Especie = "Cão", Raca = "Serra Estrela", Peso = 50, Foto = "animal2.jpg", DonoFK = 3 },
            new Animais { ID = 4, Nome = "Saltador", Especie = "Cavalo", Raca = "Lusitana", Peso = 580, Foto = "animal4.jpg", DonoFK = 3 },
            new Animais { ID = 5, Nome = "Tareco", Especie = "Gato", Raca = "siamês", Peso = 1, Foto = "animal5.jpg", DonoFK = 3 },
            new Animais { ID = 8, Nome = "Forte", Especie = "Cão", Raca = "Rottweiler", Peso = 20, Foto = "animal8.jpg", DonoFK = 7 },
            new Animais { ID = 9, Nome = "Castanho", Especie = "Vaca", Raca = "Mirandesa", Peso = 652, Foto = "animal9.jpg", DonoFK = 8 },
            new Animais { ID = 6, Nome = "Cusca", Especie = "Cão", Raca = "Labrador", Peso = 45, Foto = "animal6.jpg", DonoFK = 9 },
            new Animais { ID = 7, Nome = "Morde Tudo", Especie = "Cão", Raca = "Dobermann", Peso = 39, Foto = "animal7.jpg", DonoFK = 10 }
            );

         modelBuilder.Entity<Consultas>().HasData(
            new Consultas { ID = 1, Data = new DateTime(2020, 2, 2).Date, AnimalFK = 1, VeterinarioFK = 1 },
            new Consultas { ID = 2, Data = new DateTime(2020, 2, 2).Date, AnimalFK = 3, VeterinarioFK = 1 },
            new Consultas { ID = 3, Data = new DateTime(2020, 2, 2).Date, AnimalFK = 2, VeterinarioFK = 1 },
            new Consultas { ID = 4, Data = new DateTime(2020, 2, 3).Date, AnimalFK = 4, VeterinarioFK = 2 },
            new Consultas { ID = 5, Data = new DateTime(2020, 2, 3).Date, AnimalFK = 9, VeterinarioFK = 2 },
            new Consultas { ID = 6, Data = new DateTime(2020, 2, 4).Date, AnimalFK = 5, VeterinarioFK = 3 },
            new Consultas { ID = 7, Data = new DateTime(2020, 2, 4).Date, AnimalFK = 6, VeterinarioFK = 3 },
            new Consultas { ID = 8, Data = new DateTime(2020, 2, 4).Date, AnimalFK = 7, VeterinarioFK = 3 },
            new Consultas { ID = 9, Data = new DateTime(2020, 2, 4).Date, AnimalFK = 8, VeterinarioFK = 3 },
            new Consultas { ID = 10, Data = new DateTime(2020, 2, 4).Date, AnimalFK = 9, VeterinarioFK = 2 },
            new Consultas { ID = 11, Data = new DateTime(2020, 2, 5).Date, AnimalFK = 10, VeterinarioFK = 1 },
            new Consultas { ID = 12, Data = new DateTime(2020, 2, 5).Date, AnimalFK = 7, VeterinarioFK = 1 },
            new Consultas { ID = 13, Data = new DateTime(2020, 2, 5).Date, AnimalFK = 1, VeterinarioFK = 1 },
            new Consultas { ID = 14, Data = new DateTime(2020, 2, 5).Date, AnimalFK = 2, VeterinarioFK = 1 },
            new Consultas { ID = 15, Data = new DateTime(2020, 2, 5).Date, AnimalFK = 5, VeterinarioFK = 1 },
            new Consultas { ID = 16, Data = new DateTime(2020, 2, 5).Date, AnimalFK = 6, VeterinarioFK = 1 },
            new Consultas { ID = 17, Data = new DateTime(2020, 2, 5).Date, AnimalFK = 9, VeterinarioFK = 2 },
            new Consultas { ID = 18, Data = new DateTime(2020, 2, 6).Date, AnimalFK = 1, VeterinarioFK = 3 },
            new Consultas { ID = 19, Data = new DateTime(2020, 2, 7).Date, AnimalFK = 10, VeterinarioFK = 1 },
            new Consultas { ID = 20, Data = new DateTime(2020, 2, 7).Date, AnimalFK = 5, VeterinarioFK = 1 }
            );
      }



      // adicionar as 'tabelas' à BD
      public virtual DbSet<Animais> Animais { get; set; }
      public virtual DbSet<Donos> Donos { get; set; }
      public virtual DbSet<Veterinarios> Veterinarios { get; set; }
      public virtual DbSet<Consultas> Consultas { get; set; }


   }


}
