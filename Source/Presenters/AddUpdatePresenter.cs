using Source.Views;
using System.Text;

namespace Source.Presenters;

public class AddUpdatePresenter
{
    private readonly IAddUpdateView _add_update_View;
    public AddUpdatePresenter(IAddUpdateView _Add_Update_View)
    {
        _add_update_View = _Add_Update_View;

        _add_update_View.SaveEvent += _addView_SaveEvent;
        _add_update_View.CancelEvent += _addView_CancelEvent;
    }

    private void _addView_SaveEvent(object? sender, EventArgs e)
    {
        StringBuilder sb = new();

        if (_add_update_View.FirstName.Length < 3)
            sb.Append($"{nameof(_add_update_View.FirstName)} is wrong\n");

        if (_add_update_View.LastName.Length < 3)
            sb.Append($"{nameof(_add_update_View.LastName)} is wrong\n");

        if (DateTime.Now.Year - _add_update_View.DateOfBirth.Year < 18)
            sb.Append($"{nameof(_add_update_View.DateOfBirth)} is wrong\n");

        if (sb.Length > 0)
        {
            MessageBox.Show(sb.ToString(), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _add_update_View.DialogResult = DialogResult.OK;
    }

    private void _addView_CancelEvent(object? sender, EventArgs e)
        => _add_update_View.DialogResult = DialogResult.Cancel;
}