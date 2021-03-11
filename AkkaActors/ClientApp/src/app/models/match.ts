export interface Match {
  id: number;
  team1: string;
  team1Score: number;
  team2: string;
  team2Score: number;
  started: boolean;
  ended: boolean;
}

export interface MatchResponse {
  match: Match;
}

export interface MatchesResponse {
  matches: Match[];
}
