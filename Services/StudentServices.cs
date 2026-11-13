using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TeacherApp.Data;
using TeacherApp.Models;

namespace TeacherApp.Services
{
    public class StudentServices
    {
        private readonly StudentContext context;
        public StudentServices(StudentContext _context) 
        {
            context = _context;
        }

        private void Save()
        {
            context.SaveChanges();
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Students.ToListAsync();
        }

        public void AddStudent(Student student)
        {
            var students = context.Students.Max(id => (int?) id.StudentID) ?? 0;

            student.StudentID = students + 1;

            context.Students.Add(student);
            Save();
        }

        public void DeleteStudent(int id)
        {
            var student = context.Students.Find(id);

            if (student != null)
            {
                context.Students.Remove(student);
                Save();
            }
        }

        public void UpdateStudent(int id, Student student)
        {
            var newStudent = context.Students.Find(id);

            if (newStudent != null)
            {
                newStudent.FirstName = student.FirstName;
                newStudent.LastName = student.LastName;
                newStudent.GradeLevel = student.GradeLevel;

                Save();
            }
        }


    }
}
