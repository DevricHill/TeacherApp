using TeacherApp.ViewModels;

namespace TeacherApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(StudentViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
            
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (StudentViewModel)BindingContext;
            System.Diagnostics.Debug.WriteLine($"PAGE VM INSTANCE: {vm.GetHashCode()}");
            await vm.GetStudentsAysncCommand.ExecuteAsync(null);
        }
    }
}
