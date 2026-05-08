// Development service worker: no fetch handler to avoid no-op overhead.
// Keep this file present so the app can register a service worker during development
// if desired, but avoid an empty fetch handler which generates the "no-op fetch"
// warning in DevTools.
// If you later need offline caching during development, implement a proper
// fetch handler or enable the published worker for testing.
