using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-15
    // 작성자 : Rito
    public class Demo_EditorDIFindObj_NC : MonoBehaviour
    {
        [EditorDI(DiMethod.FindObjectOfType, includeInactive: false, nameIncludes: "AAA")]
        public UnityEngine.UI.Image _find_AAA_onlyEnabled;

        [EditorDI(DiMethod.FindObjectOfType, includeInactive: true, nameIncludes: "BBB")]
        public UnityEngine.UI.Image _find_BBB_evenDisabled;
    }
}