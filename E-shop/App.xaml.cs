using SQLite;
using Xamarin.Forms;

namespace Eshop
{
    public partial class App : Application
    {
        public static Person person;

        public App()
        {
            InitializeComponent();

            MainPage = new Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static Conection _database;

        /// <summary>
        /// Good approach is return instance of database access layer instead of db path
        /// </summary>
        public static Conection Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new Conection(DbPath);
                }

                return _database;
            }
        }

        /// <summary>
        /// Used only for Abstract and SQLiteExtensions database access
        /// Path should be private
        /// </summary>
        public static string DbPath
        {
            get
            {
                IFileHelper filehelperInstance = DependencyService.Get<IFileHelper>();
                return filehelperInstance.GetLocalFilePath("Obednavky.db3");
            }
        }
    }
}
