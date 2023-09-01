using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        private const string _smrGroup = "ShowIfSMR/Default";

        [ShowIfGroup("ShowIfSMR", Condition = nameof(ValidateSkinnedRenderer))]
        [PropertySpace(10), BoxGroup(_smrGroup)]
        [SerializeField, InlineButton(nameof(InlineSMR))]
        private SkinnedMeshRenderer _skinnedRenderer;

        [Button, BoxGroup(_smrGroup)]
        public void SetLocalAABB()
        {
            // emmm 有问题
            Undo.RecordObject(_skinnedRenderer, "SetLocalAABB");
            _skinnedRenderer.sharedMesh.RecalculateBounds();
            print(_skinnedRenderer.localBounds);
            var c = _skinnedRenderer.worldToLocalMatrix * _skinnedRenderer.sharedMesh.bounds.center;
            _skinnedRenderer.sharedMesh.bounds = new Bounds(c, _skinnedRenderer.sharedMesh.bounds.size);
            _skinnedRenderer.localBounds = _skinnedRenderer.sharedMesh.bounds;
        }

        [BoxGroup(_smrGroup)]
        [Button(DisplayParameters = true), ShowIf(nameof(ValidateSkinnedRenderer))]
        void ChangeMesh(Mesh mesh)
        {
            _skinnedRenderer.sharedMesh = mesh;
        }

        [BoxGroup(_smrGroup), Button(DisplayParameters = true)]
        void ChangeMesh(SkinnedMeshRenderer smr)
        {
            Undo.RecordObject(_skinnedRenderer, "1");
            //ChangeMesh(smr.sharedMesh);
            //_skinnedRenderer.bo
            //foreach (var smrBone in smr.bones)
            //{
            //    print(smrBone);
            //}

            //foreach (var bone  in _skinnedRenderer.bones)
            //{
            //    print(bone);
            //}

            // mesh 
            _skinnedRenderer.sharedMesh = smr.sharedMesh;

            // bone
            //_skinnedRenderer.bones = smr.bones;
            //_skinnedRenderer.rootBone = smr.rootBone;
            //_skinnedRenderer.localBounds = smr.sharedMesh.bounds;
            smr.bones = _skinnedRenderer.bones;
            smr.rootBone = _skinnedRenderer.rootBone;
            //smr.localBounds = _skinnedRenderer.localBounds;
        }

        [BoxGroup(_smrGroup), PropertySpace(SpaceAfter = 10), Button]
        void LoadChangeMesh(string resPath = "ArtRes/Prefab/Actor/Player/p01_001/b_001")
        {
            var res = Resources.Load<GameObject>(resPath);
            const string partName = "p01_001/p01_001_body";
            var body = res.transform.Find(partName);
            print(body);
            var smr = body.GetComponent<SkinnedMeshRenderer>();
            InlineSMR();
            ChangeMesh(smr);
            //Resources.UnloadAsset(res);
        }


        private void InlineSMR() => _skinnedRenderer = GetComponent<SkinnedMeshRenderer>();
        private bool ValidateSkinnedRenderer => ValidateComponent<SkinnedMeshRenderer>(false);
    }
}