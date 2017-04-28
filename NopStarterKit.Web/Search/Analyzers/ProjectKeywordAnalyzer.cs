using Lucene.Net.Analysis;

namespace NopStarterKit.Web.Search.Analyzers
{
    public class ProjectKeywordAnalyzer : KeywordAnalyzer
    {
        public override TokenStream TokenStream(string fieldName, System.IO.TextReader reader)
        {
            TokenStream result = new KeywordTokenizer(reader);

            //add in filters
            // first normalize the StandardTokenizer
            //result = new KeyWo(result);

            // makes sure everything is lower case
            result = new LowerCaseFilter(result);

            // use the default list of Stop Words, provided by the StopAnalyzer class.
            //result = new StopFilter(result, StopAnalyzer.ENGLISH_STOP_WORDS);

            // injects the synonyms. 
            //result = new SynonymFilter(result, SynonymEngine);

            //return the built token stream.
            return result;
        }
    }
}