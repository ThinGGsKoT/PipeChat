using System;
using System.IO.Pipes;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("server");
        Thread thread = new Thread(client);
        thread.Start();
    }
    static List<string> msgs = new();
    public static int count = -1;
    public static void client()
    {
        count++;
        Console.WriteLine("server current connestions: " + count);
        NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream("chatPipe", PipeDirection.InOut, 10);
        namedPipeServerStream.WaitForConnection();
        Thread thread = new Thread(client);
        thread.Start();
        Console.WriteLine("Client is connected");

        StreamReader streamReader = new StreamReader(namedPipeServerStream);
        StreamWriter streamWriter = new StreamWriter(namedPipeServerStream);
        int msgCount = 0;
        while (true)
        {
            string msg = streamReader.ReadLine();
            Console.WriteLine($"client {count} : {msg}");
            msgs.Add(msg);
            string send = "";
            for (int i = msgCount; i < msgs.Count; i++)
            {
                send = send + msgs[i];
            }
            streamWriter.AutoFlush = true;
            streamWriter.WriteLine($"сообщение от клиента {count} : {send}");
            msgCount++;
        }
    }
}