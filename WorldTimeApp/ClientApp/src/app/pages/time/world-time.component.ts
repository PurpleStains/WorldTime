import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TimeModel } from 'src/app/_models/time-model';
import { TimeService } from 'src/app/_services/time.service';

@Component({
  selector: 'app-world-time',
  templateUrl: './world-time.component.html',
  styleUrls: ['./world-time.component.scss']
})
export class WorldTimeComponent implements OnInit {

  isLoading = false;
  time: Date = new Date();
  timeZoneList: any[] = [];
  timeModel: TimeModel;

  constructor( public timeService: TimeService,
    public toastr: ToastrService) { }

  ngOnInit(): void {
    this.get()
  }

  get(){
    this.isLoading = true;
    this.timeModel = new TimeModel()
    this.timeService.get().subscribe(
      (res) => {
        this.timeModel = res;
        this.isLoading = false;
        this.showSuccess();
      },
      (error) => {
        this.isLoading = false;
        this.showDanger();
      }
    )
  }

  showSuccess(){
    this.toastr.success('Operacja wykonana poprawnie', 'Sukces', {
      timeOut: 2000,
    });
  }

  showDanger(){
    this.toastr.error('Api jest niedostępne', 'Błąd', {
      timeOut: 2000,
    });
  }

}
