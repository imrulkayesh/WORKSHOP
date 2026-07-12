using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalInsurancePolicy
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalInsurancePolicy()
    {
    }
    public int Insert(string name)
    {
        return dm.ExecuteNonQuery(@"Insert into RVS_INSURANCEPOLICY(ID,POLICYNAME)
        VALUES(SQ_RVS_INSURANCEPOLICY.NEXTVAL,'" + name + "')");
    }
    public int Update(int id, string name)
    {
        return dm.ExecuteNonQuery("update RVS_INSURANCEPOLICY set POLICYNAME='" + name + "' WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from RVS_INSURANCEPOLICY WHERE ID=" + id);
    }
    public bool CheckDuplicate(string name)
    {
        DataTable dt = dm.ExecuteQuery("Select * from RVS_INSURANCEPOLICY WHERE POLICYNAME='" + name + "'");
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