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

    // Start a short, low-volume audio tone. Useful to test whether audio
    // continues when the app is backgrounded / the screen is locked.
    api.playSampleAudio = function (loop) {
        try {
            // Prefer WebAudio when available
            var AudioCtx = window.AudioContext || window.webkitAudioContext;
            if (AudioCtx) {
                if (!api._audioContext) api._audioContext = new AudioCtx();
                if (api._audioContext.state === 'suspended' && typeof api._audioContext.resume === 'function') {
                    api._audioContext.resume();
                }

                // Stop any existing oscillator
                if (api._oscillator) {
                    try { api._oscillator.stop(); } catch (e) { }
                    try { api._oscillator.disconnect(); } catch (e) { }
                    api._oscillator = null;
                }

                var osc = api._audioContext.createOscillator();
                var gain = api._audioContext.createGain();
                osc.type = 'sine';
                osc.frequency.value = 220; // low tone
                // Very low volume so this is unobtrusive but keeps audio active
                gain.gain.value = 0.001;
                osc.connect(gain);
                gain.connect(api._audioContext.destination);
                osc.start();
                api._oscillator = osc;
                api._gainNode = gain;
                api._audioLoop = !!loop;

                if (!api._audioLoop) {
                    // Stop after one second if not looping
                    osc.stop(api._audioContext.currentTime + 1.0);
                }
                return;
            }

            // Fallback: create an <audio> element and attempt to play a tiny silent blob
            if (!api._audioEl) {
                var a = document.createElement('audio');
                a.loop = !!loop;
                // Create 1-second silent WAV via JS (very small)
                try {
                    var numSamples = 44100;
                    var buffer = new ArrayBuffer(44 + numSamples * 2);
                    var view = new DataView(buffer);
                    function writeString(view, offset, string) {
                        for (var i = 0; i < string.length; i++) view.setUint8(offset + i, string.charCodeAt(i));
                    }
                    // WAV header
                    writeString(view, 0, 'RIFF');
                    view.setUint32(4, 36 + numSamples * 2, true);
                    writeString(view, 8, 'WAVE');
                    writeString(view, 12, 'fmt ');
                    view.setUint32(16, 16, true);
                    view.setUint16(20, 1, true);
                    view.setUint16(22, 1, true);
                    view.setUint32(24, 44100, true);
                    view.setUint32(28, 44100 * 2, true);
                    view.setUint16(32, 2, true);
                    view.setUint16(34, 16, true);
                    writeString(view, 36, 'data');
                    view.setUint32(40, numSamples * 2, true);
                    // PCM data already zeros (silent)
                    var blob = new Blob([view], { type: 'audio/wav' });
                    a.src = URL.createObjectURL(blob);
                } catch (e) {
                    console.warn('Could not create silent audio fallback', e);
                }
                api._audioEl = a;
            }
            api._audioEl.play().catch(function (e) { console.error('audio play failed', e); });
        } catch (e) { console.error('playSampleAudio failed', e); }
    };

    api.stopSampleAudio = function () {
        try {
            if (api._oscillator) {
                try { api._oscillator.stop(); } catch (e) { }
                try { api._oscillator.disconnect(); } catch (e) { }
                api._oscillator = null;
            }
            if (api._gainNode) {
                try { api._gainNode.disconnect(); } catch (e) { }
                api._gainNode = null;
            }
            if (api._audioEl) {
                try { api._audioEl.pause(); } catch (e) { }
                try { api._audioEl.src = ''; } catch (e) { }
                api._audioEl = null;
            }
        } catch (e) { console.error('stopSampleAudio failed', e); }
    };

    api.isSupported = api.isSupported;
    return api;
})();
