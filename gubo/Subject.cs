namespace KretaKlon.gubo
{
    public class Subject(string name, GradeClass? grade = null) {
        public readonly List<GradeClass> Grades = grade != null ? [grade] : [];
        public string Name = name;
    }
}
