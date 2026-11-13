using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeacherApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string firstName;

        [ObservableProperty]
        string lastName;

        [ObservableProperty]
        string gradeLevel;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsItFull))]
        bool isFull;

        public bool IsItFull
        { 
            get 
            {  
                return 
                (!string.IsNullOrEmpty(FirstName) &&
                !string.IsNullOrEmpty(LastName) && 
                int.TryParse(GradeLevel, out int grade) && 
                grade >= 1 && grade <= 12); 
            }  
        }
        
    }
}
