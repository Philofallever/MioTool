using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        private const string _showTransGroup = "_showTransfrom";
        private const string _transGroup = _showTransGroup + "/Transfrom";

        [SerializeField]
        private bool _showTransfrom = false;

        public static List<Transform> GetAllChildren(Transform root, List<Transform> list = null)
        {
            if (list == null)
                list = new List<Transform>(4);
            else
                list.Clear();

            foreach (Transform trans in root)
                list.Add(trans);

            var index = 0;
            while (index != list.Count)
            {
                foreach (Transform trans in list[index])
                    list.Add(trans);

                ++index;
            }

            return list;
        }

        [ShowIfGroup(_showTransGroup)]
        [BoxGroup(_transGroup), Button]
        public void DumpTransChildren(Transform trans)
        {
            if (trans == null)
                trans = transform;
            var list = GetAllChildren(trans);
            print($"{trans.name} children count:{list.Count}");
            foreach (var t in list)
                print(t);
        }
    }
}