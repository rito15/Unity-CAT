using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    public class Demo_MethodButtonTest : MonoBehaviour
    {
        [MethodButton(nameof(PrintMsg))]
        public bool __;

        [MethodButton(nameof(PrintMsg), "Method")]
        public bool ___;

        private static void PrintMsg()
        {
            Debug.Log("Call - Method Button");
        }
    }
}