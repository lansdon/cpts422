using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPTest;

namespace maxit_tests
{
    [LPTestClass]
    public class SampleTest
    {
        /// <summary>
        /// Must have a default constructor !!!!!
        /// </summary>
        public SampleTest() 
        {
        }

        [LPTestMethod]
        public void SampleTestSucceed()
        {
            Assert.IsTrue( true );
        }

        [LPTestMethod]
        public void SampleTestFail()
        {
            Assert.IsTrue(false, "Example of something failing."); 
        }


    }
}
