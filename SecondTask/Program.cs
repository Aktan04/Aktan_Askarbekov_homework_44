using PrototypeOf44;

string currentDir = "../../../site";
HttpServer server = new HttpServer();
await server.RunAsync(currentDir, 8888);