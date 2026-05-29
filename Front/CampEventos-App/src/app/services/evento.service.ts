import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { API_CONFIG } from '../core/config/api.config';
import { Injectable } from '@angular/core';
import { map, Observable, take } from 'rxjs';
import { Evento } from '../models/Evento';
import { PaginatedResult } from '../models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class EventoService {
  private readonly baseURL = `${API_CONFIG.apiUrl}/Eventos`;
  
  constructor(private http: HttpClient) { }
  
  public getEventos(
    pageNumber?: number,
    pageSize?: number,
    term?: string,
    ): Observable<PaginatedResult<Evento[]>> {
      const paginatedResult = new PaginatedResult<Evento[]>();

      let params = new HttpParams();

      if (pageNumber && pageSize) {
        params = params.append('pageNumber', pageNumber);
        params = params.append('pageSize', pageSize);
      }

      if(term) {
        params = params.append('term', term);
      }

      return this.http.get<Evento[]>(this.baseURL, {
        observe: 'response',
        params,
      }).pipe(take(1), map((response: HttpResponse<Evento[]>) => {
        paginatedResult.result = response.body ?? [];

        const pagination = response.headers.get('Pagination');

        if(pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }

        return paginatedResult;
      })
    );
  }
  
  public getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`).pipe(take (1));
  }
  
  public post(evento: Evento): Observable<Evento> {
    return this.http.post<Evento>(this.baseURL, evento).pipe(take (1));
  }
  
  public put(evento: Evento): Observable<Evento> {
    return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento).pipe(take (1));
  }
  
  public deleteEvento(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`).pipe(take (1));
  }
  
  public postUpload(eventoId: number, file: File): Observable<Evento> {
    
    const formData = new FormData();
    
    formData.append('file', file, file.name);
    
    return this.http.post<Evento>(`${this.baseURL}/upload-image/${eventoId}`, formData).pipe(take(1));
  }
}
