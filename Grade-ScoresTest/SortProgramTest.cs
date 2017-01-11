using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Grade_Scores.Tests
{
 
   [TestClass]
    public class SortProgramTest
    {

        [TestMethod]
        public void shouldSplitStringIntoThree()
        {
            SortProgram testgrade = new SortProgram();
            string[] lines = { "test, program, 10", "program, test, 20" };
            string[,] dataArray = new string[lines.Count(), 3];
            Assert.IsTrue(testgrade.splitAndValidateLines(lines, dataArray));
        }

        [TestMethod]
        public void shouldFailToSplitIntoThree() 
        {
           SortProgram testgrade = new SortProgram();
            string[] lines = { "program test, 20" };
            string[,] dataArray = new string[lines.Count(), 3];
            Assert.IsTrue(!testgrade.splitAndValidateLines(lines, dataArray));
        }

        [TestMethod]
        public void shouldFailWithInvalidFilePath()
        {
            SortProgram testgrade = new SortProgram();
            Assert.IsTrue(!testgrade.sortTextFile(@"ljfldalfad"));
        }

        [TestMethod]
        public void shouldFailWithWhiteSpaceFileName()
        {
            SortProgram testgrade = new SortProgram();
            Assert.IsTrue(!testgrade.sortTextFile(@"   "));
        }

    }

}