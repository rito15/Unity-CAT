using Rito.CAT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorAutoInjectGetComChild : MonoBehaviour
    {
        [EditorAutoInject(EInjection.GetComponentInChildren)]
        public CapsuleCollider _getCInChildren; // 자신

        [EditorAutoInject(EInjection.GetComponentInChildrenOnly)]
        public CapsuleCollider _getCInChildrenOnly; // 자식

        [EditorAutoInject(EInjection.GetComponentInChildren, injectEvenDisabled: true)]
        public BoxCollider _getCInChildrenEvenDisabled; // 자식

        [EditorAutoInject(EInjection.GetComponentInChildrenOnly, injectEvenDisabled: true)]
        public BoxCollider _getCInChildrenOnlyEvenDisabled; // 자식
    }
}
