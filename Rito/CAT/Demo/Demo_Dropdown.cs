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
    }
}