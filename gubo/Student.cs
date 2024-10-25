namespace KretaKlon.gubo
{
    public class Student(string name, List<Subject> subjects) {
        public readonly string Name = name;
        public List<Subject> Subjects { get; set; } = subjects;
    }
}
