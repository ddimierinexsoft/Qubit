using System;

namespace QbitLib.Games.CHSH.Strategies
{
    public class CHSHConstant0Strategy : CHSHStrategy
    {
        

        public override Func<bool, bool> GetAPartyFunction()
        {
            return ZeroFun;
        }

        private bool ZeroFun(bool x)
        {
            return false;
        }

        public override Func<bool, bool> GetBPartyFunction()
        {
            return ZeroFun;
        }


    }

    
}