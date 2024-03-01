using System;

namespace Lab01Stoliarov
{
    public class UserModel
    {
        public DateTime DateOfBirth { get; set; }

        public int CalculateAge()
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
