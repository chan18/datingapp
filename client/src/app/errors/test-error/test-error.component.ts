import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BASEAPIURL } from 'src/app/Constants/AppConstants';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent implements OnInit {
  baseUrl = BASEAPIURL;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error()
  {
    this.http.get(BASEAPIURL + "buggy/not-found").subscribe({
      next: response => console.log(response),
      error: error => console.error(error), 
    })
  }

  get400Error()
  {
    this.http.get(BASEAPIURL + "buggy/bad-request").subscribe({
      next: response => console.log(response),
      error: error => console.error(error), 
    })
  }


  get500Error()
  {
    this.http.get(BASEAPIURL + "buggy/server-error").subscribe({
      next: response => console.log(response),
      error: error => console.error(error), 
    })
  }


  get401Error()
  {
    this.http.get(BASEAPIURL + "buggy/auth").subscribe({
      next: response => console.log(response),
      error: error => console.error(error), 
    })
  }

  get400ValidationError()
  {
    this.http.post(BASEAPIURL + "accounts/register",{}).subscribe({
      next: response => console.log(response),
      error: error => console.error(error), 
    })
  }


}
