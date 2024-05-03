import { Component } from '@angular/core';
import { CategoryListComponent } from '../../../components/catalogue.foder/category-list/category-list.component';
import { ProductListComponent } from '../../../components/catalogue.foder/product-list/product-list.component';

@Component({
  selector: 'app-catalogue',
  standalone: true,
  imports: [CategoryListComponent, ProductListComponent],
  templateUrl: './catalogue.component.html',
  styleUrl: './catalogue.component.css'
})
export class CatalogueComponent {

}
