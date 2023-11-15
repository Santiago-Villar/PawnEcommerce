import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './admin-user-edit.component.html',
  styleUrls: ['../admin-user-detail/admin-user-detail.component.css']
})
export class AdminUserEditComponent implements OnInit {
  user: User | null = null;
  updatedUser: User | null = null;

  constructor(
    private route: ActivatedRoute,
    private usersService: UsersService,
    private router: Router
  ) {}

  toastrService = inject(ToastrService)


  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.loadUser(id);
    });
  }

  loadUser(id: string): void {
    this.usersService.getUser(id).subscribe((data: User) => {
      this.user = data;
      this.updatedUser = data;
    });
  }

  goBack(): void {
    this.router.navigate(['/admin/users']);
  }

  compareUserObj(obj1: any, obj2: any): boolean {
    return obj1 && obj2 ? obj1.id === obj2.id : obj1 === obj2;
  }


  saveChanges(formUser: any): void {
    const { id, email, address } = formUser;
    const updatedUser = { email, address }

    this.usersService.updateUser(id, updatedUser)
    .subscribe({
      next: () => {
        this.toastrService.success("Usuario actualizado correctamente!", '', {
          progressBar: true,
          timeOut: 2000,
         });
      },
      error: () =>{
        this.toastrService.error("Ocurrio un error al actualizar el Usuario", '', {
          progressBar: true,
          timeOut: 2000,
         });
      },

      complete: () => {
        this.router.navigate([`/admin/users/${id}`]);
      },
    })
  }

}
