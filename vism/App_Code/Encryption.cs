using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public class Encryption
{
	public Encryption()
	{
	}
    public static string EncryptionWithSh(string txts)
    {
        SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
        sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(txts));
        byte[] re = sh.Hash;
        StringBuilder sb = new StringBuilder();
        foreach (byte b in re)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}