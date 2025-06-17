import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GaleryNavComponent } from './galery-nav.component';

describe('GaleryNavComponent', () => {
  let component: GaleryNavComponent;
  let fixture: ComponentFixture<GaleryNavComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GaleryNavComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GaleryNavComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
