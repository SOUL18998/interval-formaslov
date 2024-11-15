using System;

class Program
{
    static void Main()
    {
        // Ввод всех данных
        Console.WriteLine("Введите начало рабочего дня (например, 08:00):");
        int workStart = TimeToMinutes(Console.ReadLine());

        Console.WriteLine("Введите конец рабочего дня (например, 18:00):");
        int workEnd = TimeToMinutes(Console.ReadLine());

        Console.WriteLine("Введите минимальное время для консультации (в минутах):");
        int consultationTime = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество занятых промежутков:");
        int intervalCount = int.Parse(Console.ReadLine());

        int[] intervaStarts = new int[intervalCount];
        int[] intervaEnds = new int[intervalCount];

        // цикл для ввода занятых промежутков
        for (int i = 0; i < intervalCount; i++)
        {
            Console.WriteLine($"Введите начало {i + 1}-го занятого промежутка (например, 10:00):");
            intervaStarts[i] = TimeToMinutes(Console.ReadLine());

            Console.WriteLine($"Введите длительность {i + 1}-го занятого промежутка (в минутах):");
            int duration = int.Parse(Console.ReadLine());
            intervaEnds[i] = intervaStarts[i] + duration;
        }

        // Сортировка занятых промежутков
        SortBusyIntervals(intervaStarts, intervaEnds);

        // Выводим свободные промежутки
        Console.WriteLine("Свободные временные промежутки:");
        FindAndPrintFreeSlots(intervaStarts, intervaEnds, ref workStart, workEnd, consultationTime);
    }

    // Метод для сортировки занятых промежутков по началу
    static void SortBusyIntervals(int[] starts, int[] ends)
    {
        // Любимый пузырек
        for (int i = 0; i < starts.Length - 1; i++)
        {
            for (int j = i + 1; j < starts.Length; j++)
            {
                if (starts[i] > starts[j])
                {
                    int tempStart = starts[i];
                    starts[i] = starts[j];
                    starts[j] = tempStart;

                    int tempEnd = ends[i];
                    ends[i] = ends[j];
                    ends[j] = tempEnd;
                }
            }
        }
    }

    // Метод для поиска и вывода свободных промежутков
    static void FindAndPrintFreeSlots(int[] busyStarts, int[] busyEnds, ref int workStart, int workEnd, int consultationTime)
    {
        for (int i = 0; i < busyStarts.Length; i++)
        {
            // Выводим свободные промежутки перед следующим занятым интервалом
            while (workStart + consultationTime <= busyStarts[i])
            {
                Console.WriteLine($"{MinutesToTime(workStart)}-{MinutesToTime(workStart + consultationTime)}");
                workStart += consultationTime;
            }

            // Обновляем начало свободного промежутка
            if (workStart < busyEnds[i])
            {
                workStart = busyEnds[i];
            }
        }

        // Выводим оставшиеся свободные промежутки до конца рабочего дня
        while (workStart + consultationTime <= workEnd)
        {
            Console.WriteLine($"{MinutesToTime(workStart)}-{MinutesToTime(workStart + consultationTime)}");
            workStart += consultationTime;
        }
    }

    // Перевод времени из формата HH:mm в минуты
    static int TimeToMinutes(string time)
    {
        string[] parts = time.Split(':');
        int hours = int.Parse(parts[0]);
        int minutes = int.Parse(parts[1]);
        return hours * 60 + minutes;
    }

    // Перевод минут в формат HH:mm
    static string MinutesToTime(int minutes)
    {
        int hours = minutes / 60;
        int mins = minutes % 60;
        return $"{hours:D2}:{mins:D2}";
    }
}
