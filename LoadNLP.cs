using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using opennlp.tools.namefind;
using opennlp.tools.tokenize;
using opennlp.tools.sentdetect;
using opennlp.tools.postag;
using java.io;

namespace CognitiveCommunicationWCF
{
    public static class LoadNLP
    {
        static string sentenceModelPath = @"C:\Models\en-sent.bin";
        static string tokenModelPath = @"C:\Models\en-token.bin";
        static string posTaggerModelPath = @"C:\Models\en-pos-maxent.bin";
        static string nameFinderModelPath = @"C:\Models\es-ner-misc.bin";
        static string appFinderModelPath = @"C:\Models\en-app-body-names.bin";

        public static FileInputStream tokenModeInputStream = new FileInputStream(tokenModelPath);
        public static TokenizerModel tokenModel = new TokenizerModel(tokenModeInputStream);

        public static FileInputStream sentenceModeInputStream = new FileInputStream(sentenceModelPath);
        public static SentenceModel sentenceModel = new SentenceModel(sentenceModeInputStream);

        public static FileInputStream nameModelInputStream = new FileInputStream(nameFinderModelPath);
        public static TokenNameFinderModel nameModel = new TokenNameFinderModel(nameModelInputStream);

        public static FileInputStream appModelInputStream = new FileInputStream(appFinderModelPath);
        public static TokenNameFinderModel appModel = new TokenNameFinderModel(appModelInputStream);

        public static FileInputStream posTaggerModelInputStream = new FileInputStream(posTaggerModelPath);
        public static POSModel posTaggerModel = new POSModel(posTaggerModelInputStream);

    }
}