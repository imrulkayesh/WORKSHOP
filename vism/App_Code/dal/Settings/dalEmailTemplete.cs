using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;

/// <summary>
/// Summary description for dalEmailTemplete
/// </summary>
public class dalEmailTemplete
{
    DatabaseManagerORCL dm = new DatabaseManagerORCL();
    public dalEmailTemplete()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Insert(string TemplateName, string Subject, string Variables, string Body)
    {
        dm.AddParameteres("@TemplateName", TemplateName);
        dm.AddParameteres("@Subject", Subject);
        dm.AddParameteres("@Variables", Variables);
        dm.AddParameteres("@Body", Body);
        DataTable dt = dm.ExecuteQuery("USP_EmailTemplate_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int Id, string TemplateName, string Subject, string Variables, string Body)
    {
        dm.AddParameteres("@Id", Id);
        dm.AddParameteres("@TemplateName", TemplateName);
        dm.AddParameteres("@Subject", Subject);
        dm.AddParameteres("@Variables", Variables);
        dm.AddParameteres("@Body", Body);
        return dm.ExecuteNonQuery("USP_EmailTemplate_Update");

    }
    public int Delete(int Id)
    {
        dm.AddParameteres("@Id", Id);
        return dm.ExecuteNonQuery("USP_EmailTemplate_Delete");
    }
    public DataTable GetById(int Id)
    {
        dm.AddParameteres("@Id", Id);
        return dm.ExecuteQuery("USP_EmailTemplate_GetById");
        
    }
    

    public int ConfigurationUpdate(string DisplayName, string DisplayEmail, string ReplyToEmail, string SMTPServer, int Port, bool SSL, bool Authentication, string UserName, string Password, bool IsEmailSent)
    {
        ;
        dm.AddParameteres("@DisplayName", DisplayName);
        dm.AddParameteres("@DisplayEmail", DisplayEmail);
        dm.AddParameteres("@ReplyToEmail", ReplyToEmail);
        dm.AddParameteres("@SMTPServer", SMTPServer);
        dm.AddParameteres("@Port", Port);
        dm.AddParameteres("@SSL", SSL);
        dm.AddParameteres("@Authentication", Authentication);
        dm.AddParameteres("@UserName", UserName);
        dm.AddParameteres("@Password", Password);
        dm.AddParameteres("@IsEmailSent", IsEmailSent);
        return dm.ExecuteNonQuery("USP_Email_ConfigurationUpdate");
    }
}