import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowBarComponent } from './show-bar.component';

describe('ShowBarComponent', () => {
  let component: ShowBarComponent;
  let fixture: ComponentFixture<ShowBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
