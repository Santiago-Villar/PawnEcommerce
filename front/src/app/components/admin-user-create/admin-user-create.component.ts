import { Component, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-user-create',
  templateUrl: './admin-user-create.component.html', 
  styleUrls: ['../admin-user-detail/admin-user-detail.component.css']
})
export class AdminUserCreateComponent implements OnInit {

  newUser: any = {};

  constructor(
    private usersService: UsersService,
    private router: Router
  ) {}

  toastrService = inject(ToastrService)
  authService = inject(AuthService)

  ngOnInit(): void {
  }


  goBack(): void {
    this.router.navigate(['/admin/users']);
  }

  compareUserObj(obj1: any, obj2: any): boolean {
    return obj1 && obj2 ? obj1.id === obj2.id : obj1 === obj2;
  }

  createUser(formUser: any): void {
    const { email, address, password } = formUser;
    const newUserData = { email, address, password }
    // TODO: isCurrentUser should be true if the user is editing himself
    const isCurrentUser = false;
    this.usersService.createUser(newUserData)
    .subscribe({
      next: (p : User) => {
        this.toastrService.success("Usuario creado correctamente!", '', {
          progressBar: true,
          timeOut: 2000,
        });
        this.goBack();
        if(isCurrentUser) this.authService.logout();
      },
      error: () => {
        this.toastrService.error("Ocurrió un error al crear el Usuario", '', {
          progressBar: true,
          timeOut: 2000,
        });
      },
    });
  }
}
