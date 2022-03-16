import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cable-templates-grid',
  templateUrl: './cable-templates-grid.component.html',
  styleUrls: ['./cable-templates-grid.component.scss'],
})
export class CableTemplatesGridComponent implements OnInit {
  items = [
    { text: 'example1', id: "1" },
    { text: 'example2', id: "2" },
    { text: 'example3', id: "3" },
    { text: 'example4', id: "4" },
  ];

  constructor() {}

  ngOnInit(): void {}
}
