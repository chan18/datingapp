import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { Member } from 'src/app/models/memeber';
import { User } from 'src/app/models/user';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  member: Member | undefined;
  user: User | null = null;

  constructor(private accountService: AccountService,private memberService: MembersService) { 
    this.accountService.currentUser$.pipe(take(1)).subscribe(
      {
        next: user => this.user = user,
      }
    )
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember()
  {
    if (!this.user) return;

     this.memberService.getMember(this.user.userName).subscribe({
       next: member => this.member = member,
     });
  }

}
