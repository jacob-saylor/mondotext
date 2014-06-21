using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MondoText
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            //Defaults 
            IsVertical = true;
            RemoveBlankEntries = true;
            NoTextQualifiers = true;
            NoCaseChange = true;
            NoHTMLEncoding = true;
        }

        #region Properties
                
        private string _Input;
        public string Input
        {
            get
            {
                return _Input;
            }
            set
            {
                _Input = value;
                OnPropertyChanged("Input");
            }
        }
        
        private string _Output;
        public string Output
        {
            get
            {
                return _Output;
            }
            set
            {
                _Output = value;
                OnPropertyChanged("Output");
            }
        }
        
        private bool _RemoveDuplicates;
        public bool RemoveDuplicates
        {
            get
            {
                return _RemoveDuplicates;
            }
            set
            {
                _RemoveDuplicates = value;
                OnPropertyChanged("RemoveDuplicates");
            }
        }
        
        private bool _IsVertical;
        public bool IsVertical
        {
            get
            {
                return _IsVertical;
            }
            set
            {
                _IsVertical = value;
                OnPropertyChanged("IsVertical");
            }
        }
        
        private bool _IsHorizontal;
        public bool IsHorizontal
        {
            get
            {
                return _IsHorizontal;
            }
            set
            {
                _IsHorizontal = value;
                OnPropertyChanged("IsHorizontal");
            }
        }
        
        private bool _IsComma;
        public bool IsComma
        {
            get
            {
                return _IsComma;
            }
            set
            {
                _IsComma = value;
                OnPropertyChanged("IsComma");
            }
        }
        
        private bool _IsPipe;
        public bool IsPipe
        {
            get
            {
                return _IsPipe;
            }
            set
            {
                _IsPipe = value;
                OnPropertyChanged("IsPipe");
            }
        }


        private bool _QuoteEntries;
        public bool QuoteEntries
        {
            get
            {
                return _QuoteEntries;
            }
            set
            {
                _QuoteEntries = value;
                OnPropertyChanged("QuoteEntries");
            }
        }
        
        private bool _TickEntries;
        public bool TickEntries
        {
            get
            {
                return _TickEntries;
            }
            set
            {
                _TickEntries = value;
                OnPropertyChanged("TickEntries");
            }
        }
        
        private bool _RemoveBlankEntries;
        public bool RemoveBlankEntries
        {
            get
            {
                return _RemoveBlankEntries;
            }
            set
            {
                _RemoveBlankEntries = value;
                OnPropertyChanged("RemoveBlankEntries");
            }
        }
        
        private bool _NoTextQualifiers;
        public bool NoTextQualifiers
        {
            get
            {
                return _NoTextQualifiers;
            }
            set
            {
                _NoTextQualifiers = value;
                OnPropertyChanged("NoTextQualifiers");
            }
        }
        
        private string _ElementCount;
        public string ElementCount
        {
            get
            {
                return _ElementCount;
            }
            set
            {
                _ElementCount = value;
                OnPropertyChanged("ElementCount");
            }
        }
        
        private bool _NoCaseChange;
        public bool NoCaseChange
        {
            get
            {
                return _NoCaseChange;
            }
            set
            {
                _NoCaseChange = value;
                OnPropertyChanged("NoCaseChange");
            }
        }
        
        private bool _IsLower;
        public bool IsLower
        {
            get
            {
                return _IsLower;
            }
            set
            {
                _IsLower = value;
                OnPropertyChanged("IsLower");
            }
        }
        
        private bool _IsUpper;
        public bool IsUpper
        {
            get
            {
                return _IsUpper;
            }
            set
            {
                _IsUpper = value;
                OnPropertyChanged("IsUpper");
            }
        }
        
        private bool _ReplaceSingleQuotesWithDouble;
        public bool ReplaceSingleQuotesWithDouble
        {
            get
            {
                return _ReplaceSingleQuotesWithDouble;
            }
            set
            {
                _ReplaceSingleQuotesWithDouble = value;
                OnPropertyChanged("ReplaceSingleQuotesWithDouble");
            }
        }
        
        private bool _EscapeQuotes;
        public bool EscapeQuotes
        {
            get
            {
                return _EscapeQuotes;
            }
            set
            {
                _EscapeQuotes = value;
                OnPropertyChanged("EscapeQuotes");
            }
        }
        
        private bool _EscapeTicks;
        public bool EscapeTicks
        {
            get
            {
                return _EscapeTicks;
            }
            set
            {
                _EscapeTicks = value;
                OnPropertyChanged("EscapeTicks");
            }
        }
        
        private bool _HTMLEncode;
        public bool HTMLEncode
        {
            get
            {
                return _HTMLEncode;
            }
            set
            {
                _HTMLEncode = value;
                OnPropertyChanged("HTMLEncode");
            }
        }
        
        private bool _HTMLDecode;
        public bool HTMLDecode
        {
            get
            {
                return _HTMLDecode;
            }
            set
            {
                _HTMLDecode = value;
                OnPropertyChanged("HTMLDecode");
            }
        }
        
        private bool _NoHTMLEncoding;
        public bool NoHTMLEncoding
        {
            get
            {
                return _NoHTMLEncoding;
            }
            set
            {
                _NoHTMLEncoding = value;
                OnPropertyChanged("NoHTMLEncoding");
            }
        }
        
        private string _Find;
        public string Find
        {
            get
            {
                return _Find;
            }
            set
            {
                _Find = value;
                OnPropertyChanged("Find");
            }
        }
        
        private string _Replace;
        public string Replace
        {
            get
            {
                return _Replace;
            }
            set
            {
                _Replace = value;
                OnPropertyChanged("Replace");
            }
        }	
      

        #endregion

        #region Methods and Events

        private void btnTransform_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                processInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }

        private void txtOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtOutput.SelectAll();
            Clipboard.SetText(txtOutput.Text);
        }

        private void processInput()
        {   
            // Check to see if we need to replace the quotes or ticks
            string tempInput = Input;
            tempInput = ReplaceSingleQuotesWithDouble ? tempInput.Replace("\"", "\"\"") : tempInput;
            tempInput = EscapeQuotes ? tempInput.Replace("\"", "\\\"") : tempInput;
            tempInput = EscapeTicks ? tempInput.Replace("'", "\\\'") : tempInput;

            // Handle the HTML Encoding and Decoding
            tempInput = HTMLEncode ? System.Net.WebUtility.HtmlEncode(Input) : tempInput;
            tempInput = HTMLDecode ? System.Net.WebUtility.HtmlDecode(Input) : tempInput;

            // Find & Replace
            if(Find != null && !string.IsNullOrWhiteSpace(Find.Trim()))
            {
                Replace = Replace ?? string.Empty;
                tempInput = Regex.Replace(tempInput, Find, Replace, RegexOptions.IgnoreCase);
            }

            // Proceed with the rest of the options
            List<string> splitInput = tempInput.Split(new string[] { "\r\n", "\n", " ", ",","\t" }, StringSplitOptions.None).ToList();
            string tempOutput = string.Empty;
            string textQualifier = null;
            
            splitInput = RemoveDuplicates ? splitInput.Distinct().ToList() : splitInput;                       
            splitInput = RemoveBlankEntries ? splitInput.Where(s => !string.IsNullOrWhiteSpace(s)).ToList() : splitInput;

            
            if (QuoteEntries)
            {
                textQualifier = "\"";
            }
            else if (TickEntries)
            {
                textQualifier = "'";
            }
            else if (NoTextQualifiers)
            {
                textQualifier = null;
            }

            // Formatting the output (Last Step)
            
            if (IsHorizontal)
            {                
                tempOutput = formatOutput(splitInput, textQualifier, " ");
            }
            else if (IsVertical)
            {                
                tempOutput = formatOutput(splitInput, textQualifier, Environment.NewLine);
            }
            else if (IsComma)
            {                
                tempOutput = formatOutput(splitInput, textQualifier, ",");
            }
            else if (IsPipe)
            {                
                tempOutput = formatOutput(splitInput, textQualifier, "|");
            }

            Output = tempOutput;
            ElementCount = "Element Count : " + splitInput.Count();
        }

        private string formatOutput(List<string> input, string textQualifier, string Delimiter)
        {
            StringBuilder sb = new StringBuilder();
            int itemCount = 0;

            foreach (var item in input)
            {                
                itemCount++; 
                sb.Append(textQualifier != null ? textQualifier : null);
                                
                if (IsUpper)
                    sb.Append(item.ToUpper());                
                else if (IsLower)
                    sb.Append(item.ToLower());                
                else
                    sb.Append(item);                

                sb.Append(textQualifier != null ? textQualifier : null);
                if (itemCount < input.Count)
                {
                    sb.Append(Delimiter);
                }
            }

            return sb.ToString();
        }

        #endregion 

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        

               

    }
}
