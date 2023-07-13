using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-13
    // 작성자 : Rito
    public class Demo_EditorAutoInjectGetComParent : MonoBehaviour
    {
        [EditorAutoInject(EInjection.GetComponentInParents)]
        public Light _getCInParents;

        [EditorAutoInject(EInjection.GetComponentInParentsOnly)]
        public Light _getCInParentsOnly;

        [EditorAutoInject(EInjection.GetComponentInParents, injectEvenDisabled: true)]
        public Light _getCInParentsEvenDisabled;

        [EditorAutoInject(EInjection.GetComponentInParentsOnly, injectEvenDisabled: true)]
        public Light _getCInParentsOnlyEvenDisabled;
    }
}