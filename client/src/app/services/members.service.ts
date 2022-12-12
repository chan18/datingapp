import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../models/memeber';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apirul;

  constructor(private http: HttpClient) { }

  getMembers()
  {
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }

  getMember(username: string)
  {
    return this.http.get<Member>(this.baseUrl + 'user/' + username);
  }
}
