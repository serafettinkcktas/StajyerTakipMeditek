using Domain.Interface;

namespace Infrastructure.Repository;

public class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly IDbConnectionFactory _connectionFactory ;
    public BaseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    
}