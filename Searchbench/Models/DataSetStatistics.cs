using SearchBench.Services;

namespace SearchBench.Models;

/// <summary>
/// Estadísticas de preparación del banco de datos: cuánto costó generarlo,
/// ordenarlo (requisito de la búsqueda binaria) y construir la tabla hash,
/// además de la calidad de la dispersión obtenida.
/// </summary>
public sealed class DataSetStatistics
{
    /// <summary>Datos en el orden original de generación.</summary>
    public int[] UnsortedData { get; init; } = Array.Empty<int>();

    /// <summary>Datos ordenados para búsqueda binaria.</summary>
    public int[] SortedData { get; init; } = Array.Empty<int>();

    /// <summary>Tabla hash construida con el conjunto.</summary>
    public HashTableService? HashTable { get; init; }

    /// <summary>Cantidad N de elementos generados.</summary>
    public int Size { get; init; }

    /// <summary>Valor máximo posible de una clave (las claves están en el rango [1, KeyRangeMax]).</summary>
    public int KeyRangeMax { get; init; }

    /// <summary>Valor mínimo y máximo realmente generados.</summary>
    public int MinimumValue { get; init; }
    public int MaximumValue { get; init; }

    /// <summary>Muestra claves existentes en posiciones inicial, central y final.</summary>
    public int[] SampleKeys { get; init; } = Array.Empty<int>();

    /// <summary>Tiempo de generación de los datos aleatorios, en milisegundos.</summary>
    public double GenerationMilliseconds { get; init; }

    /// <summary>Tiempo del ordenamiento previo para la búsqueda binaria, en milisegundos.</summary>
    public double SortMilliseconds { get; init; }

    /// <summary>Tiempo de construcción de la tabla hash, en milisegundos.</summary>
    public double HashBuildMilliseconds { get; init; }

    /// <summary>Número M de cubetas de la tabla hash.</summary>
    public int HashTableSize { get; init; }

    /// <summary>Factor de carga α = N / M.</summary>
    public double HashLoadFactor { get; init; }

    /// <summary>Cantidad de inserciones que cayeron en una cubeta ya ocupada.</summary>
    public int HashCollisions { get; init; }

    /// <summary>Longitud de la cadena más larga de la tabla.</summary>
    public int HashMaxChainLength { get; init; }

    /// <summary>Cantidad de cubetas con al menos un elemento.</summary>
    public int HashUsedBuckets { get; init; }
}
