using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMSharp;
using LPTest;

namespace TMSharpTests
{
    [LPTestClass]
    class DefFileLoaderTests
    {
        /// <summary>
        /// Generic function for testing a full definition file after it's been loaded
        /// </summary>
        private Tape tape = new Tape();
        private FinalStates final_states = new FinalStates();
        private InputAlphabet input_alphabet = new InputAlphabet();
        private States states = new States();
        private TapeAlphabet tape_alphabet = new TapeAlphabet();
        private TransitionFunction transition_function = new TransitionFunction();

        public DefFileLoaderTests() { }
        
        /// <summary>
        /// 5.1.1 - Parse definition - check valid
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionValid()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsTrue(tm.loadDefinition("testValid.def"), "Test 5.1.1 failed, Valid defintion not loaded.");
        }

        /// <summary>
        /// 5.1.2 - Parse definition - check no state
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionNoState()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsFalse(tm.loadDefinition("testNoState.def"), "Test 5.1.2 failed, States found.");
        }

        /// <summary>
        /// 5.1.3 - Parse definition - check no input
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionNoInput()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsFalse(tm.loadDefinition("testNoInput.def"), "Test 5.1.3 failed, Input characters found.");
        }

        /// <summary>
        /// 5.1.4 - Parse definition - check no tape
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionNoTape()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsFalse(tm.loadDefinition("testNoTape.def"), "Tests 5.1.4 failed, Tape characters found");
        }

        /// <summary>
        /// 5.1.5 - Parse definition - check no transition
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionNoTransition()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsFalse(tm.loadDefinition("testNoTransition.def"), "Test 5.1.5 failed, Transitions found.");
        }

        /// <summary>
        /// 5.1.6 - Parse definition - check no initial
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionNoInitial()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsFalse(tm.loadDefinition("testNoInitial.def"), "Test 5.1.6 failed, Initial character found.");
        }

        /// <summary>
        /// 5.1.7 - Parse definition - check no blank
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionNoBlank()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsFalse(tm.loadDefinition("testNoBlank.def"), "Test 5.1.7 failed, Blank character found.");
        }

        /// <summary>
        /// 5.1.8 - Parse definition - check no final
        /// </summary>
        [LPTestMethod]
        public void TestDefinitionNoFinal()
        {

            TuringMachine tm = new TuringMachine("");
            Assert.IsFalse(tm.loadDefinition("testNoFinal.def"), "Test 5.1.8 failed, Final states found.");
        }       
    }
}
