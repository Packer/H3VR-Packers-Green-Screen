using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Packer
{
    public class ScreenSet : MonoBehaviour
    {
        public Texture2D texture;
        public Color color = Color.white;


        public void SetScreen()
        {
            GreenScreenManager.instance.SetScreen(this);
        }
    }

}