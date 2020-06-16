using System;

namespace Mobo
{
    public class CountDownTimer
    {
        private IClock _clock;
        private readonly TimeSpan _timerLength;
        private DateTime _endTime;

        public CountDownTimer(IClock clock, TimeSpan timerLength)
        {
            _clock = clock;
            _timerLength = timerLength;
        }

        public void Start() => _endTime = _clock.Now.Add(_timerLength);

        public TimeSpan TimeLeft
        {
            get
            {
                if (_endTime > _clock.Now)
                    return _endTime.Subtract(_clock.Now);
                return _endTime == default ? _timerLength : TimeSpan.Zero;
            }
        }

        public void Stop() => _clock = new PausedClock(_clock);

        class PausedClock : IClock    
        {
            public PausedClock(IClock clock) => Now = clock.Now;

            public DateTime Now { get; }
        }
        public override string ToString() => $"{TimeLeft:mm}:{TimeLeft:ss}";
    }
}