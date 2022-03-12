import { Routes } from '@angular/router';
import { CableDetailsComponent } from '../components/cable-details/cable-details.component';
import { CableTemplatesGridComponent } from '../components/cable-templates-grid/cable-templates-grid.component';

export const routes: Routes = [
  { path: '', component: CableTemplatesGridComponent },
  { path: 'details/:id', component: CableDetailsComponent },
];
