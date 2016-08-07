namespace Executor.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Executor.Contracts;
    using Executor.Exceptions;

    public class SoftUniStudent : IStudent
    {
        private string userName;
        private Dictionary<string, ICourse> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public SoftUniStudent(string userName)
        {
            this.UserName = userName;
            this.enrolledCourses = new Dictionary<string, ICourse>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.userName = value;
            }
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses
        {
            get
            {
                return this.enrolledCourses;
            }
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get
            {
                return this.marksByCourseName;
            }
        }

        public void EnrollInCourse(ICourse softUniCourse)
        {
            if (this.EnrolledCourses.ContainsKey(softUniCourse.Name))
            {
                throw new DuplicateEntryInStructureException(this.UserName, softUniCourse.Name);
            }

            this.enrolledCourses.Add(softUniCourse.Name, softUniCourse);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.EnrolledCourses.ContainsKey(courseName))
            {
                throw new KeyNotFoundException(ExceptionMessages.NotEnrolledInCourse);
            }

            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
            {
                throw new ArgumentException(ExceptionMessages.InvalidNumberOfScores);
            }

            this.marksByCourseName.Add(courseName, this.CalculateMark(scores));
        }

        public string GetMarkForCourse(string courseName)
        {
            return string.Format($"{this.userName} - {this.MarksByCourseName[courseName]}");
        }

        public int CompareTo(IStudent other) => string.Compare(this.UserName, other.UserName, StringComparison.Ordinal);

        public override string ToString() => this.UserName;

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum() / (double)(SoftUniCourse.NumberOfTasksOnExam * SoftUniCourse.MaxScoreOnExamTask);
            double mark = (percentageOfSolvedExam * 4) + 2;
            return mark;
        }
    }
}
