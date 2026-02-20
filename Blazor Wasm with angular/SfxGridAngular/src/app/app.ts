
import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
@Component({
  selector: 'app-root',
  template: `<sf-orders-grid #gridEl [pageSize]="25"></sf-orders-grid>`,
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class App  {
  
}