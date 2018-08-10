import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'decimalToString'})
export class decimalToStringPipe implements PipeTransform {
  transform(value: number): string {
    return (value != null) ? value.toString() : '-';
  }
}