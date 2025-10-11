using Spectre.Console;

namespace OOPNomina
{
    class Program
    {
        static void Main()
        {
            var context = new AppContext();
            var menu = new MenuPrincipal(context);
            menu.Mostrar();
        }
    }
}