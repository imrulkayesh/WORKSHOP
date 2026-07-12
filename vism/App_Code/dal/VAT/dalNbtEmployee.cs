using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;

/// <summary>
/// Summary description for dalNbtEmployee
/// </summary>
public class dalNbtEmployee
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public int Insert(string code, string name, string cont, string email, string remark, int stat)
    {
        return dm.ExecuteNonQuery(@"Insert into NBT_EMPM(EMPM_ID, EMPM_CODE,EMPM_NAME,EMPM_CONT, EMPM_EMAL, EMPM_REMK, EMPM_STAT)
        VALUES(SQ_NBT_EMPM.NEXTVAL,'" + code + "','" + name + "','" + cont + "','" + email + "','" + remark + "','" + stat + "')");
    }
    public int Update(int id, string code, string name, string cont, string email, string remark, int stat)
    {
        return dm.ExecuteNonQuery("update NBT_EMPM set EMPM_CODE='" + code + "',EMPM_NAME='" + name + "',EMPM_CONT='" + cont + "',EMPM_EMAL='" + email + "',EMPM_REMK='" + remark + "',EMPM_STAT='" + stat + "' WHERE EMPM_ID=" + id);
    }

    public DataTable GetData()
    {
        return dm.ExecuteQuery(@"SELECT * FROM NBT_EMPM");
    }
    
}