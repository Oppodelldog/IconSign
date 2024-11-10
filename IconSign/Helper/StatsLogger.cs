using System;
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
        
        private DateTime _start;

        public StatsLogger(string name, int logEvery)
        {
            Name = name;
            LogEvery = logEvery;
        }

        public void Start()
        {
            _start = DateTime.Now;
        }

        public void Done()
        {
            var duration = (float)(DateTime.Now - _start).TotalMilliseconds;

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