using System.Windows.Controls;

namespace WimyDuplicateChecker.Windows
{
    

    public partial class DetailUserControl : UserControl
    {
        public DetailUserControl()
        {
            InitializeComponent();
        }

        public void SetFilename (string filename)
        {
            DetailUserControlViewModel viewModel = (DetailUserControlViewModel)DataContext;
            viewModel.SetFilename(filename);
        }
    }
}
