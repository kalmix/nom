using Spectre.Console;
using OOPNomina.Models;

namespace OOPNomina.UI.Menus
{
    public class TipoEmpleadoMenu(AppContext context)
    {
        private readonly AppContext _context = context;

        public void Mostrar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE TIPOS DE EMPLEADO[/]")
                        .AddChoices([
                            "Registrar Tipo de Empleado",
                            "Listar Tipos de Empleado",
                            "Volver"
                        ]));

                switch (opcion)
                {
                    case "Registrar Tipo de Empleado":
                        Registrar();
                        break;
                    case "Listar Tipos de Empleado":
                        Listar();
                        break;
                    case "Volver":
                        return;
                }
            }
        }

        private void Registrar()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO TIPO DE EMPLEADO[/]");

            var tipo = new TipoEmpleado
            {
                Nombre = AnsiConsole.Ask<string>("Nombre del tipo:"),
                FactorSalario = AnsiConsole.Ask<decimal>("Factor de salario (1.0 = 100%):")
            };

            _context.TiposEmpleado.Add(tipo);

            AnsiConsole.MarkupLine("[green]Tipo de empleado registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        private void Listar()
        {
            AnsiConsole.Clear();

            if (_context.TiposEmpleado.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay tipos de empleado registrados[/]");
            }
            else
            {
                var tabla = new Table();
                tabla.AddColumn("ID");
                tabla.AddColumn("Nombre");
                tabla.AddColumn("Factor Salario");

                foreach (var tipo in _context.TiposEmpleado)
                {
                    tabla.AddRow(
                        tipo.Id.ToString(),
                        tipo.Nombre,
                        tipo.FactorSalario.ToString("P0")
                    );
                }

                AnsiConsole.Write(tabla);
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }
    }
}
