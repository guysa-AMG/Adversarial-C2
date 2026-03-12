using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
namespace steadystate;

class SKCS
{
    Socket stream;
    protected SocketAsyncEventArgs accept_event;
    protected Log log;

#pragma warning disable CS8618 
    public SKCS(int port=1742)
  {    
       this.Init(port);
       this.accept_event = new SocketAsyncEventArgs();
       this.accept_event.Completed += OnAccept;
       this.log=new Log();

    }
     

    public void Init(int port)
    {
       
         IPHostEntry hosts = Dns.GetHostEntry(Dns.GetHostName());
    try{
        IPAddress ip =IPAddress.Any;
        IPEndPoint pEndPoint = new IPEndPoint(ip,port);
        this.stream=new Socket(pEndPoint.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
            try{
            this.stream.Bind(pEndPoint);
            }
            catch (Exception ex)
            {
                this.log.Error(ex.Data.ToString()!);
            }
        this.log.Success($"Successfully binded to {pEndPoint.Address}:{pEndPoint.Port}.");
        }
        catch (Exception ex) { Console.Error.WriteLine(ex.Data);}
    }
   
    public  void Listen()
    {
         this.stream.Listen(100);
            this.Acceptance(); 
    }

    private void Acceptance()
    {
        log.Info("new incoming connection");
        this.accept_event.AcceptSocket = null;
        bool pending =  this.stream.AcceptAsync(this.accept_event);
       if (!pending){   OnAccept(this.stream,this.accept_event);    }   
    }

    private void OnAccept(object? sender, SocketAsyncEventArgs e)
    {
        Socket client = e.AcceptSocket!;
        log.Success("new Connection Recieved");
        this.Read(e.AcceptSocket!);
        this.Acceptance();
    }

    private void Read(Socket client)
    {
        
        SocketAsyncEventArgs read_event = new SocketAsyncEventArgs();

        byte[] byt = new byte[4096];
        read_event.SetBuffer(byt,0,byt.Length);
        read_event.UserToken=client;
        read_event.Completed+=OnReadReciev;
        ArmEvent(read_event);
    }
    protected void ArmEvent(SocketAsyncEventArgs read_event){
        Socket client = (Socket) read_event.UserToken!;
        bool pending = client.ReceiveAsync(read_event);
        if (!pending){  OnReadReciev(client,read_event);    }
    }
    public void OnReadReciev(Object? sender,SocketAsyncEventArgs e)
    {
       Socket client = (Socket) e.UserToken!;
       if (e.BytesTransferred == 0 || e.SocketError != SocketError.Success)
        {
            log.Info("client disconnected");
            client.Close();
            e.Dispose();
            return;
        }

        String? data = Console.ReadLine();
        client.
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(data??"Hello World\n");
        
        Request(client,buffer);
        ArmEvent(e);

    }
    public void Request(Socket client,byte[] byt)
    {
        SocketAsyncEventArgs send_event = new SocketAsyncEventArgs();
        send_event.UserToken=client;
        send_event.SetBuffer(byt,0,byt.Length);
        send_event.Completed+=OnSendData;
        bool pending = client.SendAsync(send_event);
        if (!pending){  OnSendData(client,send_event);  }

        
    }
    public void OnSendData(Object? sender,SocketAsyncEventArgs e)
    {
        Socket client =(Socket) e.UserToken!;
            if (e.SocketError != SocketError.Success){  client.Close();   }
        e.Dispose();
        
    }

   public void dispose()
    {
       this.accept_event.Dispose();
       this.stream.Close(); 
    }

   
}