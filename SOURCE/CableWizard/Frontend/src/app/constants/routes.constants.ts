import { Routes } from '@angular/router';
import { CableSearchComponent } from '../components/cable-search/cable-search.component';
import { EditorComponent } from '../components/editor/editor.component';

export const routes: Routes = [
  { path: '', component: CableSearchComponent },
  { path: 'details/:id', component: EditorComponent },
  { path: 'editor', component: EditorComponent },
];
