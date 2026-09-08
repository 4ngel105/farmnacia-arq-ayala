# P1.1 Detección de violaciones SOLID 
| N | Principio | Dónde vive (clase / método) | Por qué es una violación |
|---|---|---|---|
| 1 | **S**  | `GestorDePedidos.ProcesarPedido(...)` | El método hace cuatro cosas distintas: calcula el descuento, guarda en la base de datos, imprime el comprobante y manda el correo. Tiene cuatro razones independientes para cambiar, cuando debería tener una sola. |
| 2 | **O**  | `GestorDePedidos.ProcesarPedido(...)`, bloque `switch (tipoCliente)` | Para sumar un tipo de cliente nuevo hay que abrir y modificar el `switch` ya existente. La clase no está cerrada a modificación ni abierta a extensión: cada regla comercial nueva obliga a tocar código que ya funcionaba. |
| 3 | **L/I**  | `IEmpleadoDeFarmacia / Cajero`,  | La interfaz obliga al Cajero a implementar tres métodos que no le corresponden y que lanzan excepción, así que no puede sustituir a la abstracción. |