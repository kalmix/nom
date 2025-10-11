# nom
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen?style=for-the-badge)](https://github.com/kalmix/nom/actions)
[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)](LICENSE)

> Proyecto para la clase de (OOP) - Un ejemplo básico de gestión de nómina con interfaz de consola interactiva usando SpectreConsole.

## Estructura del Proyecto

```
OOPNomina/
├── Program.cs                      
├── Models/                         
│   ├── Persona.cs
│   ├── TipoEmpleado.cs
│   ├── DepartamentoEmpleado.cs
│   ├── TipoNomina.cs
│   └── DetalleNomina.cs
├── Data/
│   └── AppContext.cs           # Contexto de datos y mock data
├── Services/
│   └── NominaService.cs           
└── UI/
    ├── Menus/                      # Menus de nav
    │   ├── MenuPrincipal.cs
    │   ├── EmpleadoMenu.cs
    │   ├── TipoEmpleadoMenu.cs
    │   ├── DepartamentoMenu.cs
    │   ├── TipoNominaMenu.cs
    │   └── NominaMenu.cs
    └── Helpers/                    
        ├── NominaDisplayHelper.cs
        └── TableHelper.cs
```

## Instalación

### Prerrequisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior

### Pasos

1. **Clonar el repositorio**
```bash
git clone https://github.com/tu-usuario/OOPNomina.git
cd OOPNomina
```

2. **Restaurar dependencias**
```bash
dotnet restore
```

3. **Compilar el proyecto**
```bash
dotnet build
```

4. **Ejecutar**
```bash
dotnet run
```

### Menú Principal

Al iniciar la app, verás el menú principal con las siguientes opciones:
```
  ____  _     _                          _        _   _                 _             
 / ___|(_)___| |_ ___ _ __ ___   __ _   | |  ___ | \ | | ___  _ __ ___ (_)_ __   __ _ 
 \___ \| / __| __/ _ \ '_ ` _ \ / _` |  | | / _ \|  \| |/ _ \| '_ ` _ \| | '_ \ / _` |
  ___) | \__ \ ||  __/ | | | | | (_| |  |_|| (_) | |\  | (_) | | | | | | | | | | (_| |
 |____/|_|___/\__\___|_| |_| |_|\__,_|  (_) \___/|_| \_|\___/|_| |_| |_|_|_| |_|\__,_|
─────────────────────────────────────
> Gestionar Empleados
  Gestionar Tipos de Empleado
  Gestionar Departamentos
  Gestionar Tipos de Nomina
  Crear Nomina
  Calcular Nomina General
  Imprimir Nomina
  Salir
```

## Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## Agradecimientos
- [Spectre.Console](https://spectreconsole.net/) por la increíble librería de UI
