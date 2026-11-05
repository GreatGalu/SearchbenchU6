using System.Diagnostics;
using SearchBench.Models;

namespace SearchBench.Services;

/// <summary>
/// Servicio independiente que implementa la búsqueda binaria (iterativa).
/// Requiere un arreglo contiguo y ORDENADO; en cada iteración descarta la mitad del intervalo.
/// Complejidad: O(log N). Máximo de iteraciones = ⌊log2(N)⌋ + 1.
/// </summary>
public sealed class BinarySearchService
{
    public const string AlgorithmName = "Búsqueda binaria";
    public const string Complexity = "O(log N)";

    /// <summary>
    /// Busca <paramref name="key"/> en <paramref name="sortedData"/> (arreglo ordenado ascendentemente).
    /// Cada división del intervalo cuenta como 1 iteración y 1 comparación (comparación de tres vías con CompareTo).
    /// </summary>
    public SearchResult Search(int[] sortedData, int key)
    {
        ArgumentNullException.ThrowIfNull(sortedData);

        long comparisons = 0;
        long iterations = 0;
        int foundIndex = -1;
        int low = 0;
        int high = sortedData.Length - 1;

        Stopwatch stopwatch = Stopwatch.StartNew();

        while (low <= high)
        {
            iterations++;

            // Se evita (low + high) / 2 para impedir desbordamiento de entero.
            int middle = low + (high - low) / 2;

            comparisons++;
            int comparison = sortedData[middle].CompareTo(key);

            if (comparison == 0)
            {
                foundIndex = middle;
                break;
            }

            if (comparison < 0)
            {
                low = middle + 1;   // La clave está en la mitad derecha.
            }
            else
            {
                high = middle - 1;  // La clave está en la mitad izquierda.
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
                ? "Posición en el arreglo ordenado"
                : $"Intervalo agotado (low = {low}, high = {high})"
        };
    }
}
