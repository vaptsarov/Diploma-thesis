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
            get
            {
                if (_keywordStrings == null) _keywordStrings = new[] {DisplayName};
                return _keywordStrings;
            }
        }

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}