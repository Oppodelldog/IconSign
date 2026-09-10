using System.Diagnostics;
using UnityEngine;

namespace IconSign.Helper
{
    public class StatsLogger
    {
        private string Name { get; set; }
        private int LogEvery { get; set; }

        private int Total { get; set; } = 0;
        private float AvgDuration { get; set; } = 0;
        private float MinDuration { get; set; } = float.MaxValue;
        private float MaxDuration { get; set; } = 0;
        
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public StatsLogger(string name, int logEvery)
        {
            Name = name;
            LogEvery = logEvery;
        }

        public void Start()
        {
            if (LogEvery <= 0) return;
            _stopwatch.Restart();
        }

        public void Done()
        {
            if (LogEvery <= 0 || !_stopwatch.IsRunning) return;

            _stopwatch.Stop();
            var duration = (float)_stopwatch.Elapsed.TotalMilliseconds;

            Total++;
            AvgDuration = (AvgDuration * (Total - 1) + duration) / Total;
            MinDuration = Mathf.Min(MinDuration, duration);
            MaxDuration = Mathf.Max(MaxDuration, duration);

            if (Total % LogEvery == 0)
            {
                Jotunn.Logger.LogInfo(ToString());
            }
        }

        public override string ToString()
        {
            return $"Stats for {Name} - Total: {Total}, Avg: {AvgDuration}ms, Min: {MinDuration}ms, Max: {MaxDuration}ms";
        }

    }
}