namespace HagitAppointments.Queries.Interfaces
{
    public interface IQueryHandler<TQuery, TResult>
    {
        public Task<TResult> Handle(TQuery query);
    }
}
