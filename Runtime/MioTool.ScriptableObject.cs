using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        const string _showSOGroup = "_showSO";
        const string _soGroup = _showSOGroup + "/SOGroup";
        const string _soGroupBtn = _soGroup + "/Button";

        #region ScriptableObject

        [SerializeField]
        bool _showSO;

        [ShowIfGroup(_showSOGroup)]
        [SerializeField, BoxGroup(_soGroup), PropertySpace(10)]
        ScriptableObject _testSO;

        [SerializeField, BoxGroup(_soGroup)]
        string _path;

        [ButtonGroup(_soGroupBtn)]
        void Load()
        {
            var so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(_path);
            _testSO = so;
        }

        [ButtonGroup(_soGroupBtn)]
        void Destroy()
        {
            if (_testSO)
            {
                _testSO = null;
                Destroy(_testSO);
                print(_testSO);
                print(_testSO == null);
            }
        }

        [ButtonGroup(_soGroupBtn)]
        void DestroyImm()
        {
            if (_testSO)
            {
                _testSO = null;
                DestroyImmediate(_testSO);
                print(_testSO);
                print(_testSO == null);
            }
        }

        [ButtonGroup(_soGroupBtn)]
        void Unload()
        {
            if (_testSO) Resources.UnloadAsset(_testSO);
        }

        [ButtonGroup(_soGroupBtn)]
        void UnloadUnuse()
        {
            var asyncOp = Resources.UnloadUnusedAssets();
            asyncOp.completed += _ => MioTool.Log("unload unused completed!");
        }

        #endregion
    }
}