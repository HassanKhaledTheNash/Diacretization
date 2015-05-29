using edu.stanford.nlp.tagger.maxent;
using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Diacretization
{
    class AppLogic
    {
        static public string Diacritize(string input)
        {
            var tagger = new MaxentTagger(@"C:\Users\hassank\Documents\Visual Studio 2013\Projects\AnerV1\arabic.tagger");
            var sentences = MaxentTagger.tokenizeText(new java.io.StringReader(input)).toArray();
            string result = "";
            foreach (ArrayList s in sentences)
            {
                var taggedSenttence = tagger.tagSentence(s);
                result += taggedSenttence.ToString();
            }
            result = result.Trim('[').Trim(']');
            string outPut = "";
            string[] words = result.Split(',');

            if (words.Length <= 1)
            {
                MessageBox.Show("to short input please enter longer than one input", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                for (int i = 0; i < words.Length; i++)
                {
                    string tempWord = words[i].Substring(0, words[i].IndexOf("/"));
                    string tempTag = words[i].Substring(words[i].IndexOf("/") + 1);

                    if (tempTag == "DTNNS")
                    {
                        outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                        string tempTag1 = words[i + 1].Substring(words[i + 1].IndexOf("/") + 1);
                        if (tempTag1 == "JJ")
                        {
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/')) + " ";

                            i++;
                        }
                    }

                    else if (tempTag == "NN" || tempTag == "DTNN")
                    {
                        string tempTag1 = words[i + 1].Substring(words[i + 1].IndexOf("/") + 1);
                        if (tempTag == "DTNN" && tempTag1 == "DTJJ")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            i += 1;
                        }
                        else if (tempTag1 == "DTJJ" || tempTag1 == "DT" || tempTag1 == "DTNN" || tempTag1 == "NNP" || tempTag1 == "NNS" || tempTag1 == "DTNNP" || tempTag1 == "DTNNS")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ِ') + " ";
                            i += 1;
                        }

                    }
                    else if (tempTag == "VB")
                    {
                        if (tempWord == "اضحي" || tempWord == "امسي")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                    }
                    else if (tempTag == "VBD" && tempWord.Contains("ت") && (words[i + 1].Contains("DTNN") || words[i + 1].Contains("NN")))
                    {
                        outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace("/", "ُ") + " ";
                        outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace("/", "َ") + " ";
                        i += 2;
                    }
                    else if (words[i + 2].Contains("JJ") && tempTag == "VBD" && words[i + 1].Contains("PRP"))
                    {
                        outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace("/", "ُ") + words[i + 1].Substring(0, words[i + 1].IndexOf('/'));
                        outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                        i += 2;
                    }

                    else if (tempTag == "RP")
                    {
                        if (tempWord == "انما" || tempWord == "كانما")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            i += 2;
                        }
                    }
                    else if (tempTag == "VBD")
                    {
                        if (tempWord == "كان")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "ليس")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }

                        else if (tempWord == "بات")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "برح")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "انفك")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "كن")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "اصبح")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "ظل")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "زال")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "فتئ")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "صار")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'َ') + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'َ') + " ";
                            i += 2;
                        }
                    }
                    else if (tempTag == "VBP")
                    {

                        if (tempWord == "ان")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ّ') + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'َ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يبات")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يظل")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "ينفك")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يزال")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يفتا")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يصير")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يبرح")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "لكن")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'َ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            i += 2;
                        }
                        else if (tempWord == "ليت")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'َ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            i += 2;
                        }
                        else if (tempWord == "لعل")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ّ') + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'َ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يكون")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يصبح")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يضحي")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else if (tempWord == "يمسي")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'ً') + " ";
                            i += 2;
                        }
                        else
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 2].Substring(0, words[i + 2].IndexOf('/') + 1).Replace('/', 'َ') + " ";
                            i += 2;
                        }
                    }

                    else if (tempTag == "IN" || tempTag == "PRP")
                    {
                        if (tempWord == "من")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ْ');
                        }
                        else if (tempWord == "عن")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ْ');

                        }
                        else if (tempWord == "ك")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ُ');

                        }
                        else if (tempWord == "ب")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ِ');

                        }
                        else if (tempWord == "ل")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ِ');

                        }
                        else if (tempWord == "منذ")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ُ');

                        }
                        else
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', ' ');
                        }
                        outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ِ') + " ";
                        i += 1;
                    }
                    else if (tempTag == "CC")
                    {
                        if (tempWord == "و")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'َ');
                        }
                        else if (tempWord == "ف")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'َ');
                        }
                        else if (tempWord == "ثم")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ّ');

                        }
                        else if (tempWord == "او")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ْ');

                        }
                        else if (tempWord == "ام")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ْ');

                        }
                        else if (tempWord == "بل")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ْ');

                        }

                        else
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/'));

                        }

                    }
                    else if (tempTag == "DTNN" || tempTag == "NN")
                    {
                        tempWord = words[i + 1].Substring(0, words[i].IndexOf("/"));
                        tempTag = words[i + 1].Substring(words[i + 1].IndexOf("/") + 1);
                        if (tempTag == "NNP" || tempTag == "DTNN" || tempTag == "DTJJ")
                        {
                            outPut += words[i].Substring(0, words[i].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', 'ُ') + " ";
                            i += 1;
                        }
                    }
                    else
                    {
                        outPut += words[i].Substring(0, words[i].IndexOf('/')) + " ";

                    }

                    //  char temp = outPut[outPut.Length - 5];
                    // outPut += words[i + 1].Substring(0, words[i + 1].IndexOf('/') + 1).Replace('/', temp);
                }
            }
            return outPut;

        }
    }
}
