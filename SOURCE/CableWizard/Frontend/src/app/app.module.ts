import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HeaderComponent} from './components/header/header.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {CableTemplatesGridComponent} from './components/cable-templates-grid/cable-templates-grid.component';
import {MatCardModule} from '@angular/material/card';
import {MatToolbarModule} from '@angular/material/toolbar';
import {CableDetailsComponent} from './components/cable-details/cable-details.component';
import {CableSearchComponent} from './components/cable-search/cable-search.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import { CableEditorTabsComponent } from './components/cable-editor-tabs/cable-editor-tabs.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CableTemplatesGridComponent,
    CableDetailsComponent,
    CableSearchComponent,
    CableEditorTabsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatCardModule,
    MatToolbarModule,
    MatSidenavModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {
}
