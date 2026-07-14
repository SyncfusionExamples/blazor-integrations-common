window.axeAudit = {
    run: async function (selector) {
        if (typeof axe === 'undefined') {
            throw new Error('axe-core is not loaded');
        }

        const root = (selector && document.querySelector(selector))
            || document.querySelector('.accessibility-container .e-grid')
            || document;

        const result = await axe.run(root, {
            runOnly: { type: 'tag', values: ['wcag2a', 'wcag2aa', 'wcag21a', 'wcag21aa', 'best-practice'] },
            resultTypes: ['violations'],
            reporter: 'v2'
        });

        const project = (v) => ({
            id: v.id,
            description: v.description,
            help: v.help,
            helpUrl: v.helpUrl || '',
            impact: v.impact || null,
            nodes: (v.nodes || []).map(n => ({
                target: Array.isArray(n.target) ? n.target.map(String) : [],
                html: n.html ? String(n.html) : ''
            }))
        });

        return {
            violations: (result.violations || []).map(project),
            incomplete: (result.incomplete || []).map(project)
        };
    },

    highlightSnippets: function (containerSelector) {
        if (typeof hljs === 'undefined') {
            console.warn('highlight.js is not loaded');
            return;
        }

        const root = containerSelector
            ? document.querySelector(containerSelector)
            : document;
        if (!root) return;

        const blocks = root.querySelectorAll('pre code.hljs-source:not(.hljs)');
        blocks.forEach((block) => {
            hljs.highlightElement(block);
        });
    },

    disableSearchAutofill: function (gridId) {
        var el = gridId
            ? document.querySelector('#' + gridId + ' .e-search input')
            : document.querySelector('.e-search input');

        if (el) {
            el.setAttribute('autocomplete', 'off');
        }
    }
};