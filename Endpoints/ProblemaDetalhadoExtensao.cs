using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace WantApp.Endpoints;

public static class ProblemaDetalhadoExtensao
{
    public static Dictionary<string, string[]> ConverterParaProblemaDetalhado(this IReadOnlyCollection<Notification> notificacoes)
    {
        return notificacoes
                 .GroupBy(g => g.Key)
                 .ToDictionary(g => g.Key, g => g.Select(x => x.Message)
                 .ToArray());

    }

    public static Dictionary<string, string[]> ConverterParaProblemaDetalhado(this IEnumerable<IdentityError> erro)
    {
        var dictionary = new Dictionary<string, string[]>();
        dictionary.Add("Error", erro.Select(x => x.Description).ToArray());

        return dictionary;

    }
}
