import { Component, Input, OnInit } from '@angular/core';
import { DrawerService } from 'src/app/services/drawer.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  constructor(public drawerService: DrawerService) {}

  ngOnInit(): void {}

  toggleDrawer() {
    console.log('NASE');
    this.drawerService.toggle();
  }

  @Input()
  showBurgerMenu: boolean = true;
}
