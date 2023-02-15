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
string arquivoUpdates = path + "\\updates.txt";
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

// Faz a leitura dos registros de Updates já existentes
string updatesBase = File.ReadAllText(arquivoUpdates);
string[] splitUpdates = updatesBase.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
foreach (string update in splitUpdates)
{
    string[] info = update.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
    updates.Add(new Update(usuarios.FirstOrDefault(x => x.Nome == info[0]), Enum.Parse<Status>(info[1]), DateTime.Parse(info[2])));
}


// Atualiza cada registro de usuário
foreach (Usuario usuario in usuarios)
{
    usuario.ListaUpdates = new List<Update>();
    if (seguidores.ToList().Exists(x => x == usuario.Nome) && !usuario.MeSegue)
    {
        usuario.MeSegue = true;
        updates.Add(new Update(usuario, Status.ComecouAMeSeguir, DateTime.Today));
    }
    if (!seguidores.ToList().Exists(x => x == usuario.Nome) && usuario.MeSegue)
    {
        usuario.MeSegue = false;
        updates.Add(new Update(usuario, Status.ParouDeMeSeguir, DateTime.Today));
    }
    if (seguindo.ToList().Exists(x => x == usuario.Nome) && !usuario.EuSigo)
    {
        usuario.EuSigo = true;
        updates.Add(new Update(usuario, Status.EuSegui, DateTime.Today));
    }
    if (!seguindo.ToList().Exists(x => x == usuario.Nome) && usuario.EuSigo)
    {
        usuario.EuSigo = false;
        updates.Add(new Update(usuario, Status.EuPareiDeSeguir, DateTime.Today));
    }

    usuario.ListaUpdates.AddRange(updates.Where(x => x.Usuario?.Nome== usuario.Nome).ToList());
}

// Cria o txt que será enviado ao arquivo de usuários
string? usuariostxt = null;
foreach (Usuario usuario in usuarios)
{
    usuariostxt += usuario.ToString();
}

// Cria o txt que será enviado ao arquivo de updates
string? updatestxt = null;
foreach (Update update in updates)
{
    updatestxt += update.ToString();
}

// Grava os dados nos arquivos
File.WriteAllText(arquivoUsuarios, usuariostxt);
File.WriteAllText(arquivoUpdates, updatestxt);
File.WriteAllLines(arquivoSeguindo, seguindo);
File.WriteAllLines(arquivoSeguidores, seguidores);

// Informa no console registros que não condizem
Console.WriteLine("\nNão seguem de volta: ");
usuarios.Where(x => x.EuSigo && !x.MeSegue).ToList().ForEach(Console.WriteLine);
Console.WriteLine("\nNão sigo de volta: ");
usuarios.Where(x => !x.EuSigo && x.MeSegue).ToList().ForEach(Console.WriteLine);

Console.WriteLine("Deu certo!");
Console.ReadLine();