using OOPNomina.Models;
using Spectre.Console;

namespace OOPNomina.UI.Helpers
{
    public static class TableHelper
    {
        public static void MostrarTiposEmpleado(List<TipoEmpleado> tiposEmpleado)
        {
            if (tiposEmpleado.Count > 0)
            {
                AnsiConsole.MarkupLine("\n[bold cyan]Tipos de Empleado Disponibles:[/]");
                var tabla = new Table();
                tabla.AddColumn("ID");
                tabla.AddColumn("Nombre");
                tabla.AddColumn("Factor");

                foreach (var tipo in tiposEmpleado)
                {
                    tabla.AddRow(
                        tipo.Id.ToString(),
                        tipo.Nombre,
                        tipo.FactorSalario.ToString("P0")
                    );
                }
                AnsiConsole.Write(tabla);
            }
        }

        public static void MostrarDepartamentos(List<DepartamentoEmpleado> departamentos)
        {
            if (departamentos.Count > 0)
            {
                AnsiConsole.MarkupLine("\n[bold cyan]Departamentos Disponibles:[/]");
                var tabla = new Table();
                tabla.AddColumn("ID");
                tabla.AddColumn("Nombre");
                tabla.AddColumn("Bonificacion");

                foreach (var depto in departamentos)
                {
                    tabla.AddRow(
                        depto.Id.ToString(),
                        depto.Nombre,
                        depto.BonificacionPorcentaje.ToString() + "%"
                    );
                }
                AnsiConsole.Write(tabla);
            }
        }

        public static void MostrarTiposNomina(List<TipoNomina> tiposNomina)
        {
            if (tiposNomina.Count > 0)
            {
                AnsiConsole.MarkupLine("\n[bold cyan]Tipos de Nomina Disponibles:[/]");
                var tabla = new Table();
                tabla.AddColumn("ID");
                tabla.AddColumn("Frecuencia");
                tabla.AddColumn("Metodo");
                tabla.AddColumn("Dias");

                foreach (var tipo in tiposNomina)
                {
                    tabla.AddRow(
                        tipo.Id.ToString(),
                        tipo.FrecuenciaPago,
                        tipo.MetodoPago,
                        tipo.DiasPorPeriodo.ToString()
                    );
                }
                AnsiConsole.Write(tabla);
            }
        }
    }
}