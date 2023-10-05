import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'noStar'
})
export class NoStarPipe implements PipeTransform {
  transform(value:number): number {
    return 5 - Math.ceil(value);
  }
}
