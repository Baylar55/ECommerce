import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoppingCompleteteDialogComponent } from './shopping-completete-dialog.component';

describe('ShoppingCompleteteDialogComponent', () => {
  let component: ShoppingCompleteteDialogComponent;
  let fixture: ComponentFixture<ShoppingCompleteteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShoppingCompleteteDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShoppingCompleteteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
