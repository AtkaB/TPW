using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel
    {
        private ModelApi Api { get; set; }
        public ObservableCollection<BallModel> Balls { get; }
        public int NumberOfBalls { get; set; }
        public ICommand AddCommand { get; set; }

        public MainViewModel()
        {
            Api = ModelApi.Instance();
            Balls = Api.Balls;
            AddCommand = new RelayCommand(AddBalls);
        }

        public void AddBalls() 
        {
            Api.AddBalls(NumberOfBalls);
        }
    }
}