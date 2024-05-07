import { Component } from '@angular/core';
import { SearchBarComponent } from './search-bar/search-bar.component';
import { Router, RouterLink } from '@angular/router';
import { AppComponent } from '../../../app.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [SearchBarComponent, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  constructor(private router: Router) {}

  ShoppingCartClick() {
    if (AppComponent.usuario == undefined)
      this.router.navigate(['/log-in'], { queryParams: { page: '/shopping-cart' } });
    else this.router.navigate(['/shopping-cart']);
  }
  UserProfileClick() {
    if (AppComponent.usuario == undefined)
      this.router.navigate(['/log-in'], { queryParams: { page: '/user-profile' } });
    else this.router.navigate(['/user-profile']);
  }
}
