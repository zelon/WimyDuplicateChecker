using System.Windows;

namespace WimyDuplicateChecker
{
    /// <summary>
    /// Interaction logic for DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private readonly string _filename1;
        private readonly string _filename2;

        public DetailWindow(string filename1, string filename2)
        {
            _filename1 = filename1;
            _filename2 = filename2;

            InitializeComponent();

            _leftDetail.SetFilename(_filename1);
            _rightDetail.SetFilename(_filename2);
        }
    }
}
