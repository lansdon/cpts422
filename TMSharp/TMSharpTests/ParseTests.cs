﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSharp;
using NUnit.Framework;

namespace TMSharpTests
{
    [TestFixture]
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

        [Test]
        public void ParseDefinition_Valid0()
        {
            List<string> definition = TMTestDefinitions.ValidDefinition(0);
            TestDefinition(definition);
        }
        [Test]
        ///////////////////////////////////////////////////////////////
        //Test method:              CheckForDuplicateStates
        //Test ID:                  5.2.1
        ///////////////////////////////////////////////////////////////
        public void ParseDefinition_CheckForDuplicateStates()
        {
            List<string> definition = new List<string>()
            {
                "S1 S1",
                "INPUT_ALPHABET:"
            };

            States test_state = new States();

            Assert.IsFalse(test_state.load(ref definition), "Failed to detect duplicate states.");
        }

        ///////////////////////////////////////////////////////////////
        //Test method:              CheckForCaseSensitivity
        //Test ID:                  5.2.2
        ///////////////////////////////////////////////////////////////
        [Test]
        public void ParseDefinition_CheckCaseSensitivityForStates()
        {
            List<string> definition = new List<string>()
            {
                "state1 STATE1",
                "INPUT_ALPHABET:"
            };

            States test_state = new States();
            test_state.load(ref definition);
            Assert.IsTrue(test_state.is_element("state1"));
            Assert.IsTrue(test_state.is_element("STATE1"));
        }

        ///////////////////////////////////////////////////////////////
        //Test method:              CheckForAtLeastOneState
        //Test ID:                  5.2.3
        ///////////////////////////////////////////////////////////////
        [Test]

        public void ParseDefinition_CheckForAtLeastOneState()
        {
            List<string> definition = new List<string>()
            {
                "",
                "INPUT_ALPHABET:"
            };

            States test_state = new States();

            Assert.IsFalse(test_state.load(ref definition), "Failed to detect null states in definition file.");

        }

        ///////////////////////////////////////////////////////////////
        //Test method:              CheckForValidStateCharacters
        //Test ID:                  5.2.4
        ///////////////////////////////////////////////////////////////
        [Test]
        public void ParseDefinition_CheckForValidStateCharacters()
        {
            List<string> definition = new List<string>()
            {
                "$",
                "INPUT_ALPHABET:"
            };

            States test_state = new States();

            Assert.IsFalse(test_state.load(ref definition), "Failed to detect invalid input alphabet character.");
        }

        ///////////////////////////////////////////////////////////////
        //Test method:              CheckThatElementsAreLengthOne
        //Test ID:                  5.2.5
        ///////////////////////////////////////////////////////////////
        [Test]
        public void ParseDefinition_CheckThatElementsAreLengthOne()
        {
            List<string> definition = new List<string>()
            {
                "ab",
                "TAPE_ALPHABET:"
            };

            InputAlphabet test_inputalphabet = new InputAlphabet();

            Assert.IsFalse(test_inputalphabet.load(ref definition), "Failed to detect improper length of input alphabet element.");
        }

        ///////////////////////////////////////////////////////////////
        //Test method:              CheckForDuplicateInputAlphabetCharacters
        //Test ID:                  5.2.6
        ///////////////////////////////////////////////////////////////
        [Test]
        public void ParseDefinition_CheckForDuplicateInputAlphabetCharacters()
        {
            List<string> definition = new List<string>()
            {
                "a a",
                "TAPE_ALPHABET:"
            };

            InputAlphabet test_inputalphabet = new InputAlphabet();

            Assert.IsFalse(test_inputalphabet.load(ref definition), "Failed to detect duplicate input alphabet characters.");
        }

        [Test]
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

        [Test]
        public void ParseDefinition_InvalidStates()
        {
            List<string> definition = new List<string>() 
            {
                " ",
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
