using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorAutoInjectFindObj : MonoBehaviour
    {
        [EditorAutoInject(EInjection.FindObjectOfType, injectEvenDisabled: false)]
        public UnityEngine.UI.Image _findObjOfType;

        [EditorAutoInject(EInjection.FindObjectOfType, injectEvenDisabled: true)]
        public UnityEngine.UI.Image _findObjOfTypeEvenDisabled;

    }
}