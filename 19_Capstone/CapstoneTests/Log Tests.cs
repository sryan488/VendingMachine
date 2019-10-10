using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class Log_Tests
    {
        [TestMethod]
        public void CreateLogFile()
        {
            TransLog log = new TransLog
            {
                DirectoryForLogFilesPath = "../../../..", // When the program is run through visual studio,
                                                          // this means the files will generate in the same
                                                          // folder as the solution.
            };

            log.ItemsSold.Add("Cola", 0);


        }
    }
}
