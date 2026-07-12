using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

/// <summary>
/// Summary description for dalStudent
/// </summary>
public class dalStudent
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalStudent()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int StudentToClass(int studentId, string year, int classId, int groupId, int shiftId, int sectionId, int rollNo)
    {
        dm.AddParameteres("@StudentId", studentId);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@ShiftId", shiftId);
        dm.AddParameteres("@SectionId", sectionId);
        dm.AddParameteres("@RollNo", rollNo);
        DataTable dt = dm.ExecuteQuery("USP_StudentToClass_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int StudentToClassXml(DataTable dt, string year, int classId, int groupId, int shiftId, int sectionId)
    {
        DataSet ds = new DataSet("dsAssign");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@ShiftId", shiftId);
        dm.AddParameteres("@SectionId", sectionId);
        return dm.ExecuteNonQuery("USP_StudentToClass_InsertXML");
    }

    public int Insert(int personId, string regNo, DateTime addmissionDate, string addmissionYear, string createdBy, DateTime createdDate)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@RegNo", regNo);
        dm.AddParameteres("@AddmissionDate", addmissionDate);
        dm.AddParameteres("@AddmissionYear", addmissionYear);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        DataTable dt = dm.ExecuteQuery("USP_Student_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetByCriteria");
    }

    public DataTable UnassignStudent(string year)
    {
        dm.AddParameteres("@Year", year);
        return dm.ExecuteQuery("USP_Student_UnassignStudent");
    }
    public DataTable GetStudentInformation(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetInformationByCriteria");
    }

    public DataTable GetStudentSubjectWise(int classId, int groupId, int subjectId)
    {
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@SubjectId", subjectId);
        return dm.ExecuteQuery("USP_Student_GetStudentSubjectWise");
    }

    public int AttendenceInsert(DataTable dt, string year, string month, string createdBy, DateTime date)
    {
        DataSet ds = new DataSet("dsAttendence");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@Month", month);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@Date", date);
        return dm.ExecuteNonQuery("USP_Student_AttendenceInsertXML");
    }

    public DataTable GetByPersonId(int personId)
    {
        dm.AddParameteres("@PersonId", personId);
        return dm.ExecuteQuery("USP_Student_GetByPersonId");
    }

    public DataTable GetByStudentId(int Id)
    {
        dm.AddParameteres("@Id", Id);
        return dm.ExecuteQuery("USP_Student_GetById");
    }

    public DataTable GetStudentForAllClass(int year)
    {
        dm.AddParameteres("@Year", year);
        return dm.ExecuteQuery("USP_Student_GetByAllClass");
    }

    public DataTable GetStudentForMarksEntry(string criteria, int subjectId)
    {
        dm.AddParameteres("@Criteria", criteria);
        dm.AddParameteres("@SubjectId", subjectId);
        return dm.ExecuteQuery("USP_Student_GetWhoseMarksNotEntry");
    }
    public DataTable GetStudentForMarksEdit(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetForMarksEdit");
    }
    public DataTable ClassInfoByUserName(string userName)
    {
        dm.AddParameteres("@UserName", userName);
        return dm.ExecuteQuery("USP_Student_ClassInfoByUserName");
    }

    public DataTable ClassInfoByUserId(int userId)
    {
        dm.AddParameteres("@UserId", userId);
        return dm.ExecuteQuery("USP_Student_ClassInfoByUserId");
    }

    public DataTable GetAttendenceByCriteria(string criteria, int studentToClassId)
    {
        dm.AddParameteres("@Criteria", criteria);
        dm.AddParameteres("@StudentToClassId", studentToClassId);
        return dm.ExecuteQuery("USP_Student_GetAttendenceByCriteria");
    }

    public DataTable AttendenceByDateAndCriteria(DateTime date, int attendanceType)
    {
        dm.AddParameteres("@Date", date);
        dm.AddParameteres("@AttendanceType", attendanceType);
        return dm.ExecuteQuery("USP_Student_AttendenceByDateAndType");
    }
    public int StudentBasicInformationInsert(string nameEn, string nameBd, int gender, int religionId, DateTime dob, string birthCertificate, string phone
        , string mobile, string phnHome, string mobileHome, int bloodGroup, string userName)
    {
        dm.AddParameteres("@NameEng", nameEn);
        dm.AddParameteres("@NameBan", nameBd);
        dm.AddParameteres("@GenderId", gender);
        dm.AddParameteres("@ReligionId", religionId);
        dm.AddParameteres("@DateofBirth", dob);
        dm.AddParameteres("@BirthCertificate", birthCertificate);
        dm.AddParameteres("@PhoneNo", phone);
        dm.AddParameteres("@Mobile", mobile);
        dm.AddParameteres("@PhoneHome", phnHome);
        dm.AddParameteres("@MobileHome", mobileHome);
        dm.AddParameteres("@BloodGroup", bloodGroup);
        dm.AddParameteres("@UserName", userName);
        DataTable dt = dm.ExecuteQuery("USP_Student_BasicInformationInsert");
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    public DataTable GetRollByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetLastRoll");
    }
    public string GetStudentRegNo(int ClassId)
    {

        //DataTable dtClass = null; new dalClass().GetById(ClassId);
        int Class = 2;//Convert.ToInt32(dtClass.Rows[0]["ClassNumber"]);
        string cls = Class.ToString();
        if (Class < 100)
        {
            cls = "00" + Class.ToString();
        }
        string id = "";
        string year = (DateTime.Now.Year).ToString().Substring(2, 2);
        string month = (DateTime.Now.Month).ToString();
        DataTable dt = dm.ExecuteQuery("USP_Student_RegNo");
        string RegNo = "11" + year + cls + "000001";
        int stId = 0;
        if (dt.Rows.Count > 0)
        {
            stId = Convert.ToInt32(dt.Rows[0]["RegNos"]);
            stId += 1;
            if (stId < 10)
            {
                id = "00000" + stId.ToString();
            }
            else if (stId < 100)
            {
                id = "0000" + stId.ToString();
            }
            else if (stId < 1000)
            {
                id = "000" + stId.ToString();
            }
            else if (stId < 10000)
            {
                id = "00" + stId.ToString();
            }
            else if (stId < 100000)
            {
                id = "0" + stId.ToString();
            }
            else
            {
                id = stId.ToString();
            }
            if (dt.Rows.Count > 0)
            {
                RegNo = "11" + year + cls + id;
            }
        }


        return RegNo;
    }
}