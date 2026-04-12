using Semesterprojekt1PBA.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Semesterprojekt1PBA.Domain.ValueObjects
{
    public record DateRange
    {
        private DateRange(DateOnly start, DateOnly end)
        {
            var now = DateOnly.FromDateTime(DateTime.Now);

            Validate(start, end, now);
            Start = start;
            End = end;
        }

        public DateOnly Start { get; protected set; }
        public DateOnly End { get; protected set; }

        public static DateRange Create(DateOnly start, DateOnly end)
        {
            return new DateRange(start, end);
        }

        private void Validate(DateOnly start, DateOnly end, DateOnly now)
        {
            if (start >= end)
                throw new ErrorException("End date has to be after the start date.");

            if (start <= now)
                throw new ErrorException("Start date has to be in the future.");
        }
    }
}
