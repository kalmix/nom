namespace OOPNomina
{
    public class DepartamentoEmpleado
    {
        private static int contadorId = 1;

        public int Id { get; private set; } = 0;
        public string Nombre { get; set; } = string.Empty;

        public decimal BonificacionPorcentaje { get; set; } = 0;

        public DepartamentoEmpleado()
        {
            Id = contadorId++;
        }
    }
}