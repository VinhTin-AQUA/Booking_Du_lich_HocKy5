import { Pipe, PipeTransform } from '@angular/core';
import jwt_decode from "jwt-decode";

@Pipe({
  name: 'role'
})
export class RolePipe implements PipeTransform {

  transform(token: string): string {
    const decoded: any = jwt_decode(token);
    return decoded.role;
  }
}
