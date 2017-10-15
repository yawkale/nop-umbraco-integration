using Lucene.Net.Analysis;
using NopStarterKit.Web.Search.Tokenizers;

namespace NopStarterKit.Web.Search.Analyzers
{
    public class ListMultiValueAnalyzer :Analyzer
    {
        public override TokenStream TokenStream(string fieldName, System.IO.TextReader reader)
        {
            TokenStream result = new ListMultiValueCharTokenizer(reader);

            //add in filters
            // first normalize the StandardTokenizer
            //result = new StandardFilter(result);
            
            // makes sure everything is lower case
            result = new LowerCaseFilter(result);

            // use the default list of Stop Words, provided by the StopAnalyzer class.
            //result = new StopFilter(result,new string[]{"קבוצת"});

            // injects the synonyms. 
            //result = new SynonymFilter(result, SynonymEngine);

            //return the built token stream.
            return result;
        }
    }
}