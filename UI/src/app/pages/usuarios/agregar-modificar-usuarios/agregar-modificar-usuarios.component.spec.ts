import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarModificarUsuariosComponent } from './agregar-modificar-usuarios.component';

describe('AgregarOEditarUsuariosComponent', () => {
  let component: AgregarModificarUsuariosComponent;
  let fixture: ComponentFixture<AgregarModificarUsuariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgregarModificarUsuariosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgregarModificarUsuariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
