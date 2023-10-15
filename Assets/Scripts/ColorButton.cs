using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Packer
{
	public class ColorButton : MonoBehaviour
	{
		public int index = 1;

		public void SetCameraDisplay()
		{
			GreenScreenManager.instance.SetCameraScreen(index);
		}

        public void SetPlayerDisplay()
        {
            GreenScreenManager.instance.SetPlayerScreen(index);
        }
    }
}