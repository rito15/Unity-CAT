using System;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2021-01-15
    // 작성자 : Rito

    public class Demo_Dropdown : MonoBehaviour
    {
        [LayerDropDown]
        public int intLayer;

        [LayerDropDown]
        public string strLayer;

        [TagDropDown]
        public string strTag;

        // [StringDropDown()]
        [StringDropDown("TestA", "asdbc", "12345")]
        public string strDropdown;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log($"String Dropdown Value : {strDropdown}");
            }
        }
    }
}