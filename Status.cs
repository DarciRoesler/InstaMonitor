using System.ComponentModel;

namespace InstaMonitor
{
    public enum Status : int
    {
        [Description("Começou a me seguir")]
        ComecouAMeSeguir = 0,
        [Description("Parou de me seguir")]
        ParouDeMeSeguir = 1,
        [Description("Eu segui")]
        EuSegui = 2,
        [Description("Eu parei de seguir")]
        EuPareiDeSeguir = 3
    }
}
