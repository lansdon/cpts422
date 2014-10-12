using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPTest;
using TMSharp;


namespace TMSharpTests
{
    [LPTestClass]
    class ParseTests
    {
        private List<string> ValidDefinition1 = new List<string>() 
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
        };
        public ParseTests() { }    // Must have a default constructor for LPTest

        [LPTestMethod]
        public void ParseDefinition_Valid0()
        {
            List<string> definition = TMTestDefinitions.ValidDefinition(0);
            TestDefinition(definition);
        }

        [LPTestMethod]
        public void ParseDefinition_ValidStates()
        {
            List<string> definition = new List<string>() 
            {
                //"description stuff1",
                //"description stuff2",     // Assume definition was already extracted
                "STATES: S1 S2 S3 S4",
                "INPUT_ALPHABET: a b",      // Need to have next keyword so it's detected
                //"TAPE_ALPHABET: a b x y -",
                //"TRANSITION_FUNCTION:",
                //"s0 a   s1 X R",
                //"s0 Y   s3 Y R",
                //"s1 a   s1 a R",
                //"s1 b   s2 Y L",
                //"s1 Y   s1 Y R", 
                //"s2 a   s2 a L",
                //"s2 X   s0 X R",
                //"s2 Y   s2 Y L",
                //"s3 Y   s3 Y R",
                //"s3 -   s4 - R",
                //"INITIAL_STATE: s0",
                //"BLANK_CHARACTER: -",
                //"FINAL_STATES: s4"
            };
            States states = new States();
            Assert.IsTrue(states.load(ref definition), "Failed to parse states.");
            Assert.IsTrue(states.is_element("S1"), "S1 not found");
            Assert.IsTrue(states.is_element("S2"), "S2 not found");
            Assert.IsTrue(states.is_element("S3"), "S3 not found");
            Assert.IsTrue(states.is_element("S4"), "S4 not found");
        }

        [LPTestMethod]
        public void ParseDefinition_InvalidStates()
        {
            List<string> definition = new List<string>() 
            {
                "STATES: ",
                "INPUT_ALPHABET: a b",      // Need to have next keyword so it's detected
            };
            States states = new States();
            Assert.IsFalse(states.load(ref definition), "Should fail to parse states.");
        }
        private Tape tape = new Tape();
        private FinalStates final_states = new FinalStates();
        private InputAlphabet input_alphabet = new InputAlphabet();
        private States states = new States();
        private TapeAlphabet tape_alphabet = new TapeAlphabet();
        private TransitionFunction transition_function = new TransitionFunction();
       void TestDefinition(List<string> definition)
        {
            TuringMachine tm = new TuringMachine("");
            Assert.IsTrue(tm.parseDescription(ref definition), "Failed to parse description");
            Assert.IsTrue(states.load(ref definition), "Failed to parse states.");
            Assert.IsTrue(input_alphabet.load(ref definition), "Failed to parse input alphabet");
            Assert.IsTrue(tape_alphabet.load(ref definition), "Failed to parse tape alphabet");
            Assert.IsTrue(transition_function.load(ref definition), "Failed to parse transition function.");
            Assert.IsTrue(tm.LoadInitialState(ref definition), "Failed to parse initial state.");
            Assert.IsTrue(tape.load(ref definition), "Failed to load tape.");
            Assert.IsTrue(final_states.load(ref definition), "Failed to parse final states.");
        }
    }
}
