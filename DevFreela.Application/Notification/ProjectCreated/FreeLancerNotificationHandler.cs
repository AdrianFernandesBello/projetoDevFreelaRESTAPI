using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class FreeLancerNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"notificando os freelancer sobre o projeto {notification.Id}");
            return Task.CompletedTask;
        }
    }
}
