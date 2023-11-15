import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user.model';
import { ProfileService } from 'src/app/services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  email: string = ""
  address: string = ""

  router = inject(Router)
  profileService = inject(ProfileService);

  ngOnInit(): void {
    this.profileService.getProfile().subscribe((data : User) => {
      this.email = data.email;
      this.address = data.address;
    });
  }

  getInitial() {
    return this.email[0].toUpperCase();
  }

  goToEdition() {
    this.router.navigate(['/profile-edit']);
  }
}
