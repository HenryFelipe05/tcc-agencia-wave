import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class TitleService {
  private defaultTitle = 'Agência Wave'; 

  constructor(private title: Title) {}

  updateTitle(pageTitle: string) {
    if(pageTitle == "" || pageTitle == null) 
      this.title.setTitle(`${this.defaultTitle}`);
    else 
      this.title.setTitle(`${this.defaultTitle} | ${pageTitle}`);
  }
}