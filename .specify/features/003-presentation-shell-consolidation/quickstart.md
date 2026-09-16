# Quickstart: Validating SPEC-003 Presentation Layer Consolidation

## Prerrequisitos

- .NET 8 SDK
- Un navegador

## Cómo correrla

```bash
cd PolyConecta.Presentation
dotnet run
```

Abrir la URL que imprime la consola (típicamente `http://localhost:5xxx`).

## Guion de prueba manual (una pasada = las 7 fases)

1. Abrir `/` → debe verse el shell (topbar + sidebar) con el Dashboard, no la página de bienvenida de ASP.NET ni un 404.
2. Ir a `/pedidos/{folio}` desde el sidebar → llenar/confirmar el pedido → clic en `Confirmar` → estado pasa a `Confirmado`.
3. Clic en `Validar Ventas` y `Validar Crédito` (pueden ser dos usuarios distintos en la demo, un solo botón cada uno) → estado pasa a `Autorizado`.
4. Ir a `/manufactura/{folioOm}` → ver `OM` y sus `OF` hijas → cargar receta BOM en `OF-EXT` → asignar máquina/fecha a la `WO` → estado pasa a `En progreso`.
5. Ir a `/piso/bascula` → pesar `Rollo 1` con aprobación de calidad → pesar `Rollo 4` con rechazo → confirmar que `Rollo 4` se renombra con `.S`, se mueve a cuarentena, y el slot `4` queda libre para un rollo de reposición.
6. Volver a `/manufactura/{folioOf}/cierre` → ejecutar `Cierre Técnico` → confirmar balance de masa y mensaje de afectación consolidada.
7. Ir a `/logistica` → ejecutar Paso 1 (salida PIM) y Paso 2 (recepción Santa Cruz) del traspaso interplanta.
8. Ir a `/conversion/{folioOf}` → capturar millares + kg → confirmar cálculo de factor real → cerrar `OF-BOL` → el pedido pasa a `Hecho`.
9. **Prueba de navegación real**: en cualquier paso, usar el botón "Atrás" del navegador y confirmar que no se pierde lo capturado en esa sesión; abrir una de las rutas anteriores en una pestaña nueva (enlace directo) y confirmar que carga sin pasar por el paso 1.
10. **Prueba de concurrencia**: abrir la misma URL en una ventana de incógnito aparte y confirmar que tiene su propio pedido de ejemplo independiente (no comparte estado con la primera pestaña) — valida el ámbito `Scoped` de R2 en `research.md`.

## Definition of Done (por qué este quickstart existe)

Ninguna tarea de `tasks.md` de esta feature se marca `[x]` sin haber ejecutado este guion (o el paso específico de su historia de usuario) en un navegador real — no solo `dotnet build`. Esto es una respuesta directa a que `001-poc-end-to-end-operational-flow/tasks.md` marcó como completadas tareas que referenciaban archivos inexistentes.
