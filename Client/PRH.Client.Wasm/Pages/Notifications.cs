using Radzen;
using Microsoft.AspNetCore.Components;

namespace PRH.Client.Wasm.Pages;

public class Notifications : ComponentBase
{
    [Inject] protected NotificationService NotificationService { get; set; }
    protected void Notify(NotificationSeverity severity, string summary, string detail)
    {
        NotificationService.Notify(new NotificationMessage
        {
            Severity = severity,
            Summary = summary,
            Detail = detail,
            Duration = 4000
        });
    }

    protected void Success(string summary, string detail) => Notify(NotificationSeverity.Success, summary, detail);
    protected void Failure(string summary, string detail) => Notify(NotificationSeverity.Error, summary, detail);
    protected void Warning(string summary, string detail) => Notify(NotificationSeverity.Warning, summary, detail);
}
