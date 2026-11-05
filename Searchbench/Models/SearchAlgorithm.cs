namespace SearchBench.Models;

/// <summary>
/// Identifica la técnica de búsqueda que se desea ejecutar.
/// </summary>
public enum SearchAlgorithm
{
    /// <summary>Búsqueda secuencial (lineal) sobre el arreglo sin ordenar.</summary>
    Sequential,

    /// <summary>Búsqueda binaria sobre el arreglo ordenado.</summary>
    Binary,

    /// <summary>Búsqueda directa en la tabla hash (dispersión).</summary>
    Hash
}
