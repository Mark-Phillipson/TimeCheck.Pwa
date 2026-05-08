window.timecheck = (function () {
    const api = {};
    api._utterance = null;

    api.isSupported = function () {
        return typeof window !== 'undefined' && 'speechSynthesis' in window;
    };

    api.speak = function (text, cancelPrior) {
        try {
            if (!api.isSupported()) return;
            if (cancelPrior) {
                window.speechSynthesis.cancel();
                api._utterance = null;
            }
            const u = new SpeechSynthesisUtterance(text || '');
            api._utterance = u;
            window.speechSynthesis.speak(u);
        } catch (e) {
            console.error('TTS speak failed', e);
        }
    };

    api.cancel = function () {
        try {
            if ('speechSynthesis' in window) window.speechSynthesis.cancel();
            api._utterance = null;
        } catch (e) { }
    };

    api.storageGet = function (key) {
        try {
            return localStorage.getItem(key);
        } catch (e) {
            return null;
        }
    };

    api.storageSet = function (key, value) {
        try {
            localStorage.setItem(key, value);
        } catch (e) { }
    };

    api.requestNotificationPermission = function () {
        if (!('Notification' in window)) return Promise.resolve('unsupported');
        return Notification.requestPermission();
    };

    api.showNotification = function (title, body) {
        try {
            if (!('Notification' in window)) return;
            if (Notification.permission === 'granted') {
                new Notification(title, { body: body });
            }
        } catch (e) { console.error(e); }
    };

    api.focusElementById = function (id) {
        try {
            var el = document.getElementById(id);
            if (!el) return;
            // Focus the element and select its content so typing replaces it.
            if (typeof el.focus === 'function') el.focus();
            // Prefer the select() API for text inputs
            if (typeof el.select === 'function') {
                try { el.select(); } catch (e) { /* ignore selection errors */ }
                return;
            }
            // Fallback to setSelectionRange if available
            if (el.setSelectionRange && typeof el.value === 'string') {
                try {
                    el.setSelectionRange(0, el.value.length);
                } catch (e) { }
            }
        } catch (e) { console.error(e); }
    };

    api.isSupported = api.isSupported;
    return api;
})();
