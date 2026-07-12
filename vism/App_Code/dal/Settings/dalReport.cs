
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalReport
/// </summary>
public class dalReport
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
	public dalReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetStudentAttendence(string criteria)
    {
        dm.AddParameteres("@Criteria",criteria);
        return dm.ExecuteQuery("USP_Report_GetStudentAttendence");

    }
    public DataTable SubjectToClass()
    {
        return dm.ExecuteQuery("USP_Report_SubjectToClass");
    }
    public DataTable GetMarks(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetMarks");
    }

    public DataTable GetSubjectMarks(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetSubjectMarks");
    }
    public DataTable GetStudentforIdCard(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetforStudentIdCard");
    }
    public DataTable GetTeacherInfo()
    {
        return dm.ExecuteQuery("USP_Report_GetTeacherInfo");
    }
         
}