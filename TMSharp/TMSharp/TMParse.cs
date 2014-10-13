using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharp
{
    public class TMParse
    {
        public static bool ParseDefinitionValues(ref List<string> definition, string nextKey, out List<string> results)
        {
            results = new List<string>();
            string line = null;
            while (definition.Count() > 0)
            {
                line = definition[0];

                if (nextKey == null || !line.ToUpper().Contains(nextKey))
                {
                    definition.RemoveAt(0);
                    foreach (string s in line.Trim().Split())
                    {
                        if (s.Length > 0)
                            results.Add(s);
                    }
                }
                else
                {
                    // Remove the next keyword and put back everything after it into the current line.
                    int keywordIndex = line.LastIndexOf(nextKey);
                    int nextKeyLength = nextKey.Length;
                    int leftoverLength = line.Length - keywordIndex - nextKeyLength;
                    definition[0] = line.Substring(line.LastIndexOf(nextKey) + nextKey.Length, line.Length - line.LastIndexOf(nextKey) - nextKey.Length);
                    return true;
                }
            }
            if (nextKey == null)
                return true;
            return false;
        }
    }
}
