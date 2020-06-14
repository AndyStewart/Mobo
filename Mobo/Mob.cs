using System;
using System.Collections.Generic;

namespace Mobo
{
    public class Mob
    {
        private DateTime _startTime;
        private Turn _currentTurn;

        public Mob(string name) 
            => Name = !string.IsNullOrEmpty(name) 
                ? name 
                : throw new ArgumentException("name");

        public string Name { get; }

        public void AddMember(string memberName)
        {
            var name = !string.IsNullOrEmpty(memberName) 
                            ? memberName 
                            : throw new ArgumentException("memberName");
            Members.Add(name);
        }

        public IList<string> Members { get; } = new List<string>();
        public TimeSpan TimeLeft(DateTime now)
        {
            if (_currentTurn == null)
                return TimeSpan.FromMinutes(15);
            while (_currentTurn.EndTime < now)
            {
                _currentTurn = NextPersonsTurn(_currentTurn.Member);
            }

            return _currentTurn.EndTime.Subtract(now);
        }

        public string WhosTurnIsIt(DateTime dateTime)
        {
            if (_currentTurn == null)
                return string.Empty;
            while (_currentTurn.EndTime < dateTime)
            {
               _currentTurn = NextPersonsTurn(_currentTurn.Member);
            }

            return _currentTurn.Member;
        }

        private Turn NextPersonsTurn(string previewTurn)
        {
            var index = Members.IndexOf(previewTurn) + 1;
            if (index >= Members.Count)
                index = 0;
            return new Turn(_currentTurn.EndTime, Members[index]);
        }
        
        public void StartTimer(DateTime now)
        {
            _currentTurn = new Turn(now, Members[0]);
            _startTime = now;
        }
    }
}