import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
  name: 'dateTimeFormat',
  standalone: true,
})
export class DateTimeFormatPipe implements PipeTransform {

   transform(value: Date | string | null | undefined): string {
   
  if (!value) return '';

  const datePipe = new DatePipe('pt-BR');

  return datePipe.transform(value, 'dd/MM/yyyy HH:mm') ?? '';
 
  }

}
