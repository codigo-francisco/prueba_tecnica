import { NgModule } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDialogModule} from '@angular/material/dialog';
import {MatInputModule} from '@angular/material/input';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatCardModule} from '@angular/material/card';


/**
 * Modulos para importar y exportar de Angular Material
 */
const modules = [
  MatTableModule,
  MatButtonModule,
  MatIconModule,
  MatFormFieldModule,
  MatDialogModule,
  MatInputModule,
  MatPaginatorModule,
  MatSortModule,
  MatProgressSpinnerModule,
  MatCardModule
];

/**
 * Modulo que contiene los modulos de Angular MAterial
 */
@NgModule({
  declarations: [],
  imports: modules,
  exports: modules
})
export class MaterialModule { }
