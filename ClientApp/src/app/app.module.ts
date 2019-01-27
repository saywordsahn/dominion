import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule} from "@angular/platform-browser/animations";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { SupplyComponent } from './supply/supply.component';
import { PlayerComponent } from './player/player.component';
import { HandViewerComponent } from "./hand-viewer/hand-viewer.component";
import {PlayAreaComponent} from "./play-area/play-area.component";
import {CardOrganizerComponent}  from "./card-organizer/card-organizer.component";
import {GameComponent} from './game/game.component';
import { LobbyComponent } from './lobby/lobby.component';


import {LoginDialogComponent} from './login-dialog/login-dialog.component';

import { DataService } from './services/data.service';
import { CardService } from './services/card.service';
import { HubService } from './services/hub.service';
import {UserService} from "./services/user.service";
import {GameLogService} from './services/game-log.service';

//primeng
import {MultiSelectModule} from 'primeng/multiselect';
import {Dialog, DialogModule} from 'primeng/dialog';

//material
import {DragDropModule} from '@angular/cdk/drag-drop';
import {MatDialogModule} from "@angular/material";
import {MatMenuModule} from "@angular/material";
import {MatFormFieldModule} from "@angular/material";
import {MatInputModule} from '@angular/material';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    SupplyComponent,
    PlayerComponent,
    HandViewerComponent,
    PlayAreaComponent,
    CardOrganizerComponent,
    LoginDialogComponent,
    GameComponent,
    LobbyComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'lobby', component: LobbyComponent},
      { path: 'game', component: GameComponent},
    ]),
    MultiSelectModule,
    DialogModule,
    DragDropModule,
    MatDialogModule,
    MatMenuModule,
    MatFormFieldModule,
    MatInputModule
  ],
  entryComponents: [
      CardOrganizerComponent,
      LoginDialogComponent
  ],
  providers: [
      DataService,
      CardService,
      HubService,
      UserService,
      GameLogService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
