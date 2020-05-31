using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Reflection;

namespace Mobo
{
    public class Mob
    {
        private DateTime startTime;
        private Turn currentTurn;

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

        public string WhosTurnIsIt(DateTime dateTime)
        {
            while (currentTurn.EndTime < dateTime)
            {
               currentTurn = NextPersonsTurn(currentTurn.Member);
            }

            return currentTurn.Member;
        }

        private Turn NextPersonsTurn(string previewTurn)
        {
            var index = Members.IndexOf(previewTurn) + 1;
            if (index >= Members.Count)
                index = 0;
            return new Turn(currentTurn.EndTime, Members[index]);
        }
        
        public void StartTimer(DateTime now)
        {
            currentTurn = new Turn(now, Members[0]);
            startTime = now;
        }
    }
}