using System;

namespace Mobo
{
    public class CountDownTimer : ICountDownTimer
    {
        private readonly IClock _clock;
        public TimeSpan TimerLength { get; }

        public CountDownTimer(IClock clock, TimeSpan timerLength)
        {
            _clock = clock;
            TimerLength = timerLength;
        }

        public ICountDownTimer Start()
        {
            return new InProgressTimer(_clock, _clock.Now.Add(TimerLength));
        }

        public TimeSpan TimeLeft => TimerLength;

        public ICountDownTimer Stop() => new PausedTimer(_clock, TimeLeft);
        
        public override string ToString() => $"{TimeLeft:mm}:{TimeLeft:ss}";
    }

    public class InProgressTimer : ICountDownTimer
    {
        private readonly IClock _clock;
        private readonly DateTime _endTime;

        public InProgressTimer(IClock clock, DateTime endTime)
        {
            _clock = clock;
            _endTime = endTime;
        }
        
        public TimeSpan TimeLeft =>  _clock.Now > _endTime ? TimeSpan.Zero : _endTime.Subtract(_clock.Now);
        public ICountDownTimer Start() => this;

        public ICountDownTimer Stop() => new PausedTimer(_clock, TimeLeft);
        public override string ToString() => $"{TimeLeft:mm}:{TimeLeft:ss}";

    }

    public class PausedTimer : ICountDownTimer
    {
        private readonly IClock _clock;

        public PausedTimer(IClock clock, TimeSpan timeLeft)
        {
            _clock = clock;
            TimeLeft = timeLeft;
        }
        
        public TimeSpan TimeLeft { get; }
        public ICountDownTimer Start() => new InProgressTimer(_clock, _clock.Now.Add(TimeLeft));

        public ICountDownTimer Stop() => this;
        public override string ToString() => $"{TimeLeft:mm}:{TimeLeft:ss}";
    }

    public interface ICountDownTimer
    {
        TimeSpan TimeLeft { get; }
        string ToString();
        ICountDownTimer Start();
        ICountDownTimer Stop();
    }
}