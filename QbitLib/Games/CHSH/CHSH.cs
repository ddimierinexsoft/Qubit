using QBits.QBits;
using System;

namespace QbitLib.Games.CHSH
{
    public class CHSHGame 
    {
        public CHSHReferee Referee { get; set; }
        public CHSHParty AParty { get; set; }
        public CHSHParty BParty { get; set; }

        public bool IsWin()
        {
            var (a, b) = Referee.GetQuestions();
            var (x, y) = (AParty.GetResponse(a), BParty.GetResponse(b));
            return (a && b) == (x ^ y);
        }
    }

    public class CHSHReferee
    {
        public virtual (bool, bool) GetQuestions()
        {
            return (NP.random_bit(), NP.random_bit());
        }
         
    }

    public abstract class CHSHStrategy
    {
        public abstract Func<bool, bool> GetAPartyFunction();
        public abstract Func<bool, bool> GetBPartyFunction();

    }

    public class CHSHParty
    {
        public Func<bool, bool> Strategy { get; set; }

        public bool GetResponse(bool question)
        {
            return Strategy(question);
        }

    }
}