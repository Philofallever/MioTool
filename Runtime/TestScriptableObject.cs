using UnityEngine;
using UnityEngine.Serialization;

namespace MioTool
{
    [CreateAssetMenu]
    class TestScriptableObject : ScriptableObject
    {
        //[SerializeField]
        //private Texture2D texture;

        //[SerializeField,FormerlySerializedAs("xxx3")]
        //private string xxx;

        [SerializeField, FormerlySerializedAs("xxx")]
        private string xxx2;

        //[SerializeField, FormerlySerializedAs("xxx2")]
        //private string xxx3;

        //[SerializeField, FormerlySerializedAs("xxx")]
        //private string xxx4;


        void Awake()
        {
            MioTool.Log("so awake", this);
        }

        void OnDestroy()
        {
            MioTool.Log("so destroy", this);
        }

        void OnEnable()
        {
            MioTool.Log("so enable", this);
        }

        void OnDisable()
        {
            MioTool.Log("so disable", this);
        }
    }
}
