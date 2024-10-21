using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaKlon.gubo
{
    class Grade
    {
        public Grade(int jegy, string theme, string gradeType)
        {
            this.Jegy = jegy;
            this.Theme = theme;
            this.GradeType = gradeType;
        }

        public int Jegy { get; set; }
        public string Theme { get; set; }
        public string GradeType { get; set; }
    }
}
