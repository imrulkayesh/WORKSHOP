 using System;
using System.Configuration;

/// <summary>
/// Summary description for Notify
/// </summary>
public class Notify
{

    #region NotifyMembers

    private string _Template;
    private string _Email;
    private string _Subject;
    private string _Applicant;

    public string Subject
    {
        get { return _Subject; }
        set { _Subject = value; }
    }
    public string Template
    {
        get { return _Template; }
        set { _Template = value; }
    }
    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }
    public string Applicant
    {
        get { return _Applicant; }
        set { _Applicant = value; }
    }
    #endregion

    #region Audit Members
    private string _Module;
    private string _Action;
    private string _Log;
    private string _User;
    private bool _Audit;
    private bool _blnNotify = false;

    public string Module
    {
        get { return _Module; }
        set { _Module = value; }
    }

    public string Action
    {
        get { return _Action; }
        set { _Action = value; }
    }

    public string Log
    {
        get { return _Log; }
        set { _Log = value; }
    }

    public string UserName
    {
        get { return _User; }
        set { _User = value; }
    }

    public bool AuditInsert
    {
        get { return _Audit; }
        set { _Audit = value; }
    }
    public bool blnNotify
    {
        get { return _blnNotify; }
        set { _blnNotify = value; }
    }


    #endregion

    public Notify()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Notify(string strEmail, string strSubject, string strTemplate)
    {
        Template = strTemplate;
        Email = strEmail;
        Subject = strSubject;
    }

    public Notify(string strAction, string strLog, string strUser, bool blnAudit)
    {
        Action = strAction;
        Log = strLog;
        UserName = strUser;
        AuditInsert = blnAudit;
     }

 
    //public void runme()
    //{
    //    try
    //    {
    //        if (Email != "")
    //        {
    //            new SendEmail().SendSimpleMail("", Email, Subject, Template);
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}
}
