using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using System.Web.UI.WebControls;

public class Common
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public Common()
    {
    }

    public static bdoSessionInfo SessionInfo
    {
        get
        {
            try
            {
                if (HttpContext.Current.Session["SessionInfo"] != null)
                    return HttpContext.Current.Session["SessionInfo"] as bdoSessionInfo;
                else
                    return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["SessionInfo"] = value;
        }
    }
    public DataTable GetAll(string table)
    {
        return dm.ExecuteQuery("Select * FROM " + table);
    }

    public DataTable GetAllServiceType(string table)
    {
        return dm.ExecuteQuery("Select * FROM " + table + " order by CORDER ASC");
    }

    public DataTable GetAllServiceCat(string table)
    {
        return dm.ExecuteQuery("Select * FROM " + table);
    }

    public DataTable GetAllSCM(string table)
    {
        return dm.ExecuteQuery("Select * FROM " + table);
    }

    public DataTable GetAllFD(string UserId)
    {
        return dm.ExecuteQuery(@"Select D.*,VH.REGNO,wh.VISMWHNAME PUMPNAME,im.ITEMNAME
from RVS_FUELDMD D
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY 
INNER JOIN RVS_VEHICLE vh ON D.VEHICLEID=VH.ID
INNER JOIN RVS_VISMWH wh ON D.PUMPID = wh.ID
INNER JOIN RVS_ITEMMASTER im ON D.FUELID = im.ITEMCODE
WHERE D.CREATEDBY='"+ UserId + @"' and D.STATUSID='1' ORDER by D.UPDATEDATE DESC");
    }

    public DataTable GetAllFA(string UserId)
    {
        return dm.ExecuteQuery(@"Select D.*,CO.COMPANYNAME,VH.REGNO,wh.VISMWHNAME PUMPNAME,im.ITEMNAME
from RVS_FUELDMD D
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY 
INNER JOIN RVS_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
INNER JOIN BS_COMPANY CO ON A.COMPANYID=CO.ID 
INNER JOIN RVS_VEHICLE vh ON D.VEHICLEID=VH.ID
INNER JOIN RVS_VISMWH wh ON D.PUMPID = wh.ID
INNER JOIN RVS_ITEMMASTER im ON D.FUELID = im.ITEMCODE
where E.DEPARTMENTID=A.DEPARTMENTID
AND A.COMPANYID=E.COMPANYID
AND A.APPROVERID='" + UserId + "' and D.STATUSID='1'");
    }

    public DataTable GetAllAF(string UserId)
    {
        //        return dm.ExecuteQuery(@"select fm.TRANSACTIONID,co.COMPANYNAME, vh.REGNO,wh.VISMWHNAME PUMPNAME,fm.QTY,fm.AQTY,fm.GQTY,im.ITEMNAME  
        //from RVS_FUELDMD fm, RVS_VEHICLE vh,RVS_VISMWH wh, bs_company co, RVS_ITEMMASTER im
        //where fm.VEHICLEID = vh.ID
        //and fm.FUELID = im.ITEMCODE
        //and fm.PUMPID = wh.ID
        //and vh.companyid = co.ID
        //and fm.STATUSID = '2'");

        return dm.ExecuteQuery(@" select fm.TRANSACTIONID,co.COMPANYNAME, vh.REGNO,wh.VISMWHNAME PUMPNAME,fm.QTY,fm.AQTY,fm.GQTY,im.ITEMNAME,fm.REMARKS,fm.CREATEDATE C_DATE  
from RVS_FUELDMD fm, RVS_VEHICLE vh,RVS_VISMWH wh, bs_company co, RVS_ITEMMASTER im,EM_EMPLOYEE em
where fm.VEHICLEID = vh.ID
and fm.FUELID = im.ITEMCODE
and fm.PUMPID = wh.ID
and vh.companyid = co.ID
and em.BUSINESSCENTERID=wh.LOCATIONID
and wh.ID=fm.PUMPID
and em.STAFFID='" + UserId + @"'
and fm.STATUSID = '2'");
    }

    public DataTable GetAllAPPROVED(string UserId)
    {
        //        return dm.ExecuteQuery(@"Select D.*,d.UPDATEDATE A_DATE, CO.COMPANYNAME,VH.REGNO,wh.VISMWHNAME PUMPNAME,im.ITEMNAME,md.MODELNAME
        //from YA.RVS_FUELDMD D
        //INNER JOIN YA.EM_EMPLOYEE E ON E.USERID=D.CREATEDBY 
        //INNER JOIN YA.RVS_APPROVER A ON A.BUSINESSCENTERID=E.BUSINESSCENTERID
        //INNER JOIN YA.BS_COMPANY CO ON A.COMPANYID=CO.ID 
        //INNER JOIN YA.RVS_VEHICLE vh ON D.VEHICLEID=VH.ID
        //INNER JOIN YA.RVS_VISMWH wh ON D.PUMPID = wh.ID
        //INNER JOIN YA.RVS_ITEMMASTER im ON D.FUELID = im.ITEMCODE
        //INNER JOIN YA.RVS_MODEL md ON vh.MODELID=md.ID
        //where E.DEPARTMENTID=A.DEPARTMENTID
        //AND A.COMPANYID=E.COMPANYID
        //AND A.APPROVERID='"+ UserId + "' and D.STATUSID='2'");

        return dm.ExecuteQuery(@"Select D.*,d.UPDATEDATE A_DATE,VH.REGNO,wh.VISMWHNAME PUMPNAME,im.ITEMNAME,md.MODELNAME
from RVS_FUELDMD D
INNER JOIN EM_EMPLOYEE E ON E.USERID=D.CREATEDBY 
INNER JOIN RVS_VEHICLE vh ON D.VEHICLEID=VH.ID
INNER JOIN RVS_VISMWH wh ON D.PUMPID = wh.ID
INNER JOIN RVS_ITEMMASTER im ON D.FUELID = im.ITEMCODE
INNER JOIN RVS_MODEL md ON vh.MODELID=md.ID
WHERE D.CREATEDBY='" + UserId + @"' and D.STATUSID='2' ORDER by D.UPDATEDATE DESC");
    }







    public DataTable GetAllTechnician(int WorkshopId)
    {
        return dm.ExecuteQuery(@"SELECT M.STAFFID, T.TEAMNAME||'-'||E.EMPLOYEENAME TECHNICIAN FROM RVS_TEAMMEMBER M
    INNER JOIN EM_EMPLOYEE E ON E.STAFFID=M.STAFFID 
    INNER JOIN RVS_TEAM T ON T.ID=M.TEAMID WHERE M.WORKSHOPID = " + WorkshopId);
    }
    public DataTable Get_AllDesc(string table)
    {
        return dm.ExecuteQuery("Select * FROM " + table + " order by ID desc");
    }
    public int Delete(string table, int id)
    {
        //return dm.ExecuteNonQuery("DELETE FROM " + table + " WHERE ID=" + id);
        return dm.ExecuteNonQuery("update RVS_FUELDMD set STATUSID='99',UPDATEDBY='"+ SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID=" + id);
    }


    public int RejectCD(string table, string id)
    {
        //return dm.ExecuteNonQuery("DELETE FROM " + table + " WHERE ID=" + id);
        return dm.ExecuteNonQuery("update RVS_REQMASTER set STATUSID ='99',UPDATEDBY='" + SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID='"+ id +"' ");
    }

    public int ApproveCD(string table, string id)
    {
        //return dm.ExecuteNonQuery("DELETE FROM " + table + " WHERE ID=" + id);
        return dm.ExecuteNonQuery("update RVS_REQMASTER set STATUSID ='1',UPDATEDBY='" + SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID='" + id + "' ");
    }

    public int DeleteTeam(string table, int id)
    {
        return dm.ExecuteNonQuery("DELETE FROM " + table + " WHERE ID=" + id);
    }

    public int ToolsDelete(string table, int id)
    {
        return dm.ExecuteNonQuery("DELETE FROM " + table + " WHERE ID=" + id);
        //  return dm.ExecuteNonQuery("update YA.RVS_FUELDMD set STATUSID='99',UPDATEDBY='"+ SessionManager.SessionName.UserId + "',UPDATEDATE=SYSDATE where ID=" + id);
    }
    public int PersonIdByUserName(string userName)
    {
        dm.AddParameteres("@UserName", userName);
        DataTable dt = dm.ExecuteQuery("USP_PersonId_GetByUserName");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int GetLastRoll(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        DataTable dt = dm.ExecuteQuery("USP_Student_GetLastRoll");
        if (dt.Rows[0][0].ToString() != "")
            return Convert.ToInt32(dt.Rows[0][0]);
        return 1;
    }

    public static void LoadDropdown(DropDownList ddl, string table, int dataTextField, int dataValueField)
    {
        DataTable dt = new dalCommon().LoadDropdown(table);
        ddl.Items.Add(new ListItem("", ""));
        ddl.DataSource = dt;
        ddl.DataTextField = dt.Columns[dataTextField].ToString();
        ddl.DataValueField = dt.Columns[dataValueField].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("---Select---", string.Empty));
        ddl.SelectedIndex = 0;
    }
    public static void DropdownByCondition(DropDownList ddl, string table, string tableColumn, string whereby, int dataTextField, int dataValueField)
    {
        DataTable dt = new dalCommon().DropdownByCondition(table, tableColumn, whereby);
        ddl.Items.Add(new ListItem("", ""));
        ddl.DataSource = dt;
        ddl.DataTextField = dt.Columns[dataTextField].ToString();
        ddl.DataValueField = dt.Columns[dataValueField].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("---Select---", string.Empty));
        ddl.SelectedIndex = 0;
    }

    //public int PNupdate(string table, int id, int updatecn)
    //{
    //    return dm.ExecuteNonQuery("update YA.RVS_FUELDMD set NOOFPRINT='"+ updatecn + "' where ID=" + id);
    //}

    public int PNupdate(string table, string id, int updatecn)
    {
        return dm.ExecuteNonQuery("update RVS_FUELDMD set NOOFPRINT='" + updatecn + "' where ID=" + id);
    }

}