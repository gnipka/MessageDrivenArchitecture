namespace SyncAsyncAPI
{
    public enum State
    {
        /// <summary>
        /// Стол свободен
        /// </summary>
        Free = 0,

        /// <summary>
        /// Стол занят
        /// </summary>
        Booked = 1
    }

    public class Table
    {
        public Table(int id)
        {
            Id = id;
            State = State.Free;
            Random random = new Random();
            SeatsCount = random.Next(2, 5);
        }

        public State State { get; private set; }

        public int SeatsCount { get; }

        public int Id { get; }

        public bool SetState(State state)
        {
            if (State == state) return false;

            State = state;
            return true;
        }
    }
}
