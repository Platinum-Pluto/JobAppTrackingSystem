import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminSearchUsersComponent } from './admin-search-users.component';

describe('AdminSearchUsersComponent', () => {
  let component: AdminSearchUsersComponent;
  let fixture: ComponentFixture<AdminSearchUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminSearchUsersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminSearchUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
