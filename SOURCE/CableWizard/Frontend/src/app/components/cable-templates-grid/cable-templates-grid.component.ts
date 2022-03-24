import {Component, OnDestroy, OnInit} from '@angular/core';
import {CableList, DataService} from "../../services/data.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-cable-templates-grid',
  templateUrl: './cable-templates-grid.component.html',
  styleUrls: ['./cable-templates-grid.component.scss'],
})
export class CableTemplatesGridComponent implements OnInit, OnDestroy {
  cables : CableList = [];
  subscription : Subscription | undefined;

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    this.subscription = this.dataService.getCableList().subscribe({
      next: value => this.cables = value,
    })
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

}
