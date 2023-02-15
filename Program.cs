using InstaMonitor;

// Declaração das listas
List<Usuario> usuarios = new List<Usuario>();
List<Update> updates = new List<Update>();
List<string> seguindo = new List<string>();
List<string> seguidores = new List<string>();

// Identificação do diretório local
string path = @"c:\InstaMonitor";

// Identificação dos arquivos no diretório
string arquivoUsuarios = path + "\\usuarios.txt";
string arquivoSeguindo = path + "\\seguindo.txt";
string arquivoSeguidores = path + "\\seguidores.txt";
string arquivoSeguindoHtml = path + "\\seguindoHtml.txt";
string arquivoSeguidoresHtml = path + "\\seguidoresHtml.txt";

// Faz a leitura dos registros de usuários já existentes
string usuariosBase = File.ReadAllText(arquivoUsuarios);
string[] splitUsuarios = usuariosBase.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
foreach (string usuario in splitUsuarios)
{
    string[] info = usuario.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
    usuarios.Add(new Usuario(info[0], bool.Parse(info[1]), bool.Parse(info[2])));
}

// Faz a leitura do html dos seguidores e seguindo
string seguindoHtml = File.ReadAllText(arquivoSeguindoHtml);
string seguidoresHtml = File.ReadAllText(arquivoSeguidoresHtml);

// Segmenta o html em uma linha para cada usuário
string[] splitSeguindo = seguindoHtml.Split(new[] { "perfil de " }, StringSplitOptions.RemoveEmptyEntries);
string[] splitSeguidores = seguidoresHtml.Split(new[] { "perfil de " }, StringSplitOptions.RemoveEmptyEntries);

// Identifica cada usuário sendo seguido
foreach (string linha in splitSeguindo)
{
    string user = linha.Substring(0, linha.IndexOf('\"'));
    seguindo.Add(user);
}

// Identifica cada usuário que me segue
foreach (string linha in splitSeguidores)
{
    string user = linha.Substring(0, linha.IndexOf('\"'));
    seguidores.Add(user);
}

// Remove os splits iniciais do html
seguindo.Remove("<div style=");
seguidores.Remove("<div style=");

// Adiciona os novos seguindo à lista de usuários
foreach (string nome in seguindo)
{
    if (!usuarios.Exists(x => x.Nome == nome))
    {
        usuarios.Add(new Usuario(nome));
    }
}

// Adiciona os novos seguidores à lista de usuários
foreach (string nome in seguidores)
{
    if (!usuarios.Exists(x => x.Nome == nome))
    {
        usuarios.Add(new Usuario(nome));
    }
}

// Atualiza cada registro de usuário
foreach (Usuario usuario in usuarios)
{
    if (seguidores.ToList().Exists(x => x == usuario.Nome) && !usuario.MeSegue)
    {
        usuario.MeSegue = true;
    }
    if (!seguidores.ToList().Exists(x => x == usuario.Nome) && usuario.MeSegue)
    {
        usuario.MeSegue = false;
    }
    if (seguindo.ToList().Exists(x => x == usuario.Nome) && !usuario.EuSigo)
    {
        usuario.EuSigo = true;
    }
    if (!seguindo.ToList().Exists(x => x == usuario.Nome) && usuario.EuSigo)
    {
        usuario.EuSigo = false;
    }
}

// Cria o txt que será enviado ao arquivo
string? usuariostxt = null;
foreach (Usuario usuario in usuarios)
{
    usuariostxt += usuario.ToString();
}

// Grava os dados nos arquivos
File.WriteAllText(arquivoUsuarios, usuariostxt);
File.WriteAllLines(arquivoSeguindo, seguindo);
File.WriteAllLines(arquivoSeguidores, seguidores);

// Informa no console registros que não condizem
Console.WriteLine("\nNão seguem de volta: ");
usuarios.Where(x => x.EuSigo && !x.MeSegue).ToList().ForEach(Console.WriteLine);
Console.WriteLine("\nNão sigo de volta: ");
usuarios.Where(x => !x.EuSigo && x.MeSegue).ToList().ForEach(Console.WriteLine);

Console.WriteLine("Deu certo!");
Console.ReadLine();