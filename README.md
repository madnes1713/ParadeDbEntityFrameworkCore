# Equibles.ParadeDB.EntityFrameworkCore

EF Core integration for [ParadeDB](https://www.paradedb.com/) `pg_search` — BM25 full-text search indexes on PostgreSQL.

Provides a `[Bm25Index]` attribute for automatic index creation via migrations, and LINQ-friendly query methods for BM25 search, scoring, and snippets. No raw SQL needed.

## Requirements

- PostgreSQL with the [pg_search](https://docs.paradedb.com/search/quickstart) extension installed
- [Npgsql.EntityFrameworkCore.PostgreSQL](https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL) provider
- .NET 10+

## Installation

```bash
dotnet add package Equibles.ParadeDB.EntityFrameworkCore
```

## Setup

### 1. Enable ParadeDB in your DbContext

```csharp
services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(connectionString, npgsql => npgsql.UseParadeDb()));
```

### 2. Add BM25 indexes to your entities

```csharp
using Equibles.ParadeDB.EntityFrameworkCore;

[Bm25Index(nameof(Id), nameof(Title), nameof(Content))]
public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
```

The first parameter is the key field (primary key), followed by the columns to index for full-text search.

### 3. Create a migration

```bash
dotnet ef migrations add AddBm25Index
dotnet ef database update
```

EF Core will generate the migration automatically, creating:
- The `pg_search` PostgreSQL extension
- A BM25 index on the specified columns with the correct `key_field` storage parameter

## Querying

The library provides LINQ-compatible methods via `EF.Functions` that translate to ParadeDB SQL operators:

### Basic search (OR match)

Uses the `|||` operator — matches documents containing **any** of the query terms.

```csharp
var results = await dbContext.Articles
    .Where(a => EF.Functions.Matches(a.Content, "machine learning"))
    .ToListAsync();
```

Translates to:

```sql
SELECT * FROM "Articles" WHERE "Content" ||| 'machine learning'
```

### Conjunction search (AND match)

Uses the `&&&` operator — matches documents containing **all** of the query terms.

```csharp
var results = await dbContext.Articles
    .Where(a => EF.Functions.MatchesAll(a.Content, "machine learning"))
    .ToListAsync();
```

Translates to:

```sql
SELECT * FROM "Articles" WHERE "Content" &&& 'machine learning'
```

### BM25 relevance scoring

Use `pdb.score()` to rank results by relevance.

```csharp
var results = await dbContext.Articles
    .Where(a => EF.Functions.Matches(a.Content, "deep learning"))
    .Select(a => new
    {
        a.Title,
        a.Content,
        Score = EF.Functions.Score(a.Id)
    })
    .OrderByDescending(a => a.Score)
    .Take(10)
    .ToListAsync();
```

Translates to:

```sql
SELECT "Title", "Content", pdb.score("Id") AS "Score"
FROM "Articles"
WHERE "Content" ||| 'deep learning'
ORDER BY pdb.score("Id") DESC
LIMIT 10
```

### Highlighted snippets

Use `pdb.snippet()` to get text excerpts with matched terms highlighted.

```csharp
var results = await dbContext.Articles
    .Where(a => EF.Functions.Matches(a.Content, "neural networks"))
    .Select(a => new
    {
        a.Title,
        Snippet = EF.Functions.Snippet(a.Content),
        Score = EF.Functions.Score(a.Id)
    })
    .OrderByDescending(a => a.Score)
    .ToListAsync();
```

Translates to:

```sql
SELECT "Title", pdb.snippet("Content") AS "Snippet", pdb.score("Id") AS "Score"
FROM "Articles"
WHERE "Content" ||| 'neural networks'
ORDER BY pdb.score("Id") DESC
```

### Combining with standard LINQ filters

BM25 search composes naturally with other LINQ predicates:

```csharp
var results = await dbContext.Articles
    .Where(a => EF.Functions.Matches(a.Content, "transformers")
                && a.CreatedAt > DateTime.UtcNow.AddMonths(-6))
    .OrderByDescending(a => EF.Functions.Score(a.Id))
    .Take(20)
    .ToListAsync();
```

## How it works

### Index creation

The library hooks into EF Core's model finalization pipeline via `IConventionSetPlugin`. During model building, it:

1. Scans entity types for `[Bm25Index]` attributes
2. Creates database indexes with the `bm25` index method
3. Sets the `key_field` storage parameter (required by pg_search)
4. Registers the `pg_search` PostgreSQL extension

All of this is translated into standard EF Core migrations — no manual SQL required.

### Query translation

LINQ methods on `EF.Functions` are translated to SQL via `IMethodCallTranslatorPlugin`:

| C# Method | SQL |
|-----------|-----|
| `EF.Functions.Matches(column, query)` | `column \|\|\| 'query'` |
| `EF.Functions.MatchesAll(column, query)` | `column &&& 'query'` |
| `EF.Functions.Score(keyField)` | `pdb.score(key_field)` |
| `EF.Functions.Snippet(column)` | `pdb.snippet(column)` |

## License

MIT
