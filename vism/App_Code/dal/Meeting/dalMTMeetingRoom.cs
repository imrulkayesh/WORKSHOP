using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalMTMeetingRoom
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalMTMeetingRoom()
    {
    }
    public int Insert(string name, int BusinessCenterid, string Building, string Levelno, int Seats, string adminstaffid)
    {
        dm.ExecuteNonQuery(@"Insert into MT_MEETINGROOM(ID,ROOMTITLE,BUSINESSCENTERID,BUILDINGNO,LEVELNO,SEATS,ADMINSTAFFID,CREATEDBY)
        VALUES(SQ_MT_MEETINGROOM.NEXTVAL,'" + name + "','" + BusinessCenterid + "','" + Building + "','" + Levelno + "','" + Seats + "','" + adminstaffid + "','" + SessionManager.SessionName.StaffId + "')");
        DataTable dt = dm.ExecuteQuery(@"Select ID from MT_MEETINGROOM WHERE BUSINESSCENTERID='" + BusinessCenterid + "' AND BUILDINGNO='" + Building + "' AND LEVELNO='" + Levelno + "' AND ADMINSTAFFID='" + adminstaffid + "' AND CREATEDBY='" + SessionManager.SessionName.StaffId + "' ORDER BY ID DESC");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, string name, int BusinessCenterid, string Building, string Levelno, int Seats, string adminstaffid)
    {
        return dm.ExecuteNonQuery("update MT_MEETINGROOM set ROOMTITLE='" + name + "',BUSINESSCENTERID='" + BusinessCenterid + "',BUILDINGNO='" + Building + "',LEVELNO='" + Levelno + "',SEATS='" + Seats + "',ADMINSTAFFID='" + adminstaffid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + id);
    }
    public DataTable GetById(int id)
    {
        return dm.ExecuteQuery("Select * from MT_MEETINGROOM WHERE ID=" + id);
    }

    public bool CheckOrganizer(string staffId, int RoomId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MT_ROOMORGANIZER WHERE STAFFID='" + staffId + "' and ROOMID=" + RoomId);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool CheckCanteenUser(string staffId, int RoomId)
    {
        DataTable dt = dm.ExecuteQuery("Select * from MT_ROOMCANTEEN WHERE STAFFID='" + staffId + "' and ROOMID=" + RoomId);
        if (dt.Rows.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public DataTable Facilities(int MeetingRoomId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM  MT_ROOMFACILITY WHERE MROOMID=" + MeetingRoomId);
    }

    public void InsertOrganizer(string StaffId, int buId, int RoomId)
    {
        dm.ExecuteNonQuery(@"Insert into MT_ROOMORGANIZER(ID,STAFFID,BUSINESSCENTERID,ROOMID,CREATEDBY)
        VALUES(SQ_MT_ROOMORGANIZER.NEXTVAL,'" + StaffId + "','" + buId + "','" + RoomId + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public void InsertCanteen(string StaffId, int buId, int RoomId)
    {
        dm.ExecuteNonQuery(@"Insert into MT_ROOMCANTEEN(ID,STAFFID,BUSINESSCENTERID,ROOMID,CREATEDBY)
        VALUES(SQ_MT_ROOMCANTEEN.NEXTVAL,'" + StaffId + "','" + buId + "','" + RoomId + "','" + SessionManager.SessionName.StaffId + "')");
    }

    public DataTable FacilitiesByRoom(int MeetingRoomId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM MT_ROOMFACILITY WHERE MROOMID= " + MeetingRoomId);
    }
    public DataTable GetAll(int BusinessCenterId)
    {
        return dm.ExecuteQuery(@"SELECT MR.*,B.CENTERCODE,B.CENTERNAME,B.ADDRESS,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,E.OFFICEPHONE FROM MT_MEETINGROOM MR 
        INNER JOIN BS_BUSINESSCENTER B ON B.ID=MR.BUSINESSCENTERID INNER JOIN EM_EMPLOYEE E ON E.STAFFID = MR.ADMINSTAFFID WHERE MR.BUSINESSCENTERID=" + BusinessCenterId);
    }

    public void UpdateOrganizer(int Id, string StaffId, int buId, int RoomId)
    {
    dm.ExecuteNonQuery("update MT_ROOMORGANIZER set STAFFID='" + StaffId + "',BUSINESSCENTERID='" + StaffId + "',ROOMID='" + RoomId + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + Id);
    }

    public DataTable GetByCanteenUserId(int iD)
    {
        return dm.ExecuteQuery("Select * from MT_ROOMCANTEEN WHERE ID=" + iD);
    }

    public DataTable GetOrganizerById(int iD)
    {
        return dm.ExecuteQuery("Select * from MT_ROOMORGANIZER WHERE ID=" + iD);
    }

    public void UpdateCantenn(int Id, string StaffId, int buId, int RoomId)
    {
        dm.ExecuteNonQuery("update MT_ROOMCANTEEN set STAFFID='" + StaffId + "',BUSINESSCENTERID='" + StaffId + "',ROOMID='" + RoomId + ",UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID=" + Id);
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

    public DataTable GetAllOrganizer(int RoomId)
    {
        return dm.ExecuteQuery(@"SELECT M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE FROM MT_ROOMORGANIZER M 
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID = M.STAFFID WHERE M.ROOMID=" + RoomId);
    }
    public DataTable GetAllCanteenUser(int RoomId)
    {
        return dm.ExecuteQuery(@"SELECT M.*,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE FROM MT_ROOMCANTEEN M 
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID = M.STAFFID WHERE M.ROOMID=" + RoomId);
    }
}