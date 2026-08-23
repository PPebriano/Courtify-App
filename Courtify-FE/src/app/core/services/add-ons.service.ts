import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environtment } from '../environments/environment';
import { EquipmentAddOnsResponseType } from '../models/response/equipment-add-ons-response-type';

@Injectable({
  providedIn: 'root',
})
export class AddOnsService {
  http = inject(HttpClient);

  addOns() {
    return this.http.get<EquipmentAddOnsResponseType[]>(
      `${environtment.apiUrl}/api/equipment-add-ons`,
    );
  }
}
