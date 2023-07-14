using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorDItFindObj : MonoBehaviour
    {
        [EditorDI(DiMethod.FindObjectOfType, injectEvenDisabled: false)]
        public UnityEngine.UI.Image _findObjOfType;

        [EditorDI(DiMethod.FindObjectOfType, injectEvenDisabled: true)]
        public UnityEngine.UI.Image _findObjOfTypeEvenDisabled;

    }
}