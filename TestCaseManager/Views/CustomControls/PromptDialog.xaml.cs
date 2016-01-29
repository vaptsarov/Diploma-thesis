﻿using System.Windows;

namespace TestCaseManager.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for PromptDialog.xaml
    /// </summary>
    public partial class PromptDialog : Window
    {
        public PromptDialog(string windowTitle, string title, string defaultValue = "")
        {
            this.InitializeComponent();
            this.Owner = Application.Current.MainWindow;
            this.Loaded += new RoutedEventHandler(PromptDialog_Loaded);

            this.windowTitle.Text = windowTitle;
            Title = title;
            this.textBox.Text = defaultValue;
        }

        void PromptDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.textBox.Focus();
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
                return this.textBox.Text;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
