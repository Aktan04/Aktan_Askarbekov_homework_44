using System.Net;
using System.Text;

HttpListener server = new HttpListener();
server.Prefixes.Add("http://localhost:7777/");
server.Start();
Console.WriteLine("Ждем подключений");
Console.WriteLine("Для завершения программы напишите в браузере запрос http://localhost:7777/stop");
bool stopSignal = true;
while (stopSignal)
{
    HttpListenerContext context = server.GetContext();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;
    string urlPath = request.Url.AbsolutePath.ToLower();
    string responseStr = "";
    if (urlPath == "/stop")
    {
        stopSignal = false;
        responseStr = "<h1 style = 'color:black; font-size:50px; text-align:center; margin-top:200px'>" 
                             + "You stopped your program" + "</h1>";
    }
    else
    {
        responseStr = "<h1 style = 'color:black; font-size:50px; text-align:center; margin-top:200px'>" 
                             + urlPath.Substring(1) + "</h1>";
    }
    byte[] buffer = Encoding.UTF8.GetBytes(responseStr);
    response.ContentLength64 = buffer.Length;
    Stream output = response.OutputStream;
    output.Write(buffer, 0, buffer.Length);
    output.Close();
}
server.Stop();
Console.WriteLine("Соединение закрыто");
server.Close();