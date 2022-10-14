using Source.Models;

namespace Source.Repositories;

public class EFStudentRepository : IStudentRepository
{
    private readonly MyDbContext? _myDbContext;

    public EFStudentRepository()
    {
        _myDbContext = new MyDbContext();
    }

    public void Add(Student entity)
    {
        _myDbContext?.Students?.Add(entity);
        _myDbContext?.SaveChanges();
    }

    public Student? GetById(Guid id)
        => _myDbContext?.Students?.Find(id); 

    public List<Student>? GetList(Func<Student, bool>? predicate = null)
        => (predicate == null) switch
        {
            true => _myDbContext?.Students?.ToList(),
            false => _myDbContext?.Students?.Where(predicate).ToList(),
        };

    public Student? Get(Func<Student, bool> predicate)
        => _myDbContext?.Students?.FirstOrDefault(predicate);

    public void Remove(Student entity)
    {
        _myDbContext?.Students?.Remove(entity);
        _myDbContext?.SaveChanges();
    }

    public void Update(Student entity)
    {
        _myDbContext?.Students?.Update(entity);
        _myDbContext?.SaveChanges();
    }
}