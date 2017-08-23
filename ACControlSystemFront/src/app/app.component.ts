import { Component } from '@angular/core';
import { ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css', './bulma.css'],
    encapsulation: ViewEncapsulation.None
})
export class AppComponent {
    title = 'ACControlSystem';
} 