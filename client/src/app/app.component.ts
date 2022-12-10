import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './models/user';
import { AccountService } from './services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title: string = 'Dating app';
  user: any;

  constructor(private http: HttpClient, private accountService: AccountService)  { }

  ngOnInit(): void {
    this.getUsers(); 
    this.setCurrentUser(); 
  }

  getUsers()
  {
    this.http.get<User>("https://localhost:7001/api/users").subscribe({
      next: (response) => this.user = response,
      error: error => console.log(error),
      complete: () => console.log("Requesrt completed")
    })
  }

  setCurrentUser()
  {
    const userString = localStorage.getItem('user');
    if (!userString) return;    
    const user: User = JSON.parse(userString);    
    this.accountService.setCurrentUser(user);
  }

}
