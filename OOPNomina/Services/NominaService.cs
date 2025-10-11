using OOPNomina.Models;

namespace OOPNomina.Services
{
    public class NominaService(AppContext context)
    {
        private readonly AppContext _context = context;

        public DetalleNomina CrearNomina(Persona persona, DateTime fecha, string descripcion)
        {
            var tipo = _context.TiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
            var depto = _context.Departamentos.First(d => d.Id == persona.DepartamentoId);
            var tipoNomina = _context.TiposNomina.First(n => n.Id == persona.TipoNominaId);

            var nomina = new DetalleNomina
            {
                Fecha = fecha,
                Descripcion = descripcion,
                PersonaId = persona.Id
            };

            nomina.CalcularNomina(persona, tipo, depto, tipoNomina);
            _context.Nominas.Add(nomina);

            return nomina;
        }

        public List<DetalleNomina> CalcularNominaGeneral(DateTime fecha)
        {
            var nominasCreadas = new List<DetalleNomina>();

            foreach (var persona in _context.Personas)
            {
                var nomina = CrearNomina(persona, fecha, $"Nomina General - {fecha:yyyy-MM-dd}");
                nominasCreadas.Add(nomina);
            }

            return nominasCreadas;
        }

        public static decimal CalcularTotalNeto(List<DetalleNomina> nominas)
        {
            return nominas.Sum(n => n.SalarioNeto);
        }
    }
}