using System;
using System.Collections.ObjectModel;

namespace StarrOnline.Utilities
{
    public class Response
    {
        public String FullText { get; set; }
        public int NumberOfLines { get; set; }
        private ObservableCollection<ResponsePerLine> texts = new ObservableCollection<ResponsePerLine>();

        public ObservableCollection<ResponsePerLine> Texts
        {
            get { return texts; }
            set { texts = value; }
        }
    }

    public class ResponsePerLine
    {
        public int LineNumber { get; set; }
        public String Text { get; set; }
    }
}
