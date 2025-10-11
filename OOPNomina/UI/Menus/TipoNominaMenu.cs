using Spectre.Console;
using OOPNomina.Models;

namespace OOPNomina.UI.Menus
{
    public class TipoNominaMenu(AppContext context)
    {
        private readonly AppContext _context = context;

        public void Mostrar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE TIPOS DE NOMINA[/]")
                        .AddChoices([
                            "Registrar Tipo de Nomina",
                            "Listar Tipos de Nomina",
                            "Volver"
                        ]));

                switch (opcion)
                {
                    case "Registrar Tipo de Nomina":
                        Registrar();
                        break;
                    case "Listar Tipos de Nomina":
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
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO TIPO DE NOMINA[/]");

            var tipo = new TipoNomina
            {
                FrecuenciaPago = AnsiConsole.Ask<string>("Frecuencia de pago (Ej: Quincenal, Mensual):"),
                MetodoPago = AnsiConsole.Ask<string>("Metodo de pago (Ej: Transferencia, Cheque):"),
                DiasPorPeriodo = AnsiConsole.Ask<int>("Dias por periodo:")
            };

            _context.TiposNomina.Add(tipo);

            AnsiConsole.MarkupLine("[green]Tipo de nomina registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        private void Listar()
        {
            AnsiConsole.Clear();

            if (_context.TiposNomina.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay tipos de nomina registrados[/]");
            }
            else
            {
                var tabla = new Table();
                tabla.AddColumn("ID");
                tabla.AddColumn("Frecuencia");
                tabla.AddColumn("Metodo");
                tabla.AddColumn("Dias");

                foreach (var tipo in _context.TiposNomina)
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

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }
    }
}
