using OOPNomina.Models;

#pragma warning disable IDE0130 // No me toques las pelo...
namespace OOPNomina
{
    public class AppContext
    {
        public List<Persona> Personas { get; } = [];
        public List<TipoEmpleado> TiposEmpleado { get; } = [];
        public List<DepartamentoEmpleado> Departamentos { get; } = [];
        public List<TipoNomina> TiposNomina { get; } = [];
        public List<DetalleNomina> Nominas { get; } = [];

        public AppContext()
        {
            CargarDatosMock();
        }

        private void CargarDatosMock()
        {
            TiposEmpleado.Add(new TipoEmpleado { Nombre = "Tiempo Completo", FactorSalario = 1.0m });
            TiposEmpleado.Add(new TipoEmpleado { Nombre = "Medio Tiempo", FactorSalario = 0.5m });
            TiposEmpleado.Add(new TipoEmpleado { Nombre = "Contrato", FactorSalario = 1.2m });

            Departamentos.Add(new DepartamentoEmpleado { Nombre = "Ventas", BonificacionPorcentaje = 10 });
            Departamentos.Add(new DepartamentoEmpleado { Nombre = "Tecnologia", BonificacionPorcentaje = 15 });
            Departamentos.Add(new DepartamentoEmpleado { Nombre = "Recursos Humanos", BonificacionPorcentaje = 5 });
            Departamentos.Add(new DepartamentoEmpleado { Nombre = "Administracion", BonificacionPorcentaje = 8 });

            TiposNomina.Add(new TipoNomina { FrecuenciaPago = "Quincenal", MetodoPago = "Transferencia", DiasPorPeriodo = 15 });
            TiposNomina.Add(new TipoNomina { FrecuenciaPago = "Mensual", MetodoPago = "Transferencia", DiasPorPeriodo = 30 });
            TiposNomina.Add(new TipoNomina { FrecuenciaPago = "Quincenal", MetodoPago = "Cheque", DiasPorPeriodo = 15 });

            Personas.Add(new Persona
            {
                Nombre = "Juan",
                Apellido = "Soto",
                Cedula = "402-62346363-8",
                SalarioBase = 45000,
                TipoEmpleadoId = 1,
                DepartamentoId = 2,
                TipoNominaId = 1
            });

            Personas.Add(new Persona
            {
                Nombre = "Leonel",
                Apellido = "Abinader",
                Cedula = "002-7654321-9",
                SalarioBase = 105000,
                TipoEmpleadoId = 1,
                DepartamentoId = 1,
                TipoNominaId = 2
            });

            Personas.Add(new Persona
            {
                Nombre = "Terry A",
                Apellido = "Davis",
                Cedula = "402-0000130-9",
                SalarioBase = 5000000,
                TipoEmpleadoId = 2,
                DepartamentoId = 2,
                TipoNominaId = 1
            });
        }
    }
}