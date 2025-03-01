using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class RandomNumberGenerator
{
    private static readonly Random _random = new Random();

    public async Task<List<int>> Generate(int count, int minValue, int maxValue)
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < count; i++)
        {
            // Generowanie liczby pseudolosowej
            int number = _random.Next(minValue, maxValue + 1);
            numbers.Add(number);

            // Wyświetlanie paska postępu
            Console.Write($"\rPostęp: {((i + 1) * 100 / count)}%");

            // Opóźnienie
            await Task.Delay(500); // Opóźnienie 500 ms
        }
        Console.WriteLine(); // Nowa linia po zakończeniu paska postępu
        return numbers;
    }
}
