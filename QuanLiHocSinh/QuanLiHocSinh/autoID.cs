using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuanLiHocSinh
{
    class autoID
    {
        public static string NextID(string lastID, string prefixID)
        {
            int nextID = int.Parse(lastID.Remove(0, prefixID.Length)) + 1;
            int lengthNumberID = lastID.Length - prefixID.Length;
            string zeroNumber = "";
            for (int i = 1; i <= lengthNumberID; i++)
            {
                if (nextID < Math.Pow(10, i))
                {
                    for (int j = 1; j <= lengthNumberID - i; i++)
                    {
                        zeroNumber += "0";
                    }
                    return prefixID + zeroNumber + nextID.ToString();

                }
            }
            return prefixID + nextID;
        }
        public string NextID2()
        {
            string a = "";
            return a;


        }
    }
}
