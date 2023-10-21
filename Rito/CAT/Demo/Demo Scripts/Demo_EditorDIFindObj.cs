using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorDIFindObj : MonoBehaviour
    {
        [EditorDI(DiMethod.FindObjectOfType, includeInactive: false)]
        public UnityEngine.UI.Image _findObjOfType;

        [EditorDI(DiMethod.FindObjectOfType, includeInactive: true)]
        public UnityEngine.UI.Image _findObjOfTypeEvenDisabled;

    }
}