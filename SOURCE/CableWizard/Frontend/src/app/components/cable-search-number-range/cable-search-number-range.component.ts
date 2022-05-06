import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NumberRangeFilter} from "../../models/filter-options";

@Component({
  selector: 'app-cable-search-number-range',
  templateUrl: './cable-search-number-range.component.html',
  styleUrls: ['./cable-search-number-range.component.scss']
})
export class CableSearchNumberRangeComponent implements OnInit {
  @Input() attribute: NumberRangeFilter = new NumberRangeFilter;
  @Input() name?: string
  @Output() attributeChange = new EventEmitter

  constructor() {
  }

  ngOnInit(): void {
  }

  onAttributeChanged() {
    this.attributeChange.emit()
  }
}
