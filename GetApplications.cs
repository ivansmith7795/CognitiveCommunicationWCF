using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using opennlp.tools.namefind;
using opennlp.tools.tokenize;
using opennlp.tools.sentdetect;
using opennlp.tools.postag;
using opennlp.tools.util;

namespace CognitiveCommunicationWCF
{
    public class GetApplications
    {

        public List<string> GetApps(string bodyText)
        {
            SentenceDetectorME sentenceParser = new SentenceDetectorME(LoadNLP.sentenceModel);

            NameFinderME appFinder = new NameFinderME(LoadNLP.appModel);


            TokenizerME tokenizer = new TokenizerME(LoadNLP.tokenModel);

            List<string> results = new List<string>();

            string[] sentences = sentenceParser.sentDetect(bodyText);

            foreach (string sentence in sentences)
            {

                string[] tokens = tokenizer.tokenize(sentence);

                Span[] foundApps = appFinder.find(tokens);

                appFinder.clearAdaptiveData();

                results.AddRange(Span.spansToStrings(foundApps, tokens).AsEnumerable());


            }


            return results;

        }
    }
}