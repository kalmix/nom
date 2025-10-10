namespace OOPNomina
{
    public class TipoEmpleado
    {
        private static int contadorId = 1;

        public int Id { get; private set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public decimal FactorSalario { get; set; } = 0;

        public TipoEmpleado()
        {
            Id = contadorId++;
        }
    }
}