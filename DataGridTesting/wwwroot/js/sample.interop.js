/* =====================================================================
   sample.interop.js
   ---------------------------------------------------------------------
   Blazor JS-interop helpers used by GridClient.razor and TestDashboard.razor.
   Kept as plain global functions on `window` so they can be invoked via
   IJSRuntime.InvokeVoidAsync("functionName", ...) without a module import.

   These mirror the React helpers used in GridClient.tsx and TestDashboard.tsx
   (disableSearchAutofill, resetPaneState). highlight.js replaces CodeMirror.
   ===================================================================== */

(function (global) {
  'use strict';

  /**
   * Mirrors disableSearchAutofill() in GridClient.tsx.
   * Sets autocomplete="off" on the toolbar search input.
   * SfGrid<TValue> has no Blazor parameter for this DOM attribute,
   * so JS interop is the recommended approach.
   * @param {string} gridId  The ID of the SfGrid element.
   */
  global.disableSearchAutofill = function (gridId) {
    const grid = document.getElementById(gridId);
    if (!grid) return;
    const input = grid.querySelector('.e-search input');
    if (input) input.setAttribute('autocomplete', 'off');
  };

  /**
   * Mirrors the CodeMirror readonly highlighting used in TestDashboard.tsx.
   * highlight.js processes every <pre><code> block not yet highlighted.
   */
  global.highlightAllCode = function () {
    if (!global.hljs) return;
    document.querySelectorAll('pre code').forEach(function (block) {
      if (!block.dataset.highlighted) {
        global.hljs.highlightElement(block);
        block.dataset.highlighted = 'true';
      }
    });
  };

  /**
   * Mirrors resetPaneState(pane) in TestDashboard.tsx:
   *   1. Reset inner case-tabs (Code/Steps) to index 0
   *   2. Collapse all accordion items in the active pane
   *   3. Scroll .tab-cases-list to top
   * Inner SfTab / SfAccordion instances are rendered inside foreach loops
   * without individual @ref handles, so JS interop targeting the ej2
   * DOM instances is the cleanest equivalent to React's
   * `el.ej2_instances[0]` access.
   * @param {number} paneIndex  Zero-based index of the active framework tab.
   */
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
})(window);