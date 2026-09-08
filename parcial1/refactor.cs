// PARCIAL 1 · VARIANTE A — Farmacia "San Rafael"
// Roberto Angel Ayala Lecoña
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parcial1.Farmacia.Refactor;

// --CURA  1
// Antes: GestorDePedidos.ProcesarPedido resolvía el descuento con un switch
// sobre tipoCliente. Cada tipo nuevo obligaba a modificar ese método.
// Ahora: cada tipo de cliente es una clase que implementa IPoliticaDeDescuento.
// Agregar "tercera edad" = agregar una clase nueva. No se toca código existente.
// Refactor: Roberto Angel Ayala Lecoña

public interface IPoliticaDeDescuento
{
    string TipoCliente { get; }
    decimal CalcularDescuento(decimal total);
}

public class DescuentoParticular : IPoliticaDeDescuento
{
    public string TipoCliente => "particular";
    public decimal CalcularDescuento(decimal total) => 0m;
}

public class DescuentoAsegurado : IPoliticaDeDescuento
{
    public string TipoCliente => "asegurado";
    public decimal CalcularDescuento(decimal total) => total * 0.20m;
}

public class DescuentoConvenio : IPoliticaDeDescuento
{
    public string TipoCliente => "convenio";
    public decimal CalcularDescuento(decimal total) => total * 0.10m;
}

// Ejemplo de extensión SIN modificar nada de lo anterior
public class DescuentoTerceraEdad : IPoliticaDeDescuento
{
    public string TipoCliente => "tercera edad";
    public decimal CalcularDescuento(decimal total) => total * 0.25m;
}

public class CalculadoraDeDescuentos
{
    private readonly IEnumerable<IPoliticaDeDescuento> _politicas;

    public CalculadoraDeDescuentos(IEnumerable<IPoliticaDeDescuento> politicas)
        => _politicas = politicas;

    public static CalculadoraDeDescuentos PorDefecto() => new(new IPoliticaDeDescuento[]
    {
        new DescuentoParticular(),
        new DescuentoAsegurado(),
        new DescuentoConvenio(),
        new DescuentoTerceraEdad()
    });

    public decimal Calcular(string tipoCliente, decimal total)
    {
        var politica = _politicas.FirstOrDefault(
            p => p.TipoCliente.Equals(tipoCliente, StringComparison.OrdinalIgnoreCase));
       
        return politica?.CalcularDescuento(total) ?? 0m;
    }
}