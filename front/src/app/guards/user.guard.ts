import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ProfileService } from '../services/profile.service';

export function userGuard(): CanActivateFn {
  return () => {
    const profileService = inject(ProfileService);
    const router = inject(Router);
    
    if (profileService.isUser() ) {
      return true;
    }
    
    router.navigate(['/']);
    return false;
  };
};