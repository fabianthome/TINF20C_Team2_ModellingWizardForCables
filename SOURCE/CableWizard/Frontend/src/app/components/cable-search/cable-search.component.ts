import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDrawer } from '@angular/material/sidenav';
import { fromEvent } from 'rxjs';
import { DrawerService } from 'src/app/services/drawer.service';

@Component({
  selector: 'app-cable-search',
  templateUrl: './cable-search.component.html',
  styleUrls: ['./cable-search.component.scss'],
})
export class CableSearchComponent implements OnInit {
  @ViewChild('drawer') public drawer: MatDrawer | undefined;
  searchQuery: string = '';

  resize$ = fromEvent(window, 'resize');
  resizeSubscription = this.resize$.subscribe();

  constructor(public drawerService: DrawerService) {}

  ngOnInit(): void {
    this.drawerService.setDrawer(this.drawer!);
  }
}
