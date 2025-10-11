using Spectre.Console;
using OOPNomina.UI.Menus;

namespace OOPNomina
{
    public class MenuPrincipal
    {
        private readonly AppContext _context;
        private readonly EmpleadoMenu _empleadoMenu;
        private readonly TipoEmpleadoMenu _tipoEmpleadoMenu;
        private readonly DepartamentoMenu _departamentoMenu;
        private readonly TipoNominaMenu _tipoNominaMenu;
        private readonly NominaMenu _nominaMenu;

        public MenuPrincipal(AppContext context)
        {
            _context = context;
            _empleadoMenu = new EmpleadoMenu(context);
            _tipoEmpleadoMenu = new TipoEmpleadoMenu(context);
            _departamentoMenu = new DepartamentoMenu(context);
            _tipoNominaMenu = new TipoNominaMenu(context);
            _nominaMenu = new NominaMenu(context);
        }

        public void Mostrar()
        {
            while (true)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(new FigletText("Sistema de Nomina").Centered().Color(Color.Blue));

                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Gestionar Empleados",
                            "Gestionar Tipos de Empleado",
                            "Gestionar Departamentos",
                            "Gestionar Tipos de Nomina",
                            "Crear Nomina",
                            "Calcular Nomina General",
                            "Imprimir Nomina",
                            "Eliminar Nomina",
                            "Salir"
                        }));

                switch (opcion)
                {
                    case "Gestionar Empleados":
                        _empleadoMenu.Mostrar();
                        break;
                    case "Gestionar Tipos de Empleado":
                        _tipoEmpleadoMenu.Mostrar();
                        break;
                    case "Gestionar Departamentos":
                        _departamentoMenu.Mostrar();
                        break;
                    case "Gestionar Tipos de Nomina":
                        _tipoNominaMenu.Mostrar();
                        break;
                    case "Crear Nomina":
                        _nominaMenu.CrearNomina();
                        break;
                    case "Calcular Nomina General":
                        _nominaMenu.CalcularNominaGeneral();
                        break;
                    case "Imprimir Nomina":
                        _nominaMenu.ImprimirNomina();
                        break;
                    case "Eliminar Nomina":
                        _nominaMenu.EliminarNomina();
                        break;

                    case "Salir":
                        Salir();
                        return;
                }
            }
        }

        private void Salir()
        {
            AnsiConsole.Status().Spinner(Spinner.Known.Aesthetic).Start("Encriptando datos, nahhhh mentira jajaja...", ctx =>
            {
                System.Threading.Thread.Sleep(4000);
            });
        }
    }
}