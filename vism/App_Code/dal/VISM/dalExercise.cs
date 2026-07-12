using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalExercise
/// </summary>
public class dalExercise
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();

    public dalExercise()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Insert(string cname)
    {
       return dm.ExecuteNonQuery(@"INSERT INTO RVS_EXERCISE(ID,NAME,CREATEBY)
        VALUES(SQ_RVS_EXERCISE.NEXTVAL,'" + cname + "', '"+SessionManager.SessionName.StaffId+"') ");
    }

    public int Update(int id, string cname)
    {
        return dm.ExecuteNonQuery("update RVS_EXERCISE set NAME= '" + cname + "' where ID=" + id);
    }

    public bool CheckDuplicate(string cname)
    {
         DataTable dt = dm.ExecuteQuery("select * from RVS_Exercise where Name='"+cname+"'");
        return true;
        //if (dt.Row.Count > 0)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}
    }

    public DataTable GateByID(int id)
    {
        return dm.ExecuteQuery("select * from RVS_EXERCISE where ID=" + id);
    }

}