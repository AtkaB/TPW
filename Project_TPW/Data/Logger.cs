using System.Collections.Concurrent;
using System.Text.Json;
using System.Numerics;

namespace Data
{
    public class Logger
    {
        private class BallToSerialize
        {
            public int Id { get; }
            public float X { get; }
            public float Y { get; }
            public float SpeedHorizontal { get; }
            public float SpeedVertical { get; }
            public string Date { get; }


            public BallToSerialize(Vector2 position, Vector2 speed, int id, string date)
            {
                X = position.X;
                Y = position.Y;
                Date = date;
                SpeedHorizontal = speed.X;
                SpeedVertical = speed.Y;
                Id = id;
            }
        }

        ConcurrentQueue<BallToSerialize> _queue;
        public Logger()
        {
            _queue = new ConcurrentQueue<BallToSerialize>();
            WriteToFile();
        }

        public void AddObjectToQueue(IBall obj, string date)
        {
            _queue.Enqueue(new BallToSerialize(obj.Position, obj.Speed, obj.ID, date));
        }

        private void WriteToFile()
        {
            Task.Run(async () =>
            {
                using StreamWriter _streamWriter = new StreamWriter("logs.json");
                while (true)
                {
                    while (_queue.TryDequeue(out BallToSerialize item))
                    {
                        string jsonString = JsonSerializer.Serialize(item);
                        _streamWriter.WriteLine(jsonString);
                    }
                    await _streamWriter.FlushAsync();
                }
            });
        }
    }
}
