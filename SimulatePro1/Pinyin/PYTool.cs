using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PanGu;

namespace SimulatePro1.Pinyin
{
    /// <summary>
    /// 
    /// </summary>
    public class PYTool
    {
        public static PYTool instance;
        public static Object lockObject = new Object();

        private Dict dict;
        private MySegment mySegment;

        private string sourceWords = ""; // 原语句
        private string targetPY = ""; // 获得的拼音

        public static PYTool GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new PYTool();
                    }
                }
            }
            return instance;
        }

        private PYTool()
        {
            dict = Dict.GetInstance();
            dict.initDict();
            mySegment = MySegment.GetInstance();
        }

        public string GetPY(string sourceWords)
        {
            Console.WriteLine("开始查询拼音：" + sourceWords);
            if (sourceWords == null || sourceWords.Length == 0)
            {
                return "";
            }
            targetPY = "";
            this.sourceWords = sourceWords;
            ICollection<WordInfo> words = mySegment.DoSegment(sourceWords);
            Console.WriteLine("分词完成,开始转换拼音");
            ChangeWordsToPY(words);

            //List<string> words = mySegment.DoSegmentWithLucenePangu(sourceWords);
            //Console.WriteLine("分词完成,开始转换拼音");
            //ChangeWordsToPYFromList(words);

            Console.WriteLine("转换完成：" + targetPY);
            return targetPY;
        }

        private void ChangeWordsToPYFromList(List<string> sl)
        {
            foreach (string str in sl)
            {
                if (!str.Equals(""))
                {
                    targetPY += dict.GetWordPY(str);
                }
            }
        }

        /// <summary>
        /// 将集合里的词转化为拼音拼接起来
        /// </summary>
        /// <param name="words"></param>
        private void ChangeWordsToPY(ICollection<WordInfo> words)
        {
            foreach (WordInfo w in words)
            {
                if (w.WordType == WordType.English)
                {
                    targetPY += w.Word.ToUpper() + " ";
                }
                else if (w.WordType == WordType.Numeric)
                {
                    foreach (char c in w.Word)
                    {
                        if (c.Equals('1'))
                        {
                            targetPY += "yi ";
                        }
                        else if (c.Equals('2'))
                        {
                            targetPY += "er ";
                        }
                        else if (c.Equals('3'))
                        {
                            targetPY += "san ";
                        }
                        else if (c.Equals('4'))
                        {
                            targetPY += "si ";
                        }
                        else if (c.Equals('5'))
                        {
                            targetPY += "wu ";
                        }
                        else if (c.Equals('6'))
                        {
                            targetPY += "liu ";
                        }
                        else if (c.Equals('7'))
                        {
                            targetPY += "qi ";
                        }
                        else if (c.Equals('8'))
                        {
                            targetPY += "ba ";
                        }
                        else if (c.Equals('9'))
                        {
                            targetPY += "jiu ";
                        }
                        else if (c.Equals('0'))
                        {
                            targetPY += "ling ";
                        }
                    }
                }
                else
                {
                    targetPY += dict.GetWordPY(w.Word);
                }
            }
        }
    }
}
