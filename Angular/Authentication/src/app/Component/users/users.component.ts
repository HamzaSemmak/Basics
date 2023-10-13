import { Component, AfterViewInit, ViewChild } from '@angular/core';
import { AuthService } from 'src/app/Service/auth.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { UpdatePopUpComponent } from '../update-pop-up/update-pop-up.component';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent {
  Users: any;
  dataSource: any;
  @ViewChild(MatPaginator) paginator !:MatPaginator;
  @ViewChild(MatSort) sort !:MatSort;

  constructor(private Service: AuthService, private dialog: MatDialog) {
    this.AllUsers();
  }

  AllUsers() {
    this.Service.All().subscribe( res => {
      this.Users = res; 
      this.dataSource = new MatTableDataSource(this.Users);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    })
  }

  displayedColumns: string[] = ['id', 'name', 'email', 'role', 'status', 'action'];
  
  UpdateUser(id: any) {
    const popup = this.dialog.open(UpdatePopUpComponent, {
      enterAnimationDuration: '1000ms',
      exitAnimationDuration: '500ms',
      width: '50%',
      data: {
        userID: id
      }
    });

    popup.afterClosed().subscribe( res => {
      this.AllUsers();
    })
  }

}
