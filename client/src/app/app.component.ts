import { NgFor } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_service/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, NgFor, NavComponent, HomeComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  

  private accountService = inject(AccountService)

  //how we inject services 
  //constructor(private httpClient: HttpClient){}


  //http://localhost:5000/api/users
  ngOnInit(): void {
   // this.getUsers()
    this.setCurrentUser()
  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString){
      return
    }
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);

  }



}
