import { Component } from '@angular/core';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { bootstrapSearch } from '@ng-icons/bootstrap-icons';

@Component({
  selector: 'app-galery-nav',
  imports: [NgIcon],
  templateUrl: './galery-nav.component.html',
  viewProviders: [provideIcons({ bootstrapSearch })],
  styleUrl: './galery-nav.component.css'
})
export class GaleryNavComponent {

}
