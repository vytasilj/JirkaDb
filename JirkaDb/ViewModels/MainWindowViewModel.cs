using Caliburn.Micro;

namespace JirkaDb.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        private readonly GridViewModel m_gridModel;


        public MainWindowViewModel()
        {
            m_gridModel = new GridViewModel();
        }


        public GridViewModel GridModel => m_gridModel;
    }
}