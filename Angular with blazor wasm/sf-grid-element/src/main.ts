import { createApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { createCustomElement } from '@angular/elements';
import { SfGridComponent } from './app/components/sf-grid/sf-grid';

(async () => {
  try {
    // Create an Angular application context without bootstrapping any root component
    const app = await createApplication({
      providers: [provideHttpClient()]
    });

    const element = createCustomElement(SfGridComponent, { injector: app.injector });
    if (!customElements.get('sf-grid')) {
      customElements.define('sf-grid', element);
    }
  } catch (err) {
    console.error(err);
  }
})();

