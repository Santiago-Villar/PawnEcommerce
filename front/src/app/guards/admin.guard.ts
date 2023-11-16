import { CanActivateFn, Router } from '@angular/router';
import { ProfileService } from '../services/profile.service';
import { inject } from '@angular/core';

export function adminGuard(): CanActivateFn {
  return () => {
    const profileService = inject(ProfileService);
    const router = inject(Router);
    
    if (profileService.isAdmin() ) {
      return true;
    }
    
    router.navigate(['/']);
    return false;
  };
};
