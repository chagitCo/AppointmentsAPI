namespace HagitAppointments.Commands.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        Task Handle(TCommand command);
    }
}
