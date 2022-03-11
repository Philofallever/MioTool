using Sirenix.OdinInspector;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        private const string _showCamGroup = nameof(_hasCamera);
        private const string _camGroup     = _showCamGroup + "/Camera";
        private const string _camBtnGroup1 = _camGroup + "/Button1";
        private const string _camBtnGroup2 = _camGroup + "/Button2";
        private const string _camBtnGroup3 = _camGroup + "/Button3";

        [ShowIfGroup(_showCamGroup)]
        [BoxGroup(_camGroup)]
        [ShowInInspector]
        private Camera _camera;

        [PropertySpace(5)]
        [ButtonGroup(_camBtnGroup1)]
        void ShowCamAspect()
        {
            print(_camera.aspect);
        }

        [ButtonGroup(_camBtnGroup1)]
        void RestCamAspect()
        {
            _camera.ResetAspect();
        }

        [PropertySpace(5)]
        [ButtonGroup(_camBtnGroup2)]
        void ShowRect()
        {
            print(_camera.rect);
        }

        [ButtonGroup(_camBtnGroup2)]
        void ShowPixelRect()
        {
            print(_camera.pixelRect);
        }

        [Button]
        void Func1()
        {
        }

        [Button]
        void Func2()
        {
        }

        [Button]
        void Func3()
        {
        }

        [Button]
        void Func4()
        {
        }


        [Button, BoxGroup(_camGroup)]
        void SetCamAspect(float aspect)
        {
            _camera.aspect = aspect;
        }

        bool _hasCamera
        {
            get
            {
                if (!_camera)
                    _camera = GetComponent<Camera>();
                return _camera;
            }
        }
    }
}