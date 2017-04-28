using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Highlight;

namespace NopStarterKit.Web.Search
{
    public class LuceneHighlightHelper
    {
        private readonly Lucene.Net.Util.Version _luceneVersion = Lucene.Net.Util.Version.LUCENE_29;

        protected Dictionary<string, QueryParser> QueryParsers = new Dictionary<string, QueryParser>();

        public string Separator { get; set; }
        public int MaxNumHighlights { get; set; }
        public Formatter HighlightFormatter { get; set; }
        public Analyzer HighlightAnalyzer { get; set; }

        private static readonly LuceneHighlightHelper instance = new LuceneHighlightHelper();

        public static LuceneHighlightHelper Instance
        {
            get { return instance; }
        }

        private LuceneHighlightHelper()
        {
            Separator = "...";
            MaxNumHighlights = 5;
            HighlightAnalyzer = new StandardAnalyzer(_luceneVersion);
            HighlightFormatter = new SimpleHTMLFormatter("<em>", "</em>&nbsp;");
        }


        public string GetHighlight(string value, string highlightField, IndexSearcher searcher, string luceneRawQuery)
        {
            var query = GetQueryParser(highlightField).Parse(luceneRawQuery);
            var scorer = new QueryScorer(query.Rewrite(searcher.GetIndexReader()));

            var highlighter = new Highlighter(HighlightFormatter, scorer);

            var tokenStream = HighlightAnalyzer.TokenStream(highlightField, new StringReader(value));
            return highlighter.GetBestFragments(tokenStream, value, MaxNumHighlights, Separator);
        }

        public string GetHighlight(string value, IndexSearcher searcher, string highlightField, Query luceneQuery)
        {
            var scorer = new QueryScorer(luceneQuery.Rewrite(searcher.GetIndexReader()));
            var highlighter = new Highlighter(HighlightFormatter, scorer);

            var tokenStream = HighlightAnalyzer.TokenStream(highlightField, new StringReader(value));
            return highlighter.GetBestFragments(tokenStream, value, MaxNumHighlights, Separator);
        }

        protected QueryParser GetQueryParser(string highlightField)
        {
            if (!QueryParsers.ContainsKey(highlightField))
            {
                QueryParsers[highlightField] = new QueryParser(_luceneVersion, highlightField, HighlightAnalyzer);
            }
            return QueryParsers[highlightField];
        }

        //public string GetHighlight(string fieldValue, string fieldName, Searcher luceneSearcher, string searchTerm)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}