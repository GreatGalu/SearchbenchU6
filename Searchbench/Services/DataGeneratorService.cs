namespace SearchBench.Services;

/// <summary>
/// Servicio responsable únicamente de generar el banco de datos aleatorio.
/// </summary>
public sealed class DataGeneratorService
{
    /// <summary>
    /// Factor que define el rango de claves: [1, N * KeyRangeFactor].
    /// Un rango mayor que N permite elegir fácilmente claves inexistentes.
    /// </summary>
    public const int KeyRangeFactor = 10;

    private readonly Random _random = new();

    /// <summary>
    /// Devuelve el valor máximo que puede tomar una clave para un banco de <paramref name="count"/> elementos.
    /// </summary>
    public int GetMaxKeyValue(int count) => checked(count * KeyRangeFactor);

    /// <summary>
    /// Genera <paramref name="count"/> claves enteras aleatorias y ÚNICAS, en orden aleatorio.
    /// La unicidad evita ambigüedad en el índice reportado por cada algoritmo.
    /// </summary>
    public int[] GenerateUniqueRandomKeys(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), "N debe ser mayor que cero.");
        }

        int maxValue = GetMaxKeyValue(count);
        var data = new int[count];
        var usedKeys = new HashSet<int>(count);

        int position = 0;
        while (position < count)
        {
            int candidate = _random.Next(1, maxValue + 1);

            // HashSet.Add devuelve false si la clave ya existía: se descarta y se intenta otra.
            if (usedKeys.Add(candidate))
            {
                data[position++] = candidate;
            }
        }

        return data;
    }
}
