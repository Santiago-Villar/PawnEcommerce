import { Component, HostListener, OnInit, inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { UsersService } from 'src/app/services/users.service';
import { User } from 'src/app/models/user.model';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['../admin-products/admin-products.component.css']
})
export class AdminUsersComponent implements OnInit {
  users: User[] = [];
  innerWidth: number = window.innerWidth;
  isSmallScreen : boolean = this.innerWidth < 900;
  isMobile : boolean = this.innerWidth < 650;
  isLoading: boolean = true;

  constructor(private router: Router, private dialog: MatDialog) {}

  usersService = inject(UsersService)
  toastrService = inject(ToastrService)

  ngOnInit(): void {
    this.loadUsers();
  }

  get hasUsers(): boolean {
    return this.users.length > 0;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event : any) {
    this.innerWidth = window.innerWidth;
    this.isSmallScreen = this.innerWidth < 900;
    this.isMobile = this.innerWidth < 650;
  }

  loadUsers(): void {
    this.usersService.getUsers().subscribe((data : User[]) => {
      this.users = data.reverse();
      this.isLoading = false;
    }, (error) => this.isLoading = false);
  }

  goBack(): void {
    this.router.navigate(['/admin']);
  }

  seeUser(id: string): void {
    this.router.navigate(['/admin/users', id]);
  }

  editUser(id: string): void {
    this.router.navigate(['/admin/users/edit', id]);
  }

  createUser(): void {
    this.router.navigate(['/admin/users/create']);
  }

  deleteUser(id: string): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { modelName: 'Usuario' }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.usersService.deleteUser(id)
        .subscribe({
          next: () => {
            this.toastrService.success("Usuario eliminado correctamente!", '', {
              progressBar: true,
              timeOut: 2000,
            });
            this.users  = this.users.filter((users : User) => users.id !== id);
          },
          error: () => {
            this.toastrService.error("Ocurrió un error al eliminar el Usuario", '', {
              progressBar: true,
              timeOut: 2000,
            });
          },
        });
      }
    });
  }
}
