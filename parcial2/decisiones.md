# Decisiones de Arquitectura — Gimnasio "Fuerza Andina"
Variante B · Roberto Angel Ayala Lecoña 
## Situación 1 — Aviso de membresía vencida

El módulo de socios hoy conoce y llama uno por uno a WhatsApp, al registro de
vencidos y a recepción cada interesado nuevo obliga a modificar el módulo de socios
(viola OCP) y ese módulo termina acoplado a WhatsApp, promociones y a lo que venga.

Con Observer, el módulo de socios solo publica el evento
`MembresiaVencida` y cada interesado se suscribe por su cuenta.


**Patrón: Observer**