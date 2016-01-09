using System.Windows;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for PromptDialog.xaml
    /// </summary>
    public partial class PromptDialog : Window
    {
        public PromptDialog(string windowTitle, string title, string defaultValue = "")
        {
            InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.Loaded += new RoutedEventHandler(PromptDialog_Loaded);

            this.windowTitle.Text = windowTitle;
            Title = title;
            textBox.Text = defaultValue;
        }

        void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            textBox.Focus();
        }

        public static string Prompt(string windowTitle, string title, string defaultValue = "")
        {
            PromptDialog inst = new PromptDialog(windowTitle, title, defaultValue);
            inst.ShowDialog();
            if (inst.DialogResult == true)
                return inst.ResponseText;
            return null;
        }

        public string ResponseText
        {
            get
            {
                return textBox.Text;
            }
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
