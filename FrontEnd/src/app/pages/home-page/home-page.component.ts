import { Component } from '@angular/core';
import { CategoryListComponent } from "../../components/catalogue.foder/category-list/category-list.component";
import { ProductListComponent } from "../../components/catalogue.foder/product-list/product-list.component";
import { RouterOutlet } from '@angular/router';
import { ProductsByCategoryApiService } from '../../services/products-by-category-api.service';
import { Productos } from '../../domain/interfaces/category-products';

@Component({
    selector: 'app-home-page',
    standalone: true,
    templateUrl: './home-page.component.html',
    styleUrl: './home-page.component.css',
    imports: [CategoryListComponent, ProductListComponent, RouterOutlet]
})
export class HomePageComponent {

    category: string = ''
    productsByCategory: Productos = []


    constructor(private productsByCategoryApiService : ProductsByCategoryApiService){}


searchProductsByCategory(category: string) {
    console.log('HOME PAGE: CLICK ON CATEGORY' + category);

    this.productsByCategoryApiService.getProductsByCategory(category).subscribe({
      next: (response) => {
        console.log(response)
        this.category = category;
        this.productsByCategory = response;
      },

      error: (error) => console.log(error),
    });
    
}

}
