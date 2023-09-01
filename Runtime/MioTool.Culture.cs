using Sirenix.OdinInspector;
using System;
using System.Globalization;
using UnityEngine;

namespace MioTool
{
    public partial class MioTool
    {
        private const string _showCultureGroup = "_showCulture";
        private const string _cultureGroup = _showCultureGroup + "/Culture";

        [SerializeField]
        private bool _showCulture = false;

        [ShowIfGroup(_showCultureGroup)]
        [BoxGroup(_cultureGroup), PropertySpace(SpaceBefore = 10), Button]
        void TestBeginOfWeek()
        {
            var culture = new CultureInfo("zh-cn");
            var now = new DateTime(2022, 3, 7, culture.Calendar);
            print($"{culture} {now.Date} {now.DayOfWeek}");
        }
    }
}