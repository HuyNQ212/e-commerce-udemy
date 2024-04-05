import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ShopComponent} from './shop.component';
import {HttpClientModule} from "@angular/common/http";
import { ProductItemComponent } from './product-item/product-item.component';
import {SharedModule} from "../shared/shared.module";
import {FormsModule} from "@angular/forms";


@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent
  ],
  exports: [
    ShopComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    SharedModule,
    FormsModule
  ]
})
export class ShopModule {
}
