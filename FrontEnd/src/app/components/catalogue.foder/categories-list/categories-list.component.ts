import { Component } from '@angular/core';
import { CategoryCardComponent } from './category-card/category-card.component';

@Component({
  selector: 'app-categories-list',
  standalone: true,
  imports: [CategoryCardComponent],
  templateUrl: './categories-list.component.html',
  styleUrl: './categories-list.component.css'
})
export class CategoriesListComponent {

}
