string path = @"c:\InstaMonitor";

string arquivoSeguindo = path + "\\seguindo.txt";
string arquivoSeguidores = path + "\\seguidores.txt";
string arquivoSeguindoHtml = path + "\\seguindoHtml.txt";
string arquivoSeguidoresHtml = path + "\\seguidoresHtml.txt";
string arquivoNaoSeguemDeVolta = path + "\\naoSeguemDeVolta.txt";
string arquivoNaoSigoDeVolta = path + "\\naoSigoDeVolta.txt";

string seguindoHtml = File.ReadAllText(arquivoSeguindoHtml);
string seguidoresHtml = File.ReadAllText(arquivoSeguidoresHtml);

List<string> seguindo = new List<string>();
List<string> seguidores = new List<string>();
List<string> naoSeguemDeVolta = new List<string>();
List<string> naoSigoDeVolta = new List<string>();

string[] splitSeguindo = seguindoHtml.Split(new[] { "perfil de " }, StringSplitOptions.RemoveEmptyEntries);
string[] splitSeguidores = seguidoresHtml.Split(new[] { "perfil de " }, StringSplitOptions.RemoveEmptyEntries);

foreach (string linha in splitSeguindo)
{
    string user = linha.Substring(0, linha.IndexOf('\"'));
    seguindo.Add(user);
}

foreach (string linha in splitSeguidores)
{
    string user = linha.Substring(0, linha.IndexOf('\"'));
    seguidores.Add(user);
}

foreach (string line in seguindo)
{
    if (!seguidores.Contains(line))
    {
        naoSeguemDeVolta.Add(line);
    }
}

foreach (string line in seguidores)
{
    if (!seguindo.Contains(line))
    {
        naoSigoDeVolta.Add(line);
    }
}

seguindo.Remove("<div style=");
seguidores.Remove("<div style=");

File.WriteAllLines(arquivoSeguindo, seguindo);
File.WriteAllLines(arquivoSeguidores, seguidores);
File.WriteAllLines(arquivoNaoSeguemDeVolta, naoSeguemDeVolta);
File.WriteAllLines(arquivoNaoSigoDeVolta, naoSigoDeVolta);

Console.WriteLine("Deu certo!");
Console.ReadLine();