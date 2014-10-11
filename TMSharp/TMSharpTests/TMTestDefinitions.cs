using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSharpTests
{
    public class TMTestDefinitions
    {
        private List<List<string>> ValidDefs = new List<List<string>>()
        {
            new List<string>() 
            {
                "description stuff1",
                "description stuff2",
                "STATES: S1 S2 S3 S4",
                "INPUT_ALPHABET: a b",
                "TAPE_ALPHABET: a b x y -",
                "TRANSITION_FUNCTION:",
                "s0 a   s1 X R",
                "s0 Y   s3 Y R",
                "s1 a   s1 a R",
                "s1 b   s2 Y L",
                "s1 Y   s1 Y R", 
                "s2 a   s2 a L",
                "s2 X   s0 X R",
                "s2 Y   s2 Y L",
                "s3 Y   s3 Y R",
                "s3 -   s4 - R",
                "INITIAL_STATE: s0",
                "BLANK_CHARACTER: -",
                "FINAL_STATES: s4"
            }, 
        };
        static List<List<string>> InvalidDefs;
        private static TMTestDefinitions __instance = new TMTestDefinitions();

        private TMTestDefinitions(){}

        public static TMTestDefinitions Instance
        {
            get
            {
                return __instance;
            }
        }

        public static List<string> ValidDefinition(int index)
        {
            if (Instance.ValidDefs.Count() > index)
            {
                return Instance.ValidDefs[index];
            }
            return null;
        }
    }
}
