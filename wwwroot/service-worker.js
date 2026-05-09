// Development service worker: no fetch handler to avoid no-op overhead.
// Keep this file present so the app can register a service worker during development
// if desired, but avoid an empty fetch handler which generates the "no-op fetch"
// warning in DevTools.
// If you later need offline caching during development, implement a proper
// fetch handler or enable the published worker for testing.

// Listen for messages from the page to skip waiting (for update flow)
self.addEventListener('message', function (event) {
	try {
		if (!event.data) return;
		if (event.data.type === 'SKIP_WAITING' || event.data.action === 'skipWaiting') {
			self.skipWaiting();
		}
	} catch (e) { /* ignore */ }
});

// Ensure we claim clients when activated during development
self.addEventListener('activate', function (event) {
	event.waitUntil((async function () {
		try { await self.clients.claim(); } catch (e) { /* ignore */ }
	})());
});
