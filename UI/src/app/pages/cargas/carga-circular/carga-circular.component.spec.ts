import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CargaCircularComponent } from './carga-circular.component';

describe('CargaCircularComponent', () => {
  let component: CargaCircularComponent;
  let fixture: ComponentFixture<CargaCircularComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CargaCircularComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CargaCircularComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
