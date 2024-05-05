import { Component, EventEmitter, Output } from '@angular/core';
import { CategoryCardComponent } from "./category-card/category-card.component";
import { category } from '../../../domain/interfaces/category.interface';

@Component({
  selector: 'app-category-list',
  standalone: true,
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css',
  imports: [CategoryCardComponent],
})
export class CategoryListComponent {
  @Output() categoryEvent = new EventEmitter<string>();
  
  categories: category[] = [
    {
      name: 'Electronica',
      description: 'Portatil, smartphone, auriculares...',
      image: 'https://www.merchantcapital.co.za/hubfs/Electronic%20Online%20Header-01.jpg',
    },
    {
      name: 'Deportes',
      description: 'Pelotas, guantes, ropa deportiva...',
      image: 'https://miro.medium.com/v2/resize:fit:926/1*JgdL4tQiU7_kY5dAQdYvTQ.png',
    },
    {
      name: 'Hogar',
      description: 'Muebles, uso diario, decoraciones...',
      image: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR_jeSGARGMd-97fEm_dtmHYa55TGO1SCSmtk8Sg7E7CQ&s',
    },
    {
      name: 'Higiene pers.',
      description: 'Gel de ducha, jabón, cepillo de dientes...',
      image: 'https://convoyofhope.org/wp-content/uploads/2023/04/Travel-Hygiene-Kits-1.jpg.webp',
    },
    {
      name: 'Moda',
      description: 'Ropa, zapatos, joyería, accesorios...',
      image: 'https://hips.hearstapps.com/hmg-prod/images/propositos-moda-tendencia-1-1672662382.jpg',
    },
    {
      name: 'Supermercado',
      description: 'Conservas, frutas, pescados...',
      image:
        'https://news.mit.edu/sites/default/files/styles/news_article__image_gallery/public/images/202312/MIT_Food-Diabetes-01_0.jpg?itok=Mp8FVJkC',
    },
  ];

  clickCategory(category: string) {
    console.log('CATEGORY LIST: CLICK ON CATEGORY' + category);
    this.categoryEvent.emit(category)
  }
}
