import { Routes } from '@angular/router';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home-page.component';
import { UserProfileInitComponent } from './pages/user-profile/user-profile-init/user-profile-init.component';
import { UserProfileOrderListComponent } from './pages/user-profile/user-profile-order-list/user-profile-order-list.component';
import { ShoppingCartComponent } from './pages/shopping-cart/shopping-cart.component';
import { UserProfilePersonalInfoComponent } from './pages/user-profile/user-profile-personal-info/user-profile-personal-info.component';
import { LogInComponent } from './pages/log-in/log-in.component';
import { SignInComponent } from './pages/sign-in/sign-in.component';
import { ProcessOrderComponent } from './pages/process-order/process-order.component';

export const routes: Routes = [
  {
    path: '',
    component: HomePageComponent,
  },
  {
    path: 'user-profile',
    component: UserProfileComponent,
    children: [
      {
        path: '',
        component: UserProfileInitComponent,
      },
      {
        path: 'personal-info',
        component: UserProfilePersonalInfoComponent,
      },
      {
        path: 'order-list',
        component: UserProfileOrderListComponent,
      },
    ],
  },
  {
    path: 'shopping-cart',
    component: ShoppingCartComponent,
  },
  {
    path: 'process-order',
    component: ProcessOrderComponent,
  },

  {
    path: 'log-in',
    component: LogInComponent,
  },
  {
    path: 'sign-in',
    component: SignInComponent,
  },
];
