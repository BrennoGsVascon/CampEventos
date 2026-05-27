import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoListaComponent } from './evento-lista';

describe('EventoListaComponent', () => {
  let component: EventoListaComponent;
  let fixture: ComponentFixture<EventoListaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventoListaComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EventoListaComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
