using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PanGu;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Analysis.PanGu;
using System.IO;

namespace SimulatePro1.Pinyin
{
    /// <summary>
    /// 单实例分词类，封装开源的中文盘古分词，配置文件在pangu.xml
    /// </summary>
    class MySegment
    {
        // 线程锁
        public static Object lockObject = new Object();
        private static MySegment instance;
        private Segment segment;
        public static MySegment GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new MySegment();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        ///  初始化盘古分词
        /// </summary>
        private MySegment()
        {
            PanGu.Segment.Init();
            segment = new Segment();
        }

        /// <summary>
        /// 简单分词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ICollection<WordInfo> DoSegment(string text){
            ICollection<WordInfo> words = segment.DoSegment(text);

            Console.Write("分词结果");
            foreach (WordInfo w in words)
            {
                Console.Write(w.Word + " ");
            }
            Console.WriteLine();
            return words;
        }

        /// <summary>
        /// 盘古分词
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public List<string> DoSegmentWithLucenePangu(string text)
        {
            List<string> sl = new List<string>();
            PanGuAnalyzer analyzer = new PanGuAnalyzer();
            //Analyzer analyzer = new StandardAnalyzer();//new后面的类为分词的方法
            TokenStream ts = analyzer.TokenStream("", new StringReader(text));
            ts.AddAttribute<TermAttribute>();
            ts.AddAttribute<TypeAttribute>();
            TermAttribute termAtt = (TermAttribute) ts 
                .GetAttribute<TermAttribute>(); 
            TypeAttribute typeAtt = (TypeAttribute) ts
                .GetAttribute<TypeAttribute>(); 
            while (ts.IncrementToken()) {
                sl.Add(termAtt.Term);
            }
            return sl;
        }
    }
}
