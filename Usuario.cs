namespace InstaMonitor
{
    internal class Usuario
    {
        public string Nome { get; set; }
        public bool EuSigo { get; set; }
        public bool MeSegue { get; set; }
        public List<Update>? ListaUpdates { get; set; }

        public Usuario(string nome)
        {
            Nome = nome;
        }

        public Usuario(string nome, bool euSigo, bool meSegue)
        {
            Nome = nome;
            EuSigo = euSigo;
            MeSegue = meSegue;
        }

        public override string ToString()
        {
            return Nome + ";" + EuSigo + ";" + MeSegue + "/";
        }
    }
}
