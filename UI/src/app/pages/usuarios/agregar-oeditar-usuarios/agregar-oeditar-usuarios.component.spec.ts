import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AgregarOEditarUsuariosComponent } from './agregar-oeditar-usuarios.component';

describe('AgregarOEditarUsuariosComponent', () => {
  let component: AgregarOEditarUsuariosComponent;
  let fixture: ComponentFixture<AgregarOEditarUsuariosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AgregarOEditarUsuariosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AgregarOEditarUsuariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
