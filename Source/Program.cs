using Source.Presenters;
using Source.Repositories;
using Source.Views;

namespace Source;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        IAddUpdateView addView = new AddUpdateView();
        new AddUpdatePresenter(addView);

        IStudentRepository repository = new EFStudentRepository();
        IMainView mainView = new MainView();
        new MainPresenter(mainView, addView, repository);

        Application.Run((Form)mainView);
    }
}