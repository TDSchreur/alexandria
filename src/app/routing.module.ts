import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'landing', pathMatch: 'full' },
  {
    path: 'landing',
    loadChildren: () =>
      import('./landing/landing.module').then((m) => m.LandingModule),
  },
  {
    path: 'product',
    loadChildren: () =>
      import('./product/product.module').then((m) => m.ProductModule),
  },

  //  { path: '**', redirectTo: 'landing', pathMatch: 'full' },
  //  { path: '', component: WelcomeComponent },
  //  { path: 'signup', component: SignupComponent },
  //  { path: 'login', component: LoginComponent },
  //  { path: 'training', component: TrainingComponent },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { useHash: true, enableTracing: true }),
  ],
  exports: [RouterModule],
})
export class RoutingModule {}
