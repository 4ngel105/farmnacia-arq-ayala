# P1.1 Detección de violaciones SOLID 
| # | Principio | Dónde vive (clase / método) | Por qué es una violación |
|---|---|---|---|
| 1 | **S** — Responsabilidad Única | `GestorDePedidos.ProcesarPedido(...)` | El método hace cuatro cosas distintas: calcula el descuento, guarda en la base de datos, imprime el comprobante y manda el correo. Tiene cuatro razones independientes para cambiar, cuando debería tener una sola. |