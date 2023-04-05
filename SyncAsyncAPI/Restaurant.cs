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
            Unbooked();
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

                if (table is null)
                    Message.TableAllBooked();
                else
                    Message.TableBooked(table.Id);
            });
        }

        public void UnbookedTable(int id)
        {
            Console.WriteLine("Добрый день! Подождите секунду я найду Вашу бронь и отменю ее, оставайтесь на линии");

            var table = _tables.FirstOrDefault(t => t.Id == id && t.State == State.Booked);

            Thread.Sleep(1000 * 5); // в течение 5 секунд происходит поиск стола
            table?.SetState(State.Free);

            Console.WriteLine(table is null
                ? "К сожалению, не удалось найти Вашу бронь"
                : "Готово! Бронь отменена");
        }

        public void UnbookedTableAsync(int id)
        {
            Console.WriteLine("Добрый день! Подождите секунду я найду Вашу бронь и отменю ее, Вам придет уведомление");

            Task.Run(async () =>
            {
                var table = _tables.FirstOrDefault(t => t.Id == id && t.State == State.Booked);

                await Task.Delay(1000 * 5);
                table?.SetState(State.Free);


                Console.WriteLine(table is null
                    ? "УВЕДОМЛЕНИЕ: К сожалению, не удалось найти Вашу бронь"
                    : $" УВЕДОМЛЕНИЕ: Готово! Бронь отменена");
            });
        }

        private void Unbooked()
        {
            var timer = new Timer(
            callback =>
            {
                foreach (var t in _tables)
                {
                    if (t.State == State.Booked)
                        t.SetState(State.Free);
                }
            },
            state: null,
            dueTime: 20000,
            period: 20000);
        }
    }
}
