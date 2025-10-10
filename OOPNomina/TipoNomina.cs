namespace OOPNomina
{
    public class TipoNomina
    {
        private static int contadorId = 1;

        public int Id { get; private set; } = 0;
        public string FrecuenciaPago { get; set; } = string.Empty;
        public string MetodoPago { get; set; } = string.Empty;
        public int DiasPorPeriodo { get; set; } = 0;

        public TipoNomina()
        {
            Id = contadorId++;
        }

        // interpolacion de stringzzzzz pro
        public string Descripcion => $"{FrecuenciaPago} - {MetodoPago}";
    }
}