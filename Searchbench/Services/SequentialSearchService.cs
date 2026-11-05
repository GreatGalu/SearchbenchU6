using System.Diagnostics;
using SearchBench.Models;

namespace SearchBench.Services;

/// <summary>
/// Servicio independiente que implementa la búsqueda secuencial (lineal).
/// Recorre el arreglo elemento por elemento hasta hallar la clave o agotar los datos.
/// Complejidad: O(N) en el peor caso y en el caso promedio.
/// </summary>
public sealed class SequentialSearchService
{
    public const string AlgorithmName = "Búsqueda secuencial";
    public const string Complexity = "O(N)";

    /// <summary>
    /// Busca <paramref name="key"/> en <paramref name="data"/> (arreglo SIN ordenar).
    /// Cada vuelta del ciclo cuenta como 1 iteración y 1 comparación de claves.
    /// </summary>
    public SearchResult Search(int[] data, int key)
    {
        ArgumentNullException.ThrowIfNull(data);

        long comparisons = 0;
        long iterations = 0;
        int foundIndex = -1;

        // El cronómetro encierra únicamente el algoritmo de búsqueda.
        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < data.Length; i++)
        {
            iterations++;
            comparisons++;

            if (data[i] == key)
            {
                foundIndex = i;
                break;
            }
        }

        stopwatch.Stop();

        bool found = foundIndex >= 0;
        return new SearchResult
        {
            AlgorithmName = AlgorithmName,
            Complexity = Complexity,
            Key = key,
            Found = found,
            Index = foundIndex,
            Comparisons = comparisons,
            Iterations = iterations,
            ElapsedTicks = stopwatch.ElapsedTicks,
            Details = found
                ? "Posición en el arreglo original (sin ordenar)"
                : $"Se recorrieron los {data.Length:N0} elementos sin éxito"
        };
    }
}
