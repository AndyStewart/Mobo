using System;

namespace Mobo
{
    public class Turn
    {
        public string Member { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public Turn(DateTime startTime, string member)
        {
            Member = member;
            StartTime = startTime;
            EndTime = startTime.AddMinutes(15);
        }

    }
}