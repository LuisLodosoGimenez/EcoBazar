import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [],
  templateUrl: './category-card.component.html',
  styleUrl: './category-card.component.css',
})
export class CategoryCardComponent {
  @Input() categoryName!: string;
  @Input() categoryDescription!: string;
  @Input() categoryImage!: string;

  @Output() CategoryEvent = new EventEmitter<string>();

  clickCategory() {
    this.CategoryEvent.emit(this.categoryName)
    console.log('CATEGORY CARD: CLICK ON CATEGORY' + this.categoryName);
  }
}
