var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
let SharedService = class SharedService {
    constructor(http) {
        this.http = http;
        this.APIUrl = 'https://localhost:44395/api';
        this.headers = new HttpHeaders({ 'Access-Control-Allow-Origin': 'https://localhost:44395' });
    }
    getBarbersList() {
        return this.http.get(this.APIUrl + '/Barbers');
    }
};
SharedService = __decorate([
    Injectable()
], SharedService);
export { SharedService };
//# sourceMappingURL=shared.service.js.map