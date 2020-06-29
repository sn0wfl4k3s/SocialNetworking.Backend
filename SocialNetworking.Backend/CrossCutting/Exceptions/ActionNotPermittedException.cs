using System;

namespace CrossCutting.Exceptions
{
    public class ActionNotPermittedException : Exception
    {
        public override string Source =>
            "Erro de permissionamento";
        public override string Message =>
            "Usuário não autorizado para tal ação.";
    }
}
