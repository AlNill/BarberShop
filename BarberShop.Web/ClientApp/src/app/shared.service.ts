import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable()
export class SharedService {
  readonly APIUrl = 'https://localhost:44395/api';
  private headers: HttpHeaders;

  constructor(private http:HttpClient) {
    this.headers = new HttpHeaders({'Access-Control-Allow-Origin': 'https://localhost:44395'}); 
  }

  getBarbersList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/Barbers');
  }
}
