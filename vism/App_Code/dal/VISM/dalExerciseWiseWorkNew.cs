using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalExerciseWiseWorkNew
/// </summary>
public class dalExerciseWiseWorkNew
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalExerciseWiseWorkNew()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataTable GetByExerciseID(int ExerciseID)
    {
        return dm.ExecuteQuery("select * from EXERCISEWISEWORK where EXERCISETYPE= " + ExerciseID);
    }

    public int Insert(string tbxExWorkName, int ddExercise)
    {
        return dm.ExecuteNonQuery(@"insert into EXERCISEWISEWORK(ID,EXERCISETYPE,WORKNAME,CREATEBY,CREATEDATE)
        values (SQ_EXERCISEWISEWORK.NEXTVAL,'" + ddExercise + "', '" + tbxExWorkName + "','"+ SessionManager.SessionName.StaffId +"', SYSDATE)");
    }

    public bool DuplicateCheck(string tbxExWorkName, int ddExercise)
    {
        DataTable dt = dm.ExecuteQuery("select * from EXERCISEWISEWORK where WORKNAME='" + tbxExWorkName + "' and EXERCISETYPE='" + ddExercise + "'");
        if (dt.Rows.Count>0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}