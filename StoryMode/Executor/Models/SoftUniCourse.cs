﻿namespace Executor.Models
{
    using System;
    using System.Collections.Generic;
    using Executor.Contracts;
    using Executor.Exceptions;

    public class SoftUniCourse : ICourse
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        private string name;
        private Dictionary<string, IStudent> studentsByName;

        public SoftUniCourse(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName
        {
            get
            {
                return this.studentsByName;
            }
        }

        public void EnrollStudent(IStudent softUniStudent)
        {
            if (this.studentsByName.ContainsKey(softUniStudent.UserName))
            {
                throw new DuplicateEntryInStructureException(softUniStudent.UserName, this.Name);
            }

            this.studentsByName.Add(softUniStudent.UserName, softUniStudent);
        }

        public int CompareTo(ICourse other) => string.Compare(this.Name, other.Name, StringComparison.Ordinal);

        public override string ToString() => this.Name;
    }
}
