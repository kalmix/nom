namespace OOPNomina.Models
{
    public class Persona
    {
        private static int contadorId = 1;

        public int Id { get; private set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public decimal SalarioBase { get; set; } = 0;
        public int TipoEmpleadoId { get; set; } = 0;
        public int DepartamentoId { get; set; } = 0;
        public int TipoNominaId { get; set; } = 0;

        public Persona()
        {
            Id = contadorId++;
        }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}