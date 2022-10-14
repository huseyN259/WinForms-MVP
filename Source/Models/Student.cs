namespace Source.Models;

public class Student
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public DateTime DateOfBirth { get; set; } // for EF
    public float Score { get; set; }

    public Student()
    {
        Id = Guid.NewGuid();
    }

    public Student(string firstname, string lastname, DateTime dateOfBirth, float score)
        : this()
    {
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        Score = score;
    }

    public override string ToString()
        => $"Id: {Id.ToString().Remove(8)}    Firstname: {Firstname}    Lastname: {Lastname}    DateofBirth: {DateOfBirth.ToShortDateString()}    Score: {Score}";
}