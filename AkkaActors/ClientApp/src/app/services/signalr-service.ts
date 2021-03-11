import * as signalR from "@aspnet/signalr";
import { HubConnection } from "@aspnet/signalr";
import { Observable, Subject } from "rxjs";
import { environment } from "../../environments/environment";

export class SignalRService {
  private hub: HubConnection;

  constructor() {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.url}akkaActorsHub`)
      .build();

    this.hub.start().then(() => {
      console.log(`Hub connection started`);
    }).catch(err => document.write(err));
  }

  public on<T>(name: string): Observable<T> {
    const subject = new Subject<T>();

    this.hub.on(name, (data) => {
      subject.next(data);
    });

    return subject.asObservable();
  }
} 
