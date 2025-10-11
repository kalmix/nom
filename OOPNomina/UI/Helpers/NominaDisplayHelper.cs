using OOPNomina.Models;
using Spectre.Console;

namespace OOPNomina.UI.Helpers
{
    public static class NominaDisplayHelper
    {
        public static void MostrarDetalleNomina(DetalleNomina nomina, Persona persona,
            TipoEmpleado tipo, DepartamentoEmpleado depto, TipoNomina tipoNomina)
        {
            var panel = new Panel(new Markup(
                $"[bold yellow]DETALLE DE NOMINA[/]\n\n" +
                $"[cyan]ID Nomina:[/] {nomina.Id}\n" +
                $"[cyan]Fecha:[/] {nomina.Fecha:yyyy-MM-dd}\n" +
                $"[cyan]Descripcion:[/] {nomina.Descripcion}\n\n" +
                $"[bold green]DATOS DEL EMPLEADO[/]\n" +
                $"[cyan]Nombre:[/] {persona.NombreCompleto}\n" +
                $"[cyan]Cedula:[/] {persona.Cedula}\n" +
                $"[cyan]Departamento:[/] {depto.Nombre}\n" +
                $"[cyan]Tipo de Empleado:[/] {tipo.Nombre}\n\n" +
                $"[bold green]DETALLES DE PAGO[/]\n" +
                $"[cyan]Tipo de Nomina:[/] {tipoNomina.Descripcion}\n" +
                $"[cyan]Frecuencia de Pago:[/] {tipoNomina.FrecuenciaPago}\n" +
                $"[cyan]Metodo de Pago:[/] {tipoNomina.MetodoPago}\n\n" +
                $"[bold green]CALCULOS[/]\n" +
                $"[cyan]Salario Base:[/] {nomina.SalarioBase:C}\n" +
                $"[cyan]Salario Ajustado:[/] {nomina.SalarioAjustado:C}\n" +
                $"[cyan]Salario por Periodo ({tipoNomina.DiasPorPeriodo} dias):[/] {(nomina.SalarioAjustado / 30 * tipoNomina.DiasPorPeriodo):C}\n" +
                $"[cyan]Bonificacion ({depto.BonificacionPorcentaje}%):[/] {nomina.Bonificacion:C}\n" +
                $"[cyan]Total Bruto:[/] {nomina.TotalBruto:C}\n\n" +
                $"[bold red]DEDUCCIONES[/]\n" +
                $"[cyan]AFP (2.87%):[/] {nomina.DeduccionAFP:C}\n" +
                $"[cyan]ARS (3.04%):[/] {nomina.DeduccionARS:C}\n" +
                $"[cyan]ISR:[/] {nomina.DeduccionISR:C}\n" +
                $"[cyan]Total Deducciones:[/] {nomina.TotalDeducciones:C}\n\n" +
                $"[bold green]SALARIO NETO:[/] [bold yellow]{nomina.SalarioNeto:C}[/]"
            ));

            AnsiConsole.Write(panel);
        }

        public static void MostrarDetalleNominaCompacto(DetalleNomina nomina, Persona persona,
            TipoEmpleado tipo, DepartamentoEmpleado depto, TipoNomina tipoNomina)
        {
            var tabla = new Table();
            tabla.Border(TableBorder.Rounded);
            tabla.Title($"[bold cyan]NOMINA ID: {nomina.Id} - {persona.NombreCompleto}[/]");
            tabla.AddColumn("[bold]Concepto[/]");
            tabla.AddColumn("[bold]Detalle[/]");
            tabla.AddColumn("[bold]Monto[/]");

            tabla.AddRow("Cedula", persona.Cedula, "");
            tabla.AddRow("Departamento", depto.Nombre, "");
            tabla.AddRow("Tipo Empleado", tipo.Nombre, "");
            tabla.AddRow("Tipo Nomina", tipoNomina.Descripcion, "");
            tabla.AddEmptyRow();

            tabla.AddRow("[cyan]Salario Base[/]", "", $"[cyan]{nomina.SalarioBase:C}[/]");
            tabla.AddRow("[cyan]Salario Ajustado[/]", $"(Factor: {tipo.FactorSalario:P0})", $"[cyan]{nomina.SalarioAjustado:C}[/]");
            tabla.AddRow("[cyan]Salario por Periodo[/]", $"({tipoNomina.DiasPorPeriodo} dias)",
                $"[cyan]{(nomina.SalarioAjustado / 30 * tipoNomina.DiasPorPeriodo):C}[/]");
            tabla.AddRow("[cyan]Bonificacion[/]", $"({depto.BonificacionPorcentaje}%)", $"[cyan]{nomina.Bonificacion:C}[/]");
            tabla.AddRow("[bold green]Total Bruto[/]", "", $"[bold green]{nomina.TotalBruto:C}[/]");
            tabla.AddEmptyRow();

            tabla.AddRow("[red]AFP[/]", "(2.87%)", $"[red]-{nomina.DeduccionAFP:C}[/]");
            tabla.AddRow("[red]ARS[/]", "(3.04%)", $"[red]-{nomina.DeduccionARS:C}[/]");
            tabla.AddRow("[red]ISR[/]", "", $"[red]-{nomina.DeduccionISR:C}[/]");
            tabla.AddRow("[bold red]Total Deducciones[/]", "", $"[bold red]-{nomina.TotalDeducciones:C}[/]");
            tabla.AddEmptyRow();

            tabla.AddRow("[bold yellow]SALARIO NETO[/]", "", $"[bold yellow]{nomina.SalarioNeto:C}[/]");

            AnsiConsole.Write(tabla);
        }
    }
}