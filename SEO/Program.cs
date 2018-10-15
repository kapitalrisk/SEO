using SEO.Business;
using SEO.Configuration;
using SEO.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter ngram size wanted : ");
            string value = Console.ReadLine();
            Console.Write(Environment.NewLine);
            int n = 0;

            while (!int.TryParse(value, out n))
            {
                Console.Write("Error bad integer value try again : ");
                value = Console.ReadLine();
                Console.Write(Environment.NewLine);
            }

            try
            {
                // Get texts content
                IEnumerable<string> texts = FileManager.GetFilesContent(Defaults.dataFilesPath);

                // Get ngrams for all texts
                List<string> nGramsResult = new List<string>();
                Ngrams ng = new Ngrams();
                var options = new ParallelOptions
                {
                    MaxDegreeOfParallelism = 20
                };

                Parallel.ForEach(texts, options, text =>
                    {
                        var temp = ng.GetNgramsWords(text, n);
                        nGramsResult.AddRange(temp);
                    }
                );
                // Save Ngrams to a file
                FileManager.SaveFile(Defaults.resultNGramFilePath, nGramsResult);

                // Sort ngrams (and delete duplicate in the same time) with tf idf
                var tfIdfResult = TfIdf.SortNGrams(nGramsResult);

                // Save sorted tf idf to a file
                FileManager.SaveFile(Defaults.resultTfIdfFilePath, tfIdfResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}
