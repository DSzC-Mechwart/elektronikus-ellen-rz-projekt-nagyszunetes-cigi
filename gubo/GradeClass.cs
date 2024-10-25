namespace KretaKlon.gubo
{
    public class GradeClass(int grade, string theme, string gradeType) {
        public int Grade { get; set; } = grade;
        public string Theme { get; set; } = theme;
        public string GradeType { get; set; } = gradeType;
    }
}
