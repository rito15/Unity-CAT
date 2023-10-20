using Rito.CAT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rito.CAT.Demo
{
    // 2023-07-15
    // 작성자 : Rito
    public class Demo_EditorDIGetComChild_NC : MonoBehaviour
    {
        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "NONO", includeInactive: false)]
        public CapsuleCollider _children_Fail1;

        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "DI", includeInactive: false)]
        public CapsuleCollider _children_Suc1;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "DI", includeInactive: false)]
        public CapsuleCollider _children_Fail2;

        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "Cap", includeInactive: false)]
        public CapsuleCollider _children_Suc2;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "Cap", includeInactive: false)]
        public CapsuleCollider _children_Suc3;



        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "DI", includeInactive:true)]
        public CapsuleCollider _children_dsv_Suc1;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "DI", includeInactive: true)]
        public CapsuleCollider _children_dsv_Fail1;

        [EditorDI(DiMethod.GetComponentInChildren, nameIncludes: "Cap", includeInactive: true)]
        public CapsuleCollider _children_dsv_Suc2;

        [EditorDI(DiMethod.GetComponentInChildrenOnly, nameIncludes: "Cap", includeInactive: true)]
        public CapsuleCollider _children_dsv_Suc3;
    }
}
