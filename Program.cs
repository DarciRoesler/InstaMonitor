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