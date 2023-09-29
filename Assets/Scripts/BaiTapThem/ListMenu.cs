using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ListMenu : MonoBehaviour
{
    private Object[] texturesArr;
    public GameObject BttDan;
    public ScrollRect ScrollMenu;
    public class item
    {
        public string Anh;
        public string Ten;
        public item(string newAnh, string newTen)
        {
            Anh = newAnh;
            Ten = newTen;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Load resources(texture) to convert to sprite 
        texturesArr = Resources.LoadAll("Textures", typeof(Texture2D));

        //Create list for Item list
        List<item> DanhSachItem = new List<item>();
        item _item;
        _item = new item("Gun1t","Dan loai 1");
        DanhSachItem.Add(_item);

        _item = new item("Gun2t","Dan loai 2");
        DanhSachItem.Add(_item);

        //Spawn image to list from exist GameObject
        GameObject NewBttDan;
        Texture2D texture=null;
        foreach(item it in DanhSachItem)
        {
            NewBttDan = Instantiate(BttDan,ScrollMenu.transform.GetChild(0).GetChild(0).transform, true);
            NewBttDan.SetActive(true);
            NewBttDan.transform.GetChild(0).GetComponent<Text>().text = it.Ten;
            for(int i = 0; i < texturesArr.Length; i++)
            {
                if(texturesArr[i].name == it.Anh)
                {   
                    texture = texturesArr[i] as Texture2D;
                    Rect rec = new Rect( 0, 0, texture.width, texture.height); 
                    NewBttDan.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
