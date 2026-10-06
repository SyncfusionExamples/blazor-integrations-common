// // Appointment Planner — Theme runtime helpers
// // Used by Blazor via IJSRuntime after first render to swap themes.
// // First-paint theme is handled by the inline init script in App.razor.

// window.apTheme = (function () {
//     const STORAGE_KEY = 'ap-theme';

//     function getLinks() {
//         return {
//             light: document.getElementById('ap-sf-light'),
//             dark: document.getElementById('ap-sf-dark')
//         };
//     }

//     function swapSyncfusionTheme(theme) {
//         var links = getLinks();
//         if (!links.light || !links.dark) return;
//         var dark = theme === 'dark';
//         links.light.disabled = dark;
//         links.dark.disabled = !dark;
//     }

//     function applyTheme(theme, persist) {
//         var t = theme === 'dark' ? 'dark' : 'light';
//         document.documentElement.setAttribute('data-theme', t);
//         swapSyncfusionTheme(t);
//         if (persist) {
//             try { localStorage.setItem(STORAGE_KEY, t); } catch (e) { /* ignore */ }
//         }
//         window.dispatchEvent(new CustomEvent('ap-theme-changed', { detail: t }));
//         return t;
//     }

//     function current() {
//         return document.documentElement.getAttribute('data-theme') === 'dark' ? 'dark' : 'light';
//     }

//     function toggle() {
//         return applyTheme(current() === 'dark' ? 'light' : 'dark', true);
//     }

//     function getStored() {
//         try { return localStorage.getItem(STORAGE_KEY); } catch (e) { return null; }
//     }

//     return {
//         applyTheme: applyTheme,
//         toggle: toggle,
//         current: current,
//         getStored: getStored
//     };
// })();
window.apTheme = (function () {
    const STORAGE_KEY = 'ap-theme';

    function getLinks() {
        return {
            light: document.getElementById('ap-sf-light'),
            dark: document.getElementById('ap-sf-dark')
        };
    }

    function swapSyncfusionTheme(theme) {
        const links = getLinks();

        if (!links.light || !links.dark) {
            return;
        }

        const dark = theme === 'dark';

        links.light.disabled = dark;
        links.dark.disabled = !dark;
    }

    function applyTheme(theme, persist) {
        const t = theme === 'dark' ? 'dark' : 'light';

        document.documentElement.setAttribute('data-theme', t);
        swapSyncfusionTheme(t);

        if (persist) {
            try {
                localStorage.setItem(STORAGE_KEY, t);
            } catch {
                // Storage may be unavailable.
            }
        }

        window.dispatchEvent(
            new CustomEvent('ap-theme-changed', { detail: t })
        );

        return t;
    }

    function current() {
        return document.documentElement.getAttribute('data-theme') === 'dark'
            ? 'dark'
            : 'light';
    }

    function toggle() {
        return applyTheme(
            current() === 'dark' ? 'light' : 'dark',
            true
        );
    }

    function getStored() {
        try {
            return localStorage.getItem(STORAGE_KEY);
        } catch {
            return null;
        }
    }

    return {
        applyTheme,
        toggle,
        current,
        getStored
    };
})();