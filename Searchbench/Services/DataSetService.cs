using System.Diagnostics;
using SearchBench.Models;

namespace SearchBench.Services;

/// <summary>
/// Administra el banco de datos de la práctica: genera los N datos aleatorios y
/// prepara las tres estructuras que necesita cada técnica de búsqueda:
/// arreglo sin ordenar (secuencial), copia ordenada (binaria) y tabla hash.
/// </summary>
public sealed class DataSetService
{
    /// <summary>Tamaño mínimo permitido para N.</summary>
    public const int MinSize = 1;

    /// <summary>Tamaño máximo permitido para N.</summary>
    public const int MaxSize = 1_000_000;

    private readonly DataGeneratorService _generator;
    private readonly HashTableService _hashTable;
    private readonly Random _random = new();

    public DataSetService(DataGeneratorService generator, HashTableService hashTable)
    {
        _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        _hashTable = hashTable ?? throw new ArgumentNullException(nameof(hashTable));
    }

    /// <summary>Datos en el orden aleatorio en que fueron generados (para la búsqueda secuencial).</summary>
    public int[] UnsortedData { get; private set; } = Array.Empty<int>();

    /// <summary>Copia ordenada ascendentemente de los datos (para la búsqueda binaria).</summary>
    public int[] SortedData { get; private set; } = Array.Empty<int>();

    /// <summary>Tabla hash construida con los mismos datos.</summary>
    public HashTableService HashTable => _hashTable;

    /// <summary>Estadísticas de la última generación; null si aún no hay datos.</summary>
    public DataSetStatistics? Statistics { get; private set; }

    /// <summary>Cantidad N de elementos actuales.</summary>
    public int Size => UnsortedData.Length;

    /// <summary>Indica si ya se generó un banco de datos.</summary>
    public bool HasData => UnsortedData.Length > 0;

    /// <summary>
    /// Genera un nuevo banco de N claves únicas, ordena una copia y construye la tabla hash,
    /// midiendo el costo de cada fase de preparación.
    /// </summary>
    public void Generate(int size)
    {
        if (size < MinSize || size > MaxSize)
        {
            throw new ArgumentOutOfRangeException(nameof(size), $"N debe estar entre {MinSize} y {MaxSize:N0}.");
        }

        Stopwatch stopwatch = Stopwatch.StartNew();

        // Fase 1: generación de datos aleatorios.
        int[] data = _generator.GenerateUniqueRandomKeys(size);
        double generationMs = stopwatch.Elapsed.TotalMilliseconds;

        // Fase 2: ordenamiento previo, necesario para la búsqueda binaria.
        stopwatch.Restart();
        int[] sorted = (int[])data.Clone();
        Array.Sort(sorted);
        double sortMs = stopwatch.Elapsed.TotalMilliseconds;

        // Fase 3: construcción de la tabla hash.
        stopwatch.Restart();
        _hashTable.Build(data);
        double hashMs = stopwatch.Elapsed.TotalMilliseconds;

        UnsortedData = data;
        SortedData = sorted;
        Statistics = new DataSetStatistics
        {
            Size = size,
            KeyRangeMax = _generator.GetMaxKeyValue(size),
            GenerationMilliseconds = generationMs,
            SortMilliseconds = sortMs,
            HashBuildMilliseconds = hashMs,
            HashTableSize = _hashTable.TableSize,
            HashLoadFactor = _hashTable.LoadFactor,
            HashCollisions = _hashTable.CollisionCount,
            HashMaxChainLength = _hashTable.MaxChainLength,
            HashUsedBuckets = _hashTable.UsedBuckets
        };
    }

    /// <summary>Devuelve una clave que SÍ existe en el banco (elegida al azar).</summary>
    public int GetRandomExistingKey()
    {
        EnsureData();
        return UnsortedData[_random.Next(UnsortedData.Length)];
    }

    /// <summary>
    /// Devuelve una clave que NO existe en el banco (útil para provocar el peor caso).
    /// Se toma dentro del rango de claves para que la búsqueda sea realista.
    /// </summary>
    public int GetMissingKey()
    {
        EnsureData();

        int maxValue = _generator.GetMaxKeyValue(Size);
        for (int attempt = 0; attempt < 1000; attempt++)
        {
            int candidate = _random.Next(1, maxValue + 1);

            // Array.BinarySearch devuelve un valor negativo cuando la clave no está.
            if (Array.BinarySearch(SortedData, candidate) < 0)
            {
                return candidate;
            }
        }

        // Respaldo: un valor fuera del rango generado nunca puede existir.
        return maxValue + 1;
    }

    private void EnsureData()
    {
        if (!HasData)
        {
            throw new InvalidOperationException("Primero debe generarse el banco de datos.");
        }
    }
}
