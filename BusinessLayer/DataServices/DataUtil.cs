using System;
using BusinessLayer.Interfaces;

namespace BusinessLayer.DataServices
{
    public static class DataUtil
    {
        public static string LoadArticle(string path)
        {
            return path;
        }

        public static string GenerateUniqueAddress<TKey>(IAddressedData<TKey> data, int length)
        {
            int iter = 0;
            string address;
            do address = Guid.NewGuid().ToString("N").Substring(0, length);
            while (data.GetByUniqueAddress(address) != null && iter++ < 500);
            return iter >= 500 ? null : address;
        }
    }
}