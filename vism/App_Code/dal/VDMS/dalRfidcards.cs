using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalRfidcards
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalRfidcards()
    {
    }
    public int Insert(string cardtagid, string cardno, string chassisno)
    {
        return dm.ExecuteNonQuery(@"Insert into VD_RFIDCARDS(ID,CARDTAGID,CARDNO,CHASSISNO)
        VALUES(SQ_VD_RFIDCARDS.NEXTVAL,'" + cardtagid + "','" + cardno + "','" + chassisno + "')");
    }
    public int Update(int id, string cardtagid, string cardno, string chassisno)
    {
        return dm.ExecuteNonQuery("update VD_RFIDCARDS set CARDTAGID='" + cardtagid + "',CARDNO='" + cardno + "',CHASSISNO='" + chassisno + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from VD_RFIDCARDS WHERE ID=" + id);
    }
    public bool CheckDuplicate(string cardtagid)
    {
        DataTable dt = dm.ExecuteQuery("Select * from VD_RFIDCARDS WHERE CARDTAGID='" + cardtagid + "'");
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
