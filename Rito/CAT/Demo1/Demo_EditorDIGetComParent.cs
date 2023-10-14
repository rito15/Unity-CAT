using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorDIGetComParent : MonoBehaviour
    {
        [EditorDI(DiMethod.GetComponentInParents)]
        public Light _getCInParents;

        [EditorDI(DiMethod.GetComponentInParentsOnly)]
        public Light _getCInParentsOnly;

        [EditorDI(DiMethod.GetComponentInParents, evenDisabled: true)]
        public Light _getCInParentsEvenDisabled;

        [EditorDI(DiMethod.GetComponentInParentsOnly, evenDisabled: true)]
        public Light _getCInParentsOnlyEvenDisabled;
    }
}