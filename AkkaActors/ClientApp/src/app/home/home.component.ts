import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Match, MatchResponse } from '../models/match';
import { TeamStanding, StandingsResponse, TeamStandingDict } from '../models/standings';
import { MatchService } from '../services/match-service';
import { SignalRService } from '../services/signalr-service';
import { StandingsService } from '../services/standings-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  matches: Match[] = [];
  teamStandings: TeamStanding[] = [];

  constructor(
    private signalRService: SignalRService,
    private matchService: MatchService,
    private standingsService: StandingsService
  ) {
  }

  ngOnInit() {
    this.matchService.getMatches()
      .subscribe(response => { this.matches = response.matches });

    this.standingsService.getStandings()
      .subscribe(response => { this.teamStandings = this.orderedArrayFromDictionary(response.teams) });

    const onBroadcastMatchChanged$: Observable<MatchResponse> = this.signalRService.on(`BroadcastMatchChanged`);

    onBroadcastMatchChanged$
      .subscribe((response: MatchResponse) => {
        console.log(response.match);
        this.upsertMatch(response.match);
      });

    const onBroadcastStandingsChanged$: Observable<StandingsResponse> = this.signalRService.on(`BroadcastStandingsChanged`);

    onBroadcastStandingsChanged$
      .subscribe((response: StandingsResponse) => {
        console.log(response.teams);
        this.teamStandings = this.orderedArrayFromDictionary(response.teams)
      });
  }

  private upsertMatch(match: Match) {
    let mm = this.matches.find(m => m.id == match.id);
    if (mm) {
      Object.keys(mm).forEach(key => mm[key] = match[key]);
    } else {
      this.matches.push(match);
    }
  }

  private orderedArrayFromDictionary(teamStanding: TeamStandingDict) {
    var orderedArray: TeamStanding[] = [];
    Object.keys(teamStanding).forEach(key => orderedArray.push({ team: key, points: teamStanding[key] }));
    return orderedArray.sort((a, b) => b.points - a.points);
  }
}
