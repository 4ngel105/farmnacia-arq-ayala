# Decisiones de Arquitectura — Gimnasio "Fuerza Andina"
Variante B · Roberto Angel Ayala Lecoña 
## Situación 1 — Aviso de membresía vencida

El módulo de socios hoy conoce y llama uno por uno a WhatsApp, al registro de
vencidos y a recepción cada interesado nuevo obliga a modificar el módulo de socios
(viola OCP) y ese módulo termina acoplado a WhatsApp, promociones y a lo que venga.

Con Observer, el módulo de socios solo publica el evento
`MembresiaVencida` y cada interesado se suscribe por su cuenta.


**Patrón: Observer**

## Situación 2 — Cálculo de tarifa por franja

Mañana, noche y fin de semana son tres algoritmos completos e intercambiables
para la misma pregunta: cuánto cobrar por N horas, el if/else vive copiado en dos módulos y se desincroniza — el
dueño cambia la regla de temporada en cobros y cotizaciones sigue cotizando mal.

 Strategy los encapsula detrás
de un contrato único (ITarifaStrategy) y tanto cobros como cotizaciones lo
consumen, eliminando el if/else duplicado.

**Patrón: Strategy**

## Situación 3 — Pasarela de pago externa

El SDK no es modificable y habla un idioma ajeno al dominio: inglés, montos en
centavos y customerToken , la conversión Bs→centavos y el manejo de tokens quedan regados
por el módulo de membresías, y cambiar de proveedor el próximo año obliga a tocar
todo ese código en vez de escribir un adapter nuevo.


