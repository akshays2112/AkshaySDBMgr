using System;
using UnityEngine;

public class ClassDefs
{
    [Serializable]
    public class Margins
    {
        public float leftMargin = 5f;
        public float rightMargin = 5f;
        public float topMargin = 5f;
        public float bottomMargin = 5f;
        public float allMargins
        {
            get { return leftMargin; }
            set { if (value >= 0) { leftMargin = rightMargin = topMargin = bottomMargin = value; } }
        }
    }

    [Serializable]
    public class ListControlDataProperties
    {
        public string title;
        public bool genericHeaderItems;
        public bool genericListItems;
        public GameObject listHeaderGameObject;
        public GameObject[] listRowGameObjects;
        public Margins margins;
    }
}
