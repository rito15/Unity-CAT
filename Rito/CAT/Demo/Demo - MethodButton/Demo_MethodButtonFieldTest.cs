using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 날짜 : #DATE#
// 작성자 : Rito15

namespace Rito.CAT.Demo
{
    public class Demo_MethodButtonFieldTest : MonoBehaviour
    {
        public MethodButton btn1;
        public MethodButton btn2 = nameof(TestMethod01);
        public MethodButton btn3 = (nameof(TestMethod01), "Custom Button Name");
        public MethodButton btn4 = (nameof(TestMethod01), 40);

        public MethodButton errorButton = "ABCD";

        private void TestMethod01()
        {
            Debug.Log("Test 01");
        }
    }
}