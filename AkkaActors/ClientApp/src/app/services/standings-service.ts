import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { StandingsResponse } from "../models/standings";

@Injectable()
export class StandingsService {
  private standingsApi = `${environment.url}api/standings`;

  constructor(
    private http: HttpClient
  ) {
  }

  public getStandings(): Observable<StandingsResponse> {
    return this.http.get<StandingsResponse>(this.standingsApi);
  }
}
