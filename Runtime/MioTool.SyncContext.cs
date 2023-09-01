using Sirenix.OdinInspector;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MioTool
{
    partial class MioTool
    {
        private const string _showSyncGroup = nameof(_showSyncContext);
        private const string _syncGroup = _showSyncGroup + "/SyncContext";
        private const string _syncBtnGroup = _syncGroup + "/Button";

        [SerializeField]
        private bool _showSyncContext = false;

        [BoxGroup(_syncGroup), ShowInInspector]
        private SynchronizationContext syncContext;

        [ShowIfGroup(_showSyncGroup)]
        [BoxGroup(_syncGroup), ShowInInspector]
        private int treadId;

        [ButtonGroup(_syncBtnGroup)]
        void CatchSyncContext()
        {
            syncContext = SynchronizationContext.Current;
            treadId = Thread.CurrentThread.ManagedThreadId;
        }

        [ButtonGroup(_syncBtnGroup)]
        void TestSend()
        {
            Task.Run(() =>
                     {
                         Thread.Sleep(1000);
                         syncContext.Send(_ =>
                             {
                                 print("In send");
                             }, new object());
                         Thread.Sleep(1000);
                         print("Test Send");
                     });
        }

        [ButtonGroup(_syncBtnGroup)]
        void TestPost()
        {
            Task.Run(() =>
                     {
                         Thread.Sleep(1000);
                         syncContext.Post(_ =>
                                          {
                                              print("In post");
                                          },
                                          new object());
                         print("Test Post");
                     });
        }

        [ButtonGroup(_syncBtnGroup)]
        void TestSleepMain()
        {
            syncContext.Send(_ =>
                             {
                                 Thread.Sleep(1000);
                                 print(1);
                                 Thread.Sleep(1000);
                                 print(2);
                                 Thread.Sleep(1000);
                                 print(3);
                                 Thread.Sleep(1000);
                                 print(4);

                             }, new object());
        }
    }
}
