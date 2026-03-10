using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace Equibles.ParadeDB.EntityFrameworkCore;

public sealed class ParadeDbMethodCallTranslator : IMethodCallTranslator {
    private readonly ISqlExpressionFactory _sql;
    private readonly IRelationalTypeMappingSource _typeMappingSource;

    private static readonly MethodInfo MatchesMethod = typeof(ParadeDbFunctions)
        .GetMethod(nameof(ParadeDbFunctions.Matches), [typeof(DbFunctions), typeof(string), typeof(string)])!;

    private static readonly MethodInfo MatchesAllMethod = typeof(ParadeDbFunctions)
        .GetMethod(nameof(ParadeDbFunctions.MatchesAll), [typeof(DbFunctions), typeof(string), typeof(string)])!;

    private static readonly MethodInfo ScoreMethod = typeof(ParadeDbFunctions)
        .GetMethod(nameof(ParadeDbFunctions.Score), [typeof(DbFunctions), typeof(object)])!;

    private static readonly MethodInfo SnippetMethod = typeof(ParadeDbFunctions)
        .GetMethod(nameof(ParadeDbFunctions.Snippet), [typeof(DbFunctions), typeof(string)])!;

    public ParadeDbMethodCallTranslator(ISqlExpressionFactory sql, IRelationalTypeMappingSource typeMappingSource) {
        _sql = sql;
        _typeMappingSource = typeMappingSource;
    }

    public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments,
        IDiagnosticsLogger<DbLoggerCategory.Query> logger) {
        // arguments[0] is always DbFunctions (ignored)

        if (method == MatchesMethod) {
            // column ||| 'query' — BM25 disjunction (OR) match
            return new PgUnknownBinaryExpression(arguments[1], arguments[2], "|||",
                typeof(bool), _typeMappingSource.FindMapping(typeof(bool)));
        }

        if (method == MatchesAllMethod) {
            // column &&& 'query' — BM25 conjunction (AND) match
            return new PgUnknownBinaryExpression(arguments[1], arguments[2], "&&&",
                typeof(bool), _typeMappingSource.FindMapping(typeof(bool)));
        }

        if (method == ScoreMethod) {
            // pdb.score(key_field) — BM25 relevance score
            return _sql.Function("pdb.score", [arguments[1]],
                nullable: true, argumentsPropagateNullability: [true],
                typeof(double), _typeMappingSource.FindMapping(typeof(double)));
        }

        if (method == SnippetMethod) {
            // pdb.snippet(column) — highlighted match excerpt
            return _sql.Function("pdb.snippet", [arguments[1]],
                nullable: true, argumentsPropagateNullability: [true],
                typeof(string), _typeMappingSource.FindMapping(typeof(string)));
        }

        return null;
    }
}
