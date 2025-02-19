using System.IO.Pipes;

internal class Client
{
    private static void Main(string[] args)
    {
        Console.WriteLine("client");
        NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream("chatPipe");
        namedPipeClientStream.Connect();
        StreamWriter streamWriter = new StreamWriter(namedPipeClientStream);
        StreamReader streamReader = new StreamReader(namedPipeClientStream);
        streamWriter.AutoFlush = true;

        while (true)
        {
            string output = Console.ReadLine();
            streamWriter.WriteLine(output);

            string input = streamReader.ReadLine();
            Console.WriteLine("Cервер ответил: " + input);
        }
    }
}