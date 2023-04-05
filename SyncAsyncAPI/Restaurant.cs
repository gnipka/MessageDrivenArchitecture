namespace SyncAsyncAPI
{
    public class Restaurant
    {
        private readonly List<Table> _tables = new();

        public Restaurant()
        {
            for (ushort i = 1; i <= 10; i++)
            {
                _tables.Add(new Table(i));
            }
        }

        public void BookFreeTable(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду я подберу столик и подтвержу вашу бронь, оставайтесь на линии");

            var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons && t.State == State.Free);

            Thread.Sleep(1000 * 5); // в течение 5 секунд происходит поиск стола
            table?.SetState(State.Booked);

            Console.WriteLine(table is null
                ? "К сожалению, сейчас все столики заняты"
                : $"Готово! Ваш столик номер {table.Id}");
        }

        public void BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду я подберу столик и подтвержу вашу бронь, Вам придет уведомление");

            Task.Run(async () =>
            {
                var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons && t.State == State.Free);

                await Task.Delay(1000 * 5);
                table?.SetState(State.Booked);


                Console.WriteLine(table is null
                    ? "УВЕДОМЛЕНИЕ: К сожалению, сейчас все столики заняты"
                    : $" УВЕДОМЛЕНИЕ: Готово! Ваш столик номер {table.Id}");
            });
        }
    }
}
