using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;

namespace Equibles.ParadeDB.EntityFrameworkCore;

public class ParadeDbParameterBasedSqlProcessor : NpgsqlParameterBasedSqlProcessor {
    private readonly RelationalParameterBasedSqlProcessorDependencies _dependencies;
    private readonly RelationalParameterBasedSqlProcessorParameters _parameters;

    public ParadeDbParameterBasedSqlProcessor(RelationalParameterBasedSqlProcessorDependencies dependencies,
        RelationalParameterBasedSqlProcessorParameters parameters)
        : base(dependencies, parameters) {
        _dependencies = dependencies;
        _parameters = parameters;
    }

    protected override Expression ProcessSqlNullability(Expression expression, ParametersCacheDecorator parametersDecorator) {
        return new ParadeDbSqlNullabilityProcessor(_dependencies, _parameters)
            .Process(expression, parametersDecorator);
    }
}
