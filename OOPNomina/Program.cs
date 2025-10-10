using Spectre.Console;

namespace OOPNomina
{
    class Program
    {
        static List<Persona> personas = new List<Persona>();
        static List<TipoEmpleado> tiposEmpleado = new List<TipoEmpleado>();
        static List<DepartamentoEmpleado> departamentos = new List<DepartamentoEmpleado>();
        static List<TipoNomina> tiposNomina = new List<TipoNomina>();
        static List<DetalleNomina> nominas = new List<DetalleNomina>();

        static void Main()
        {
            CargarDatosMock();

            while (true)
            {
                AnsiConsole.Clear();
                //var image = new CanvasImage("C:\\Users\\jasie\\Programming\\OOPNomina\\OOPNomina\\crystal_01c.png");
                //image.MaxWidth(16);


                //AnsiConsole.Write(new Align(
                //        new Padder(image, new Padding(0, 1, 0, 0)),
                //        HorizontalAlignment.Center,
                //        VerticalAlignment.Top
                //    ));


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
                            //"Calcular Nomina Individual",
                            "Calcular Nomina General",
                            "Imprimir Nomina",
                            "Salir"
                        }));

                switch (opcion)
                {
                    case "Gestionar Empleados":
                        GestionarEmpleados();
                        break;
                    case "Gestionar Tipos de Empleado":
                        GestionarTiposEmpleado();
                        break;
                    case "Gestionar Departamentos":
                        GestionarDepartamentos();
                        break;
                    case "Gestionar Tipos de Nomina":
                        GestionarTiposNomina();
                        break;
                    case "Crear Nomina":
                        CrearNomina();
                        break;
                    //case "Calcular Nomina Individual":
                    //    CalcularNominaIndividual();
                    //    break;
                    case "Calcular Nomina General":
                        CalcularNominaGeneral();
                        break;
                    case "Imprimir Nomina":
                        ImprimirNomina();
                        break;
                    case "Salir":
                        Salir();
                        return;
                }
            }
        }

        static void CargarDatosMock()
        {
            tiposEmpleado.Add(new TipoEmpleado { Nombre = "Tiempo Completo", FactorSalario = 1.0m });
            tiposEmpleado.Add(new TipoEmpleado { Nombre = "Medio Tiempo", FactorSalario = 0.5m });
            tiposEmpleado.Add(new TipoEmpleado { Nombre = "Contrato", FactorSalario = 1.2m });

            departamentos.Add(new DepartamentoEmpleado { Nombre = "Ventas", BonificacionPorcentaje = 10 });
            departamentos.Add(new DepartamentoEmpleado { Nombre = "Tecnologia", BonificacionPorcentaje = 15 });
            departamentos.Add(new DepartamentoEmpleado { Nombre = "Recursos Humanos", BonificacionPorcentaje = 5 });
            departamentos.Add(new DepartamentoEmpleado { Nombre = "Administracion", BonificacionPorcentaje = 8 });

            tiposNomina.Add(new TipoNomina { FrecuenciaPago = "Quincenal", MetodoPago = "Transferencia", DiasPorPeriodo = 15 });
            tiposNomina.Add(new TipoNomina { FrecuenciaPago = "Mensual", MetodoPago = "Transferencia", DiasPorPeriodo = 30 });
            tiposNomina.Add(new TipoNomina { FrecuenciaPago = "Quincenal", MetodoPago = "Cheque", DiasPorPeriodo = 15 });

            personas.Add(new Persona
            {
                Nombre = "Juan",
                Apellido = "Soto",
                Cedula = "402-62346363-8",
                SalarioBase = 45000,
                TipoEmpleadoId = 1,
                DepartamentoId = 2,
                TipoNominaId = 1
            });
            


            personas.Add(new Persona
            {
                Nombre = "Leonel",
                Apellido = "Abinader",
                Cedula = "002-7654321-9",
                SalarioBase = 105000,
                TipoEmpleadoId = 1,
                DepartamentoId = 1,
                TipoNominaId = 2
            });
        }

        static void Salir()
        {
            AnsiConsole.Status().Spinner(Spinner.Known.Aesthetic).Start("Encriptando datos, nahhhh mentira jajaja...", ctx =>
            {
                System.Threading.Thread.Sleep(4000);
            });
        }

        static void GestionarEmpleados()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE EMPLEADOS[/]")
                        .AddChoices(new[] {
                            "Registrar Empleado",
                            "Listar Empleados",
                            "Editar Empleado",
                            "Eliminar Empleado",
                            "Volver"
                        }));

                switch (opcion)
                {
                    case "Registrar Empleado":
                        RegistrarEmpleado();
                        break;
                    case "Listar Empleados":
                        ListarEmpleados();
                        break;
                    case "Editar Empleado":
                        EditarEmpleado();
                        break;
                    case "Eliminar Empleado":
                        EliminarEmpleado();
                        break;
                    case "Volver":
                        return;
                }
            }
        }

        static void RegistrarEmpleado()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO EMPLEADO[/]");

            var persona = new Persona();

            persona.Nombre = AnsiConsole.Ask<string>("Nombre:");
            persona.Apellido = AnsiConsole.Ask<string>("Apellido:");
            persona.Cedula = AnsiConsole.Ask<string>("Cedula:");
            persona.SalarioBase = AnsiConsole.Ask<decimal>("Salario Base:");

            MostrarTiposEmpleado();
            var tipoEmpleado = AnsiConsole.Prompt(
                new SelectionPrompt<TipoEmpleado>()
                    .Title("Seleccione el tipo de empleado:")
                    .UseConverter(t => $"{t.Id}. {t.Nombre} (Factor: {t.FactorSalario:P0})")
                    .AddChoices(tiposEmpleado));
            persona.TipoEmpleadoId = tipoEmpleado.Id;

            MostrarDepartamentos();
            var departamento = AnsiConsole.Prompt(
                new SelectionPrompt<DepartamentoEmpleado>()
                    .Title("Seleccione el departamento:")
                    .UseConverter(d => $"{d.Id}. {d.Nombre} (Bonificacion: {d.BonificacionPorcentaje}%)")
                    .AddChoices(departamentos));
            persona.DepartamentoId = departamento.Id;

            MostrarTiposNomina();
            var tipoNomina = AnsiConsole.Prompt(
                new SelectionPrompt<TipoNomina>()
                    .Title("Seleccione el tipo de nomina:")
                    .UseConverter(t => $"{t.Id}. {t.Descripcion}")
                    .AddChoices(tiposNomina));
            persona.TipoNominaId = tipoNomina.Id;

            personas.Add(persona);

            AnsiConsole.MarkupLine("[green]Empleado registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void ListarEmpleados()
        {
            AnsiConsole.Clear();

            if (personas.Count() == 0)
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

                foreach (var persona in personas)
                {
                    var tipo = tiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
                    var depto = departamentos.First(d => d.Id == persona.DepartamentoId);
                    var nomina = tiposNomina.First(n => n.Id == persona.TipoNominaId);

                    tabla.AddRow(
                        persona.Id.ToString(),
                        persona.NombreCompleto,
                        persona.Cedula,
                        persona.SalarioBase.ToString("C"), // Formateo a moneda
                        tipo?.Nombre ?? "N/A",
                        depto?.Nombre ?? "N/A",
                        nomina?.FrecuenciaPago ?? "N/A"
                    );
                }

                AnsiConsole.Write(tabla);
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void EditarEmpleado()
        {
            AnsiConsole.Clear();

            if (personas.Count() == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            var persona = AnsiConsole.Prompt(
                new SelectionPrompt<Persona>()
                    .Title("Seleccione el empleado a editar:")
                    .UseConverter(p => $"{p.Id}. {p.NombreCompleto} - {p.Cedula}")
                    .AddChoices(personas));

            persona.Nombre = AnsiConsole.Ask("Nombre:", persona.Nombre);
            persona.Apellido = AnsiConsole.Ask("Apellido:", persona.Apellido);
            persona.Cedula = AnsiConsole.Ask("Cedula:", persona.Cedula);
            persona.SalarioBase = AnsiConsole.Ask("Salario Base:", persona.SalarioBase);

            MostrarTiposEmpleado();
            var tipoEmpleado = AnsiConsole.Prompt(
                new SelectionPrompt<TipoEmpleado>()
                    .Title("Seleccione el tipo de empleado:")
                    .UseConverter(t => $"{t.Id}. {t.Nombre} (Factor: {t.FactorSalario:P0})")
                    .AddChoices(tiposEmpleado));
            persona.TipoEmpleadoId = tipoEmpleado.Id;

            MostrarDepartamentos();
            var departamento = AnsiConsole.Prompt(
                new SelectionPrompt<DepartamentoEmpleado>()
                    .Title("Seleccione el departamento:")
                    .UseConverter(d => $"{d.Id}. {d.Nombre} (Bonificacion: {d.BonificacionPorcentaje}%)")
                    .AddChoices(departamentos));
            persona.DepartamentoId = departamento.Id;

            MostrarTiposNomina();
            var tipoNomina = AnsiConsole.Prompt(
                new SelectionPrompt<TipoNomina>()
                    .Title("Seleccione el tipo de nomina:")
                    .UseConverter(t => $"{t.Id}. {t.Descripcion}")
                    .AddChoices(tiposNomina));
            persona.TipoNominaId = tipoNomina.Id;

            AnsiConsole.MarkupLine("[green]Empleado actualizado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void EliminarEmpleado()
        {
            AnsiConsole.Clear();

            if (personas.Count() == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            var persona = AnsiConsole.Prompt(
                new SelectionPrompt<Persona>()
                    .Title("Seleccione el empleado a eliminar:")
                    .UseConverter(p => $"{p.Id}. {p.NombreCompleto} - {p.Cedula}")
                    .AddChoices(personas));

            if (AnsiConsole.Confirm($"Esta seguro de eliminar a {persona.NombreCompleto}?"))
            {
                personas.Remove(persona);
                AnsiConsole.MarkupLine("[green]Empleado eliminado exitosamente![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Operacion cancelada[/]");
            }

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void GestionarTiposEmpleado()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE TIPOS DE EMPLEADO[/]")
                        .AddChoices(new[] {
                            "Registrar Tipo de Empleado",
                            "Listar Tipos de Empleado",
                            "Volver"
                        }));

                switch (opcion)
                {
                    case "Registrar Tipo de Empleado":
                        RegistrarTipoEmpleado();
                        break;
                    case "Listar Tipos de Empleado":
                        ListarTiposEmpleado();
                        break;
                    case "Volver":
                        return;
                }
            }
        }

        static void RegistrarTipoEmpleado()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO TIPO DE EMPLEADO[/]");

            var tipo = new TipoEmpleado();
            tipo.Nombre = AnsiConsole.Ask<string>("Nombre del tipo:");
            tipo.FactorSalario = AnsiConsole.Ask<decimal>("Factor de salario (1.0 = 100%):");

            tiposEmpleado.Add(tipo);

            AnsiConsole.MarkupLine("[green]Tipo de empleado registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void ListarTiposEmpleado()
        {
            AnsiConsole.Clear();

            if (tiposEmpleado.Count() == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay tipos de empleado registrados[/]");
            }
            else
            {
                var tabla = new Table();
                tabla.AddColumn("ID");
                tabla.AddColumn("Nombre");
                tabla.AddColumn("Factor Salario");

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

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void GestionarDepartamentos()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE DEPARTAMENTOS[/]")
                        .AddChoices(new[] {
                            "Registrar Departamento",
                            "Listar Departamentos",
                            "Volver"
                        }));

                switch (opcion)
                {
                    case "Registrar Departamento":
                        RegistrarDepartamento();
                        break;
                    case "Listar Departamentos":
                        ListarDepartamentos();
                        break;
                    case "Volver":
                        return;
                }
            }
        }

        static void RegistrarDepartamento()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO DEPARTAMENTO[/]");

            var depto = new DepartamentoEmpleado();
            depto.Nombre = AnsiConsole.Ask<string>("Nombre del departamento:");
            depto.BonificacionPorcentaje = AnsiConsole.Ask<decimal>("Porcentaje de bonificacion:");

            departamentos.Add(depto);

            AnsiConsole.MarkupLine("[green]Departamento registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void ListarDepartamentos()
        {
            AnsiConsole.Clear();

            if (departamentos.Count() == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay departamentos registrados[/]");
            }
            else
            {
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

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void GestionarTiposNomina()
        {
            while (true)
            {
                AnsiConsole.Clear();
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold cyan]GESTION DE TIPOS DE NOMINA[/]")
                        .AddChoices(new[] {
                            "Registrar Tipo de Nomina",
                            "Listar Tipos de Nomina",
                            "Volver"
                        }));

                switch (opcion)
                {
                    case "Registrar Tipo de Nomina":
                        RegistrarTipoNomina();
                        break;
                    case "Listar Tipos de Nomina":
                        ListarTiposNomina();
                        break;
                    case "Volver":
                        return;
                }
            }
        }

        static void RegistrarTipoNomina()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[bold yellow]REGISTRAR NUEVO TIPO DE NOMINA[/]");

            var tipo = new TipoNomina();
            tipo.FrecuenciaPago = AnsiConsole.Ask<string>("Frecuencia de pago (Ej: Quincenal, Mensual):");
            tipo.MetodoPago = AnsiConsole.Ask<string>("Metodo de pago (Ej: Transferencia, Cheque):");
            tipo.DiasPorPeriodo = AnsiConsole.Ask<int>("Dias por periodo:");

            tiposNomina.Add(tipo);

            AnsiConsole.MarkupLine("[green]Tipo de nomina registrado exitosamente![/]");
            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void ListarTiposNomina()
        {
            AnsiConsole.Clear();

            if (tiposNomina.Count() == 0)
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

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void CrearNomina()
        {
            AnsiConsole.Clear();

            if (personas.Count() == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            AnsiConsole.MarkupLine("[bold yellow]CREAR NUEVA NOMINA[/]");

            var nomina = new DetalleNomina();
                                                                
            nomina.Fecha = AnsiConsole.Ask("Fecha (YYYY-MM-DD):", DateTime.Now.Date); // fecha actual por defecto
            nomina.Descripcion = AnsiConsole.Ask<string>("Descripcion:", "N/A");

            var persona = AnsiConsole.Prompt(
                new SelectionPrompt<Persona>()
                    .Title("Seleccione el empleado:")
                    .UseConverter(p => $"{p.Id}. {p.NombreCompleto} - {p.Cedula}")
                    .AddChoices(personas));

            nomina.PersonaId = persona.Id;

            var tipo = tiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
            var depto = departamentos.First(d => d.Id == persona.DepartamentoId);
            var tipoNomina = tiposNomina.First(n => n.Id == persona.TipoNominaId);

            nomina.CalcularNomina(persona, tipo, depto, tipoNomina);
            nominas.Add(nomina);

            AnsiConsole.MarkupLine($"\n[green]Nomina creada exitosamente![/]");
            AnsiConsole.MarkupLine($"ID de Nomina: {nomina.Id}");
            AnsiConsole.MarkupLine($"Tipo de Nomina: {tipoNomina.Descripcion}");
            AnsiConsole.MarkupLine($"Salario Neto: {nomina.SalarioNeto:C}");

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        //static void CalcularNominaIndividual()
        //{
        //    AnsiConsole.Clear();

        //    if (!personas.Count()==0)
        //    {
        //        AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
        //        AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        //        return;
        //    }

        //    var persona = AnsiConsole.Prompt(
        //        new SelectionPrompt<Persona>()
        //            .Title("Seleccione el empleado para calcular nomina:")
        //            .UseConverter(p => $"{p.Id}. {p.NombreCompleto} - {p.Cedula}")
        //            .AddChoices(personas));

        //    var tipo = tiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
        //    var depto = departamentos.First(d => d.Id == persona.DepartamentoId);
        //    var tipoNomina = tiposNomina.First(n => n.Id == persona.TipoNominaId);

        //    var nomina = new DetalleNomina();
        //    nomina.PersonaId = persona.Id;
        //    nomina.Descripcion = $"Calculo individual para {persona.NombreCompleto}";
        //    nomina.CalcularNomina(persona, tipo, depto, tipoNomina);

        //    MostrarDetalleNomina(nomina, persona, tipo, depto, tipoNomina);

        //    AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        //}

        static void CalcularNominaGeneral()
        {
            AnsiConsole.Clear();

            if (personas.Count() == 0)
            {
                AnsiConsole.MarkupLine("[red]No hay empleados registrados[/]");
                AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
                return;
            }

            AnsiConsole.MarkupLine("[bold yellow]CALCULO DE NOMINA GENERAL[/]\n");

            // fecha por defecto (hoy)
            var fecha = AnsiConsole.Ask("Fecha para todas las nominas (YYYY-MM-DD):", DateTime.Now.Date);

            // Lista temporal para mostrar las nominas creadas
            var nominasCreadas = new List<DetalleNomina>();
            decimal totalNeto = 0;

            foreach (var persona in personas)
            {
                var tipo = tiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
                var depto = departamentos.First(d => d.Id == persona.DepartamentoId);
                var tipoNomina = tiposNomina.First(n => n.Id == persona.TipoNominaId);

                var nomina = new DetalleNomina
                {
                    Fecha = fecha,
                    Descripcion = $"Nomina General - {fecha:yyyy-MM-dd}",
                    PersonaId = persona.Id
                };

                nomina.CalcularNomina(persona, tipo, depto, tipoNomina);
                nominas.Add(nomina); // Agregamos las nominas a la lista principal
                nominasCreadas.Add(nomina); // Agregamos a la lista temporal para mostrar
                totalNeto += nomina.SalarioNeto;
            }

            AnsiConsole.MarkupLine($"[green]Se crearon {nominasCreadas.Count} nominas exitosamente![/]\n");

            foreach (var nomina in nominasCreadas)
            {
                var persona = personas.First(p => p.Id == nomina.PersonaId);
                var tipo = tiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
                var depto = departamentos.First(d => d.Id == persona.DepartamentoId);
                var tipoNomina = tiposNomina.First(n => n.Id == persona.TipoNominaId);

                MostrarDetalleNominaCompacto(nomina, persona, tipo, depto, tipoNomina);
                AnsiConsole.WriteLine();
            }

            AnsiConsole.MarkupLine($"[bold green]═══════════════════════════════════════════════[/]");
            AnsiConsole.MarkupLine($"[bold green]Total Nomina General: {totalNeto:C}[/]");
            AnsiConsole.MarkupLine($"[bold green]═══════════════════════════════════════════════[/]");

            AnsiConsole.Prompt(new TextPrompt<string>("\nPresione Enter para continuar...").AllowEmpty());
        }

        static void MostrarDetalleNominaCompacto(DetalleNomina nomina, Persona persona, 
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

        static void ImprimirNomina()
        {
            AnsiConsole.Clear();

            if (nominas.Count() == 0)
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
                        var persona = personas.First(p => p.Id == n.PersonaId);
                        var nombreCompleto = persona?.NombreCompleto ?? "N/A";
                        return $"ID: {n.Id} - {nombreCompleto} - Fecha: {n.Fecha:yyyy-MM-dd} - {n.Descripcion}";
                    })
                    .AddChoices(nominas));

            var persona = personas.First(p => p.Id == nomina.PersonaId);
            var tipo = tiposEmpleado.First(t => t.Id == persona.TipoEmpleadoId);
            var depto = departamentos.First(d => d.Id == persona.DepartamentoId);
            var tipoNomina = tiposNomina.First(n => n.Id == persona.TipoNominaId);

            MostrarDetalleNomina(nomina, persona, tipo, depto, tipoNomina);

            AnsiConsole.Prompt(new TextPrompt<string>("Presione Enter para continuar...").AllowEmpty());
        }

        static void MostrarDetalleNomina(DetalleNomina nomina, Persona persona, TipoEmpleado tipo, DepartamentoEmpleado depto, TipoNomina tipoNomina)
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
                $"[cyan]Salario por Periodo ({tipoNomina.DiasPorPeriodo} dias):[/] {
                    (nomina.SalarioAjustado / 30 * tipoNomina.DiasPorPeriodo):C}\n" +
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

        static void MostrarTiposEmpleado()
        {
            if (tiposEmpleado.Any())
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

        static void MostrarDepartamentos()
        {
            if (departamentos.Any())
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

        static void MostrarTiposNomina()
        {
            if (tiposNomina.Any())
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