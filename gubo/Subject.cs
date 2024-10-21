using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaKlon.gubo
{
    class Subject
    {
        public List<Grade> Grades = new List<Grade>();
        public string Nev;
        public Subject(string nev)
        {
            Nev = nev;
            Grades = new List<Grade>();
        }
        public void Jegy(Grade Jegy)
        {
            Grades.Append(Jegy);
        }
    }
}
