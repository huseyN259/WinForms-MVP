using Source.Models;
using Source.Repositories;
using Source.Views;

namespace Source.Presenters;

public class MainPresenter
{
    private readonly IMainView? _mainView;
    private readonly IAddUpdateView? _add_update_View;
    private readonly IStudentRepository? _repository;

    private readonly BindingSource _bindingSource;

    public MainPresenter(IMainView mainView, IAddUpdateView _Add_Update_View, IStudentRepository studentRepository)
    {
        _mainView = mainView;
        _add_update_View = _Add_Update_View;
        _repository = studentRepository;

        // Binding Source
        _bindingSource = new();
        _bindingSource.DataSource = _repository?.GetList();
        _mainView.SetStudentListBindingSource(_bindingSource);

        // Events
        _mainView.SearchEvent += _mainView_SearchEvent;
        _mainView.DeleteEvent += _mainView_DeleteEvent;
        _mainView.AddEvent += _mainView_AddEvent;
        _mainView.UpdateEvent += _mainView_UpdateEvent;
    }

    private void _mainView_SearchEvent(object? sender, EventArgs e)
    {
        var searchValue = _mainView?.SearchValue;

        if (!string.IsNullOrWhiteSpace(searchValue))
            _bindingSource.DataSource = _repository?.GetList()
                .Where(s =>
                            s.Firstname
                            .ToLower()
                            .Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                            ||
                            s.Lastname
                            .ToLower()
                            .Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                .ToList();
        else
            _bindingSource.DataSource = _repository?.GetList();
    }

    private void _mainView_DeleteEvent(object? sender, EventArgs e)
    {
        var deletedItem = _bindingSource.Current as Student;

        if (deletedItem is null)
            return;

        _repository.Remove(deletedItem);
        _bindingSource.Remove(deletedItem);

        MessageBox.Show("Successful...", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void _mainView_AddEvent(object? sender, EventArgs e)
    {
        var result = _add_update_View.ShowDialog();

        if (result == DialogResult.Cancel)
            return;

        var student = new Student
        {
            Firstname = _add_update_View.FirstName,
            Lastname = _add_update_View.LastName,
            DateOfBirth = _add_update_View.DateOfBirth,
            Score = (float)_add_update_View.Score
        };
        
        _repository.Add(student);
        _bindingSource.Add(student);
        
        MessageBox.Show("Successful...", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void _mainView_UpdateEvent(object? sender, EventArgs E)
    {
        var student = _bindingSource.Current as Student;

        if (student is null)
        {
            MessageBox.Show("An Error Occured...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        _add_update_View.FirstName = student.Firstname;
        _add_update_View.LastName = student.Lastname;
        _add_update_View.DateOfBirth = student.DateOfBirth;
        _add_update_View.Score = (decimal)student.Score;

        if (_add_update_View.ShowDialog() == DialogResult.Cancel)
            return;

        student.Firstname = _add_update_View.FirstName;
        student.Lastname = _add_update_View.LastName;
        student.DateOfBirth = _add_update_View.DateOfBirth;
        student.Score = (float)_add_update_View.Score;

        _repository.Update(student);
        _bindingSource.ResetCurrentItem();

        MessageBox.Show("Successful...", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}