using Rito.CAT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorDIGetComChild : MonoBehaviour
    {
        [EditorDI(DiMethod.GetComponentInChildren, false)]
        public CapsuleCollider _getCInChildren; // 자신

        [EditorDI(DiMethod.GetComponentInChildrenOnly, false)]
        public CapsuleCollider _getCInChildrenOnly; // 자식

        [EditorDI(DiMethod.GetComponentInChildren, evenDisabled: true)]
        public BoxCollider _getCInChildrenEvenDisabled; // 자식

        [EditorDI(DiMethod.GetComponentInChildrenOnly, evenDisabled: true)]
        public BoxCollider _getCInChildrenOnlyEvenDisabled; // 자식
    }
}
