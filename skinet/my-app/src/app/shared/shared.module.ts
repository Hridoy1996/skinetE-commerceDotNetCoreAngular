import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PaginationModule } from 'ngx-bootstrap';


@NgModule({
  declarations: [PagingHeaderComponent],
  imports: [
    CommonModule,
    PaginationModule.foeRoot()
  ],
  exports:[
    PaginationModule,
    PagingHeaderComponent
  ]
})
export class SharedModule { }
