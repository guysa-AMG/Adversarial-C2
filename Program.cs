// See https://aka.ms/new-console-template for more information

namespace steadystate;

class SteadyState
{
    static int Main(string[] argv)
    {

        ManualResetEvent resetEvent = new ManualResetEvent(false);
       Log log=new Log();
      

        SKCS sk = new SKCS();
        sk.Init(8888);
        
        sk.Listen();
        resetEvent.WaitOne();
        return 0;
    }

    

}