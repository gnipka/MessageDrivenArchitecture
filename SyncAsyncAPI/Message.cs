namespace SyncAsyncAPI
{
    public static class Message
    {
        public static async void TableAllBooked()
        {
            await Task.Delay(1000 * 2);
            Console.WriteLine("\nУВЕДОМЛЕНИЕ: К сожалению, сейчас все столики заняты");
        }

        public static async void TableBooked(int id)
        {
            await Task.Delay(1000 * 2);
            Console.WriteLine($"\nУВЕДОМЛЕНИЕ: Готово! Ваш столик номер {id}");
        }
    }
}
