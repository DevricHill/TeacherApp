using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TeacherApp.Data;
using TeacherApp.Models;
using TeacherApp.Services;

namespace TeacherApp.ViewModels
{
    public partial class StudentViewModel : BaseViewModel
    {
        IConnectivity connectivity;
        StudentServices studentServices;

        public StudentViewModel(IConnectivity _connectivity, StudentServices _studentServices)
        {
            connectivity = _connectivity;
            studentServices = _studentServices;
        }

        private void Reset()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            GradeLevel = string.Empty;
        }

        [ObservableProperty]
        public ObservableCollection<Student> students = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(DeleteStudentCommand))]
        [NotifyCanExecuteChangedFor(nameof(UpdateStudentCommand))]
        private Student? selectedStudent;

        private bool HasSelected { get {  return selectedStudent != null; }  }

        
        partial void OnSelectedStudentChanged(Student? value)
        {
            if(HasSelected == null)
            {
                Reset();
                return;
            }
            

            FirstName = selectedStudent.FirstName;
            LastName = selectedStudent.LastName;
            GradeLevel = selectedStudent.GradeLevel.ToString();
        }

        [RelayCommand]
        private async Task CreateStudentsAsync()
        {
            if (!IsItFull)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Please fill out the textboxes correctly", "Ok");
                return;
            }
            var student = new Student { FirstName = this.FirstName, LastName = this.LastName, GradeLevel = int.Parse(this.GradeLevel) };
            studentServices.AddStudent(student);
            Students.Add(student);

            Reset();
        }

        [RelayCommand]
        private async Task GetStudentsAysnc()
        {
            var currentStudents = await studentServices.GetStudentsAsync();

            Students.Clear();
            foreach (Student student in currentStudents)
                Students.Add(student);


        }

        [RelayCommand]
        async Task DeleteStudentAsync()
        {
            if(selectedStudent == null)
            {
                return;
            }

            studentServices.DeleteStudent(selectedStudent.StudentID);
            selectedStudent = null;

            Reset();

            await GetStudentsAysnc();
        }

        [RelayCommand]
        async Task UpdateStudentAsync()
        {
            if(selectedStudent == null || !IsItFull)
            {
                return;
            }

            Student newStudent = new Student {FirstName = this.FirstName, LastName = this.LastName, GradeLevel = int.Parse(this.GradeLevel) };

            studentServices.UpdateStudent(selectedStudent.StudentID, newStudent);
            selectedStudent = null;

            Reset();

            await GetStudentsAysnc();
        }
    }
}
