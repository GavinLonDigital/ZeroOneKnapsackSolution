using System;

namespace ZeroOneKnapsackSolution
{
    enum TextStyle
    {
        Normal,
        Success,
        Danger
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*
                1 - Microwave - weight: 8, value: 50 
                2 - Drone - weight: 2, value: 150
                3 - Monitor - weight: 6, value: 210
                4 - Kettle - weight: 1, value: 30

                Total capacity of container can hold 10kgs

            */
            int[] weights = new int[] { 0, 8, 2, 6, 1 };
            int[] values = new int[] { 0, 50, 150, 210, 30 };

            string[] itemNames = new string[] { "", "Microwave", "Drone", "Monitor", "Kettle" };

            int[,] data = new int[5, 11];

            const int n = 4;

            const int maxCapacity = 10;

            int result = GetMaxValueInContainer(n, maxCapacity, weights, values, data);

            WriteTextToScreen($"Max value of items included in container: {data[n, maxCapacity]}", TextStyle.Normal);

            Console.WriteLine();
            Console.WriteLine();

            OutputItemInclusionStatus(n, maxCapacity, weights, values, itemNames, data);

            Console.ReadKey();

        }
        private static int GetMaxValueInContainer(int n, int maxCapacity, int[] weights, int[] values, int[,] data)
        {
            for (int itemNum = 0; itemNum <= n; itemNum++)
            {
                for (int capacity = 0; capacity <= maxCapacity; capacity++)
                {
                    if (itemNum == 0 || capacity == 0)
                    {
                        data[itemNum, capacity] = 0;
                    }
                    else if (weights[itemNum] <= capacity)
                    {
                        data[itemNum, capacity] = Math.Max(
                            values[itemNum] + data[itemNum - 1, capacity - weights[itemNum]],
                                              data[itemNum - 1, capacity]
                                              );
                    }
                    else
                    {
                        data[itemNum, capacity] = data[itemNum - 1, capacity];
                    }
                }
            }
            return data[n, maxCapacity];
        }
        private static void OutputItemInclusionStatus(int n, int maxCapacity, int[] weights, int[] values, string[] itemNames, int[,] data)
        {
            int i = n;
            int j = maxCapacity;

            WriteTextToScreen("Items excluded from the container are printed with a red background", TextStyle.Danger);
            WriteTextToScreen("Items included in the container are printed with a green background", TextStyle.Success);
            Console.WriteLine();
            Console.WriteLine();

            while (i > 0 && j > 0)
            {
                if (data[i, j] == data[i - 1, j])
                {
                    //item excluded
                    WriteTextToScreen($"Item Name:{itemNames[i]}, Item Weight: {weights[i]}, Item Value: {values[i]}", TextStyle.Danger);

                }
                else
                {
                    //item included
                    WriteTextToScreen($"Item Name:{itemNames[i]}, Item Weight: {weights[i]}, Item Value: {values[i]}", TextStyle.Success);

                    j = j - weights[i];
                }
                i--;
            }
        }




        private static void WriteTextToScreen(string text, TextStyle textStyle)
        {
            switch (textStyle)
            {
                case TextStyle.Normal:
                    Console.ResetColor();
                    break;
                case TextStyle.Success:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case TextStyle.Danger:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
