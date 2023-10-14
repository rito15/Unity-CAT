using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorDIFindObj : MonoBehaviour
    {
        [EditorDI(DiMethod.FindObjectOfType, evenDisabled: false)]
        public UnityEngine.UI.Image _findObjOfType;

        [EditorDI(DiMethod.FindObjectOfType, evenDisabled: true)]
        public UnityEngine.UI.Image _findObjOfTypeEvenDisabled;

    }
}