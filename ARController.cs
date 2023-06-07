using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Vuforia.EditorClasses;
public class ARController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject GinjalToScale, ParuToScale, HatiToScale, KulitToScale; // objek yang akan diubah skala
    [SerializeField]
    private float scaleSpeed = 0.1f; // kecepatan perubahan skala // skala maksimal objek
    [SerializeField]
    private Button scaleUpButton; // tombol untuk memperbesar objek
    [SerializeField]
    private Button scaleDownButton; // tombol untuk memperkecil objek
    public float rotationSpeed = 100f;
    public Button rotateLeftButton;
    public Text Informastion;
    public Image infoImage, ARInfoImage, ARAlveolus, ARNefron;
    public int numberOFInfo;
    private bool isRotatingLeft = false, tracked = false, infos3D = false;
    public string case3DObject;
    public int numberCallInfo;
    public Button[] buttonInfo;
    public Button btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btn10, btn11, btn12;

    public Canvas ICScan, ICMainAr;
    public DefaultTrackableEventHandler Hati, Ginjal, Paru, Kulit;
    //unity 2019.1.16f
    private void Start()

    {
        rotateLeftButton.onClick.AddListener(StartRotatingLeft);
        // Menambahkan event listener pada tombol scaleUpButton
        scaleUpButton.onClick.AddListener(ScaleUp);
        // Menambahkan event listener pada tombol scaleDownButton
        scaleDownButton.onClick.AddListener(ScaleDown);
        ICShow(true, false);
        setVisible(false);
        VisibilityImageOnInfo(false, false, false);
        LoppHideButtonInfo();

        btn1.onClick.AddListener(one);
        // Menambahkan event listener pada tombol btn no 1 informasi
        btn2.onClick.AddListener(two);
        // Menambahkan event listener pada tombol btn no 2 informasi
        btn3.onClick.AddListener(three);
        // Menambahkan event listener pada tombol btn no 3 informasi
        btn4.onClick.AddListener(four);
        // Menambahkan event listener pada tombol btn no 4 informasi
        btn5.onClick.AddListener(five);
        // Menambahkan event listener pada tombol btn no 5 informasi
        btn6.onClick.AddListener(six);
        btn7.onClick.AddListener(seven);
        // Menambahkan event listener pada tombol btn no 6 informasi
        btn8.onClick.AddListener(eight);
        // Menambahkan event listener pada tombol btn no 6 informasi
        btn9.onClick.AddListener(nine);
        // Menambahkan event listener pada tombol btn no 6 informasi
        btn10.onClick.AddListener(ten);
        // Menambahkan event listener pada tombol btn no 6 informasi
        btn11.onClick.AddListener(eleven);
        // Menambahkan event listener pada tombol btn no 6 informasi
        btn12.onClick.AddListener(tweelve);
        // Menambahkan event listener pada tombol btn no 6 informasi
        // btn7.onClick.AddListener(one);
        // btn8.onClick.AddListener(one);
        // Load file JSON dari direktori Resources
        Hati.OnTargetFound.AddListener(onTractHatiFounded);
        Ginjal.OnTargetFound.AddListener(onTractGinjalFounded);
        Kulit.OnTargetFound.AddListener(onTractKulitFounded);
        Paru.OnTargetFound.AddListener(onTractParuFounded);
        Hati.OnTargetLost.AddListener(onTrackedRemoved);
        Ginjal.OnTargetLost.AddListener(onTrackedRemoved);
        Kulit.OnTargetLost.AddListener(onTrackedRemoved);
        Paru.OnTargetLost.AddListener(onTrackedRemoved);

    }
    void Update()
    {
        // Check if the left or right button is being pressed
        if (isRotatingLeft)
        {
            switch (PlayerPrefs.GetString("3DName"))
            {
                case "Ginjal":
                    GinjalToScale.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    break;
                case "Paru":
                    ParuToScale.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    break;
                case "Hati":
                    HatiToScale.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    break;
                case "Kulit":
                    KulitToScale.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
                    break;
                default:
                    break;
            }

        }
        if (tracked == false)
        {
            ICShow(true, false);
            LoppHideButtonInfo();
            //Debug.Log("tracked You" + tracked);
        }
        if (tracked == true)
        {
            ICShow(false, true);
            LoppShowButtonInfo(numberOFInfo);
            ChooseNumberInfromation();

            Debug.Log("tracked Me" + numberOFInfo);
        }
    }

    private void ICShow(bool scan, bool main)
    {
        ICMainAr.gameObject.SetActive(main);
        ICScan.gameObject.SetActive(scan);
    }
    private void ScaleUp()
    {

        switch (PlayerPrefs.GetString("3DName"))
        {
            case "Ginjal":
                GinjalToScale.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            case "Paru":
                ParuToScale.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            case "Hati":
                HatiToScale.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            case "Kulit":
                KulitToScale.transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            default:
                break;
        }
    }

    private void ScaleDown()
    {
        switch (PlayerPrefs.GetString("3DName"))
        {
            case "Ginjal":
                GinjalToScale.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            case "Paru":
                ParuToScale.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            case "Hati":
                HatiToScale.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            case "Kulit":
                KulitToScale.transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
                break;
            default:
                break;
        }
        // Skala objek diperkecil
        // GinjalToScale.transform.localScale = Vector3.ClampMagnitude(objectToScale.transform.localScale, minScale);
        // Skala objek tidak boleh kurang dari skala minimal
    }
    public void onTractHatiFounded()
    {
        tracked = true;
        case3DObject = "Hati";
        PlayerPrefs.SetString("3DName", case3DObject);
        numberOFInfo = 7;
        
    }
    public void onTractParuFounded()
    {
        tracked = true;
        case3DObject = "Paru";
        PlayerPrefs.SetString("3DName", case3DObject);
        numberOFInfo = 5;
       
    }
    public void onTractKulitFounded()
    {
        tracked = true;
        case3DObject = "Kulit";
        PlayerPrefs.SetString("3DName", case3DObject);
        numberOFInfo = 7;
        
    }
    public void onTractGinjalFounded()
    {
        tracked = true;
        case3DObject = "Ginjal";
        PlayerPrefs.SetString("3DName", case3DObject);
        numberOFInfo = 5;
       
    }
    public void LoppShowButtonInfo(int btn)
    {
        for (int i = 0; i <= btn; i++)
        {
            buttonInfo[i].gameObject.SetActive(true);
        }
    }
    public void LoppHideButtonInfo()
    {
        for (int i = 0; i <= 11; i++)
        {
            buttonInfo[i].gameObject.SetActive(false);
        }
    }
    private void onTrackedRemoved()
    {
        //tracked = false;
        infos3D = false;
        // PlayerPrefs.DeleteAll();
       
    }
    public void StartRotatingLeft()
    {
        if (isRotatingLeft == false)
        {
            isRotatingLeft = true;

        }
        else
        {
            isRotatingLeft = false;
        }
    }
    private void VisibilityImageOnInfo(bool defaults, bool alveolus, bool nefron)
    {
        ARNefron.gameObject.SetActive(nefron);
        ARAlveolus.gameObject.SetActive(alveolus);
        ARInfoImage.gameObject.SetActive(defaults);
    }
    public void setVisible(bool info)
    {
        infoImage.gameObject.SetActive(info);
        Informastion.gameObject.SetActive(info);

    }
    // Update is called once per frame
    public void ChooseNumberInfromation()
    {
        switch (PlayerPrefs.GetString("3DName"))
        {
            case "Ginjal":
                // GinjalInformasi(numberCallInfo);
                break;
            case "Paru":
                // ParuInformasi(numberCallInfo);
                break;
            case "Hati":
                // HatiInformasi(numberCallInfo);
                break;
            case "Kulit":
                // KulitInformasi(numberCallInfo);
                break;
            default:
                break;
        }

    }

    public void one()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 0;
    }
    public void two()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 1;
    }
    public void three()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 2;
    }
    public void four()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 3;
    }
    public void five()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 4;
    }
    public void six()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 5;
    }
    public void seven()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 6;
    }
    public void eight()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 7;
    }
    public void nine()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 8;
    }
    public void ten()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 9;
    }
    public void eleven()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 10;
    }
    public void tweelve()
    {
        infos3D = true;
        setVisible(infos3D);
        numberCallInfo = 11;
    }


}

