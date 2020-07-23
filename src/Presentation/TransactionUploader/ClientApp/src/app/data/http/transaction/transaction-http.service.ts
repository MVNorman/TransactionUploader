import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class TransactionHttpService {

  constructor(private httpClient: HttpClient) { }

  uploadFile(formData: FormData): Observable<void> {
    var url = environment.apiUrl + '/api/transaction';

    return this.httpClient.post<void>(url, formData);
  }
}
