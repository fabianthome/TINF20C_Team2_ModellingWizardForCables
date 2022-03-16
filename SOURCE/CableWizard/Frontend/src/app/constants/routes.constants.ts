import { Routes } from '@angular/router';
import { CableDetailsComponent } from '../components/cable-details/cable-details.component';
import {CableSearchComponent} from "../Components/cable-search/cable-search.component";

export const routes: Routes = [
  { path: '', component: CableSearchComponent },
  { path: 'details/:id', component: CableDetailsComponent },
];
