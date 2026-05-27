import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventoDetalheComponent} from './evento-detalhe';

describe('EventoDetalheComponent', () => {
  let component: EventoDetalheComponent;
  let fixture: ComponentFixture<EventoDetalheComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventoDetalheComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EventoDetalheComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
