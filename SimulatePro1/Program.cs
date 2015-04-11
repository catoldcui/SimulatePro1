﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulatePro1.Pinyin;

namespace SimulatePro1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            //Dict dict = Dict.GetInstance();
            //dict.initDict();

            PYTool pytool = PYTool.GetInstance();
            string str = Console.ReadLine();
            string pinyin = pytool.GetPY("");
            Console.WriteLine(pinyin);

            //Dict dict = Dict.GetInstance();
            //dict.initDict();
            //dict.SaveWordsToFile();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
