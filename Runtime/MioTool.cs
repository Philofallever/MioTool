using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MioTool
{
    public sealed partial class MioTool : MonoBehaviour
    {
        public static bool LogEnabled { get; set; } = true;

        public static void Log(object message, Object context = null)
        {
            if (!LogEnabled) return;
            Debug.Log(message, context);
        }

        public static void LogError(object message, Object context = null)
        {
            if (!LogEnabled) return;
            Debug.LogError(message, context);
        }

        public static void LogException(Exception exception, Object context = null)
        {
            if (!LogEnabled) return;
            Debug.LogException(exception, context);
        }

        bool ValidateComponent<T>(bool includeChildren = true) where T : Component
        {
            if (includeChildren)
                return GetComponentInChildren<T>();
            return GetComponent<T>();
        }

        /// <summary>
        /// 锚定限定方式
        /// </summary>
        [Flags]
        public enum AnchorLimitType
        {
            /// <summary>
            /// <para>限定左边</para>
            /// </summary>
            Left = 1,

            /// <summary>
            /// <para>限定右边</para>
            /// </summary>
            Right = 2,

            /// <summary>
            /// <para>限定顶边</para>
            /// </summary>
            Top = 4,

            /// <summary>
            /// <para>限定底边</para>
            /// </summary>
            Bottom = 8,

            /// <summary>
            /// <para>所有边都可以,自动选择</para>
            /// </summary>
            All = 15,
        }

        private static Vector3[] corners = new Vector3[4];


        /// <summary>
        /// 相对于目标点
        /// </summary>
        /// <param name="item">要锚定的item,一个transfrom</param>
        /// <param name="worldPos">世界坐标</param>
        /// <param name="limitType">锚定限制方向</param>
        /// <param name="space">锚定相对目标位置的间距</param>
        /// <param name="margin">item相对于自己父物体边缘空白</param>
        public static void RelativeAnchorItem(RectTransform item, Vector3 worldPos, AnchorLimitType limitType, float space, float margin = 5f)
        {
            space = Mathf.Abs(space);
            margin = Mathf.Abs(margin);
            var itemPanel = item.parent as RectTransform;
            Debug.Assert(itemPanel, "要锚定的item必须是UI的子物体");

            Vector2 targetLocalPos = itemPanel.InverseTransformPoint(worldPos);
            var preferAnchorH = AnchorLimitType.Left;
            if (targetLocalPos.x < 0 && limitType.HasFlag(AnchorLimitType.Right))
                preferAnchorH = AnchorLimitType.Right;

            var preferAnchorV = AnchorLimitType.Bottom;
            if (targetLocalPos.y < 0 && limitType.HasFlag(AnchorLimitType.Top))
                preferAnchorV = AnchorLimitType.Top;

            var preferAchorAll = Mathf.Abs(targetLocalPos.x) >= Mathf.Abs(targetLocalPos.y) ? preferAnchorH : preferAnchorV;

            AnchorLimitType realAnchor = limitType;
            if (limitType.HasFlag(preferAchorAll))
                realAnchor = preferAchorAll;
            else if (limitType.HasFlag(preferAnchorH))
                realAnchor = preferAnchorH;
            else if (limitType.HasFlag(preferAnchorV))
                realAnchor = preferAnchorV;
            else; // 必然单一边限定,且不是最优的边

            Debug.Log($"item应该锚定在相对itemPanel于{realAnchor}边的位置");

            var localPos = Vector2.zero;
            var itemPivot = Vector2.zero;
            switch (realAnchor)
            {
                case AnchorLimitType.Left:
                    localPos = targetLocalPos + Vector2.left * space;
                    itemPivot = new Vector2(1, 0.5f);
                    break;
                case AnchorLimitType.Right:
                    localPos = targetLocalPos + Vector2.right * space;
                    itemPivot = new Vector2(0, 0.5f);
                    break;
                case AnchorLimitType.Top:
                    localPos = targetLocalPos + Vector2.up * space;
                    itemPivot = new Vector2(0.5f, 0);
                    break;
                case AnchorLimitType.Bottom:
                    localPos = targetLocalPos + Vector2.down * space;
                    itemPivot = new Vector2(0.5f, 1);
                    break;
            }

            item.pivot = itemPivot;
            item.localPosition = localPos;

            // 判断item是否超出自己parent范围..
            item.GetWorldCorners(corners);
            var adjustPivot = itemPivot;
            switch (realAnchor)
            {
                case AnchorLimitType.Top:
                case AnchorLimitType.Bottom:
                    {
                        var leftCorner = itemPanel.InverseTransformPoint(corners[0]);
                        var xMin = itemPanel.rect.xMin + margin;
                        var pivotX = adjustPivot.x;

                        if (leftCorner.x < xMin)
                        {
                            var offset = xMin - leftCorner.x;
                            pivotX -= offset / item.rect.width;
                        }

                        var rightCorner = itemPanel.InverseTransformPoint(corners[3]);
                        var xMax = itemPanel.rect.xMax - margin;
                        if (rightCorner.x > xMax)
                        {
                            var offset = rightCorner.x - xMax;
                            pivotX += offset / item.rect.width;
                        }

                        adjustPivot = new Vector2(pivotX, item.pivot.y);
                    }
                    break;
                case AnchorLimitType.Left:
                case AnchorLimitType.Right:
                    {
                        var bottomCorner = itemPanel.InverseTransformPoint(corners[0]);
                        var yMin = itemPanel.rect.yMin + margin;
                        var pivotY = adjustPivot.y;
                        if (bottomCorner.y < yMin)
                        {
                            var offset = yMin - bottomCorner.y;
                            pivotY -= offset / item.rect.height;
                        }

                        var topCorner = itemPanel.InverseTransformPoint(corners[1]);
                        var yMax = item.rect.yMax - margin;
                        if (topCorner.y > yMax)
                        {
                            var offset = topCorner.y - yMax;
                            pivotY += offset / item.rect.height;
                        }

                        adjustPivot = new Vector2(item.pivot.x, pivotY);
                    }
                    break;
            }

            item.pivot = adjustPivot;
            item.localPosition = localPos;
        }

        /// <summary>
        /// 把item锚在target的某边
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        /// <param name="edge"></param>
        public static void RelativeAnchorItem(RectTransform item, RectTransform target, AnchorLimitType limitType, float padding, float padding2)
        {
            var wTargePos = target.position;
            var anchorPanel = item.parent as RectTransform;
            var pos = anchorPanel.InverseTransformPoint(wTargePos);

            var preferAnchorH = AnchorLimitType.Left;
            if (pos.x < 0 && limitType.HasFlag(AnchorLimitType.Right))
                preferAnchorH = AnchorLimitType.Right;

            var preferAnchorV = AnchorLimitType.Bottom;
            if (pos.y < 0 && limitType.HasFlag(AnchorLimitType.Top))
                preferAnchorV = AnchorLimitType.Top;

            var preferAchorAll = Mathf.Abs(pos.x) >= Mathf.Abs(pos.y) ? preferAnchorH : preferAnchorV;

            AnchorLimitType realAnchor = limitType;
            if (limitType.HasFlag(preferAchorAll))
                realAnchor = preferAchorAll;
            else if (limitType.HasFlag(preferAnchorH))
                realAnchor = preferAnchorH;
            else if (limitType.HasFlag(preferAnchorV))
                realAnchor = preferAnchorV;
            else
                ; // 必然单一边限定,且不是最优的边

            Debug.Log($"item应该锚在target的{realAnchor}边");
            var targetSize = target.rect.size;
            Vector2 targetPos2D = target.localPosition; // 相对于中心点计算
            Debug.Log($"{targetPos2D}  {targetSize}");
            var localPos = Vector2.zero;
            var itemEdge = AnchorLimitType.Left;
            var itemPivot = Vector2.zero;
            switch (realAnchor)
            {
                case AnchorLimitType.Left:
                    localPos = targetPos2D + targetSize * Vector2.left / 2; // 目标位置
                    itemEdge = AnchorLimitType.Right;
                    itemPivot = new Vector2(1, 0.5f);
                    break;
                case AnchorLimitType.Right:
                    localPos = targetPos2D + targetSize * Vector2.right / 2;
                    itemEdge = AnchorLimitType.Left;
                    itemPivot = new Vector2(0, 0.5f);
                    break;
                case AnchorLimitType.Top:
                    localPos = targetPos2D + targetSize * Vector2.up / 2;
                    itemEdge = AnchorLimitType.Bottom;
                    itemPivot = new Vector2(0.5f, 0);
                    break;
                case AnchorLimitType.Bottom:
                    localPos = targetPos2D + targetSize * Vector2.down / 2;
                    itemEdge = AnchorLimitType.Top;
                    itemPivot = new Vector2(0.5f, 1);
                    break;
            }

            var wAnchorPos = target.parent.TransformPoint(localPos);
            Debug.Log($"{localPos} {wAnchorPos}");
            localPos = anchorPanel.InverseTransformPoint(wAnchorPos); // 相对于中心点的位置;
            item.pivot = itemPivot;
            item.localPosition = localPos;

            // 判断是否超出parent范围..
            switch (realAnchor)
            {
                case AnchorLimitType.Top:
                case AnchorLimitType.Bottom:
                    item.GetWorldCorners(corners);
                    var itemPanelPos1 = anchorPanel.InverseTransformPoint(corners[0]);
                    if (itemPanelPos1.x < anchorPanel.rect.xMin)
                    {
                        var offset = anchorPanel.rect.xMin - itemPanelPos1.x;
                        var pivotX = 0.5 - offset / item.sizeDelta.x;
                        var originPos = item.localPosition;
                        item.pivot = new Vector2((float)pivotX, item.pivot.y);
                        item.localPosition = originPos;
                    }

                    break;
            }


            //localPos = item.InverseTransformPoint(wAnchorPos);
            //localPos = localPos / item.rect.size;
            //var localPos2 = new Vector2(Mathf.Clamp01(localPos.x), Mathf.Clamp01(localPos.y));
            //item.pivot = localPos2;
            //item.localPosition = localPos;
        }
    }
}