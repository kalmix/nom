using Spectre.Console;
using OOPNomina.UI.Helpers;
using OOPNomina.Models;

namespace OOPNomina.UI.Menus
{
    public class EmpleadoMenu(AppContext context)
    {
        private readonly AppContext _context = context;

        public void Mostrar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE EMPLEADOS[/]")
                        .AddChoices([
                            "Registrar Empleado",
                            "Listar Empleados",
                            "Editar Empleado",
                            "Eliminar Empleado",
                            "Volver"
                        ]));

                switch (opcion)
                {
                    case "Registrar Empleado":
                        Registrar();
                        break;
                    case "Listar Empleados":
                        Listar();
                        break;
                    case "Editar Empleado":
                        Editar();
                        break;
                    case "Eliminar Empleado":
                        Eliminar();
                        break;
                    case "Volver":
                        return;
                }
            }
        }

        private void Registrar()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO EMPLEADO[/]");

            var persona = new Persona
            {
                Nombre = AnsiConsole.Ask<string>("Nombre:"),
                Apellido = AnsiConsole.Ask<string>("Apellido:"),
                Cedula = AnsiConsole.Ask<string>("Cedula:"),
                SalarioBase = AnsiConsole.Ask<decimal>("Salario Base:")
            };

            TableHelper.MostrarTiposEmpleado(_context.TiposEmpleado);
            var tipoEmpleado = AnsiConsole.Prompt(
                new SelectionPrompt<TipoEmpleado>()
                    .Title("Seleccione el tipo de empleado:")
                    .UseConverter(t => $"{t.Id}. {t.Nombre} (Factor: {t.FactorSalario:P0})")
                    .AddChoices(_context.TiposEmpleado));
            persona.TipoEmpleadoId = tipoEmpleado.Id;

            TableHelper.MostrarDepartamentos(_context.Departamentos);
            var departamento = AnsiConsole.Prompt(
                new SelectionPrompt<DepartamentoEmpleado>()
                    .Title("Seleccione el departamento:")
                    .UseConverter(d => $"{d.Id}. {d.Nombre} (Bonificacion: {d.BonificacionPorcentaje}%)")
                    .AddChoices(_context.Departamentos));
            persona.DepartamentoId = departamento.Id;

            TableHelper.MostrarTiposNomina(_context.TiposNomina);
            var tipoNomina = AnsiConsole.Prompt(
                new SelectionPrompt<TipoNomina>()
                    .Title("Seleccione el tipo de nomina:")
                    .UseConverter(t => $"{t.Id}. {t.Descripcion}")
                    .AddChoices(_context.TiposNomina));
            persona.TipoNominaId = tipoNomina.Id;

            _context.Personas.Add(persona);

            AnsiConsole.MarkupLine("[green]Empleado registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        private void Listar()
        {
            AnsiConsole.Clear();

            if (_context.Personas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
            }
            else
            {
                var tabla = new Table();
                tabla.Border(TableBorder.Rounded);
                tabla.AddColumn("ID");
                tabla.AddColumn("Nombre");
                tabla.AddColumn("Cedula");
                tabla.AddColumn("Salario Base");
                tabla.AddColumn("Tipo");
                tabla.AddColumn("Departamento");
                tabla.AddColumn("Nomina");

                foreach (var persona in _context.Personas)
                {
                    var tipo = _context.TiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
                    var depto = _context.Departamentos.First(d => d.Id == persona.DepartamentoId);
                    var nomina = _context.TiposNomina.First(n => n.Id == persona.TipoNominaId);

                    tabla.AddRow(
                        persona.Id.ToString(),
                        persona.NombreCompleto,
                        persona.Cedula,
                        persona.SalarioBase.ToString("C"),
                        tipo?.Nombre ?? "N/A",
                        depto?.Nombre ?? "N/A",
                        nomina?.FrecuenciaPago ?? "N/A"
                    );
                }

                AnsiConsole.Write(tabla);
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        private void Editar()
        {
            AnsiConsole.Clear();

            if (_context.Personas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            var persona = AnsiConsole.Prompt(
                new SelectionPrompt<Persona>()
                    .Title("Seleccione el empleado a editar:")
                    .UseConverter(p => $"{p.Id}. {p.NombreCompleto} - {p.Cedula}")
                    .AddChoices(_context.Personas));

            persona.Nombre = AnsiConsole.Ask("Nombre:", persona.Nombre);
            persona.Apellido = AnsiConsole.Ask("Apellido:", persona.Apellido);
            persona.Cedula = AnsiConsole.Ask("Cedula:", persona.Cedula);
            persona.SalarioBase = AnsiConsole.Ask("Salario Base:", persona.SalarioBase);

            TableHelper.MostrarTiposEmpleado(_context.TiposEmpleado);
            var tipoEmpleado = AnsiConsole.Prompt(
                new SelectionPrompt<TipoEmpleado>()
                    .Title("Seleccione el tipo de empleado:")
                    .UseConverter(t => $"{t.Id}. {t.Nombre} (Factor: {t.FactorSalario:P0})")
                    .AddChoices(_context.TiposEmpleado));
            persona.TipoEmpleadoId = tipoEmpleado.Id;

            TableHelper.MostrarDepartamentos(_context.Departamentos);
            var departamento = AnsiConsole.Prompt(
                new SelectionPrompt<DepartamentoEmpleado>()
                    .Title("Seleccione el departamento:")
                    .UseConverter(d => $"{d.Id}. {d.Nombre} (Bonificacion: {d.BonificacionPorcentaje}%)")
                    .AddChoices(_context.Departamentos));
            persona.DepartamentoId = departamento.Id;

            TableHelper.MostrarTiposNomina(_context.TiposNomina);
            var tipoNomina = AnsiConsole.Prompt(
                new SelectionPrompt<TipoNomina>()
                    .Title("Seleccione el tipo de nomina:")
                    .UseConverter(t => $"{t.Id}. {t.Descripcion}")
                    .AddChoices(_context.TiposNomina));
            persona.TipoNominaId = tipoNomina.Id;

            AnsiConsole.MarkupLine("[green]Empleado actualizado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        private void Eliminar()
        {
            AnsiConsole.Clear();

            if (_context.Personas.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            var persona = AnsiConsole.Prompt(
                new SelectionPrompt<Persona>()
                    .Title("Seleccione el empleado a eliminar:")
                    .UseConverter(p => $"{p.Id}. {p.NombreCompleto} - {p.Cedula}")
                    .AddChoices(_context.Personas));

            if (AnsiConsole.Confirm($"Esta seguro de eliminar a {persona.NombreCompleto}?"))
            {
                _context.Personas.Remove(persona);
                AnsiConsole.MarkupLine("[green]Empleado eliminado exitosamente![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Operacion cancelada[/]");
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }
    }
}
