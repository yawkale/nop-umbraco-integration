using System.IO;
using Lucene.Net.Analysis;

namespace NopStarterKit.Web.Search.Tokenizers
{
    public class ListMultiValueCharTokenizer : CharTokenizer
    {
        
        public ListMultiValueCharTokenizer(TextReader input):base(input)
        {

        }
        
        protected override bool IsTokenChar(char c)
        {
            var t= !c.Equals(',') && !c.Equals(' ');

            return t;
        }
    }
}