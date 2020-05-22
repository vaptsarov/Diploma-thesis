namespace TestCaseManager.Views.CustomControls
{
    using System.Windows;

    /// <summary>
    ///     Interaction logic for PromptDialog.xaml
    /// </summary>
    public partial class PromptDialog : Window
    {
        public PromptDialog(string windowTitle, string title, string defaultValue = "")
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            Loaded += PromptDialog_Loaded;

            this.windowTitle.Text = windowTitle;
            Title = title;
            textBox.Text = defaultValue;
        }

        public string ResponseText => textBox.Text;

        private void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
        }

        public static string Prompt(string windowTitle, string title, string defaultValue = "")
        {
            var inst = new PromptDialog(windowTitle, title, defaultValue);
            inst.ShowDialog();
            if (inst.DialogResult == true)
                return inst.ResponseText;

            return null;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}