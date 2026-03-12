// See https://aka.ms/new-console-template for more information

using System.Net;

namespace steadystate;

class SteadyState
{
    static int Main(string[] argv)
    {
       Log log=new Log();
       log.Info("HI");

        SKCS sk = new SKCS();
        sk.Init(8888);
        return 0;
    }

    

}