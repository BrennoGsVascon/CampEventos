import { formatDate } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateTimeFormat',
  standalone: true
})
export class DateTimeFormatPipe implements PipeTransform {

  transform(value: string | Date | null | undefined): string {
    const data = this.converterParaData(value);

    if (!data) {
      return '';
    }

    return formatDate(data, 'dd/MM/yyyy HH:mm', 'pt-BR');
  }

  private converterParaData(value: string | Date | null | undefined): Date | null {
    if (!value) return null;

    if (value instanceof Date) {
      return isNaN(value.getTime()) ? null : value;
    }

    const valor = String(value).trim();

    if (!valor) return null;

    if (/^\d{4}-\d{2}-\d{2}/.test(valor)) {
      const dataIso = new Date(valor);
      return isNaN(dataIso.getTime()) ? null : dataIso;
    }

    const dataPtBr = valor.match(/^(\d{1,2})\/(\d{1,2})\/(\d{4})(?:\s+(\d{1,2}):(\d{2}))?/);

    if (dataPtBr) {
      const [, dia, mes, ano, hora = '0', minuto = '0'] = dataPtBr;
      return new Date(Number(ano), Number(mes) - 1, Number(dia), Number(hora), Number(minuto));
    }

    const dataFallback = new Date(valor);
    return isNaN(dataFallback.getTime()) ? null : dataFallback;
  }
}
