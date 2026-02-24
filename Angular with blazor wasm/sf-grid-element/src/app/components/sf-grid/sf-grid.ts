import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

// Syncfusion Grid
import { GridModule } from '@syncfusion/ej2-angular-grids';

@Component({
  selector: 'app-sf-grid',                 // (internal selector; not used by the custom element tag)
  standalone: true,
  imports: [CommonModule, GridModule],
  template: `
    <ejs-grid [dataSource]="data" [allowPaging]="true" [allowSorting]="true">
      <e-columns>
        <e-column field="OrderID" headerText="Order ID" width="120" textAlign="Right"></e-column>
        <e-column field="CustomerID" headerText="Customer ID" width="150"></e-column>
        <e-column field="ShipCountry" headerText="Ship Country" width="150"></e-column>
      </e-columns>
    </ejs-grid>
  `
})
export class SfGridComponent {
  public data = [
    { OrderID: 10248, CustomerID: 'VINET', ShipCountry: 'France' },
    { OrderID: 10249, CustomerID: 'TOMSP', ShipCountry: 'Germany' },
    { OrderID: 10250, CustomerID: 'HANAR', ShipCountry: 'Brazil' },
    { OrderID: 10251, CustomerID: 'VICTE', ShipCountry: 'France' }
  ];
}