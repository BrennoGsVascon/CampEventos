import {  formatDate } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateTimeFormat',
  standalone: true
})
export class DateTimeFormatPipe implements PipeTransform {

  transform(value: string): string {

    if (!value) {
      return '';
    }

    // separa data e hora
    const partes = value.split(' ');

    // separa dia mes ano
    const data = partes[0].split('/');

    // pega apenas hora e minuto
    const hora = partes[1];

    // cria uma data válida para o Angular
    const dataConvertida = new Date(
      Number(data[2]),
      Number(data[1]) - 1,
      Number(data[0]),
      Number(hora.split(':')[0]),
      Number(hora.split(':')[1])
    );

    return formatDate(
      dataConvertida,
      'dd/MM/yyyy HH:mm',
      'pt-BR'
    );
  }
}