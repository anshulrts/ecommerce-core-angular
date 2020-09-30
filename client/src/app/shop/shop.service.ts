import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService implements OnInit{
  baseUrl = "https://localhost:44382/api/"

  constructor(private http: HttpClient) { }

  
  ngOnInit() {
    this.getProducts();
  }

  getProducts() {
    return this.http.get<IPagination>(this.baseUrl + 'products?pageSize=50');
  }
}
