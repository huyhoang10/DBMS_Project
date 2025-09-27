using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_DBMS.Model;

namespace Final_Project_DBMS.Utils
{
    internal static class Constants
    {
        public static Product choosedProduct = new Product();

        public static Dictionary<String,String> dicAttribute = new Dictionary<String, String>() {
            { "Thương hiệu","ThuongHieu" },
            {"Phân loại","PhanLoai" },
            {"Màu sắc","MauSac" },
            {"Chất liệu","ChatLieu" },
            {"Đơn vị tính","DonViTinh" },
        };

        public static Dictionary<String, int> dicBrand = new Dictionary<String, int>() {
        };
        public static Dictionary<String, int> dicCategory = new Dictionary<String, int>()
        {
        };
        public static Dictionary<String, int> dicColor = new Dictionary<String, int>()
        {
        };
        public static Dictionary<String, int> dicMaterial = new Dictionary<String, int>()
        {
        };
        public static Dictionary<String, int> dicUnit = new Dictionary<String, int>()
        {
        };

    }
}
