﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp27
{
    public class Quiz
    {
        public List<string> Questions { get; set; }
        public string Name { get; set; }
        public List<List<string>> Answers { get; set; }
        public List<List<string>> Right_answer { get; set; }
        public Dictionary<User, int[]> StudentsResults { get; set; }
        public List<byte[]> ImageList { get; set; }
        public User Teacher { get; set; }
        public string StudentPassword { get; set; }
        public Quiz() 
        {

        }
        public Quiz(string name, List<string> questions, List<List<string>> answers, List<List<string>> right_answer, Dictionary<User, int[]> StudentsResults, string TeacherName, string TeacherPassword, string StudentPassword)
        {
            Name = name;
            Questions = questions;
            Answers = answers;
            Right_answer = right_answer;
            this.StudentsResults = StudentsResults;
            this.StudentPassword = StudentPassword;

        }
    }
}
