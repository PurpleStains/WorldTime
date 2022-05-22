import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TimeModel } from '../_models/time-model';

@Injectable({
  providedIn: 'root'
})
export class TimeService {

  constructor(private http: HttpClient) { }

  get(){
    return this.http.get<TimeModel>(`WorldTime/GetTime`)
  }
}
