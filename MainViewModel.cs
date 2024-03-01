using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Lab01Stoliarov
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public ICommand CalculateCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private UserModel user;
        private string ageMessage;
        private string westernZodiacSign;
        private string chineseZodiac;
        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (dateOfBirth != value)
                {
                    dateOfBirth = value;
                    OnPropertyChanged(nameof(DateOfBirth));
                  
                }
            }
        }

        public MainViewModel()
        {
            this.user = new UserModel();
            this.DateOfBirth = DateTime.Today;
            CalculateCommand = new RelayCommand(ExecuteCalculate);

            AgeMessage = "";
            WesternZodiacSign = "";
            ChineseZodiac = "";
        }

        private void ExecuteCalculate(object parameter)
        {
            
            user.DateOfBirth = this.dateOfBirth;
            UpdateUserInfo(this.dateOfBirth);
        }


        public string AgeMessage
        {
            get => ageMessage;
            set
            {
                ageMessage = value;
                OnPropertyChanged(nameof(AgeMessage));
            }
        }

        public string WesternZodiacSign
        {
            get => westernZodiacSign;
            set
            {
                westernZodiacSign = value;
                OnPropertyChanged(nameof(WesternZodiacSign));
            }
        }


        public string ChineseZodiac
        {
            get => chineseZodiac;
            set
            {
                chineseZodiac = value;
                OnPropertyChanged(nameof(ChineseZodiac));
            }
        }

        public void UpdateUserInfo(DateTime dateOfBirth)
        {
            user.DateOfBirth = dateOfBirth;
            int age = user.CalculateAge();

            if (dateOfBirth > DateTime.Now)
            {
                MessageBox.Show("Date of birth cannot be in the future.", "Invalid Date of Birth", MessageBoxButton.OK, MessageBoxImage.Error);
                AgeMessage = "";
                WesternZodiacSign = "";
                ChineseZodiac = "";
                return;
            }
            else if (age > 135)
            {
                MessageBox.Show("The age calculated is unrealistically high. Please enter a valid date of birth.", "Invalid Age", MessageBoxButton.OK, MessageBoxImage.Error);
                AgeMessage = "";
                WesternZodiacSign = "";
                ChineseZodiac = "";
                return;
            }
            else
            {
                AgeMessage = $"Your age is {age}.";
                WesternZodiacSign = "Your Western Zodiac sign is: " + CalculateWesternZodiac(dateOfBirth).ToString();
                ChineseZodiac = "Your Chinese Zodiac sign is: " + CalculateChineseZodiac(dateOfBirth).ToString();
            }

            if (dateOfBirth.Day == DateTime.Today.Day && dateOfBirth.Month == DateTime.Today.Month)
            {
                MessageBox.Show("Happy Birthday!", "Birthday Greeting", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private WesternZodiac CalculateWesternZodiac(DateTime dateOfBirth)
        {
            int day = dateOfBirth.Day;
            switch (dateOfBirth.Month)
            {
                case 1:
                    return day <= 19 ? WesternZodiac.Capricorn : WesternZodiac.Aquarius;
                case 2:
                    return day <= 18 ? WesternZodiac.Aquarius : WesternZodiac.Pisces;
                case 3:
                    return day <= 20 ? WesternZodiac.Pisces : WesternZodiac.Aries;
                case 4:
                    return day <= 19 ? WesternZodiac.Aries : WesternZodiac.Taurus;
                case 5:
                    return day <= 20 ? WesternZodiac.Taurus : WesternZodiac.Gemini;
                case 6:
                    return day <= 20 ? WesternZodiac.Gemini : WesternZodiac.Cancer;
                case 7:
                    return day <= 22 ? WesternZodiac.Cancer : WesternZodiac.Leo;
                case 8:
                    return day <= 22 ? WesternZodiac.Leo : WesternZodiac.Virgo;
                case 9:
                    return day <= 22 ? WesternZodiac.Virgo : WesternZodiac.Libra;
                case 10:
                    return day <= 22 ? WesternZodiac.Libra : WesternZodiac.Scorpio;
                case 11:
                    return day <= 21 ? WesternZodiac.Scorpio : WesternZodiac.Sagittarius;
                case 12:
                    return day <= 21 ? WesternZodiac.Sagittarius : WesternZodiac.Capricorn;
                default:
                    throw new ArgumentOutOfRangeException("Invalid month"); //Unreachable
            }
        }



        private ChineseZodiac CalculateChineseZodiac(DateTime dateOfBirth)
        {
            int index = (dateOfBirth.Year - 1900) % 12;
            if (index < 0) index += 12; 
            return (ChineseZodiac)index;
        }

    }
}
