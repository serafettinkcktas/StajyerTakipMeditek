using System.Data;

namespace Domain.Interface;

public interface IDbConnectionFactory
{ 
    IDbConnection CreateConnection();
}