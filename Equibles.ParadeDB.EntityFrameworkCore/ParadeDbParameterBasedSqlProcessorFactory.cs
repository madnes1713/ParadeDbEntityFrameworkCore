using Microsoft.EntityFrameworkCore.Query;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;

namespace Equibles.ParadeDB.EntityFrameworkCore;

public class ParadeDbParameterBasedSqlProcessorFactory : NpgsqlParameterBasedSqlProcessorFactory {
    private readonly RelationalParameterBasedSqlProcessorDependencies _dependencies;

    public ParadeDbParameterBasedSqlProcessorFactory(RelationalParameterBasedSqlProcessorDependencies dependencies)
        : base(dependencies) {
        _dependencies = dependencies;
    }

    public override RelationalParameterBasedSqlProcessor Create(RelationalParameterBasedSqlProcessorParameters parameters)
        => new ParadeDbParameterBasedSqlProcessor(_dependencies, parameters);
}
