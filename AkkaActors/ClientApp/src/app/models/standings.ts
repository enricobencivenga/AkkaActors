export type TeamStandingDict = { [key: string]: number };

export interface StandingsResponse {
  teams: TeamStandingDict;
}

export interface TeamStanding {
  team: string;
  points: number;
}
