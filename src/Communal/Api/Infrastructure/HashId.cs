using System;
using HashidsNet;

namespace Communal.Api.Infrastructure
{
    public static class HashId
    {
        private const string HashSalt =
            "harponey-ualag812R9tG67gDfjHdfFkGh123asfjFHbKhg6J2KWe3r4#vgwsrKJa2faL25HFJ";
        private const int HashLength = 12;
        private const string HashAlphabets = "abcdefghklmnoprstuvw123456789";

        private static readonly Hashids Hasher =
            new Hashids(HashSalt, HashLength, HashAlphabets);

        public static string Encode(this int id)
        {
            return Hasher.Encode(id);
        }

        public static int Decode(this string eid)
        {
            try
            {
                return Hasher.Decode(eid)[0];
            }
            catch
            {
                return -1;
                throw new ArgumentException("Invalid encoded Id value");
            }
        }
    }
}