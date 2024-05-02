import { Component } from '@angular/core';
import { CategoriesListComponent } from '../../../components/catalogue.foder/categories-list/categories-list.component';

@Component({
  selector: 'app-catalogue',
  standalone: true,
  imports: [CategoriesListComponent],
  templateUrl: './catalogue.component.html',
  styleUrl: './catalogue.component.css'
})
export class CatalogueComponent {

}
