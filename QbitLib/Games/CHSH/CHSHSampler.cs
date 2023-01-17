namespace QbitLib.Games.CHSH
{
    public static class CHSHSampler
    {
        public static CHSHSample est_win_probability(CHSHStrategy strategy, int n_games = 1000)
        {
            var game = new CHSHGame
            {
                Referee = new CHSHReferee(),
                AParty = new CHSHParty() { Strategy = strategy.GetAPartyFunction(), },
                BParty = new CHSHParty() { Strategy = strategy.GetBPartyFunction(), },
            };

            var ret = new CHSHSample
            {
                Strategy = strategy,
                WinProbability = 0,
            };

            for(int i = 0; i < n_games; i++)
            {
                if (game.IsWin())
                {
                    ret.WinProbability += 1;
                }
            }
            ret.WinProbability = ret.WinProbability / n_games;

            return ret;
        }
         
    }
     
}