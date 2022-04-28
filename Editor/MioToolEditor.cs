using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace MioTool.Editor
{
    public class MioEditor
    {
        [MenuItem("MioTool/Edit/Rename %#r")]
        private static void Rename()
        {
            var list = Selection.gameObjects.ToList();
            if (list.Count == 0) return;

            var parent = list[0].transform.parent;
            list.RemoveAll(x => x.transform.parent != parent);
            list.Sort((obj1, obj2) => obj1.transform.GetSiblingIndex() - obj2.transform.GetSiblingIndex());
            var n = list[0].name;
            var m = Regex.Match(n, "[A-z]+");
            if (m.Success)
                n = m.Value.Replace(m.Value.First(), char.ToUpper(m.Value.First()));

            var index = -1;
            if (list[0].transform.parent)
            {
                if (list[0].transform.GetSiblingIndex() != 0)
                {
                    var succOnce = false;
                    var prefix = "";
                    for (int i = list[0].name.Length - 1; i >= 0; i--)
                    {
                        if (char.IsLetter(list[0].name, i))
                        {
                            prefix = list[0].name.Substring(0, i + 1);
                            break;
                        }
                    }

                    foreach (var obj in list[0].transform.parent)
                    {
                        var child = obj as Transform;
                        if (list.Contains(child.gameObject)) continue;

                        var m1 = Regex.Match(child.name, $"{prefix}([0-9]+)");
                        if (!m1.Success) continue;

                        succOnce = true;

                        var index1 = Convert.ToInt32(m1.Groups[1].Value);
                        index = index1 > index ? index1 : index;
                    }

                    if (!succOnce)
                    {
                        var m1 = Regex.Match(list[0].name, "[A-z]+([0-9]+)");
                        if (m1.Success)
                            index = Convert.ToInt32(m1.Groups[1].Value) - 1;
                    }
                }
            }
            else
            {
                var m1 = Regex.Match(list[0].name, "[A-z]+([0-9]+)");
                if (m1.Success)
                    index = Convert.ToInt32(m1.Groups[1].Value) - 1;
            }

            Undo.RecordObjects(list.Select(x => (Object)x).ToArray(), "rename objects");
            foreach (var obj in list)
                obj.name = $"{n}{++index}";
        }

        [MenuItem("MioTool/Edit/ToggleActive %#H")]
        private static void ToggleActive()
        {
            Undo.RecordObjects(Selection.gameObjects, "ToggleActive");
            foreach (var obj in Selection.gameObjects)
                obj.SetActive(!obj.activeInHierarchy);
        }

        [MenuItem("MioTool/Edit/LowerName %#M")]
        static void LowerName()
        {
            var list = Selection.gameObjects.ToList();
            if (list.Count == 0) return;

            var parent = list[0].transform.parent;
            list.RemoveAll(x => x.transform.parent != parent);
            Undo.RecordObjects(list.Select(x => (Object)x).ToArray(), "ToLowerName");
            list.ForEach(x => x.name = x.name.ToLower());
            list.ForEach(x =>
                         {
                             if (AssetDatabase.Contains(x))
                             {
                                 var path = AssetDatabase.GetAssetPath(x);
                                 AssetDatabase.RenameAsset(path, Path.GetFileName(path).ToLower());
                                 AssetDatabase.Refresh();
                             }
                         });
        }

        [MenuItem("Tools/cmds.lua")]
        static void CmdMarkDown()
        {
            Process.Start("code", "-r ../client-system/doc/cmd.md");
        }
    }
}