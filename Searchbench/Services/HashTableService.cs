using System.Diagnostics;
using SearchBench.Models;

namespace SearchBench.Services;

/// <summary>
/// Servicio que implementa una tabla hash propia (sin usar Dictionary ni HashSet).
/// - Función de dispersión (método de la división): h(k) = k mod M, con M primo.
/// - Resolución de colisiones: encadenamiento separado (lista enlazada por cubeta).
/// - Búsqueda: O(1) en promedio; O(N) en el peor caso teórico (todas las claves en una cubeta).
/// </summary>
public sealed class HashTableService
{
    public const string AlgorithmName = "Tabla hash";
    public const string Complexity = "O(1) promedio";

    /// <summary>Factor de carga objetivo al dimensionar la tabla (α ≈ 0.75).</summary>
    private const double TargetLoadFactor = 0.75;

    /// <summary>Tamaño mínimo de la tabla para conjuntos muy pequeños.</summary>
    private const int MinimumTableSize = 11;

    /// <summary>Nodo de la lista enlazada que forma cada cadena (cubeta).</summary>
    private sealed class Node
    {
        public readonly int Key;
        public Node? Next;

        public Node(int key, Node? next)
        {
            Key = key;
            Next = next;
        }
    }

    private Node?[] _buckets = Array.Empty<Node?>();

    /// <summary>Número M de cubetas de la tabla.</summary>
    public int TableSize => _buckets.Length;

    /// <summary>Cantidad de claves almacenadas.</summary>
    public int Count { get; private set; }

    /// <summary>Inserciones que cayeron en una cubeta que ya tenía elementos.</summary>
    public int CollisionCount { get; private set; }

    /// <summary>Longitud de la cadena más larga.</summary>
    public int MaxChainLength { get; private set; }

    /// <summary>Cubetas con al menos un elemento.</summary>
    public int UsedBuckets { get; private set; }

    /// <summary>Factor de carga α = N / M.</summary>
    public double LoadFactor => TableSize == 0 ? 0 : (double)Count / TableSize;

    /// <summary>
    /// Construye la tabla a partir de las claves dadas: dimensiona la tabla e inserta cada clave.
    /// </summary>
    public void Build(int[] keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        Initialize(keys.Length);
        foreach (int key in keys)
        {
            Insert(key);
        }
    }

    /// <summary>
    /// Crea una tabla vacía con M = primo ≥ esperado / 0.75 y reinicia las estadísticas.
    /// Usar un tamaño primo mejora la distribución del método de la división.
    /// </summary>
    public void Initialize(int expectedCount)
    {
        int size = Math.Max(MinimumTableSize, (int)Math.Ceiling(expectedCount / TargetLoadFactor));
        _buckets = new Node?[NextPrime(size)];

        Count = 0;
        CollisionCount = 0;
        MaxChainLength = 0;
        UsedBuckets = 0;
    }

    /// <summary>
    /// Función de dispersión h(k) = k mod M. Se corrige el resultado negativo
    /// para que el índice siempre quede en el rango [0, M - 1].
    /// </summary>
    public int ComputeHash(int key)
    {
        int remainder = key % _buckets.Length;
        return remainder < 0 ? remainder + _buckets.Length : remainder;
    }

    /// <summary>
    /// Inserta una clave al inicio de la cadena de su cubeta.
    /// Devuelve false si la clave ya existía (no se admiten duplicados).
    /// </summary>
    public bool Insert(int key)
    {
        EnsureInitialized();

        int bucketIndex = ComputeHash(key);
        int chainLength = 0;

        // Se recorre la cadena para detectar duplicados y medir su longitud actual.
        for (Node? current = _buckets[bucketIndex]; current is not null; current = current.Next)
        {
            if (current.Key == key)
            {
                return false;
            }

            chainLength++;
        }

        if (chainLength == 0)
        {
            UsedBuckets++;
        }
        else
        {
            CollisionCount++; // La cubeta ya estaba ocupada: hay colisión.
        }

        _buckets[bucketIndex] = new Node(key, _buckets[bucketIndex]);
        Count++;
        MaxChainLength = Math.Max(MaxChainLength, chainLength + 1);
        return true;
    }

    /// <summary>
    /// Búsqueda directa: calcula h(k) para saltar a la cubeta y recorre solo su cadena.
    /// Cada nodo visitado cuenta como 1 iteración y 1 comparación de claves.
    /// </summary>
    public SearchResult Search(int key)
    {
        EnsureInitialized();

        long comparisons = 0;
        long iterations = 0;
        bool found = false;

        Stopwatch stopwatch = Stopwatch.StartNew();

        int bucketIndex = ComputeHash(key);
        Node? current = _buckets[bucketIndex];

        while (current is not null)
        {
            iterations++;
            comparisons++;

            if (current.Key == key)
            {
                found = true;
                break;
            }

            current = current.Next;
        }

        stopwatch.Stop();

        // Fuera del cronómetro: solo sirve para mostrar información en la interfaz.
        int chainLength = GetChainLength(bucketIndex);

        return new SearchResult
        {
            AlgorithmName = AlgorithmName,
            Complexity = Complexity,
            Key = key,
            Found = found,
            Index = found ? bucketIndex : -1,
            Comparisons = comparisons,
            Iterations = iterations,
            ElapsedTicks = stopwatch.ElapsedTicks,
            Details = $"h(k) = k mod {TableSize:N0} = cubeta {bucketIndex:N0}; cadena de {chainLength} nodo(s)"
        };
    }

    /// <summary>Cuenta los nodos de la cadena de una cubeta (uso informativo, fuera de la medición).</summary>
    private int GetChainLength(int bucketIndex)
    {
        int length = 0;
        for (Node? current = _buckets[bucketIndex]; current is not null; current = current.Next)
        {
            length++;
        }

        return length;
    }

    private void EnsureInitialized()
    {
        if (_buckets.Length == 0)
        {
            throw new InvalidOperationException("La tabla hash aún no ha sido construida.");
        }
    }

    /// <summary>Devuelve el menor número primo mayor o igual que <paramref name="candidate"/>.</summary>
    private static int NextPrime(int candidate)
    {
        if (candidate <= 2)
        {
            return 2;
        }

        if (candidate % 2 == 0)
        {
            candidate++;
        }

        while (!IsPrime(candidate))
        {
            candidate += 2;
        }

        return candidate;
    }

    /// <summary>Prueba de primalidad por división de prueba (suficiente para los tamaños usados).</summary>
    private static bool IsPrime(int number)
    {
        if (number < 2)
        {
            return false;
        }

        if (number % 2 == 0)
        {
            return number == 2;
        }

        for (int divisor = 3; (long)divisor * divisor <= number; divisor += 2)
        {
            if (number % divisor == 0)
            {
                return false;
            }
        }

        return true;
    }
}
