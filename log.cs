
namespace steadystate;
class Log
{
    public void Success(string data)
    {
        Console.ForegroundColor=ConsoleColor.Green;

        Console.Write("[SUCCESS] ");
       
        Console.ForegroundColor=ConsoleColor.White;
         Console.WriteLine(data);
    }

    public void Info(string data)
    {
        Console.ForegroundColor=ConsoleColor.DarkGray;

        Console.Write("[INFO] ");
       
        Console.ForegroundColor=ConsoleColor.White;
         Console.WriteLine(data);
    }

    public void Warning(string data)
    {
        Console.ForegroundColor=ConsoleColor.Yellow;

        Console.Write("[WARNING] ");
       
        Console.ForegroundColor=ConsoleColor.White;
         Console.WriteLine(data);
    }
    public void Error(string data)
    {
        Console.ForegroundColor=ConsoleColor.DarkRed;

        Console.Write("[ERROR] ");
       
        Console.ForegroundColor=ConsoleColor.White;
         Console.WriteLine(data);
    }
}