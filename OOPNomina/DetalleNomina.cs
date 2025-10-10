using System;

namespace OOPNomina
{
    public class DetalleNomina
    {
        private static int contadorId = 1;
        
        public int Id { get; private set; } = 0;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; } = string.Empty;
        public int PersonaId { get; set; } = 0;
        
        // Calculos
        public decimal SalarioBase { get; set; } = 0;
        public decimal SalarioAjustado { get; set; } = 0;
        public decimal Bonificacion { get; set; } = 0;
        public decimal TotalBruto { get; set; } = 0;
        public decimal DeduccionAFP { get; set; } = 0;
        public decimal DeduccionARS { get; set; } = 0;
        public decimal DeduccionISR { get; set; } = 0;
        public decimal TotalDeducciones { get; set; } = 0;
        public decimal SalarioNeto { get; set; } = 0;
        
        // Porcentajes RD
        private const decimal PORCENTAJE_AFP = 0.0287m;
        private const decimal PORCENTAJE_ARS = 0.0304m;
        
        public DetalleNomina()
        {
            Id = contadorId++;
            Fecha = DateTime.Now;
        }
        
        public void CalcularNomina(Persona persona, TipoEmpleado tipoEmpleado, DepartamentoEmpleado departamento, TipoNomina tipoNomina)
        {
            SalarioBase = persona.SalarioBase;
            SalarioAjustado = SalarioBase * tipoEmpleado.FactorSalario;
            
            decimal salarioPeriodo = (SalarioAjustado / 30) * tipoNomina.DiasPorPeriodo;
            
            Bonificacion = salarioPeriodo * (departamento.BonificacionPorcentaje / 100);
            
            TotalBruto = salarioPeriodo + Bonificacion;
            
            DeduccionAFP = TotalBruto * PORCENTAJE_AFP;
            DeduccionARS = TotalBruto * PORCENTAJE_ARS;
            DeduccionISR = CalcularISR(TotalBruto * 12);
            
            TotalDeducciones = DeduccionAFP + DeduccionARS + DeduccionISR;
            SalarioNeto = TotalBruto - TotalDeducciones;
        }
        
        private decimal CalcularISR(decimal salarioAnual)
        {
            decimal isr = 0;
            
            if (salarioAnual <= 416220)
            {
                isr = 0;
            }
            else if (salarioAnual <= 624329)
            {
                isr = (salarioAnual - 416220) * 0.15m;
            }
            else if (salarioAnual <= 867123)
            {
                isr = 31216.35m + (salarioAnual - 624329) * 0.20m;
            }
            else
            {
                isr = 79776.15m + (salarioAnual - 867123) * 0.25m;
            }
            
            return isr / 12;
        }
    }
}

