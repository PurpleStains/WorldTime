import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { WorldTimeComponent } from './pages/time/world-time.component';
import { ToastContainerModule, ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    WorldTimeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass :'toast-top-right'
    }),
    ToastContainerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
