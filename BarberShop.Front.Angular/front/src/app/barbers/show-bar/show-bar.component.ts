import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';


@Component({
  selector: 'app-show-bar',
  templateUrl: './show-bar.component.html',
  styleUrls: ['./show-bar.component.scss']
})
export class ShowBarComponent implements OnInit {

  constructor(private service:SharedService) { }
  toggle = true
  BarbersList:any=[]

  ngOnInit(): void {
    this.refreshBarbersList();
  }

  refreshBarbersList(){
    this.service.getBarbersList().subscribe(
      data=>{
        this.BarbersList=data;
      }
    );
  }

  toggleCards(){
    this.toggle = !this.toggle
  }
}
