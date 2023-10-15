using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FistVR;

namespace Packer
{

    public class GreenScreenManager : MonoBehaviour
    {
        public static GreenScreenManager instance;
        public Material greenscreenMaterial;

        public ScreenSet playerScreen;
        public ScreenSet cameraScreen;

        public MeshRenderer[] meshRender;

        [Header("World Light")]
        public FVRPhysicalObject sunDirection;
        public Transform worldGizmo;

        public GameObject[] pageList;

        private CameraMatManager mainCamera;
        private Camera[] cameras;

        [Header("Lights")]
        public Light sunLight;
        public Slider sunIntensitySlider;
        public Slider sunR;
        public Slider sunG;
        public Slider sunB;
        public Image sunPreview;


        public ScreenSet[] screenSets;
        public GameObject buttonPrefab;
        public Transform playerTab;
        public Transform cameraTab;

        // Use this for initialization
        void Start()
        {
            instance = this;
            OpenPage(0);
            UpdateSunRGB();
            UpdateSunSlider();

            GenerateButtons();
            SetPlayerScreen(1);
            SetCameraScreen(1);
        }

        // Update is called once per frame
        void Update()
        {
            if (mainCamera == null)
                SetupCamera();


            //Sun Control
            if (!sunDirection.IsHeld)
            {
                sunDirection.transform.rotation = sunLight.transform.rotation;
            }
            else
            {
                sunLight.transform.rotation = sunDirection.gameObject.transform.rotation;
            }

            sunDirection.transform.position = worldGizmo.transform.position;
            worldGizmo.rotation = Quaternion.identity;
        }

        public void OpenPage(int i)
        {
            CloseAllPages();
            pageList[i].SetActive(true);
        }

        public void CloseAllPages()
        {
            for (int i = 0; i < pageList.Length; i++)
            {
                pageList[i].SetActive(false);
            }
        }

        public void SetPlayerScreen(int input)
        {
            SM.PlayGlobalUISound(SM.GlobalUISound.Beep, transform.position);
            playerScreen = screenSets[input];
        }

        public void SetCameraScreen(int input)
        {
            SM.PlayGlobalUISound(SM.GlobalUISound.Beep, transform.position);
            cameraScreen = screenSets[input];
        }

        public void DisplayPlayerScreen()
        {
            if (playerScreen == null)
                return;

            SetScreen(playerScreen);
            //Debug.Log("Player Set");
        }

        public void DisplayCameraScreen()
        {
            if (cameraScreen == null)
                return;

            SetScreen(cameraScreen);
            //Debug.Log("Camera Set");
        }

        public void GenerateButtons()
        {
            //Player Camera
            for (int i = 0; i < screenSets.Length; i++)
            {
                Button btn = Instantiate(buttonPrefab, playerTab).GetComponent<Button>();
                btn.gameObject.SetActive(true);

                ColorButton colorBtn = btn.gameObject.AddComponent<ColorButton>();
                colorBtn.index = i;

                //btn.onClick.AddListener(delegate { SetCameraScreen(i); });
                btn.onClick.AddListener(() => colorBtn.SetPlayerDisplay());


                if (screenSets[i].texture != null)
                    btn.image.sprite = Sprite.Create(screenSets[i].texture, new Rect(0, 0, screenSets[i].texture.width, screenSets[i].texture.height), new Vector2(0, 0), 100.0f);
                btn.image.color = screenSets[i].color;

                FVRPointableButton point = btn.GetComponent<FVRPointableButton>();

                point.ColorSelected = screenSets[i].color;
                point.ColorUnselected = screenSets[i].color;
            }

            //Spectator Screen
            for (int x = 0; x < screenSets.Length; x++)
            {
                Button btn = Instantiate(buttonPrefab, cameraTab).GetComponent<Button>();
                btn.gameObject.SetActive(true);

                ColorButton colorBtn = btn.gameObject.AddComponent<ColorButton>();
                colorBtn.index = x;

                //btn.onClick.AddListener(delegate { SetPlayerScreen(x); });
                //btn.onClick.AddListener(() => SetPlayerScreen(x));
                btn.onClick.AddListener(() => colorBtn.SetCameraDisplay());

                if (screenSets[x].texture != null)
                    btn.image.sprite = Sprite.Create(screenSets[x].texture, new Rect(0, 0, screenSets[x].texture.width, screenSets[x].texture.height), new Vector2(0, 0), 100.0f);
                btn.image.color = screenSets[x].color;

                FVRPointableButton point = btn.GetComponent<FVRPointableButton>();

                point.ColorSelected = screenSets[x].color;
                point.ColorUnselected = screenSets[x].color;
            }

        }

		void SetScreen(ScreenSet input)
        {
            //Debug.Log("Changing texture!");
            greenscreenMaterial.SetColor("_Color", input.color);
			greenscreenMaterial.SetTexture("_MainTex", input.texture);



            for (int i = 0; i < meshRender.Length; i++)
            {
                meshRender[i].materials[0].SetColor("_Color", input.color);
                meshRender[i].materials[0].SetTexture("_MainTex", input.texture);
            }
        }

        void SetupCamera()
        {
            cameras = FindObjectsOfType<Camera>();

            if (cameras.Length > 0)
            {

                Debug.Log("Green screen Camera found!");
                mainCamera = cameras[0].gameObject.AddComponent<CameraMatManager>();
            }
        }

        public void AdjustIntensity(float amount)
        {
            sunIntensitySlider.value += amount;
            UpdateSunSlider();
        }

        public void AdjustR(float amount)
        {
            sunR.value += amount;
            UpdateSunRGB();
        }

        public void AdjustG(float amount)
        {
            sunG.value += amount;
            UpdateSunRGB();
        }

        public void AdjustB(float amount)
        { 
            sunB.value += amount;
            UpdateSunRGB();
        }

        public void UpdateSunRGB()
        {
            sunLight.color = new Color(sunR.value, sunG.value, sunB.value);
            sunPreview.color = sunLight.color;
        }

        public void UpdateSunSlider()
        {
            sunLight.intensity = sunIntensitySlider.value;
        }
    }
}