import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApresentadoresComponent } from './apresentadores';

describe('ApresentadoresComponent', () => {
  let component: ApresentadoresComponent;
  let fixture: ComponentFixture<ApresentadoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApresentadoresComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ApresentadoresComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
