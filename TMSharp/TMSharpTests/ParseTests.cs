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
        public ParseTests() { }    // Must have a default constructor for LPTest

        [LPTestMethod]
        public void ParseDefinition_Valid0()
        {
            List<string> definition = TMTestDefinitions.ValidDefinition(0);
            TestDefinition(definition);
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
