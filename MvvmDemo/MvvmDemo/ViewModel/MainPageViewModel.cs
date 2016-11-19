using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDemo.Service;


namespace MvvmDemo.ViewModel
{

    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
                CheckCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand CheckCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            CheckCommand=
                new RelayCommand(CheckCommandExecute,CanCheckCommandExecute);
        }

        private bool CanCheckCommandExecute()
        {
           return Title != "dupa";
        }

        private void CheckCommandExecute()
        {
            _navigationService.Go<NextPageViewModel>();
        }
    }
}