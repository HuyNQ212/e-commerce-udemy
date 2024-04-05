import {Component, OnInit} from '@angular/core';
import {Product} from "../shared/models/product";
import {ShopService} from "./shop.service";
import {Brand} from "../shared/models/brand";
import {Type} from "../shared/models/type";
import {Pagination} from "../shared/models/pagination";
import {count} from "rxjs";

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  productPaginated!: Pagination<Product>;
  pageIndex: number = 1;
  pageSize: number = 6;
  pageSizes = [6, 12, 24, 60];
  brandIdSelected: number = 0;
  typeIdSelected: number = 0;
  sortSelected = "name";
  sortOptions = [
    {name: "Alphabetical", value: "name"},
    {name: "Price: Low to high", value: "priceAsc"},
    {name: "Price: High to low", value: "priceDesc"},
  ]

  constructor(private shopService: ShopService) {
  }

  ngOnInit(): void {
    this.getProduct(this.pageIndex, this.pageSize);
    this.getBrands();
    this.getTypes();
  }

  getProduct(pageIndex: number, pageSize: number) {
    this.shopService.getProducts(pageIndex, pageSize, this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe({
      next: response => {
        this.productPaginated = response
      },
      error: err => console.log(err)
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => this.brands = [{id: 0, name: "All"}, ...response],
      error: err => console.log(err)
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: response => this.types = [{id: 0, name: "All"}, ...response],
      error: err => console.log(err)
    });
  }

  onBrandSelected(brandId: number) {
    this.brandIdSelected = brandId;
    this.getProduct(this.pageIndex, this.pageSize);
  }

  onTypeSelected(typeId: number) {
    this.typeIdSelected = typeId;
    this.getProduct(this.pageIndex, this.pageSize);
  }

  onSortSelected(event: any) {
    this.sortSelected = event.target.value;
    console.log(this.sortSelected);
    this.getProduct(this.pageIndex, this.pageSize);
  }

  onNext() {
    this.pageIndex++;
    this.getProduct(this.pageIndex, this.pageSize);
  }

  pageChange(event: any): void {
    this.pageIndex = event.page;
    this.getProduct(this.pageIndex, this.pageSize);
  }

  pageSizeChanged():void {
    this.getProduct(this.pageIndex, this.pageSize);
  }

  protected readonly count = count;
}
