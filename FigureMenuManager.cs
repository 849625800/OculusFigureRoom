using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FigureMenuManager : MonoBehaviour
{
    public int MaxFigureAmount = 9;
    private int FigureCount = 0;
    public Transform figureBornPosition;
    public Transform itemBornPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadFiguresBar());
        //this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddScrollButton(Texture2D avatar, string name, string figurePath) 
    {
        //load scroll button prefeb and hierorchy.
        GameObject figureButton = Instantiate(Resources.Load<GameObject>("Prefeb/Toggle"));

        //initialize button with icon and name.
        RawImage[] image = figureButton.GetComponentsInChildren<RawImage>();
        image[1].texture = avatar;

        TextMeshProUGUI text = figureButton.GetComponentInChildren(typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
        text.SetText(name);

        //set event listeners, poke to create or delete figure.
        Toggle t = figureButton.GetComponent<Toggle>();
        t.onValueChanged.AddListener(delegate {
            
            if (t.isOn)
            {
                LoadFigureByPath(figurePath,name);

            }
            else 
            {
                DeleteFigureByName(name);
            }
        });

        //set button as child of scroll bar.
        figureButton.transform.SetParent(transform.Find("CurvedUnityCanvas/Unity Canvas/LeftSide/Scroll View/Viewport/Content"), false);

    }

    //void LoadScrollBar()
    //{
    //    Get the path of the Levels folder from Resources
    //    string assetsFolderPath = Application.dataPath;
    //    string folder = assetsFolderPath + "/Resources/GenshinModels";

    //    string[] rawNames = Directory.GetFiles(folder, "*.fbx", SearchOption.AllDirectories);

    //    foreach (string s in rawNames) 
    //    {
    //        string[] ss = s.Split("\\");
    //        string withSuffix = ss[ss.Length - 1];
    //        string name = withSuffix.Substring(0, withSuffix.Length - 4);

    //        get avatars and add a button with name and avatars.

    //        string resourcesRawPath = s.Split("Resources/")[1];
    //        string resourcesPath = resourcesRawPath.Substring(0, resourcesRawPath.Length - withSuffix.Length) + "avatar";

    //        Texture2D avatar = Resources.Load<Texture2D>(resourcesPath);

    //        if (avatar == null)
    //        {
    //            avatar = Resources.Load<Texture2D>("GenshinModels/default");
    //        }

    //        string path = resourcesRawPath.Substring(0, resourcesRawPath.Length - 4);
    //        AddScrollButton(avatar, name, path);
            
    //    }

    //}

    IEnumerator LoadFiguresBar()
    {
        string allPaths = Instantiate(Resources.Load<TextAsset>("GenshinModels/FigurePath")).text;

        string[] paths = allPaths.Split("\n");

        foreach (string path in paths)
        {
            if (path.Length == 0) continue;

            path.Replace("\n", "");

            string name = path.Split(@"\")[path.Split(@"\").Length - 1];
            name = name.Substring(0, name.Length - 1);

            Texture2D avatar = Resources.Load<Texture2D>(path.Substring(0, path.Length - name.Length - 1) + "avatar");

            if (avatar == null)
            {
                avatar = Resources.Load<Texture2D>("GenshinModels/default");
            }

            AddScrollButton(avatar, name, path);
            yield return null;
        }
    }

    void LoadFigureByPath(string path, string name)
    {
        if (FigureCount >= MaxFigureAmount) return;

        GameObject newFigure = Instantiate(Resources.Load<GameObject>(path[0..^1]));

        newFigure.layer = 6; //set to interaction layer.

        if (name.Length >= 4)
        {
            if (name.Substring(name.Length - 4, 4) == "(µÀ¾ß)")
            {
                newFigure.tag = "FigureItem";
                Vector3 born = itemBornPosition.position + new Vector3(0, 1, 0);
                newFigure.transform.position = born;
            }
            else 
            {
                newFigure.tag = "Figure";
                newFigure.transform.position = figureBornPosition.position;
            }

        }
        else
        {
            newFigure.tag = "Figure";
            newFigure.transform.position = figureBornPosition.position;
        }

        //add a Figure object to this new figure.
        newFigure.AddComponent<Figure>();
        newFigure.GetComponent<Figure>().figureName = name;
        newFigure.transform.SetParent(FindObjectOfType<FigureManager>().transform, false);

        //newFigure.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);


        FigureCount += 1;
    }

    void DeleteFigureByName(string name)
    {
        if (FigureCount <= 0) return;


        GameObject container = GameObject.Find("Figures");
        Figure[] allFigures = container.GetComponentsInChildren<Figure>();

        foreach (Figure f in allFigures)
        {
            if (f.gameObject.name == (name + "(Clone)"))
            {
                f.setContainerEmpty();
                Destroy(f.gameObject);
            }
        }
        FigureCount -= 1;
    }

}
