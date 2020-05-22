namespace TestCaseManager.Models
{
    public class AutoCompleteModel
    {
        private string[] _keywordStrings;

        public AutoCompleteModel(string name, params string[] keywords)
        {
            DisplayName = name;
            _keywordStrings = keywords;
        }

        public string[] KeywordStrings
        {
            get { return _keywordStrings ?? (_keywordStrings = new[] {DisplayName}); }
        }

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}