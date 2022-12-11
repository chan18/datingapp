import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode: boolean = false;
  users: User | null = null;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle()
  {
    this.registerMode = !this.registerMode;
  }

  getUsers()
  {
    this.http.get<User>("https://localhost:7001/api/users").subscribe({
      next: (response) => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("Requesrt completed")
    })
  }

  cancelRegisterMode(event: boolean)
  {
    this.registerMode = event;
  }

}
