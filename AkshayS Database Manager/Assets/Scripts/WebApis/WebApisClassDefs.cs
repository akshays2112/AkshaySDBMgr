using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebApisClassDefs
{
    public class TestWebSvcResponse
    {
        public class TestString
        {
            public string testString1 { get; set; }
        }

        public TestString[] testStrings { get; set; }
    }
}
