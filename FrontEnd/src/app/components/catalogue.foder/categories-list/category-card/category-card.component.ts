import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [],
  templateUrl: './category-card.component.html',
  styleUrl: './category-card.component.css',
})
export class CategoryCardComponent {

  @Input() categoryName: string = 'titulo';
  @Input() categoryDescription: string = 'descripción';

  
}
