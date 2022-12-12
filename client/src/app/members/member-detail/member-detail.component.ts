import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/models/memeber';
import { MembersService } from 'src/app/services/members.service';
import {NgxGalleryOptions} from '@kolkov/ngx-gallery';
import {NgxGalleryImage} from '@kolkov/ngx-gallery';
import {NgxGalleryAnimation} from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member | undefined;
  galleryOptions: NgxGalleryOptions[] = [];
  galleryImages: NgxGalleryImage[] = [];

  constructor(private memberService: MembersService, private route: ActivatedRoute) { }

  ngOnInit(): void {    
    this.loadMember();

    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false,
      }
    ]
  }

  getImages()
  {
    if(!this.member) return [];
    const imageUrls = [];
    for(const photo of this.member.photos)
    {
      imageUrls.push({
        small: photo.url,
        medium: photo.url,
        big: photo.url
      })
    }

    return imageUrls;
  }

  loadMember()
  {
    const username = this.route.snapshot.paramMap.get('username');

    if (!username) return;

    this.memberService.getMember(username).subscribe({
      next:member => {
        this.member = member;
        this.galleryImages = this.getImages();
      },
    })
  }

}
