using Spectre.Console;
using OOPNomina.Models;

namespace OOPNomina.UI.Menus
{
    public class DepartamentoMenu(AppContext context)
    {
        private readonly AppContext _context = context;

        public void Mostrar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE DEPARTAMENTOS[/]")
                        .AddChoices([
                            "Registrar Departamento",
                            "Listar Departamentos",
                            "Volver"
                        ]));

                switch (opcion)
                {
                    case "Registrar Departamento":
                        Registrar();
                        break;
                    case "Listar Departamentos":
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
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO DEPARTAMENTO[/]");

            var depto = new DepartamentoEmpleado
            {
                Nombre = AnsiConsole.Ask<string>("Nombre del departamento:"),
                BonificacionPorcentaje = AnsiConsole.Ask<decimal>("Porcentaje de bonificacion:")
            };

            _context.Departamentos.Add(depto);

            AnsiConsole.MarkupLine("[green]Departamento registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        private void Listar()
        {
            AnsiConsole.Clear();

            if (_context.Departamentos.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay departamentos registrados[/]");
            }
            else
            {
                var tabla = new Table();
                tabla.AddColumn("ID");
                tabla.AddColumn("Nombre");
                tabla.AddColumn("Bonificacion");

                foreach (var depto in _context.Departamentos)
                {
                    tabla.AddRow(
                        depto.Id.ToString(),
                        depto.Nombre,
                        depto.BonificacionPorcentaje.ToString() + "%"
                    );
                }

                AnsiConsole.Write(tabla);
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }
    }
}