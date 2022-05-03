import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HeaderComponent} from './components/header/header.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {CableSearchGridComponent} from './components/cable-search-grid/cable-search-grid.component';
import {MatCardModule} from '@angular/material/card';
import {MatToolbarModule} from '@angular/material/toolbar';
import {CableDetailsComponent} from './components/cable-details/cable-details.component';
import {CableSearchComponent} from './components/cable-search/cable-search.component';
import {CableSearchNumberRangeComponent} from "./components/cable-search-number-range/cable-search-number-range.component";
import {MatSidenavModule} from '@angular/material/sidenav';
import {HttpClientModule} from '@angular/common/http';
import {MatChipsModule} from '@angular/material/chips';
import {MatExpansionModule} from '@angular/material/expansion';
import {CableEditorTabsComponent} from './components/cable-editor-tabs/cable-editor-tabs.component';
import {EditorComponent} from './components/editor/editor.component';
import {MatDividerModule} from "@angular/material/divider";
import {MatTooltipModule} from "@angular/material/tooltip";

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    CableDetailsComponent,
    CableSearchComponent,
    CableSearchGridComponent,
    CableSearchNumberRangeComponent,
    CableEditorTabsComponent,
    EditorComponent,
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
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
        ReactiveFormsModule,
        MatChipsModule,
        MatExpansionModule,
        MatDividerModule,
        MatTooltipModule,
    ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {
}
