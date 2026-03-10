using Microsoft.EntityFrameworkCore;

namespace Equibles.ParadeDB.EntityFrameworkCore;

/// <summary>
/// Provides CLR methods that translate to ParadeDB pg_search SQL operators and functions.
/// These methods can only be used in EF Core LINQ queries — they have no in-memory implementation.
/// </summary>
public static class ParadeDbFunctions {
    /// <summary>
    /// BM25 disjunction match (OR). Translates to: column ||| 'query'.
    /// Matches documents containing any of the query terms.
    /// </summary>
    public static bool Matches(this DbFunctions _, string column, string query)
        => throw new InvalidOperationException("This method can only be used in EF Core LINQ queries.");

    /// <summary>
    /// BM25 conjunction match (AND). Translates to: column &&& 'query'.
    /// Matches documents containing all of the query terms.
    /// </summary>
    public static bool MatchesAll(this DbFunctions _, string column, string query)
        => throw new InvalidOperationException("This method can only be used in EF Core LINQ queries.");

    /// <summary>
    /// BM25 relevance score. Translates to: pdb.score(key_field).
    /// Returns the BM25 relevance score for the matched document.
    /// </summary>
    public static double Score(this DbFunctions _, object keyField)
        => throw new InvalidOperationException("This method can only be used in EF Core LINQ queries.");

    /// <summary>
    /// BM25 snippet with highlighted matches. Translates to: pdb.snippet(column).
    /// Returns a text excerpt with matched terms highlighted.
    /// </summary>
    public static string Snippet(this DbFunctions _, string column)
        => throw new InvalidOperationException("This method can only be used in EF Core LINQ queries.");
}
