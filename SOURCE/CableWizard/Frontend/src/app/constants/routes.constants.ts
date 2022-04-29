import { Routes } from '@angular/router';
import { CableDetailsComponent } from '../components/cable-details/cable-details.component';
import { CableSearchComponent } from '../components/cable-search/cable-search.component';
import { EditorComponent } from '../components/editor/editor.component';

export const routes: Routes = [
  { path: '', component: CableSearchComponent },
  { path: 'details/:id', component: CableDetailsComponent },
  { path: 'editor', component: EditorComponent },
];
