using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Packer
{
	public class CameraMatManager : MonoBehaviour
    {

        void OnPreRender()
        {
            GreenScreenManager.instance.DisplayPlayerScreen();
        }

        void OnPostRender()
        {
            GreenScreenManager.instance.DisplayCameraScreen();
        }
    }

}