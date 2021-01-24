using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_Reference_Visualizer
{
    public class SearchEngine
    {
        public static int[] tableshift;

        ///<summary>Compiling an offset table</summary>
        ///<param name="readtemplate">template</param>
        public static void TableShift(string readtemplate)
        {
            tableshift = new int[char.MaxValue];

            for (int i = 0; i < tableshift.Length; i++)
            {
                tableshift[i] = readtemplate.Length;
            }
            for (int i = 0; i < readtemplate.Length; i++)
            {
                tableshift[readtemplate[i]] = readtemplate.Length - i;
            }
        }
        ///<summary>Boyer-Moore algorithm</summary>
        ///<param name="readsource">source string</param>
        ///<param name="readtemplate">template</param>
        ///<param name="sensitivity">Case sensitivity</param>
        public static string BoyerMoore(string readsource, string readtemplate, bool sensitivity = false)
        {
            var source = readsource;
            var template = readtemplate;

            if (!sensitivity)
            {
                source = readsource.ToLower();
                template = readtemplate.ToLower();
            }

            TableShift(template);

            if (template.Length > source.Length)
            {
                return ("Error: tmp > src");
            }

            if (template == source)
            {
                return ("Шаблон и исходная строка равны");
            }

            for (int i = template.Length; i < source.Length + 1;)                         // Основной цикл
            {
                for (int j = template.Length - 1; j >= 0; j--)                            // Цикл проверки на совпадения
                {
                    if (template[j] == source[i - template.Length + j])                   // Проверка на совпадения
                    {
                        if (j == 0)                                                       // Если первый символ шаблона схож с текущим символом исходной строки
                        {
                            return ($"Шаблон найден на {((i - template.Length) + 1)} символе строки.");
                        }
                    }
                    else
                    {
                        i += tableshift[source[i]];
                        break;
                    }
                }
            }
            return ("Шаблон не был найден в исходной строке.");
        }
    }
}
