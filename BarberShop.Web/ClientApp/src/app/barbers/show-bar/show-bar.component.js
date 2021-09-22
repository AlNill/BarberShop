var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from '@angular/core';
let ShowBarComponent = class ShowBarComponent {
    constructor(service) {
        this.service = service;
        this.toggle = true;
        this.BarbersList = [];
    }
    ngOnInit() {
        this.refreshBarbersList();
    }
    refreshBarbersList() {
        this.service.getBarbersList().subscribe(data => {
            this.BarbersList = data;
        });
    }
    toggleCards() {
        this.toggle = !this.toggle;
    }
};
ShowBarComponent = __decorate([
    Component({
        selector: 'app-show-bar',
        templateUrl: './show-bar.component.html',
        styleUrls: ['./show-bar.component.scss']
    })
], ShowBarComponent);
export { ShowBarComponent };
//# sourceMappingURL=show-bar.component.js.map