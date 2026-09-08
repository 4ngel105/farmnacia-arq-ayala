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

// CURA 2 
// Antes: ProcesarPedido calculaba, guardaba, imprimía y notificaba: cuatro
// razones para cambiar en un solo método.
// Ahora: cada tarea vive en su propia clase y GestorDePedidos solo coordina.
// Refactor: Roberto Angel Ayala Lecoña

public class Pedido
{
    public string Cliente { get; }
    public string TipoCliente { get; }
    public string Medicamento { get; }
    public int Cantidad { get; }
    public decimal PrecioUnitario { get; }

    public Pedido(string cliente, string tipoCliente, string medicamento, int cantidad, decimal precioUnitario)
    {
        Cliente = cliente;
        TipoCliente = tipoCliente;
        Medicamento = medicamento;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }

    public decimal Subtotal => Cantidad * PrecioUnitario;
}

public class RepositorioPedidosMySql
{
    public void Guardar(Pedido pedido, decimal totalFinal)
        => Console.WriteLine($"[MYSQL] INSERT INTO pedidos VALUES ('{pedido.Cliente}', " +
                             $"'{pedido.Medicamento}', {pedido.Cantidad}, {totalFinal})");
}

//  Presentar el comprobante.
public class ImpresoraDeComprobante
{
    public void Imprimir(Pedido pedido, decimal totalFinal)
    {
        Console.WriteLine("----- COMPROBANTE -----");
        Console.WriteLine($"{pedido.Cantidad} x {pedido.Medicamento}");
        Console.WriteLine($"Cliente: {pedido.Cliente} ({pedido.TipoCliente})");
        Console.WriteLine($"TOTAL: {totalFinal:0.00} Bs");
    }
}

// notificar.
public class NotificadorPorCorreo
{
    public void Notificar(Pedido pedido)
        => Console.WriteLine($"[SMTP] Su pedido de {pedido.Medicamento} fue registrado, {pedido.Cliente}");
}
