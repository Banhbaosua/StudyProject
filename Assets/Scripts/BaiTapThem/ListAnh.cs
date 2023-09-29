using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListAnh : MonoBehaviour
{
    public GameObject prefabbtnAnh;
    //tạo biến để gán prefab
    private Object[] textureArr;
    //tạo array để load toàn bộ hình trong resourses
    public ScrollRect ScrollAnh;
    //biến ScrollRect để quản lý scrollview
    public Button btnAdd;
    //biến cho button Add
    public Button btnRemove;
    //biến cho button Remove
    public int count = 0;
    public bool imgCanvas=true;
    public int indexCanvas;
    //biến đếm dùng cho Add và Remove
    public GameObject[] newbtnAnh;
    //khai báo array cho Button được tạo ra trong scroll view
    List<HinhAnh> DanhSachAnh = new List<HinhAnh>();
    //khai báo danh sách chứa các ảnh và tên ảnh
    Texture2D texture=null;
    //biến dùng để chuyển từ  texture sang sprite
    public Canvas canvas;
    //tạo biến canvas để thay đổi image cho canvas image
    public class HinhAnh
    {
        public string Anh;
        public string Ten;
        public HinhAnh(string newAnh, string newTen)
        {
            Anh = newAnh;
            Ten = newTen;
        }
    } 
    // Start is called before the first frame update
    void Start()
    {
        //load toàn bộ ảnh từ resourses vào array textureArr
        textureArr = Resources.LoadAll("Textures", typeof(Texture2D));
        //khai bố độ dài array Button bằng số ảnh có trong resoures
        newbtnAnh = new GameObject[textureArr.Length];
        //Thêm thông tin hình ảnh vào array danh sách ảnh
        HinhAnh _HinhAnh;
        _HinhAnh = new HinhAnh("Gun1t","Hinh 1");
        DanhSachAnh.Add(_HinhAnh);
        _HinhAnh = new HinhAnh("Gun2t","Hinh 2");
        DanhSachAnh.Add(_HinhAnh);
        _HinhAnh = new HinhAnh("Gun3t","Hinh 3");
        DanhSachAnh.Add(_HinhAnh);
        _HinhAnh = new HinhAnh("Gun4t","Hinh 4");
        DanhSachAnh.Add(_HinhAnh);
        //event click thêm button
        btnAdd.GetComponent<Button>().onClick.AddListener(delegate{
            if(count < DanhSachAnh.Count)
            {
                AddImageAndName(count);
                count++;
            }
        });

        //event click xóa button
        btnRemove.GetComponent<Button>().onClick.AddListener(delegate{
            if(count >0)
            {
                if(indexCanvas==count-1)
                    canvas.transform.GetChild(3).GetComponent<Image>().sprite = null;
                Destroy(newbtnAnh[count-1]);
                count--;
            }
        });
    }
    //Hàm thêm ảnh và tên cho button
    public void AddImageAndName(int a)
    {
        //tạo button thứ a từ prefab button đã tạo trước đó, vị trí nằm trong scrollview 
        newbtnAnh[a] = Instantiate(prefabbtnAnh,ScrollAnh.transform.GetChild(0).GetChild(0).transform,true);
        //Gán text cho button là tên của ảnh
        newbtnAnh[a].transform.GetChild(0).GetComponent<Text>().text = DanhSachAnh[a].Ten;
        //hiện nút
        newbtnAnh[a].SetActive(true);
        //Gán ảnh cho button, chuyển từ texture sang sprite
        for(int i = 0; i < textureArr.Length;i++)
        {
            if(textureArr[i].name==DanhSachAnh[a].Anh)
            {
                texture = textureArr[i] as Texture2D;
                newbtnAnh[a].transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height),new Vector2(0.5f,0.5f),100);
                break;
            }
        }
        //Gán event onclick cho button vừa được tạo ra, khi nhấn vào button sẽ gán ảnh của button sang cho canvas image
        newbtnAnh[a].GetComponent<Button>().onClick.AddListener(delegate{
            canvas.transform.GetChild(3).GetComponent<Image>().sprite = newbtnAnh[a].transform.GetChild(1).GetComponent<Image>().sprite;
            indexCanvas= a;
            
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
