using System.Diagnostics;

namespace SearchBench.Models;

/// <summary>
/// Modelo que empaqueta las métricas obtenidas al ejecutar una búsqueda.
/// Es un contenedor de datos inmutable: no contiene lógica de búsqueda.
/// </summary>
public sealed class SearchResult
{
    /// <summary>Nombre de la técnica utilizada (se muestra en la interfaz).</summary>
    public string AlgorithmName { get; init; } = string.Empty;

    /// <summary>Complejidad teórica en notación Big-O de la técnica.</summary>
    public string Complexity { get; init; } = string.Empty;

    /// <summary>Clave que se buscó.</summary>
    public int Key { get; init; }

    /// <summary>Indica si la clave fue encontrada.</summary>
    public bool Found { get; init; }

    /// <summary>
    /// Índice asignado. Secuencial: posición en el arreglo original.
    /// Binaria: posición en el arreglo ordenado. Hash: número de cubeta h(k).
    /// Vale -1 cuando la clave no fue encontrada.
    /// </summary>
    public int Index { get; init; } = -1;

    /// <summary>Número exacto de comparaciones de claves realizadas.</summary>
    public long Comparisons { get; init; }

    /// <summary>Número exacto de iteraciones (vueltas de ciclo / saltos entre nodos).</summary>
    public long Iterations { get; init; }

    /// <summary>Tiempo transcurrido medido por Stopwatch, expresado en ticks.</summary>
    public long ElapsedTicks { get; init; }

    /// <summary>Información adicional de la ejecución (cubeta, intervalo final, etc.).</summary>
    public string Details { get; init; } = string.Empty;

    /// <summary>Tiempo transcurrido en microsegundos (conversión de ticks usando la frecuencia del Stopwatch).</summary>
    public double ElapsedMicroseconds => ElapsedTicks * 1_000_000.0 / Stopwatch.Frequency;

    /// <summary>Tiempo transcurrido en milisegundos.</summary>
    public double ElapsedMilliseconds => ElapsedTicks * 1_000.0 / Stopwatch.Frequency;

    /// <summary>Estado de la búsqueda en texto legible para la interfaz.</summary>
    public string StatusText => Found ? "Encontrado" : "No encontrado";
}
