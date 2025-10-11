using Spectre.Console;
using OOPNomina.Services;
using OOPNomina.UI.Helpers;
using OOPNomina.Models;

namespace OOPNomina.UI.Menus
{
    public class NominaMenu
    {
        private readonly AppContext _context;
        private readonly NominaService _nominaService;

        public NominaMenu(AppContext context)
        {
            _context = context;
            _nominaService = new NominaService(context);
        }

        public void CrearNomina()
        {
            AnsiConsole.Clear();

            if (_context.Personas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            AnsiConsole.MarkupLine("[bold yellow]CREAR NUEVA NOMINA[/]");

            var fecha = AnsiConsole.Ask("Fecha (YYYY-MM-DD):", DateTime.Now.Date);
            var descripcion = AnsiConsole.Ask<string>("Descripcion:", "N/A");

            var persona = AnsiConsole.Prompt(
                new SelectionPrompt<Persona>()
                    .Title("Seleccione el empleado:")
                    .UseConverter(p => $"{p.Id}. {p.NombreCompleto} - {p.Cedula}")
                    .AddChoices(_context.Personas));

            var nomina = _nominaService.CrearNomina(persona, fecha, descripcion);
            var tipoNomina = _context.TiposNomina.First(n => n.Id == persona.TipoNominaId);

            AnsiConsole.MarkupLine($"\n[green]Nomina creada exitosamente![/]");
            AnsiConsole.MarkupLine($"ID de Nomina: {nomina.Id}");
            AnsiConsole.MarkupLine($"Tipo de Nomina: {tipoNomina.Descripcion}");
            AnsiConsole.MarkupLine($"Salario Neto: {nomina.SalarioNeto:C}");

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        public void CalcularNominaGeneral()
        {
            AnsiConsole.Clear();

            if (_context.Personas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            AnsiConsole.MarkupLine("[bold yellow]CALCULO DE NOMINA GENERAL[/]\n");

            var fecha = AnsiConsole.Ask("Fecha para todas las nominas (YYYY-MM-DD):", DateTime.Now.Date);

            var nominasCreadas = _nominaService.CalcularNominaGeneral(fecha);
            var totalNeto = NominaService.CalcularTotalNeto(nominasCreadas);

            AnsiConsole.MarkupLine($"[green]Se crearon {nominasCreadas.Count} nominas exitosamente![/]\n");

            foreach (var nomina in nominasCreadas)
            {
                var persona = _context.Personas.First(p => p.Id == nomina.PersonaId);
                var tipo = _context.TiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
                var depto = _context.Departamentos.First(d => d.Id == persona.DepartamentoId);
                var tipoNomina = _context.TiposNomina.First(n => n.Id == persona.TipoNominaId);

                NominaDisplayHelper.MostrarDetalleNominaCompacto(nomina, persona, tipo, depto, tipoNomina);
                AnsiConsole.WriteLine();
            }

            AnsiConsole.MarkupLine($"[bold green]═══════════════════════════════════════════════[/]");
            AnsiConsole.MarkupLine($"[bold green]Total Nomina General: {totalNeto:C}[/]");
            AnsiConsole.MarkupLine($"[bold green]═══════════════════════════════════════════════[/]");

            AnsiConsole.Prompt(new TextPrompt<string>("\nPresione Enter para continuar...").AllowEmpty());
        }

        public void ImprimirNomina()
        {
            AnsiConsole.Clear();

            if (_context.Nominas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay nominas registradas[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            var nomina = AnsiConsole.Prompt(
                new SelectionPrompt<DetalleNomina>()
                    .Title("Seleccione la nomina a imprimir:")
                    .UseConverter(n =>
                    {
                        var persona = _context.Personas.First(p => p.Id == n.PersonaId);
                        var nombreCompleto = persona?.NombreCompleto ?? "N/A";
                        return $"ID: {n.Id} - {nombreCompleto} - Fecha: {n.Fecha:yyyy-MM-dd} - {n.Descripcion}";
                    })
                    .AddChoices(_context.Nominas));

            var persona = _context.Personas.First(p => p.Id == nomina.PersonaId);
            var tipo = _context.TiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
            var depto = _context.Departamentos.First(d => d.Id == persona.DepartamentoId);
            var tipoNomina = _context.TiposNomina.First(n => n.Id == persona.TipoNominaId);

            NominaDisplayHelper.MostrarDetalleNomina(nomina, persona, tipo, depto, tipoNomina);

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        public void EliminarNomina()
        {
            AnsiConsole.Clear();
            if (_context.Nominas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay nominas registradas[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }
            var nomina = AnsiConsole.Prompt(
                new SelectionPrompt<DetalleNomina>()
                    .Title("Seleccione la nomina a eliminar:")
                    .UseConverter(n =>
                    {
                        var persona = _context.Personas.First(p => p.Id == n.PersonaId);
                        var nombreCompleto = persona?.NombreCompleto ?? "N/A";
                        return $"ID: {n.Id} - {nombreCompleto} - Fecha: {n.Fecha:yyyy-MM-dd} - {n.Descripcion}";
                    })
                    .AddChoices(_context.Nominas));
            if (AnsiConsole.Confirm($"Esta seguro que desea eliminar la nomina ID {nomina.Id}?"))
            {
                _context.Nominas.Remove(nomina);
                AnsiConsole.MarkupLine("[green]Nomina eliminada.[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Operacion cancelada.[/]");
            }
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }
    }
}