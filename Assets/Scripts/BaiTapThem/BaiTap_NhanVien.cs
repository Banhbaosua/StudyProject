using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaiTap_NhanVien : MonoBehaviour 
{
	public class NhanVien
	{
		//public string Khoa {get; set; }
		public string HoTen {get; set; }
		public string GioiTinh {get; set; }
		public int Tuoi { get; set; }
	}
	List <NhanVien> DanhSachNhanVien = new List<NhanVien> ();

	// Use this for initialization
	void Start () 
	{
		
		/*Dictionary <string, List <NhanVien>> BenhVienTamTri = new Dictionary <string, List<NhanVien>> ();
		// var DanhSachKhoa = new Dictionary <string, List <NhanVien>> ();

		NhanVien nhanvien_ngoai;

		 DanhSachNhanVien = new List<NhanVien>();
		nhanvien_ngoai = new NhanVien ();
		nhanvien_ngoai.HoTen = "Nguyen Van A";
		nhanvien_ngoai.GioiTinh = "Nam";
		nhanvien_ngoai.Tuoi = 28;
		DanhSachNhanVien.Add(DanhSachNhanVien);

		nhanvien_ngoai = new NhanVien ();
		nhanvien_ngoai.HoTen = "Nguyen Thi Y";
		nhanvien_ngoai.GioiTinh = "Nu";
		nhanvien_ngoai.Tuoi = 25;
		DanhSachNhanVien.Add (DanhSachNhanVien);

		BenhVienTamTri.Add ("KhoaNgoai", lstNhanvien_ngoai);

		NhanVien nhanvien_noi;

		DanhSachNhanVien = new List<NhanVien>();
		nhanvien_noi = new NhanVien ();
		nhanvien_noi.HoTen = "Nguyen Van B";
		nhanvien_noi.GioiTinh = "Nam";
		nhanvien_noi.Tuoi = 29;
		lstNhanvien_noi.Add (DanhSachNhanVien);

		nhanvien_noi = new NhanVien ();
		nhanvien_noi.HoTen = "Nguyen Thi X";
		nhanvien_noi.GioiTinh = "Nu";
		nhanvien_noi.Tuoi = 26;
		lstNhanvien_noi.Add (DanhSachNhanVien);

		BenhVienTamTri.Add ("KhoaNoi", lstNhanvien_noi);

		NhanVien nhanvien_nhi;

		DanhSachNhanVien = new List<NhanVien>();
		nhanvien_nhi = new NhanVien ();
		nhanvien_nhi.HoTen = "Nguyen Van C";
		nhanvien_nhi.GioiTinh = "Nam";
		nhanvien_nhi.Tuoi = 27;
		lstNhanvien_nhi.Add (nhanvien_nhi);

		nhanvien_nhi = new NhanVien ();
		nhanvien_nhi.HoTen = "Nguyen Thi Z";
		nhanvien_nhi.GioiTinh = "Nu";
		nhanvien_nhi.Tuoi = 25;
		lstNhanvien_nhi.Add (nhanvien_nhi);

		BenhVienTamTri.Add ("KhoaNhi", DanhSachNhanVien);

		foreach (KeyValuePair <string, List <NhanVien>> item in BenhVienTamTri)
		{
			Debug.Log (item.Key);

			foreach (NhanVien nvk in item.Value)
			{
				Debug.Log ("--->" + nvk.HoTen + " ***" + nvk.GioiTinh + "****" + nvk.Tuoi );
			}
		}*/
	}
	// Update is called once per frame
	void Update () 
	{
		
	}
}
