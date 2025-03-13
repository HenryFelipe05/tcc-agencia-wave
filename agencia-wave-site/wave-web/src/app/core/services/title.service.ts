import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class TitleService {
  private defaultTitle = 'Agência Wave'; 

  constructor(private title: Title) {}

  updateTitle(pageTitle: string) {
    this.title.setTitle(`${this.defaultTitle} | ${pageTitle}`);
  }
}