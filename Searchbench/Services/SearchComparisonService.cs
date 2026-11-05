using SearchBench.Models;

namespace SearchBench.Services;

/// <summary>
/// Orquesta la ejecución de las búsquedas sobre un mismo banco de datos y una misma clave,
/// aplicando una metodología de medición uniforme para las tres técnicas:
/// 1) una ejecución de calentamiento (JIT y caché) que se descarta y
/// 2) varias ejecuciones medidas, de las cuales se reporta la de tiempo mediano.
/// Las comparaciones e iteraciones son deterministas, por lo que no cambian entre ejecuciones.
/// </summary>
public sealed class SearchComparisonService
{
    /// <summary>Número de ejecuciones medidas por algoritmo (impar, para tener mediana exacta).</summary>
    private const int MeasurementRuns = 11;

    private readonly SequentialSearchService _sequentialSearch;
    private readonly BinarySearchService _binarySearch;

    public SearchComparisonService(SequentialSearchService sequentialSearch, BinarySearchService binarySearch)
    {
        _sequentialSearch = sequentialSearch ?? throw new ArgumentNullException(nameof(sequentialSearch));
        _binarySearch = binarySearch ?? throw new ArgumentNullException(nameof(binarySearch));
    }

    /// <summary>Ejecuta una sola técnica de búsqueda sobre el banco de datos.</summary>
    public SearchResult RunSingle(SearchAlgorithm algorithm, DataSetService dataSet, int key)
    {
        ArgumentNullException.ThrowIfNull(dataSet);

        if (!dataSet.HasData)
        {
            throw new InvalidOperationException("Primero debe generarse el banco de datos.");
        }

        return algorithm switch
        {
            SearchAlgorithm.Sequential => Measure(() => _sequentialSearch.Search(dataSet.UnsortedData, key)),
            SearchAlgorithm.Binary => Measure(() => _binarySearch.Search(dataSet.SortedData, key)),
            SearchAlgorithm.Hash => Measure(() => dataSet.HashTable.Search(key)),
            _ => throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, "Algoritmo no soportado.")
        };
    }

    /// <summary>
    /// Ejecuta las tres técnicas con la misma clave y el mismo banco de datos
    /// para poder compararlas lado a lado.
    /// </summary>
    public IReadOnlyList<SearchResult> RunAll(DataSetService dataSet, int key)
    {
        return new[]
        {
            RunSingle(SearchAlgorithm.Sequential, dataSet, key),
            RunSingle(SearchAlgorithm.Binary, dataSet, key),
            RunSingle(SearchAlgorithm.Hash, dataSet, key)
        };
    }

    /// <summary>
    /// Calienta el código, ejecuta la búsqueda varias veces y devuelve la ejecución de tiempo mediano.
    /// La mediana reduce el efecto de interrupciones del sistema operativo y de la resolución del temporizador.
    /// </summary>
    private static SearchResult Measure(Func<SearchResult> search)
    {
        search(); // Calentamiento: el resultado se descarta.

        var runs = new SearchResult[MeasurementRuns];
        for (int i = 0; i < runs.Length; i++)
        {
            runs[i] = search();
        }

        Array.Sort(runs, (left, right) => left.ElapsedTicks.CompareTo(right.ElapsedTicks));
        return runs[MeasurementRuns / 2];
    }
}
