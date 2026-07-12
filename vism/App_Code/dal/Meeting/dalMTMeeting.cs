using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalMTMeeting
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMTMeeting()
    {
    }
    public int Update(int id, string meetingTitle, string Agenda, string ChairPerson, int MeetingTypeId)
    {
        return dm.ExecuteNonQuery("update MT_MEETING set MEETINGTITLE='" + meetingTitle + "',AGENDA='" + Agenda + "',CHAIRPERSONSTAFFID='" + ChairPerson + "',ISOPEN=0,MEETINGTYPEID='" + MeetingTypeId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }

    public DataTable GetMeetingDetails(int meetingId)
    {
        return dm.ExecuteQuery(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERNAME,B.CENTERCODE,MR.ROOMTITLE,T.MTYPENAME FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID
        LEFT OUTER JOIN MT_MEETINGTYPE T ON T.ID=M.MEETINGTYPEID
        WHERE M.ID=" + meetingId);
    }

    public void DeleteMeeting(int meetingId)
    {
        dm.ExecuteNonQuery(@"DELETE FROM MT_MEETING WHERE ID=" + meetingId);
        DeletePerticipants(meetingId);
        dm.ExecuteNonQuery(@"DELETE FROM MT_TRANSPORTDEMAND WHERE MEETINGID=" + meetingId);
        DeleteFoodDemand(meetingId);
    }

    public DataTable GetTask(string staffId)
    {
        return dm.ExecuteQuery(@"SELECT P.*,M.STARTTIME FROM MT_PERTICIPANT P INNER JOIN MT_MEETING M ON M.ID=P.MEETINGID
        WHERE P.STAFFID = '" + staffId + "' AND LENGTH(P.TASK) > 0");
    }

    public void ApproveRejectMeeting(int meetingId, int Status, string remarks)
    {
        dm.ExecuteNonQuery("update MT_MEETING set ISAPPROVED='" + Status + "',APPROVERSTAFFID='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + meetingId);
    }
    public void SendMeetingMail(int meetingId)
    {
        DataTable dt = dm.ExecuteQuery(@"SELECT (SELECT E.EMPLOYEENAME FROM EM_EMPLOYEE E WHERE RO.STAFFID=E.STAFFID) ORNAME,
        (SELECT E.OFFICEMOBILE FROM EM_EMPLOYEE E WHERE RO.STAFFID=E.STAFFID) ORMOBILE,
        (SELECT E.OFFICEMAIL FROM EM_EMPLOYEE E WHERE RO.STAFFID=E.STAFFID) OREMAIL,
        E.OFFICEMAIL,E.OFFICEMOBILE,E.EMPLOYEENAME,M.*,MR.*,B.CENTERCODE, MT.MTYPENAME,
         MD.DISCRIPTION, RO.STAFFID FROM MT_PERTICIPANT P 
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=P.STAFFID
        INNER JOIN MT_MEETING M ON M.ID=P.MEETINGID
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID
        INNER JOIN MT_MEETINGTYPE MT ON MT.ID = M.MEETINGTYPEID
        INNER JOIN MT_MTPERTICIPENT MD ON MD.MEETINGTYPEID = MT.ID 
        LEFT OUTER JOIN MT_ROOMORGANIZER RO ON RO.ROOMID = M.MROOMID WHERE P.MEETINGID=" + meetingId);

        foreach (DataRow dr in dt.Rows)
        {
            string msg = "";

            string sms = "Request to : " + dr["DISCRIPTION"].ToString() + ", Please on " + dr["MTYPENAME"].ToString();
            sms += ",Meeting Title :" + dr["MEETINGTITLE"].ToString();
            sms += ", Date : " + Convert.ToDateTime(dr["STARTTIME"]).ToString("dd/MM/yyyy") + " ,Time : " + Convert.ToDateTime(dr["STARTTIME"]).ToShortTimeString() + " To " + Convert.ToDateTime(dr["ENDTIME"]).ToShortTimeString();
            sms += ", Venue : " + dr["BUILDINGNO"].ToString() + ", " + dr["LEVELNO"].ToString() + ", Meeting Room- " + dr["ROOMTITLE"].ToString();
            sms += " Contact Person : " + dr["ORNAME"].ToString() + "," + dr["ORMOBILE"].ToString();

            msg += "Dear Sir/Madam,<br><br>";
            msg += "<br> You are requested to attend the bellow meeting: <br><br>";

            msg += "<table border='1' cellspacing='0' cellpadding='0' width='400'>";
            msg += "<tr><td>Subject : <td/>" + dr["MEETINGTITLE"].ToString() + "</td></tr>";
            msg += "<tr><td>Meeting Type : <td/>" + dt.Rows[0]["MTYPENAME"].ToString() + "</td></tr>";
            msg += "<tr><td>Perticipant : <td/>" + dt.Rows[0]["DISCRIPTION"].ToString() + "</td></tr>";
            msg += "<tr><td>Venue : <td/>" + dt.Rows[0]["BUILDINGNO"].ToString() + ", Level-" + dr["LEVELNO"].ToString() + "</td></tr>";
            msg += "<tr><td>Meeting Room : <td/>" + dt.Rows[0]["ROOMTITLE"].ToString() + "</td></tr>";
            msg += "<tr><td>Date : <td/>" + Convert.ToDateTime(dt.Rows[0]["STARTTIME"]).ToString("dd/MM/yyyy") + "</td></tr>";
            msg += "<tr><td>Duration : <td/>" + Convert.ToDateTime(dr["STARTTIME"]).ToShortTimeString() + " To " + Convert.ToDateTime(dr["ENDTIME"]).ToShortTimeString() + "</td></tr>";
            //msg += "<tr><td>Meeting Area : <td/>" + dt.Rows[0][""].ToString() + "</td></tr>";
            //msg += "<tr><td>Total : <td/>" + dt.Rows[0][""].ToString() + "</td></tr>";
            //msg += "<tr><td>Remarks : <td/>" + dt.Rows[0][""].ToString() + "</td></tr>";
            //msg += "<tr><td>Group : <td/>" + dt.Rows[0][""].ToString() + "</td></tr>";
            msg += "</table>";
            msg += "<br>";
            msg += "Therefore, you are requested to join this meeting positively as per the schedule and make this event flourishing. However, any update in relation to this issue will be informed you as earliest possible to me.<br><br>";
            msg += "<h3>NB: Please login to <a href='http://172.17.4.28/ya/' target='_blank'>Your Admin(Local)</a> or <a href='http://apps.pranrflgroup.com/ya/' target='_blank'>Your Admin(Web)</a> for more details</h3>";

            msg += "Regards,<br> ";
            msg += "<br>" + dt.Rows[0]["ORNAME"].ToString();
            msg += "<br>" + dt.Rows[0]["ORMOBILE"].ToString();
            msg += "<br>" + dt.Rows[0]["OREMAIL"].ToString();

            SendEmail.SendSingleMail(dr["OFFICEMAIL"].ToString(), "Meeting- " + dr["MEETINGTITLE"].ToString(), msg);
            //  dalCommon.SendSMS(dr["OFFICEMOBILE"].ToString(), sms);
        }
    }

    public void InsertActualPerticipans(int meetingId, int NoofPerticpants, string plist, string accountemail)
    {
        dm.ExecuteNonQuery("Update MT_MEETING set STATUS=2, ACTUALPERTICIPANT='" + NoofPerticpants + "',PLISTFILE='" + plist + "', ACCOUNTEMAIL='" + accountemail + "' Where ID=" + meetingId);
    }

    public DataTable GetAllPerticipants(int meetingId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM MT_MEETING Where ID=" + meetingId);
    }
    public DataTable GetFoodDemands(int meetingId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM MT_FOODDEMAND WHERE MEETINGID= " + meetingId);
    }
    public DataTable GetFoodDemandDetailss(int meetingId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM MT_FOODDEMAND FD INNER JOIN MT_FOODS F ON F.ID=FD.FOODID WHERE FD.MEETINGID= " + meetingId);
    }
    public DataTable GetDefaultPerticipants(int MeetingId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM  MT_PERTICIPANT WHERE MEETINGID=" + MeetingId);
    }

    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MT_MEETINGROOM WHERE ID=" + id);
    }
    public DataTable GetMeetingList(int Status)
    {
        return dm.ExecuteQuery(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID
        Where M.ISAPPROVED='" + Status + "' AND STATUS=0 AND ((SELECT COUNT(MP.STAFFID) FROM MT_PERTICIPANT MP WHERE M.ID=MP.MEETINGID AND MP.STAFFID = '" + SessionManager.SessionName.StaffId + "') > 0 OR M.CHAIRPERSONSTAFFID = '" + SessionManager.SessionName.StaffId + "')");
    }
    public DataTable GetMeetingListUpcoming(int Status, string Critaria)
    {
        return dm.ExecuteQuery(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID
        Where  M.ISAPPROVED=1 AND STATUS=0 AND M.STARTTIME > SYSDATE AND 
        ((SELECT COUNT(MP.STAFFID) FROM MT_PERTICIPANT MP WHERE M.ID=MP.MEETINGID AND MP.STAFFID = '" + SessionManager.SessionName.StaffId + "') > 0 OR MR.ADMINSTAFFID= '" + SessionManager.SessionName.StaffId + "' OR M.CHAIRPERSONSTAFFID = '" + SessionManager.SessionName.StaffId + "') " + Critaria + " ORDER BY M.STARTTIME ASC ");
    }
    public DataTable GetUpcomingMeetingByRoomID(int RoomId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM VW_MEETINGDISPLAY V WHERE V.MROOMID=" + RoomId + " AND TRUNC(V.ENDTIME)>=TRUNC(SYSDATE) ORDER BY V.STARTTIME ASC ");
    }

    public DataTable GetMeetingCurrent(int RoomId)
    {
        return dm.ExecuteQuery(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID 
        Where M.ISAPPROVED=1 AND STATUS=0 AND M.STARTTIME < SYSDATE AND M.ENDTIME >=SYSDATE AND M.MROOMID=" + RoomId);
    }
    public DataTable GetMeetingCurrentSch(string criteria)
    {
        return dm.ExecuteQuery(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID 
        Where M.ISAPPROVED=1 AND STATUS=0 AND M.STARTTIME < SYSDATE AND M.ENDTIME >=SYSDATE " + criteria);
    }
    public static DataTable GetCurrentMeetingSummary(string criteria)
    {

        return DatabaseManagerORCL.StaticDT(@"SELECT  M.MEETINGTITLE,TO_CHAR(M.STARTTIME,'HH:MI AM')||'-'||TO_CHAR(M.ENDTIME,'HH:MI AM') MEETINGTIME,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID 
        Where M.STARTTIME < SYSDATE AND M.ENDTIME >=SYSDATE " + criteria);


    }
    public static DataSet GetCurrentMeeting(string criteria)
    {
        //DataSet ds = new DataSet("ds");
        //try
        //{
        //    DataTable dt = new DataTable("dt");
        //    dt = DatabaseManagerORCL.StaticDT(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        //INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        //INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        //INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID 
        //Where trunc(M.STARTTIME) = trunc(SYSDATE) " + criteria);

        //    ds.Tables.Add(dt);

        //}
        //catch (Exception ex)
        //{
        //}
        return DatabaseManagerORCL.GetDataSet(@"SELECT  M.MEETINGTITLE,TO_CHAR(M.STARTTIME,'HH:MI AM') STARTTIME, TO_CHAR(M.ENDTIME,'HH:MI AM') ENDTIME,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID 
        Where trunc(M.STARTTIME) = trunc(SYSDATE) " + criteria + " ORDER BY M.STARTTIME ASC");
    }
    public DataTable GetMeetingListOrGanizer(int Status)
    {
        return dm.ExecuteQuery(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID
        INNER JOIN MT_ROOMORGANIZER O ON O.ROOMID=MR.ID
        Where M.ISAPPROVED=1 AND STATUS='" + Status + "' AND O.STAFFID='" + SessionManager.SessionName.StaffId + "'");
    }
    public DataTable GetMeetingListCanteen()
    {
        return dm.ExecuteQuery(@"SELECT  M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE,
        (SELECT COUNT(MP.STAFFID) FROM MT_PERTICIPANT MP WHERE M.ID=MP.MEETINGID) PERTICIPANTS
        FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=MR.ADMINSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID
        INNER JOIN MT_ROOMCANTEEN O ON O.ROOMID=MR.ID
        Where M.ISAPPROVED=1 AND M.STATUS IN(0,1,2) AND O.STAFFID='" + SessionManager.SessionName.StaffId + "'");
    }
    public void InsertMOM(int meetingId, string mom)
    {
        dm.ExecuteNonQuery("update MT_MEETING set STATUS=1,MOMFILE='" + mom + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + meetingId);
    }

    public DataTable GetRoomAdminMeetingList(int ApprovalStatus)
    {
        return dm.ExecuteQuery(@"SELECT  M.*,D.DEPARTMENTCODE||'-'||DS.DESIGNATIONNAME||'-'||E.EMPLOYEENAME CHAIRPERSON,E.OFFICEMAIL,E.OFFICEMOBILE,B.CENTERCODE,MR.ROOMTITLE FROM MT_MEETING M
        INNER JOIN MT_MEETINGROOM MR ON MR.ID=M.MROOMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=M.CHAIRPERSONSTAFFID
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID
        INNER JOIN BS_DEPARTMENT D ON D.ID=E.DEPARTMENTID
        INNER JOIN BS_DESIGNATION DS ON DS.ID=E.DESIGNATIONID
        INNER JOIN BS_COMPANY C ON C.ID=E.COMPANYID
        WHERE M.ISAPPROVED='" + ApprovalStatus + "' AND MR.ADMINSTAFFID='" + SessionManager.SessionName.StaffId + "'");
    }

    public void UpdateFoodItem(int meetingId, int FoodId, decimal quantity)
    {
        dm.ExecuteNonQuery(@"update MT_FOODDEMAND set SERVEDQUANTITY='" + quantity + "' WHERE FOODID='" + FoodId + "' and  MEETINGID=" + meetingId);
    }
    public void UpdatePerticipant(int meetingId, string StaffId, string task, int IsPresent)
    {
        dm.ExecuteNonQuery(@"update MT_PERTICIPANT set ISPRESENT='" + IsPresent + "',TASK='" + task + "' WHERE STAFFID='" + StaffId + "' and  MEETINGID=" + meetingId);
    }
    public DataTable Facilities(string MeetingRoomId)
    {
        string query = "";
        query = string.IsNullOrEmpty(MeetingRoomId) ? "" : "AND RF.MROOMID='" + MeetingRoomId + "'";
        return dm.ExecuteQuery(@"SELECT * FROM MT_FACILITY F LEFT OUTER JOIN MT_ROOMFACILITY RF ON RF.FACILITYID=F.ID WHERE F.ID>0 " + query);
    }

    public void DeleteFoodDemand(int meetingId)
    {
        dm.ExecuteNonQuery(@"DELETE FROM MT_FOODDEMAND WHERE MEETINGID=" + meetingId);
    }
    public void InsertFoodDemand(int Foodid, int meetingId, decimal Quantity)
    {
        dm.ExecuteNonQuery(@"INSERT INTO MT_FOODDEMAND(FOODID,MEETINGID,QUANTITY) VALUES('" + Foodid + "','" + meetingId + "','" + Quantity + "') ");
    }
    public void DeletePerticipants(int meetingId)
    {
        dm.ExecuteNonQuery(@"DELETE FROM MT_PERTICIPANT WHERE MEETINGID=" + meetingId);
    }
    public void InsertPerticipant(string staffId, string Name, string Designation, string Groups, string Bases, string Zone, string Mobiel, string Email, int meetingId)
    {
        if (CheckDuplicatePerticipants(staffId, meetingId))
        {
            dm.ExecuteNonQuery(@"INSERT INTO MT_PERTICIPANT(STAFFID,NAME,DESIGNATION,GROUPS,BASES,ZONE,MOBILE,EMAIL,MEETINGID) VALUES('" + staffId + "','" + Name + "','" + Designation + "','" + Groups + "','" + Bases + "','" + Zone + "','" + Mobiel + "','" + Email + "','" + meetingId + "') ");
        }

    }
    public bool CheckDuplicatePerticipants(string staffid, int MeetingId)
    {
        try
        {
            DataTable dt = dm.ExecuteQuery("select * from MT_PERTICIPANT where STAFFID='" + staffid + "' AND MEETINGID=" + MeetingId);
            if (dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
    public DataTable FacilitiesByRoom(int MeetingRoomId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM MT_ROOMFACILITY  WHERE MROOMID= " + MeetingRoomId);
    }

    public void UpdateProgress(int MeetingId, int Progress)
    {
        dm.ExecuteNonQuery(@"update MT_PERTICIPANT set TASKPROGRESS='" + Progress + "' WHERE STAFFID='" + SessionManager.SessionName.StaffId + "' and  MEETINGID=" + MeetingId);
    }

    public DataTable GetAll(int BusinessCenterId)
    {
        return dm.ExecuteQuery(@"SELECT MR.ROOMTITLE||' @Capacity:-'||MR.SEATS ROOMTITLE, MR.*,B.CENTERCODE,B.CENTERNAME,B.ADDRESS,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,E.OFFICEPHONE FROM MT_MEETINGROOM MR 
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID INNER JOIN EM_EMPLOYEE E ON E.STAFFID = MR.ADMINSTAFFID WHERE MR.BUSINESSCENTERID=" + BusinessCenterId);
    }
    public DataTable GetAllMeetingRoom()
    {
        return dm.ExecuteQuery(@"SELECT MR.ID, B.CENTERNAME||'['||B.ADDRESS||']-'||MR.ROOMTITLE||' @Capacity:-'||MR.SEATS ROOMTITLE, B.CENTERCODE,B.CENTERNAME,B.ADDRESS FROM MT_MEETINGROOM MR 
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID ORDER BY B.CENTERNAME||'['||B.ADDRESS||']-'||MR.ROOMTITLE||' @Capacity:-'||MR.SEATS ASC");
    }
    public DataTable GetAllMeetingRoomName()
    {
        return dm.ExecuteQuery(@"SELECT MR.ID, MR.ROOMTITLE ROOMTITLE, B.CENTERCODE,B.CENTERNAME,B.ADDRESS FROM MT_MEETINGROOM MR 
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID ORDER BY B.CENTERNAME||'['||B.ADDRESS||']-'||MR.ROOMTITLE||' @Capacity:-'||MR.SEATS ASC");
    }
    public DataTable GetAllMeetingRoom(int BusinessCenterId)
    {
        return dm.ExecuteQuery(@"SELECT M.*, B.CENTERNAME FROM MT_MEETINGROOM  M
            INNER JOIN BS_BUSINESSCENTER B ON B.ID = M.BUSINESSCENTERID  WHERE M.BUSINESSCENTERID=" + BusinessCenterId);
    }

    public void DeleteRoomFacility(int roomId)
    {
        dm.ExecuteNonQuery(@"DELETE FROM MT_ROOMFACILITY  WHERE MROOMID= " + roomId);

    }
    public void InsertRoomFacility(int facilityId, int roomId, int Quantity)
    {
        dm.ExecuteNonQuery(@"Insert into MT_ROOMFACILITY(FACILITYID,MROOMID,QUANTITY)
        VALUES('" + facilityId + "','" + roomId + "','" + Quantity + "')");
    }
}