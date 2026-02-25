// grid-elements/src/main.ts
import { createApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { createCustomElement } from '@angular/elements';
import { App } from './app/app';

(async () => {
  try {
    const app = await createApplication({
      providers: [ provideHttpClient() ]
    });

    const element = createCustomElement(App, { injector: app.injector });
    if (!customElements.get('sf-grid')) {
      customElements.define('sf-grid', element);
    }
  } catch (err) {
    console.error(err);
  }
})();