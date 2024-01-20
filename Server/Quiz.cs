using System.Collections.Generic;
using System.Windows.Media;

namespace ServerProgramm
{
    public class Quiz
    {
        public Dictionary<string, List<string>> Questions { get; set; }
        public Dictionary<string, int> StudentsResults { get; set; }
        public List<byte[]> ImageList { get; set; }
        private string TeacherPassword { get; set; }
        public string Name { get; set; }
        public string StudentPassword { get; set; }
        public Quiz() 
        {

        }
    }
}
