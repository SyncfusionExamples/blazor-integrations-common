(function (global) {
  'use strict';

  global.disableSearchAutofill = function (gridId) {
    const grid = document.getElementById(gridId);
    if (!grid) return;
    const input = grid.querySelector('.e-search input');
    if (input) input.setAttribute('autocomplete', 'off');
  };


  global.resetDashboardPane = function (paneIndex) {
    const tabRoot = document.querySelector('.dashboard-wrap .e-tab');
    if (!tabRoot) return;

    const panes = tabRoot.querySelectorAll('.e-content .e-item');
    const pane = panes && panes[paneIndex] ? panes[paneIndex] : null;
    if (!pane) return;

    // 1. Reset inner case-tabs to the first tab (index 0)
    pane.querySelectorAll('.case-tabs-wrap .e-tab').forEach(function (el) {
      const inst = el && el.ej2_instances && el.ej2_instances[0];
      try { if (inst && inst.select) inst.select(0); } catch (e) { }
    });

    // 2. Collapse all accordion items
    pane.querySelectorAll('.e-accordion').forEach(function (el) {
      const acc = el && el.ej2_instances && el.ej2_instances[0];
      if (!acc) return;
      const count = (acc.items && acc.items.length) ||
        el.querySelectorAll('.e-acrdn-item').length || 0;
      for (let i = 0; i < count; i++) {
        try { if (acc.expandItem) acc.expandItem(false, i); } catch (e) { }
      }
    });

    // 3. Scroll the cases list to top
    const list = pane.querySelector('.tab-cases-list');
    if (list) list.scrollTo({ top: 0 });
  };

  function highlightOne(block) {
    try {
      if (!block.classList.contains('hljs')) {
        if (!block.className || block.className.indexOf('language-') === -1) {
          block.classList.add('language-csharp');
        }
        global.hljs.highlightElement(block);
      }
      block.dataset.highlighted = 'true';
    } catch (e) { /* swallow per-block failures */ }
  }

  function highlightAll() {
    if (!global.hljs) return false;
    var blocks = document.querySelectorAll('pre code:not([data-highlighted])');
    for (var i = 0; i < blocks.length; i++) highlightOne(blocks[i]);
    return blocks.length > 0;
  }
  global.highlightAllCode = highlightAll;

  var pollHandle = null;
  function ensureHljsThenHighlight() {
    if (global.hljs) { highlightAll(); return; }
    if (pollHandle) return;
    var attempts = 0;
    pollHandle = setInterval(function () {
      attempts++;
      if (global.hljs) {
        clearInterval(pollHandle); pollHandle = null;
        highlightAll();
      } else if (attempts > 50) { // give up after ~5s
        clearInterval(pollHandle); pollHandle = null;
      }
    }, 100);
  }

  function startObserver() {
    if (!global.MutationObserver) return;
    if (!document.body) return;
    var mo = new MutationObserver(function () { ensureHljsThenHighlight(); });
    mo.observe(document.body, { childList: true, subtree: true });
  }

  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', function () {
      startObserver();
      ensureHljsThenHighlight();
    });
  } else {
    startObserver();
    ensureHljsThenHighlight();
  }
})(window);