using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using static MioTool.MioTool;

namespace MioTool.Editor
{
    class MioToolEditorOdin
    {

    }

    public class OdinAttrProcessor : OdinAttributeProcessor
    {
        public override bool CanProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member)
        {
            //Log(parentProperty);
            //if (member.ReflectedType == typeof(Bounds))
            //{
            //    Log(member);
            //}

            return false;

        }
    }

    //public class Odin: OdinPropertyResolverLocator

}
