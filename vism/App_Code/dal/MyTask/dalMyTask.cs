using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


public class dalMyTask
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMyTask()
    {
    }
    public int Update(int id, string taskTitle, string Details, string AssignedTo, int TaskType, string Approver)
    {
        return dm.ExecuteNonQuery("update MYT_TASK set APPROVERSTAFFID='" + Approver + "', TASKTITLE='" + taskTitle + "',DETAILS='" + Details + "',ASSIGNEDTO='" + AssignedTo + "',ISOPEN=0,TASKTYPEID='" + TaskType + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }

    public DataTable GetTaskDetails(int TaskId)
    {
        return dm.ExecuteQuery(@"SELECT  T.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,TT.TASKTYPE FROM MYT_TASK T
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=T.ASSIGNEDTO
        LEFT OUTER JOIN MYT_TASKTYPE TT ON TT.ID=T.TASKTYPEID
        WHERE T.ID=" + TaskId);
    }

    public DataTable GetMyTaskList(int ApproveStatus)
    {
        return dm.ExecuteQuery(@"SELECT  T.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,TT.TASKTYPE FROM MYT_TASK T
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=T.ASSIGNEDTO
        LEFT OUTER JOIN MYT_TASKTYPE TT ON TT.ID=T.TASKTYPEID
        WHERE T.ISAPPROVED='" + ApproveStatus + "' AND T.APPROVERSTAFFID=" + SessionManager.SessionName.StaffId);
    }
    public DataTable GetAssignedList(int ApproveStatus)
    {
        return dm.ExecuteQuery(@"SELECT  T.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,TT.TASKTYPE FROM MYT_TASK T
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=T.ASSIGNEDTO
        LEFT OUTER JOIN MYT_TASKTYPE TT ON TT.ID=T.TASKTYPEID
        WHERE T.ISAPPROVED='" + ApproveStatus + "' AND T.ASSIGNEDTO=" + SessionManager.SessionName.StaffId);
    }
    public DataTable GetTask(string staffId)
    {
        return dm.ExecuteQuery(@"SELECT P.*,M.STARTTIME FROM MT_PERTICIPANT P INNER JOIN MT_MEETING M ON M.ID=P.MEETINGID
        WHERE P.STAFFID = '" + staffId + "' AND LENGTH(P.TASK) > 0");
    }


    public DataTable GetTaskList(int myTaskId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM MYT_TASKITEM WHERE TASKID=" + myTaskId);
    }

    public void UpdateProgress(int ID, int Progress)
    {
        dm.ExecuteNonQuery(@"update MYT_TASKITEM set PROGRESS='" + Progress + "',UPDATEDATE=SYSDATE WHERE ID='" + ID + "'");
    }

    public void InsertTaskItem(int myTaskId, string title, int Duration, string details, string taskAttachment)
    {
        dm.ExecuteNonQuery(@"Insert into MYT_TASKITEM(ID,TASKID,ITEMTITLE,ITEMDETAILS,ATTACHMENT,DURATION,UPDATEDATE)
        VALUES(SQ_MYT_TASK.NEXTVAL,'" + myTaskId + "','" + title + "','" + details + "','" + taskAttachment + "','" + Duration + "',SYSDATE)");
    }
    public DataTable GetTask(int MytId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM MYT_TASKITEM TASKID=" + MytId);
    }

    public void ApproveTask(int myTaskId, int ApproveStatus)
    {
        dm.ExecuteNonQuery("update MYT_TASK set ISAPPROVED='" + ApproveStatus + "',APPROVERSTAFFID='" + SessionManager.SessionName.StaffId + "',APPROVEDATE=SYSDATE WHERE ID=" + myTaskId);
    }
}