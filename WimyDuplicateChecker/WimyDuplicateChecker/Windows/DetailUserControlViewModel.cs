
namespace WimyDuplicateChecker.Windows
{
    class DetailUserControlViewModel : NotifyBase
    {
        public string Filename { get; private set; }

        public DetailUserControlViewModel()
        {

        }

        public void SetFilename(string filename)
        {
            Filename = filename;

            NotifyPropertyChanged("Filename");
        }
    }
}
