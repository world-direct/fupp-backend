﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events
{
    public class TestStarted
    {
        public TestStarted(string testName) {
            TestName = testName;
        }

        public string TestName { get; }
    }
}
