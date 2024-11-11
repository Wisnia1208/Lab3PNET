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

            // Wy�wietlanie paska post�pu
            Console.Write($"\rPost�p: {((i + 1) * 100 / count)}%");

            // Op�nienie
            await Task.Delay(500); // Op�nienie 500 ms
        }
        Console.WriteLine(); // Nowa linia po zako�czeniu paska post�pu
        return numbers;
    }
}
