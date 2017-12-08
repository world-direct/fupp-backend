﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events
{
    public class TestStarted
    {
        public TestStarted(string testName, string url) {
            TestName = testName;
            Url = url;
        }

        public string TestName { get; }
        public string Url { get; }
    }
}
