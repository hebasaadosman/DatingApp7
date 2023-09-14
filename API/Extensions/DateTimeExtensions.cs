using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {

        public static int CalculateAge(this DateOnly dob) // CalculateAge method
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow); // Today
            var age = today.Year - dob.Year; // Age
            if (dob.AddYears(age) > today) age--; // If dob.AddYears(age) > today, age--
            return age; // Return age
        }
    }
}