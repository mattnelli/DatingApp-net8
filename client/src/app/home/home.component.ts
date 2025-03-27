import { NgIf } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  imports: [NgIf, RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient)
  registerMode = false;
  users: any;

  registerToggle(){
    this.registerMode = !this.registerMode
  }

  cancelRegisterMode(event: boolean){
    this.registerMode = event; 
  }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(){
    this.http.get('http://localhost:5000/api/users').subscribe({
      next: Response => this.users = Response,
      error: error => console.log(error),
      complete: () => console.log('Request Completed')
    })
  }




}
