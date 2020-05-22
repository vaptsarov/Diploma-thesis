namespace TestCaseManager.Views.CustomControls.MicrosoftAutoComplete
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Models;

    public partial class AutoCompleteTextBox : Canvas
    {
        private readonly ObservableCollection<AutoCompleteModel> _autoCompletionList;
        private readonly ComboBox _comboBox;
        private readonly VisualCollection _controls;
        private readonly Timer _keypressTimer;
        private readonly TextBox _textBox;
        private bool _insertText;

        public AutoCompleteTextBox()
        {
            _controls = new VisualCollection(this);
            InitializeComponent();

            _autoCompletionList = new ObservableCollection<AutoCompleteModel>();
            Threshold = 3; // default threshold to 3 char

            // set up the key press timer
            _keypressTimer = new Timer();
            _keypressTimer.Elapsed += OnTimedEvent;

            // set up the text box and the combo box
            _comboBox = new ComboBox
            {
                IsSynchronizedWithCurrentItem = true,
                IsTabStop = false
            };
            _comboBox.SelectionChanged += comboBox_SelectionChanged;

            _textBox = new TextBox();
            _textBox.TextChanged += textBox_TextChanged;
            _textBox.VerticalContentAlignment = VerticalAlignment.Center;

            _controls.Add(_comboBox);
            _controls.Add(_textBox);
        }

        public string Text
        {
            get => _textBox.Text;
            set
            {
                _insertText = true;
                _textBox.Text = value;
            }
        }

        public int DelayTime { get; set; }

        public int Threshold { get; set; }

        protected override int VisualChildrenCount => _controls.Count;

        public void AddItem(AutoCompleteModel entry)
        {
            _autoCompletionList.Add(entry);
        }

        public void RemoveItem(AutoCompleteModel entry)
        {
            _autoCompletionList.Remove(entry);
        }

        public void RemoveItem(string key)
        {
            var autoCompleteItem = _autoCompletionList.Where(x => x.DisplayName.Equals(key)).FirstOrDefault();
            if (_autoCompletionList != null) _autoCompletionList.Remove(autoCompleteItem);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != _comboBox.SelectedItem)
            {
                _insertText = true;
                var cbItem = (ComboBoxItem) _comboBox.SelectedItem;
                _textBox.Text = cbItem.Content.ToString();
            }
        }

        private void TextChanged()
        {
            try
            {
                _comboBox.Items.Clear();
                if (_textBox.Text.Length >= Threshold)
                {
                    foreach (var entry in _autoCompletionList)
                    foreach (var word in entry.KeywordStrings)
                        if (word.StartsWith(_textBox.Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            var cbItem = new ComboBoxItem
                            {
                                Content = entry.ToString()
                            };
                            _comboBox.Items.Add(cbItem);
                            break;
                        }

                    _comboBox.IsDropDownOpen = _comboBox.HasItems;
                }
                else
                {
                    _comboBox.IsDropDownOpen = false;
                }
            }
            catch
            {
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _keypressTimer.Stop();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                new TextChangedCallback(TextChanged));
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // text was not typed, do nothing and consume the flag
            if (_insertText)
            {
                _insertText = false;
            }

            // if the delay time is set, delay handling of text changed
            else
            {
                if (DelayTime > 0)
                {
                    _keypressTimer.Interval = DelayTime;
                    _keypressTimer.Start();
                }
                else
                {
                    TextChanged();
                }
            }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            _textBox.Arrange(new Rect(arrangeSize));
            _comboBox.Arrange(new Rect(arrangeSize));
            return base.ArrangeOverride(arrangeSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _controls[index];
        }

        private delegate void TextChangedCallback();
    }
}