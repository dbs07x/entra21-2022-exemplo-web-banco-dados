using Entra21.CSharp.ClinicaVeterinaria.Repositorio.Entidades;
using System.Data.Entity;

namespace Entra21.CSharp.ClinicaVeterinaria.Repositorio.BancoDados
{
    internal class ClinicaVeterinariaSeed
        : DropCreateDatabaseAlways<ClinicaVeterinariaContexto>
    {
        protected override void Seed(ClinicaVeterinariaContexto context)
        {
            var raca = new Raca
            {
                Nome = "Frajola",
                Especie = "Gato"
            };
            var raca2 = new Raca
            {
                Nome = "Piupiu",
                Especie = "Capivara"
            };

            context.Racas.AddRange(raca, raca2);

            base.Seed(context);
        }
    }
}
