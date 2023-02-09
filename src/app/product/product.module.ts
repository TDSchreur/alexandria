import { NgModule } from '@angular/core';
import { ProductComponent } from './product.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared.module';

const routes: Routes = [{ path: '', component: ProductComponent }];

@NgModule({
  declarations: [ProductComponent],
  imports: [SharedModule, RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ProductModule {}
