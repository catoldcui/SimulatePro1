using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SimulatePro1.Pinyin
{
    /// <summary>
    /// 单实例字典
    /// </summary>
    public class Dict
    {
        public static Dict instance;
        public static Object lockObject = new Object();
        private Dictionary<string, string[]> pydict;
        private Dictionary<string, string> pysymbols;
        private Dictionary<string, string> pysingle;

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static Dict GetInstance()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new Dict();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 
        /// </summary>
        private Dict()
        {
            pydict = new Dictionary<string, string[]>();
            pysymbols = new Dictionary<string, string>();
            pysingle = new Dictionary<string, string>();
        }

        public void initDict()
        {
            try
            {
                Console.WriteLine("开始读取：" + SystemConst.PYSINGLE_JSON);
                using (StreamReader sr = new StreamReader(SystemConst.PYSINGLE_JSON))
                {
                    String json = sr.ReadToEnd();
                    //Console.WriteLine(json);
                    pysingle = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                Console.WriteLine("读取成功：" + SystemConst.PYSINGLE_JSON);

                Console.WriteLine("开始读取：" + SystemConst.PYDICT_JSON);
                using (StreamReader sr = new StreamReader(SystemConst.PYDICT_JSON))
                {
                    String json = sr.ReadToEnd();
                    //Console.WriteLine(json);
                    pydict = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
                }
                Console.WriteLine("读取成功：" + SystemConst.PYDICT_JSON);

                Console.WriteLine("开始读取：" + SystemConst.PYSIMBOLS_JSON);
                using (StreamReader sr = new StreamReader(SystemConst.PYSIMBOLS_JSON))
                {
                    String json = sr.ReadToEnd();
                    //Console.WriteLine(json);
                    pysymbols = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                Console.WriteLine("读取成功：" + SystemConst.PYSIMBOLS_JSON);
            }
            catch (Exception e)
            {
                Console.WriteLine("Pinyin file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 获取单个词的拼音
        /// </summary>
        /// <param name="word">要查询的词</param>
        /// <returns>pair对，key：是否存在，value：拼音（如果多音词差不到，返回多音字的第一个拼音）</returns>
        public string GetWordPY(string word, string addbefore = "", string addafter = "")
        {
            string finalPY = "";
            string tempPY = ""; // 用来缓存发现多音字之前的拼音
            bool isPolyWord = false;
            List<KeyValuePair<bool, string>> list = new List<KeyValuePair<bool,string>>();
            foreach (char ch in word)
            {
                KeyValuePair<bool, string> result = IsPolyphone(ch);
                list.Add(result);
                if (result.Key)
                {
                    // 是多音词，不跳出循环（防止词库没有），进行下一步
                    isPolyWord = true;
                    tempPY += result.Value + " ";
                }
                else
                {
                    // 目前不是多音词
                    tempPY += result.Value + " ";
                }
            }

            if (isPolyWord)
            {
                if (word.Length != 1)
                {
                    // 是多音词,而且是个词，去查pydict
                    KeyValuePair<bool, string> result = GetPYFromDict(word);

                    if (result.Key)
                    {
                        // 存在，覆盖之前的finalPY
                        tempPY = result.Value;
                    }
                    else
                    {
                        string wordtemp = word.Substring(1, word.Length - 1);

                        if (wordtemp.Length > 1)
                        {
                            //tempPY = GetWordPY(wordtemp, "", list.ElementAt(word.Length - 1).Value + " ");
                            tempPY = GetWordPY(wordtemp, list.ElementAt(0).Value + " ");

                        }
                    }
                }
                else
                {
                    // 单个多音字,靠输出每次把单个读音放到第一个
                    Console.WriteLine("单个多音字出现了，“" + word + "”,依照读音，看是否调节词库");
                }
            }
               
            finalPY += tempPY;

            finalPY = addbefore + ConvertSymbols(finalPY) + addafter;
            return finalPY;
        }

        /// <summary>
        /// 从词库中查询拼音（只用于有多音字的词）
        /// </summary>
        /// <param name="w"></param>
        /// <returns>pair对，key：是否存在，value：拼音（如果多音词，返回空字符串）</returns>
        private KeyValuePair<bool, string> GetPYFromDict(string w)
        {
            bool isExist = false; // 是否存在
            string pinyin = ""; 

            if (pydict.ContainsKey(w))
            {
                // 存在，格式化拼音
                isExist = true;
                string[] pinyintemp = pydict[w];
                foreach (string str in pinyintemp)
                {
                    pinyin += str + " ";
                }
            }
            else
            {
                // 不存在
                Console.WriteLine("1002:多音词，没有“" + w + "”词");
            }

            return new KeyValuePair<bool, string>(isExist, pinyin);
        }

        /// <summary>
        /// 从字库中查询拼音，有多个拼音就是多音字
        /// </summary>
        /// <param name="ch">查询的字</param>
        /// <returns>pair对，key：是否存在，value：拼音（如果是多音字，返回第一个拼音）</returns>
        private KeyValuePair<bool, string> IsPolyphone(char ch)
        {
            bool isPolyphone = false;
            string pinyin = "";

            string str = ch.ToString();
            if (pysingle.ContainsKey(str))
            {
                string pinyintemp = pysingle[str];
                string[] values = pinyintemp.Split(',');
                // 判断音标个数
                if (values.Length > 1)
                {
                    // 是多音字，直取第一个
                    isPolyphone = true;
                    pinyin = values[0];
                }
                else
                {
                    isPolyphone = false;
                    pinyin = pinyintemp;
                }
            }
            else
            {
                isPolyphone = false;
                Console.WriteLine("1001:没有“" + str + "”字");
            }

            return new KeyValuePair<bool, string>(isPolyphone, pinyin);
        }

        /// <summary>
        /// 将拼音中的音标符号转换为不带音标的
        /// </summary>
        /// <param name="pinyin">带音标的拼音字符</param>
        /// <returns>不带音标的字符</returns>
        private string ConvertSymbols(string pinyin)
        {
            // 遍历符号数组
            foreach (KeyValuePair<string, string> pair in pysymbols)
            {
                pinyin = pinyin.Replace(pair.Key, pair.Value);
            }

            // 将音标放在最后
            string[] pinyinlist = pinyin.Split(' ');
            char charOfPhone = '_';
            string finalpy = ""; // 最后的py
            for (int i = 0; i < pinyinlist.Length; i++)
            {
                string pytemp = pinyinlist[i];

                if (pytemp.Equals(""))
                {
                    continue;
                }
                foreach (char ch in pytemp)
                {
                    try
                    {
                        Int32.Parse(ch.ToString());
                        charOfPhone = ch;
                        break;
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

                if (charOfPhone != '_')
                {
                    pytemp = pytemp.Remove(pytemp.IndexOf(charOfPhone), 1);
                    pytemp += charOfPhone;
                    //Console.WriteLine(pytemp);
                }

                finalpy += pytemp + " ";
            }


            return finalpy;
        }

        /// <summary>
        /// 将拼音字典文件写入txt
        /// </summary>
        public void SaveWordsToFile()
        {
            Console.WriteLine("写入文件：" + SystemConst.WORDS_SAVE_PATH);
            using (StreamWriter outfile = new StreamWriter(SystemConst.WORDS_SAVE_PATH))
            {
                foreach (string str in pydict.Keys)
                {
                    outfile.WriteLine(str);
                }
            }
            Console.WriteLine("写入文件完成");
        }
    }
}
