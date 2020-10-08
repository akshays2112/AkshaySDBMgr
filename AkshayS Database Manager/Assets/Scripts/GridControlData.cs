using System;
using UnityEngine;
using UnityEngine.UI;

public class GridControlData : MonoBehaviour
{
    public void DrawData(ClassDefs.ListControlDataProperties listControlDataProperties)
    {
        Text titleText = gameObject.transform.parent.parent.parent.GetComponentInChildren<Text>();
        if (titleText != null)
        {
            Text text = titleText.GetComponent<Text>();
            if (text != null)
            {
                text.text = listControlDataProperties.title;
            }
        }
        if (listControlDataProperties.listRowGameObjects != null && listControlDataProperties.listRowGameObjects.Length > 0)
        {
            float[] gObjsGreatestHeightArray = new float[listControlDataProperties.listRowGameObjects.Length];
            float[] gObjsGreatestWidthArray = new float[listControlDataProperties.listRowGameObjects[0].transform.childCount];
            for (int rowIndex = 0; rowIndex < listControlDataProperties.listRowGameObjects.Length; rowIndex++)
            {
                float greatestChildHeight = 0;
                for (int childIndex = 0; childIndex < listControlDataProperties.listRowGameObjects[rowIndex].transform.childCount; childIndex++)
                {
                    Transform childTransform = listControlDataProperties.listRowGameObjects[rowIndex].transform.GetChild(childIndex);
                    Text childTransformText = childTransform.GetComponent<Text>();
                    if (childTransformText == null)
                    {
                        RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
                        gObjsGreatestWidthArray[childIndex] = Math.Max(childRectTransform.rect.width, gObjsGreatestWidthArray[childIndex]);
                        greatestChildHeight = Math.Max(childRectTransform.rect.height, greatestChildHeight);
                    }
                    else
                    {
                        (float width, float height) textSizeChildTransform = Utilities.GetTextSize(childTransformText);
                        gObjsGreatestWidthArray[childIndex] = Math.Max(textSizeChildTransform.width, gObjsGreatestWidthArray[childIndex]);
                        greatestChildHeight = Math.Max(textSizeChildTransform.height, greatestChildHeight);
                    }
                }
                gObjsGreatestHeightArray[rowIndex] = greatestChildHeight;
            }
            float[] headerWidthArray = new float[listControlDataProperties.listHeaderGameObject.transform.childCount];
            float greatestHeaderChildHeight = 0;
            for (int childIndex = 0; childIndex < listControlDataProperties.listHeaderGameObject.transform.childCount; childIndex++)
            {
                Transform childTransform = listControlDataProperties.listHeaderGameObject.transform.GetChild(childIndex);
                Text childTransformText = childTransform.GetComponent<Text>();
                if (childTransformText == null)
                {
                    RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
                    if (childRectTransform != null)
                    {
                        headerWidthArray[childIndex] = childRectTransform.rect.width;
                        greatestHeaderChildHeight = Math.Max(childRectTransform.rect.height, greatestHeaderChildHeight);
                    }
                    else
                    {
                        Text textCustomHeader = childTransform.GetComponentInChildren<Text>();
                        if (textCustomHeader != null && textCustomHeader.text != null && textCustomHeader.text.Length > 0)
                        {
                            (float width, float height) textSizeCustomHeader = Utilities.GetTextSize(textCustomHeader);
                            headerWidthArray[childIndex] = textSizeCustomHeader.width;
                            greatestHeaderChildHeight = Math.Max(textSizeCustomHeader.height, greatestHeaderChildHeight);
                        }
                    }
                }
                else
                {
                    if (childTransformText.text != null && childTransformText.text.Length > 0)
                    {
                        (float width, float height) textSizeChildTransform = Utilities.GetTextSize(childTransformText);
                        headerWidthArray[childIndex] = textSizeChildTransform.width;
                        greatestHeaderChildHeight = Math.Max(textSizeChildTransform.height, greatestHeaderChildHeight);
                    }
                }
            }
            if (headerWidthArray.Length == gObjsGreatestWidthArray.Length)
            {
                for (int colIndex = 0; colIndex < headerWidthArray.Length; colIndex++)
                {
                    gObjsGreatestWidthArray[colIndex] = Math.Max(headerWidthArray[colIndex], gObjsGreatestWidthArray[colIndex]);
                }
            }
            float totalGObjsWidth = 0;
            foreach (float gObjsWidth in gObjsGreatestWidthArray)
            {
                totalGObjsWidth += gObjsWidth + listControlDataProperties.margins.leftMargin + listControlDataProperties.margins.rightMargin;
            }
            float totalGObjsHeight = 0;
            foreach (float gObjsHeight in gObjsGreatestHeightArray)
            {
                totalGObjsHeight += gObjsHeight + listControlDataProperties.margins.topMargin + listControlDataProperties.margins.bottomMargin;
            }
            float contentScrollbarWidth = gameObject.transform.parent.parent.GetChild(2).GetComponent<RectTransform>().rect.width;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(totalGObjsWidth -
                gameObject.transform.parent.parent.GetComponent<RectTransform>().rect.width + contentScrollbarWidth, totalGObjsHeight);
            gameObject.transform.parent.parent.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta =
                new Vector2(totalGObjsWidth - gameObject.transform.parent.parent.parent.GetChild(1).GetComponent<RectTransform>().rect.width +
                contentScrollbarWidth, greatestHeaderChildHeight + listControlDataProperties.margins.topMargin +
                listControlDataProperties.margins.bottomMargin);
            float totalWidth = listControlDataProperties.margins.leftMargin;
            for (int childIndex = 0; childIndex < listControlDataProperties.listHeaderGameObject.transform.childCount; childIndex++)
            {
                Transform childTransform = listControlDataProperties.listHeaderGameObject.transform.GetChild(childIndex);
                RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
                if (childRectTransform != null && childRectTransform.GetComponent<Text>() != null)
                {
                    childRectTransform.sizeDelta = new Vector2(gObjsGreatestWidthArray[childIndex], greatestHeaderChildHeight);
                }
                childTransform.localPosition = new Vector3(totalWidth + (gObjsGreatestWidthArray[childIndex] / 2f),
                    -((greatestHeaderChildHeight + listControlDataProperties.margins.topMargin +
                    listControlDataProperties.margins.bottomMargin) / 2f), childTransform.localPosition.z);
                totalWidth += gObjsGreatestWidthArray[childIndex] + listControlDataProperties.margins.rightMargin +
                    (childIndex == listControlDataProperties.listHeaderGameObject.transform.childCount - 1 ? 0 :
                    listControlDataProperties.margins.leftMargin);
            }
            float totalHeight = -listControlDataProperties.margins.topMargin;
            for (int rowIndex = 0; rowIndex < listControlDataProperties.listRowGameObjects.Length; rowIndex++)
            {
                totalWidth = listControlDataProperties.margins.leftMargin;
                for (int colIndex = 0; colIndex < listControlDataProperties.listRowGameObjects[rowIndex].transform.childCount; colIndex++)
                {
                    Transform childTransform = listControlDataProperties.listRowGameObjects[rowIndex].transform.GetChild(colIndex);
                    RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
                    if (childTransform.GetComponent<Text>() != null)
                    {
                        childRectTransform.sizeDelta = new Vector2(gObjsGreatestWidthArray[colIndex], gObjsGreatestHeightArray[rowIndex]);
                    }
                    childTransform.localPosition = new Vector3(totalWidth + (gObjsGreatestWidthArray[colIndex] / 2f),
                        totalHeight - (gObjsGreatestHeightArray[rowIndex] / 2f), childTransform.localPosition.z);
                    totalWidth += gObjsGreatestWidthArray[colIndex] + listControlDataProperties.margins.rightMargin +
                        (colIndex == listControlDataProperties.listRowGameObjects[rowIndex].transform.childCount - 1 ? 0 :
                        listControlDataProperties.margins.leftMargin);
                }
                totalHeight -= gObjsGreatestHeightArray[rowIndex] + listControlDataProperties.margins.bottomMargin +
                    (rowIndex == listControlDataProperties.listRowGameObjects.Length - 1 ? 0 :
                    listControlDataProperties.margins.topMargin);
            }
            Rect rectContentScrollView = gameObject.transform.parent.parent.GetComponent<RectTransform>().rect;
            RectTransform rectTransformTitle = gameObject.transform.parent.parent.parent.GetChild(0).GetComponent<RectTransform>();
            RectTransform rectTransformHeaderScrollView = gameObject.transform.parent.parent.parent.GetChild(1).GetComponent<RectTransform>();
            rectTransformHeaderScrollView.sizeDelta = new Vector2(rectContentScrollView.width, greatestHeaderChildHeight +
                listControlDataProperties.margins.topMargin + listControlDataProperties.margins.bottomMargin);
            rectTransformHeaderScrollView.localPosition = new Vector3(rectTransformHeaderScrollView.localPosition.x,
                (greatestHeaderChildHeight / 2f) + (rectContentScrollView.height / 2f) +
                listControlDataProperties.margins.bottomMargin, rectTransformHeaderScrollView.localPosition.z);
            if (rectTransformTitle != null)
            {
                Text textTitle = rectTransformTitle.GetComponent<Text>();
                if (textTitle != null && textTitle.text != null && textTitle.text.Length > 0)
                {
                    (float width, float height) textSizeChildTransform = Utilities.GetTextSize(textTitle);
                    rectTransformTitle.sizeDelta = new Vector2(textSizeChildTransform.width, textSizeChildTransform.height);
                    rectTransformTitle.localPosition = new Vector3(rectTransformTitle.localPosition.x,
                        (rectContentScrollView.height / 2f) + rectTransformHeaderScrollView.rect.height + 
                        (rectTransformTitle.rect.height / 2f) + listControlDataProperties.margins.bottomMargin, 
                        rectTransformTitle.localPosition.z);
                }
            }
        }
        else if (listControlDataProperties.listHeaderGameObject != null && listControlDataProperties.listHeaderGameObject.transform.childCount > 0)
        {
            float[] headerWidthArray = new float[listControlDataProperties.listHeaderGameObject.transform.childCount];
            float greatestHeaderChildHeight = 0;
            for (int childIndex = 0; childIndex < listControlDataProperties.listHeaderGameObject.transform.childCount; childIndex++)
            {
                Transform childTransform = listControlDataProperties.listHeaderGameObject.transform.GetChild(childIndex);
                Text childTransformText = childTransform.GetComponent<Text>();
                if (childTransformText == null)
                {
                    RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
                    if (childRectTransform != null)
                    {
                        headerWidthArray[childIndex] = childRectTransform.rect.width +
                            listControlDataProperties.margins.leftMargin + listControlDataProperties.margins.rightMargin;
                        greatestHeaderChildHeight = Math.Max(childRectTransform.rect.height + listControlDataProperties.margins.topMargin +
                            listControlDataProperties.margins.bottomMargin, greatestHeaderChildHeight);
                    }
                    else
                    {
                        Text textCustomHeader = childTransform.GetComponentInChildren<Text>();
                        if (textCustomHeader != null)
                        {
                            (float width, float height) textSizeCustomHeader = Utilities.GetTextSize(textCustomHeader);
                            headerWidthArray[childIndex] = textSizeCustomHeader.width +
                            listControlDataProperties.margins.leftMargin + listControlDataProperties.margins.rightMargin;
                            greatestHeaderChildHeight = Math.Max(textSizeCustomHeader.height + listControlDataProperties.margins.topMargin +
                            listControlDataProperties.margins.bottomMargin, greatestHeaderChildHeight);
                        }
                    }
                }
                else
                {
                    (float width, float height) textSizeChildTransform = Utilities.GetTextSize(childTransformText);
                    headerWidthArray[childIndex] = textSizeChildTransform.width +
                            listControlDataProperties.margins.leftMargin + listControlDataProperties.margins.rightMargin;
                    greatestHeaderChildHeight = Math.Max(textSizeChildTransform.height + listControlDataProperties.margins.topMargin +
                            listControlDataProperties.margins.bottomMargin, greatestHeaderChildHeight);
                }
            }
            float totalWidth = listControlDataProperties.margins.leftMargin;
            for (int childIndex = 0; childIndex < listControlDataProperties.listHeaderGameObject.transform.childCount; childIndex++)
            {
                Transform childTransform = listControlDataProperties.listHeaderGameObject.transform.GetChild(childIndex);
                RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
                if (childRectTransform != null)
                {
                    childRectTransform.sizeDelta = new Vector2(headerWidthArray[childIndex], greatestHeaderChildHeight);
                }
                else
                {
                    RectTransform subChildRectTransform = childTransform.GetComponentInChildren<RectTransform>();
                    if (subChildRectTransform != null)
                    {
                        subChildRectTransform.sizeDelta = new Vector2(headerWidthArray[childIndex],
                            greatestHeaderChildHeight);
                    }
                }
                childTransform.localPosition = new Vector3(totalWidth + (headerWidthArray[childIndex] / 2f),
                    -(greatestHeaderChildHeight / 2f), childTransform.localPosition.z);
                totalWidth += headerWidthArray[childIndex];
            }
            float contentScrollbarWidth = gameObject.transform.parent.parent.GetChild(2).GetComponent<RectTransform>().rect.width;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(totalWidth -
                gameObject.transform.parent.parent.GetComponent<RectTransform>().rect.width + contentScrollbarWidth +
                listControlDataProperties.margins.rightMargin, greatestHeaderChildHeight);
            gameObject.transform.parent.parent.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta =
                new Vector2(totalWidth - gameObject.transform.parent.parent.parent.GetChild(1).GetComponent<RectTransform>().rect.width + 
                contentScrollbarWidth + listControlDataProperties.margins.rightMargin, greatestHeaderChildHeight);
            Rect rectContentScrollView = gameObject.transform.parent.parent.GetComponent<RectTransform>().rect;
            RectTransform rectTransformTitle = gameObject.transform.parent.parent.parent.GetChild(0).GetComponent<RectTransform>();
            RectTransform rectTransformHeaderScrollView = gameObject.transform.parent.parent.parent.GetChild(1).GetComponent<RectTransform>();
            rectTransformTitle.localPosition = new Vector3(rectTransformTitle.localPosition.x, (rectContentScrollView.height / 2f) +
                rectTransformHeaderScrollView.rect.height + (rectTransformTitle.rect.height / 2f), rectTransformTitle.localPosition.z);
            rectTransformHeaderScrollView.localPosition = new Vector3(rectTransformHeaderScrollView.localPosition.x,
                (greatestHeaderChildHeight / 2f) + (rectContentScrollView.height / 2f), rectTransformHeaderScrollView.localPosition.z);
            rectTransformHeaderScrollView.sizeDelta = new Vector2(rectContentScrollView.width, greatestHeaderChildHeight);
        }
        else if ((listControlDataProperties.listHeaderGameObject == null && listControlDataProperties.listRowGameObjects == null) ||
          (listControlDataProperties.listHeaderGameObject.transform.childCount == 0 && listControlDataProperties.listRowGameObjects.Length == 0))
        {
            Rect rectContentScrollView = gameObject.transform.parent.parent.GetComponent<RectTransform>().rect;
            RectTransform rectTransformTitle = gameObject.transform.parent.parent.parent.GetChild(0).GetComponent<RectTransform>();
            RectTransform rectTransformHeaderScrollView = gameObject.transform.parent.parent.parent.GetChild(1).GetComponent<RectTransform>();
            rectTransformTitle.localPosition = new Vector3(rectTransformTitle.localPosition.x, (rectContentScrollView.height / 2f) +
                rectTransformHeaderScrollView.rect.height + (rectTransformTitle.rect.height / 2f), rectTransformTitle.localPosition.z);
            rectTransformHeaderScrollView.localPosition = new Vector3(rectTransformHeaderScrollView.localPosition.x,
                (rectTransformHeaderScrollView.rect.height / 2f) + (rectContentScrollView.height / 2f), rectTransformHeaderScrollView.localPosition.z);
            rectTransformHeaderScrollView.sizeDelta = new Vector2(rectContentScrollView.width, 30);
        }
    }
}
