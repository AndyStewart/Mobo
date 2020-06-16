using System;

namespace Mobo
{
    public class CountDownTimer
    {
        private IClock _clock;
        private DateTime _endTime;
        public TimeSpan TimerLength { get; }

        public CountDownTimer(IClock clock, TimeSpan timerLength)
        {
            _clock = clock;
            TimerLength = timerLength;
        }

        public void Start() => _endTime = _clock.Now.Add(TimerLength);

        public TimeSpan TimeLeft
        {
            get
            {
                if (_endTime > _clock.Now)
                    return _endTime.Subtract(_clock.Now);
                return _endTime == default ? TimerLength : TimeSpan.Zero;
            }
        }

        public bool Running => TimeLeft < TimerLength && !(_clock is PausedClock);

        public void Stop() => _clock = new PausedClock(_clock);
        
        public override string ToString() => $"{TimeLeft:mm}:{TimeLeft:ss}";
        class PausedClock : IClock    
        {
            public PausedClock(IClock clock) => Now = clock.Now;

            public DateTime Now { get; }
        }
    }
}