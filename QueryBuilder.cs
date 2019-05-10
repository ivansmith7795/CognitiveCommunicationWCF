using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Search;
using System.IO;
using System.Diagnostics;

namespace CognitiveCommunicationWCF
{
    public class QueryBuilder
    {

            public BooleanQuery GetCommQuery(string changecommBody, string changecommGroup, string changecommApps, string changecommCI)
            {

                Lucene.Net.Analysis.Analyzer commsAnalyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

                Lucene.Net.Search.BooleanQuery.MaxClauseCount = 25000;


                TextReader textReadCommBody = new StringReader(changecommBody);
                TextReader textReadCommGroup = new StringReader(changecommGroup);
                TextReader textReadCommApps = new StringReader(changecommApps);
                TextReader textReadCommCI = new StringReader(changecommCI);


                Lucene.Net.Analysis.TokenStream tokenizedCommBody = commsAnalyzer.TokenStream(changecommBody, textReadCommBody);
                Lucene.Net.Analysis.TokenStream tokenizedCommGroup = commsAnalyzer.TokenStream(changecommGroup, textReadCommGroup);
                Lucene.Net.Analysis.TokenStream tokenizedCommApps = commsAnalyzer.TokenStream(changecommApps, textReadCommApps);
                Lucene.Net.Analysis.TokenStream tokenizedCommCI = commsAnalyzer.TokenStream(changecommCI, textReadCommCI);


                Lucene.Net.Search.BooleanQuery query1 = new Lucene.Net.Search.BooleanQuery();
                try
                {
                    int tokenCount = 0;
                    tokenizedCommBody.Reset();

                    var termAttrText = tokenizedCommBody.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();

                    while (tokenizedCommBody.IncrementToken())
                    {
                        tokenCount++;

                        string Term = termAttrText.Term;

                        query1.Add(new Lucene.Net.Search.TermQuery(new Term("change_description", Term)), Lucene.Net.Search.Occur.SHOULD);


                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                Lucene.Net.Search.BooleanQuery query2 = new Lucene.Net.Search.BooleanQuery();

                try
                {
                    tokenizedCommGroup.Reset();

                    var termAttrTicker = tokenizedCommGroup.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();

                    int tokenCount = 0;

                    while (tokenizedCommGroup.IncrementToken())
                    {
                        tokenCount++;

                        string Term = termAttrTicker.Term;

                        query2.Add(new Lucene.Net.Search.TermQuery(new Term("change_group", Term)), Lucene.Net.Search.Occur.SHOULD);

                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                Lucene.Net.Search.BooleanQuery query3 = new Lucene.Net.Search.BooleanQuery();

                try
                {
                    tokenizedCommApps.Reset();

                    var termAttrTicker = tokenizedCommApps.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();

                    int tokenCount = 0;

                    while (tokenizedCommApps.IncrementToken())
                    {
                        tokenCount++;

                        string Term = termAttrTicker.Term;

                        query3.Add(new Lucene.Net.Search.TermQuery(new Term("application", Term)), Lucene.Net.Search.Occur.SHOULD);

                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }


                Lucene.Net.Search.BooleanQuery query4 = new Lucene.Net.Search.BooleanQuery();

                try
                {
                    tokenizedCommCI.Reset();

                    var termAttrTicker = tokenizedCommCI.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();

                    int tokenCount = 0;

                    while (tokenizedCommCI.IncrementToken())
                    {
                        tokenCount++;

                        string Term = termAttrTicker.Term;

                        query4.Add(new Lucene.Net.Search.TermQuery(new Term("change_CI", Term)), Lucene.Net.Search.Occur.SHOULD);

                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                Lucene.Net.Search.BooleanQuery comQuery = new Lucene.Net.Search.BooleanQuery();


                query4.Boost = 5;
                query3.MinimumNumberShouldMatch = 1;

                comQuery.Add(query1, Lucene.Net.Search.Occur.SHOULD);
                comQuery.Add(query2, Lucene.Net.Search.Occur.SHOULD);
                comQuery.Add(query3, Lucene.Net.Search.Occur.SHOULD);
                comQuery.Add(query4, Lucene.Net.Search.Occur.SHOULD);

                return comQuery;

            }

            public BooleanQuery GetCommQuery_NoApps(string changecommBody, string changecommGroup, string changecommCI)
            {

                Lucene.Net.Analysis.Analyzer commsAnalyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

                Lucene.Net.Search.BooleanQuery.MaxClauseCount = 25000;


                TextReader textReadCommBody = new StringReader(changecommBody);
                TextReader textReadCommGroup = new StringReader(changecommGroup);
             
                TextReader textReadCommCI = new StringReader(changecommCI);

                Lucene.Net.Analysis.TokenStream tokenizedCommBody = commsAnalyzer.TokenStream(changecommBody, textReadCommBody);
                Lucene.Net.Analysis.TokenStream tokenizedCommGroup = commsAnalyzer.TokenStream(changecommGroup, textReadCommGroup);
                Lucene.Net.Analysis.TokenStream tokenizedCommCI = commsAnalyzer.TokenStream(changecommCI, textReadCommCI);
         

                Lucene.Net.Search.BooleanQuery query1 = new Lucene.Net.Search.BooleanQuery();
                try
                {
                    int tokenCount = 0;
                    tokenizedCommBody.Reset();

                    var termAttrText = tokenizedCommBody.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();

                    while (tokenizedCommBody.IncrementToken())
                    {
                        tokenCount++;

                        string Term = termAttrText.Term;

                        query1.Add(new Lucene.Net.Search.TermQuery(new Term("change_description", Term)), Lucene.Net.Search.Occur.SHOULD);


                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                Lucene.Net.Search.BooleanQuery query2 = new Lucene.Net.Search.BooleanQuery();

                try
                {
                    tokenizedCommGroup.Reset();

                    var termAttrTicker = tokenizedCommGroup.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();

                    int tokenCount = 0;

                    while (tokenizedCommGroup.IncrementToken())
                    {
                        tokenCount++;

                        string Term = termAttrTicker.Term;

                        query2.Add(new Lucene.Net.Search.TermQuery(new Term("change_group", Term)), Lucene.Net.Search.Occur.SHOULD);

                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                Lucene.Net.Search.BooleanQuery query3 = new Lucene.Net.Search.BooleanQuery();

                try
                {
                    tokenizedCommCI.Reset();

                    var termAttrTicker = tokenizedCommCI.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();

                    int tokenCount = 0;

                    while (tokenizedCommCI.IncrementToken())
                    {
                        tokenCount++;

                        string Term = termAttrTicker.Term;

                        query3.Add(new Lucene.Net.Search.TermQuery(new Term("change_CI", Term)), Lucene.Net.Search.Occur.SHOULD);

                    }


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

              
                Lucene.Net.Search.BooleanQuery comQuery = new Lucene.Net.Search.BooleanQuery();


                    query3.Boost = 8;

                    comQuery.Add(query1, Lucene.Net.Search.Occur.SHOULD);
                    comQuery.Add(query2, Lucene.Net.Search.Occur.SHOULD);
                    comQuery.Add(query3, Lucene.Net.Search.Occur.SHOULD);
                    

                    return comQuery;

                }


            }
    
}