// Solucion: Roberto Angel Ayala Lecoña
// Situacion 2 — Calculo de tarifa por franja horaria

using System;

using System.Collections.Generic;
namespace FuerzaAndina.Cobrosñ{

    public interface ITarifaStrategy {
        string Nombre { get; }
        decimal CalcularMonto(int horas, decimal tarifaBasePorHora);
    }
     // Manana: tarifa plena, sin ajustes. -ROBERTO AYALA LECOÑA
    public class TarifaManana : ITarifaStrategy
    {
        public string Nombre => "Manana (tarifa plena)";

        public decimal CalcularMonto(int horas, decimal tarifaBasePorHora)
        {
            return horas * tarifaBasePorHora;
        }
    }
     // Noche: recargo del 20% por demanda. -ROBERTO AYALA LECOÑA
    public class TarifaNoche : ITarifaStrategy
    {
        private const decimal RECARGO = 1.20m;
        public string Nombre => "Noche (recargo 20%)";

        public decimal CalcularMonto(int horas, decimal tarifaBasePorHora)
        {
            return horas * tarifaBasePorHora * RECARGO;
        }

 // Fin de semana: descuento del 30% con tope de 3 horas cobrables.  -ROBERTO AYALA LECOÑA
    public class TarifaFinDeSemana : ITarifaStrategy
    {
        private const decimal DESCUENTO = 0.70m;
        private const int TOPE_HORAS = 3;
        public string Nombre => "Fin de semana (descuento 30%, tope 3h)";

        public decimal CalcularMonto(int horas, decimal tarifaBasePorHora)
        {
            int horasCobrables = horas > TOPE_HORAS ? TOPE_HORAS : horas;
            return horasCobrables * tarifaBasePorHora * DESCUENTO;
        }
    }
//CONSUMIDOR: no conoce ninguna franja, solo el contrato  -ROBERTO AYALA LECOÑA
    public class CalculadoraCobro
    {
        private readonly ITarifaStrategy _tarifa;
        private readonly decimal _tarifaBasePorHora;

        public CalculadoraCobro(ITarifaStrategy tarifa, decimal tarifaBasePorHora)
        {
            _tarifa = tarifa;
            _tarifaBasePorHora = tarifaBasePorHora;
        }

        public decimal CobrarSesion(string socio, int horas)
        {
            decimal monto = _tarifa.CalcularMonto(horas, _tarifaBasePorHora);
            Console.WriteLine($"Socio: {socio,-22} | {_tarifa.Nombre,-40} | {horas}h -> Bs {monto:0.00}");
            return monto;
        }
    }


}

//DEMO 

    public class Programa
    {
        public static void Main()
        {
            const decimal TARIFA_BASE = 30m; 

            Console.WriteLine("=== Gimnasio Fuerza Andina - Cobro de sesiones ===\n");

            var cobroManana = new CalculadoraCobro(new TarifaManana(), TARIFA_BASE);
            cobroManana.CobrarSesion("Ana Quispe", 2);          // 2 * 30 = 60.00 

            var cobroNoche = new CalculadoraCobro(new TarifaNoche(), TARIFA_BASE);
            cobroNoche.CobrarSesion("Luis Mamani", 2);          // 2 * 30 * 1.20 = 72.00 

            var cobroFinde = new CalculadoraCobro(new TarifaFinDeSemana(), TARIFA_BASE);
            cobroFinde.CobrarSesion("Carla Choque", 5);         // tope 3h: 3 * 30 * 0.70 = 63.00 

            // El modulo de cotizaciones reusa EXACTAMENTE las mismas estrategias: 
            // ya no hay if/else duplicado entre cobros y cotizaciones. -ROBERTO AYALA LECOÑA
            Console.WriteLine("\n--- Cotizacion de paquete mensual (misma estrategia) ---");
            var franjas = new List<ITarifaStrategy>
            {
                new TarifaManana(),
                new TarifaNoche(),
                new TarifaFinDeSemana()
            };

            foreach (var franja in franjas)
            {
                var cotizador = new CalculadoraCobro(franja, TARIFA_BASE);
                cotizador.CobrarSesion("COTIZACION 4h", 4);
            }
        }
    }
}