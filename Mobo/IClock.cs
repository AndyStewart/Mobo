using System;
namespace Mobo
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}