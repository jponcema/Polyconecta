// ==============================================================================
// PolyConecta - Recorrido completo del sistema y captura de pantallas
// ==============================================================================
// Navega cada pantalla de la SPA y guarda una captura .jpeg numerada en
// docs/screenshots/. La navegacion se renderiza al 90% (zoom out) via
// deviceScaleFactor, igual que reducir el zoom del navegador.
//
// Uso:
//   node capture.mjs                     # asume la app ya corriendo en :9000
//   BASE_URL=http://localhost:9000 node capture.mjs
// ==============================================================================

import { chromium } from 'playwright';
import { mkdir, readdir, rm } from 'node:fs/promises';
import { dirname, join } from 'node:path';
import { fileURLToPath } from 'node:url';

const HERE = dirname(fileURLToPath(import.meta.url));
const REPO_ROOT = join(HERE, '..', '..');
const OUT_DIR = join(REPO_ROOT, 'docs', 'screenshots');

const BASE_URL = process.env.BASE_URL ?? 'http://localhost:9000';
const JPEG_QUALITY = 90;

// Zoom out al 90%, equivalente a Cmd+- en el navegador: el viewport CSS se
// agranda (entra mas contenido) y la imagen se reescala de vuelta al tamano de
// ventana, con lo que todo se ve proporcionalmente mas pequeno.
const ZOOM = 0.9;
const WINDOW = { width: 1600, height: 900 }; // tamano final de la imagen
const VIEWPORT = {
  width: Math.round(WINDOW.width / ZOOM),
  height: Math.round(WINDOW.height / ZOOM),
};

// Folios reales del seed (OperationalFlowState), para que las vistas de
// formulario abran con datos en vez de "no encontrado".
const PEDIDO = 'IV310-26';
const OF_BOLSEO = 'BOL-2026-0001';
const OF_IMPRESION = 'IMP-2026-0001';
const OF_EXTRUSION = 'EXT-2026-0001';

/**
 * Recorrido del sistema, en orden de flujo operativo.
 * `tab` hace clic en una pestana del formulario antes de capturar.
 */
const SCREENS = [
  { name: 'hub-aplicaciones', path: '/' },

  { name: 'pedidos-lista', path: '/pedidos' },
  { name: 'pedidos-kanban', path: '/pedidos', view: 'Kanban' },
  { name: 'pedido-detalle', path: `/pedidos/${PEDIDO}` },

  { name: 'fabricacion-lista', path: '/fabricacion' },
  { name: 'fabricacion-kanban', path: '/fabricacion', view: 'Kanban' },
  { name: 'of-extrusion-componentes', path: `/fabricacion/${OF_EXTRUSION}` },
  { name: 'of-extrusion-subproductos', path: `/fabricacion/${OF_EXTRUSION}`, tab: 'Subproductos' },
  { name: 'of-extrusion-produccion', path: `/fabricacion/${OF_EXTRUSION}`, tab: 'Producción' },
  { name: 'of-extrusion-planeacion', path: `/fabricacion/${OF_EXTRUSION}`, tab: 'Planeación' },
  { name: 'of-impresion-componentes', path: `/fabricacion/${OF_IMPRESION}` },
  { name: 'of-impresion-produccion', path: `/fabricacion/${OF_IMPRESION}`, tab: 'Producción' },
  { name: 'of-bolseo-componentes', path: `/fabricacion/${OF_BOLSEO}` },
  { name: 'of-bolseo-produccion', path: `/fabricacion/${OF_BOLSEO}`, tab: 'Producción' },

  { name: 'calidad-lista', path: '/calidad' },
  { name: 'calidad-detalle', path: `/calidad/${OF_IMPRESION}` },

  { name: 'recepcion-lista', path: '/recepcion' },
  { name: 'recepcion-detalle', path: '/recepcion/1' },

  { name: 'traslados-lista', path: '/traslados' },
  { name: 'traslado-detalle', path: '/traslados/1' },

  { name: 'entregas-lista', path: '/entregas' },
  { name: 'entrega-detalle', path: '/entregas/1' },

  { name: 'captura-masiva', path: '/captura-masiva' },
  { name: 'incidencias', path: '/incidencias' },

  { name: 'terminal-bascula', path: '/piso/bascula' },
];

/** Espera a que el circuito interactivo de Blazor este conectado. */
async function waitForBlazor(page) {
  await page.waitForFunction(
    () => !document.body.classList.contains('components-reconnect-show'),
    { timeout: 15_000 },
  );
  // El primer render interactivo llega por SignalR, no por load.
  await page.waitForTimeout(600);
}

async function main() {
  await rm(OUT_DIR, { recursive: true, force: true });
  await mkdir(OUT_DIR, { recursive: true });

  const browser = await chromium.launch();
  const context = await browser.newContext({
    viewport: VIEWPORT,
    deviceScaleFactor: ZOOM,
  });
  const page = await context.newPage();

  const failures = [];
  let n = 0;

  for (const screen of SCREENS) {
    n += 1;
    const index = String(n).padStart(2, '0');
    const file = `${index}-${screen.name}.jpeg`;

    try {
      const response = await page.goto(`${BASE_URL}${screen.path}`, {
        waitUntil: 'networkidle',
        timeout: 30_000,
      });
      if (response && !response.ok()) {
        throw new Error(`HTTP ${response.status()}`);
      }
      await waitForBlazor(page);

      if (screen.view === 'Kanban') {
        await page.click('.o_view_switcher .btn-view[title="Vista kanban"]');
        await page.waitForTimeout(400);
      }

      if (screen.tab) {
        await page.click(`.nav-tabs .nav-link:text-is("${screen.tab}")`);
        await page.waitForTimeout(400);
      }

      await page.screenshot({
        path: join(OUT_DIR, file),
        type: 'jpeg',
        quality: JPEG_QUALITY,
        fullPage: true,
      });
      console.log(`  ✓ ${file}`);
    } catch (err) {
      failures.push({ file, reason: err.message.split('\n')[0] });
      console.log(`  ✗ ${file} — ${err.message.split('\n')[0]}`);
    }
  }

  await browser.close();

  const written = (await readdir(OUT_DIR)).filter((f) => f.endsWith('.jpeg'));
  console.log(`\n${written.length}/${SCREENS.length} capturas en docs/screenshots/`);

  if (failures.length) {
    console.log('\nFallaron:');
    for (const f of failures) console.log(`  ${f.file} — ${f.reason}`);
    process.exitCode = 1;
  }
}

await main();
