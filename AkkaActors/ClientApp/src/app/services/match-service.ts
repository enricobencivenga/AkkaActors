import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";
import { MatchesResponse } from "../models/match";

@Injectable()
export class MatchService {
  private matchApi = `${environment.url}api/matches`;

  constructor(
    private http: HttpClient
  ) {
  }

  public getMatches(): Observable<MatchesResponse> {
    return this.http.get<MatchesResponse>(this.matchApi);
  }
}
