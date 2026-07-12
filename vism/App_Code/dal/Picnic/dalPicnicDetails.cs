using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

public class dalPicnicDetails
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalPicnicDetails()
    {
    }
    public DataTable GetPicnicDetails(int picnicId)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PC_SCHEDULE WHERE ID=" + picnicId);
    }

    public void Update(int picnicId, string Picnictitle, string Details, string ResPerson, string Venue, string ArrangedBy, decimal donation)
    {
        dm.ExecuteNonQuery(@"UPDATE PC_SCHEDULE SET PICNICTITLE='" + Picnictitle + "',ARRANGEDBY='" + ArrangedBy + "',RESPERSON='" + ResPerson + "',VENUE='" + Venue + "',DESCRIPTION='" + Details + "',PICNICFEE='" + donation + "' ,UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE where id=" + picnicId);
    }

    public DataTable GetPerticipantDetails(string StaffId, int PicnicId)
    {
        return dm.ExecuteQuery(@"SELECT P.*,L.LOCATIONNAME,L.ADDRESS,G.GENDERNAME,R.RELIGIONNAME FROM PC_PARTICIPANT P INNER JOIN PC_REGLOCATION L ON L.ID=P.LOCATIONID
INNER JOIN BS_GENDER G ON G.ID=P.GENDERID INNER JOIN BS_RELIGION R ON R.ID=P.RELIGIONID WHERE P.STAFFID='" + StaffId + "' AND P.PICNICID=" + PicnicId);
    }

    public DataTable GetPicnicList()
    {
        return dm.ExecuteQuery(@"SELECT * FROM PC_SCHEDULE WHERE ISAPPROVED=1 AND STARTDATE>=SYSDATE");
    }

    public void InsertPerticipant(int LocationId, string staffid, string name, string mobile, string email, int genderid, int age, int height, int weight, int picnicid, int religionid, decimal donation)
    {
        dm.ExecuteNonQuery(@"Insert into PC_PARTICIPANT(ID,LOCATIONID,STAFFID,NAME,MOBILE,EMAIL,GENDERID,AGE,HEIGHT,WEIGHT,PICNICID,RELIGIONID,CREATEDBY,DONATION)
        VALUES(SQ_PC_PARTICIPANT.NEXTVAL,'" + LocationId + "','" + staffid + "','" + name + "','" + mobile + "','" + email + "','" + genderid + "','" + age + "','" + height + "','" + weight + "','" + picnicid + "','" + religionid + "','" + SessionManager.SessionName.StaffId + "','" + donation + "')");
    }

    public DataTable GetCosts(int picnicId)
    {
        return dm.ExecuteQuery(@"SELECT C.*,CI.COSTITEM,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE,CR.CURRENCYCODE FROM PC_COSTS C
        INNER JOIN PC_COSTITEM CI ON CI.ID=C.COSTITEMID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=C.COSTBYSTAFFID
        INNER JOIN ASSET_CURRENCY CR ON CR.ID=C.CURRENCYID
        WHERE C.PICNICID=" + picnicId);
    }
    public DataTable GetCostById(int Id)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PC_COSTS WHERE ID=" + Id);
    }
    public void InsertCost(string Respstaffid, int PicnicId, int CostItemid, decimal amount, int currencyid, string Details, DateTime CostDate)
    {
        dm.ExecuteNonQuery(@"Insert into PC_COSTS(ID,COSTBYSTAFFID,PICNICID,COSTITEMID,AMOUNT,CURRENCYID,DETAILS,CREATEDBY,COSTDATE)
        VALUES(SQ_PC_COSTS.NEXTVAL,'" + Respstaffid + "','" + PicnicId + "','" + CostItemid + "','" + amount + "','" + currencyid + "','" + Details + "','" + SessionManager.SessionName.StaffId + "',TO_DATE('" + CostDate + "','MM/dd/yyyy HH:MI:SSAM'))");
    }
    public void UpdateCost(int id, string Respstaffid, int PicnicId, int CostItemid, decimal amount, int currencyid, string Details, DateTime CostDate)
    {
        dm.ExecuteNonQuery(@"UPDATE PC_COSTS SET COSTBYSTAFFID='" + Respstaffid + "',PICNICID='" + PicnicId + "',COSTITEMID='" + CostItemid + "',AMOUNT='" + amount + "',CURRENCYID='" + currencyid + "',DETAILS='" + Details + "',COSTDATE= ,UPDATEDBY=TO_DATE('" + CostDate + "','MM/dd/yyyy HH:MI:SSAM')'" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE where id=" + id);
    }
    public DataTable GetResponsibility(int picnicId)
    {
        return dm.ExecuteQuery(@"SELECT RP.*,R.RESPONSIBILITY,E.EMPLOYEENAME,E.OFFICEMAIL,E.OFFICEMOBILE FROM PC_RESPERSONS RP
        INNER JOIN PC_RESPONSIBILITY R ON R.ID=RP.RESPID
        INNER JOIN EM_EMPLOYEE E ON E.STAFFID=RP.RESPSTAFFID
        WHERE RP.PICNICID=" + picnicId);
    }
    public DataTable GetResponsibilityById(int Id)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PC_RESPERSONS WHERE ID=" + Id);
    }
    public void InsertResponsibility(int Picnicid, int respid, int status, string respstaffid)
    {
        dm.ExecuteNonQuery(@"Insert into PC_RESPERSONS(ID,PICNICID,RESPID,STATUS,RESPSTAFFID,CREATEDBY)
        VALUES(SQ_PC_RESPERSONS.NEXTVAL,'" + Picnicid + "','" + respid + "','" + status + "','" + respstaffid + "','" + SessionManager.SessionName.StaffId + "')");
    }
    public void UpdateResponsibility(int id, int Picnicid, int respid, int status, string respstaffid)
    {
        dm.ExecuteNonQuery(@"UPDATE PC_RESPERSONS SET PICNICID='" + Picnicid + "',RESPID='" + respid + "',STATUS='" + status + "',RESPSTAFFID='" + respstaffid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE where id=" + id);
    }
    public void UpdatePerticipant(int LocationId, string staffid, string name, string mobile, string email, int genderid, int age, int height, int weight, int picnicid, int religionid, decimal donation)
    {
        dm.ExecuteNonQuery(@"Update PC_PARTICIPANT Set LOCATIONID='" + LocationId + "',NAME='" + name + "',MOBILE='" + mobile + "',EMAIL='" + email + "',GENDERID='" + genderid + "',AGE='" + age + "',HEIGHT='" + height + "',WEIGHT='" + weight + "',RELIGIONID='" + religionid + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE    ,DONATION='" + donation + "' WHERE STAFFID='" + staffid + "' AND PICNICID='" + picnicid + "'");
    }

    public DataTable GetGuestById(int iD)
    {
        return dm.ExecuteQuery(@"SELECT * FROM PC_GEUSTS WHERE ID=" + iD);
    }
    public DataTable GetGeustDetails(int ParticipantId)
    {
        return dm.ExecuteQuery(@"SELECT P.*,L.LOCATIONNAME,L.ADDRESS,G.GENDERNAME,R.RELIGIONNAME FROM PC_GEUSTS P INNER JOIN PC_REGLOCATION L ON L.ID=P.LOCATIONID
INNER JOIN BS_GENDER G ON G.ID=P.GENDERID INNER JOIN BS_RELIGION R ON R.ID=P.RELIGIONID WHERE P.PARTICIPANTID=" + ParticipantId);
    }
    public void InsertGuest(int LocationId, string name, string mobile, int genderid, int age, int height, int weight, int religionid, int PerticipantId, string Relation)
    {
        dm.ExecuteNonQuery(@"Insert into PC_GEUSTS(ID,PARTICIPANTID,LOCATIONID,NAME,MOBILE,GENDERID,AGE,HEIGHT,WEIGHT,RELIGIONID,CREATEDBY,RELATION)
        VALUES(SQ_PC_GEUSTS.NEXTVAL,'" + PerticipantId + "','" + LocationId + "','" + name + "','" + mobile + "','" + genderid + "','" + age + "','" + height + "','" + weight + "','" + religionid + "','" + SessionManager.SessionName.StaffId + "','" + Relation + "')");
    }

    public void UpdateGuest(int Id, int LocationId, string name, string mobile, int genderid, int age, int height, int weight, int religionid, int PerticipantId, string Relation)
    {
        dm.ExecuteNonQuery(@"Update PC_GEUSTS Set PARTICIPANTID='" + PerticipantId + "', LOCATIONID='" + LocationId + "',NAME='" + name + "',MOBILE='" + mobile + "',GENDERID='" + genderid + "',AGE='" + age + "',HEIGHT='" + height + "',WEIGHT='" + weight + "',RELIGIONID='" + religionid + "',RELATION='" + Relation + "',UPDATEDBY='" + SessionManager.SessionName.StaffId + "',UPDATEDATE=SYSDATE WHERE ID='" + Id + "'");
    }
    public DataTable GetPerticipanList(int PicnicId)
    {
        return dm.ExecuteQuery(@"SELECT P.STAFFID||'-'||P.NAME HOSTNAME, P.NAME,P.MOBILE,P.EMAIL,P.AGE,P.HEIGHT,P.WEIGHT,L.LOCATIONNAME,G.GENDERNAME,R.RELIGIONNAME FROM PC_PARTICIPANT P INNER JOIN PC_REGLOCATION L ON L.ID=P.LOCATIONID
        INNER JOIN BS_GENDER G ON G.ID=P.GENDERID INNER JOIN BS_RELIGION R ON R.ID=P.RELIGIONID WHERE P.PICNICID='" + PicnicId + "'" +
        @" UNION
        SELECT PR.STAFFID||'-'||PR.NAME||'-'||P.RELATION HOSTNAME, P.NAME,P.MOBILE,PR.EMAIL,P.AGE,P.HEIGHT,P.WEIGHT,L.LOCATIONNAME,G.GENDERNAME,R.RELIGIONNAME FROM PC_GEUSTS P INNER JOIN PC_REGLOCATION L ON L.ID=P.LOCATIONID
        INNER JOIN BS_GENDER G ON G.ID=P.GENDERID INNER JOIN BS_RELIGION R ON R.ID=P.RELIGIONID INNER JOIN PC_PARTICIPANT PR ON PR.ID=P.PARTICIPANTID WHERE PR.PICNICID=" + PicnicId);
    }
}