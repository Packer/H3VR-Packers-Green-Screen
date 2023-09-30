using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Packer
{

	public class GreenScreenManager : MonoBehaviour
	{
		public static GreenScreenManager instance;
		public Material greenscreenMaterial;

		public MeshRenderer[] meshRender;

		// Use this for initialization
		void Start()
		{
			instance = this;
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void SetScreen(ScreenSet input)
        {
            Debug.Log("Changing texture!");
            greenscreenMaterial.SetColor("_Color", input.color);

			if (input.texture != null)
				greenscreenMaterial.SetTexture("_MainTex", input.texture);


			for (int i = 0; i < meshRender.Length; i++)
            {
                if (input.texture != null)
                    meshRender[i].materials[0].SetTexture("_MainTex", input.texture);

                meshRender[i].materials[0].SetColor("_Color", input.color);
            }
        }
	}
}