import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentNotificationComponent } from './comment-notification.component';

describe('CommentNotificationComponent', () => {
  let component: CommentNotificationComponent;
  let fixture: ComponentFixture<CommentNotificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommentNotificationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CommentNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
