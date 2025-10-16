import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomepageContentComponent } from './homepage-content.component';

describe('HomepageContentComponent', () => {
  let component: HomepageContentComponent;
  let fixture: ComponentFixture<HomepageContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HomepageContentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomepageContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
