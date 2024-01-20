using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp27
{
    public class Quiz
    {
        public Dictionary<string, List<string>> Questions { get; set; }
        public Dictionary<string, int> StudentsResults { get; set; }
        public List<byte[]> ImageList { get; set; }
        private string TeacherPassword { get; set; }
        public string Name { get; set; }
        public string StudentPassword { get; set; }
        public string TeacherName { get; set; }
        public Quiz()
        {

        }
    }
}
