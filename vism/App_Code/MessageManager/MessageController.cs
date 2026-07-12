using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

public enum MessageType
{
    Confirmation,
    Error,
    Information,
    Warning
}

public enum MessageCode
{
    SaveSucceeded,
    SaveFailed,
    UpdateSucceeded,
    UpdateFailed,
    DeleteSucceeded,
    ErrorLog_ClearSucceeded,
    ErrorLog_ClearFailed,
    DeleteFailed,
    DisplayImage_Remove,
    DisplayImage_Select,
 

}

public class MessageController
{
    private static string _strSaveSucceeded = "Saved successfully.";
    private static string _strSaveFailed = "Error occurred when saving data.";
    private static string _strUpdateSucceeded = "Updated successfully.";
    private static string _strUpdateFailed = "Update failed.";
    private static string _strDeleteSucceeded = "Deleted successfully.";
    private static string _strDeleteFailed = "Delete failed.";
    private static string _strImageRemove = "Display image removed.";
    private static string _strImageDelect = "Please select gif/jpg/bmp/png image.";
    private static string _strErrorLogClear = "Error Log Cleared.";
    private static string _strErrorLogFailed = "Error Log clear failed.";



    public static void Show(MessageCode enmMessegeCode, MessageType enmMessageType, Page p)
    {
        switch (enmMessageType)
        {
            case MessageType.Confirmation:
                {
                    ShowMessage(GetMessage(enmMessegeCode), "Confirmation", "alert alert-success", p);
                    
                    break;
                }
            case MessageType.Error:
                {
                    ShowMessage(GetMessage(enmMessegeCode), "Error", "alert alert-danger", p);
                    break;
                }
            case MessageType.Information:
                {
                    ShowMessage(GetMessage(enmMessegeCode), "Information", "alert alert-success", p);
                    break;
                }
            case MessageType.Warning:
                {
                    ShowMessage(GetMessage(enmMessegeCode), "Warning", "alert alert-warning", p);
                    break;
                }
            default:
                break;
        }
    }

    public static void Show(string enmMessegeCode, MessageType enmMessageType, Page p)
    {
        switch (enmMessageType)
        {
            case MessageType.Confirmation:
                {
                    ShowMessage(enmMessegeCode, "Confirmation", "alert alert-success", p);

                    break;
                }
            case MessageType.Error:
                {
                    ShowMessage(enmMessegeCode, "Error", "alert alert-danger", p);
                    break;
                }
            case MessageType.Information:
                {
                    ShowMessage(enmMessegeCode, "Information", "alert alert-success", p);
                    break;
                }
            case MessageType.Warning:
                {
                    ShowMessage(enmMessegeCode, "Warning", "alert alert-warning", p);
                    break;
                }
            default:
                break;
        }
    }

    private static string GetMessage(MessageCode enmMessegeCode)
    {
        switch (enmMessegeCode)
        {
            case MessageCode.SaveSucceeded:
                return _strSaveSucceeded;
            case MessageCode.SaveFailed:
                return _strSaveFailed;
            case MessageCode.UpdateSucceeded:
                return _strUpdateSucceeded;
            case MessageCode.UpdateFailed:
                return _strUpdateFailed;
            case MessageCode.DeleteSucceeded:
                return _strDeleteSucceeded;
            case MessageCode.DeleteFailed:
                return _strDeleteFailed;
            case MessageCode.DisplayImage_Remove:
                return _strImageRemove;
            case MessageCode.DisplayImage_Select:
                return _strImageDelect;
            case MessageCode.ErrorLog_ClearFailed:
                return _strErrorLogFailed;
            case MessageCode.ErrorLog_ClearSucceeded:
                return _strErrorLogClear;
            default:
                return "Something strange happened. Please contact your system administrator.";
        }
    }

    private static void ShowMessage(string strMessage, string strTitle, string strCSSClassName, Page p)
    {
        
        ((Panel)p.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = true;
        ((Label)(((Panel)p.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).FindControl("lblMessageTitle"))).Text = strTitle;
        ((Label)(((Panel)p.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).FindControl("lblMessageDetails"))).Text = strMessage;
        ((HtmlControl)(((Panel)p.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).FindControl("dvmessage"))).Attributes["class"] = strCSSClassName;
    }

    public static void Clear(Page p)
    {
        ((Panel)p.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).Visible = false;
        ((Label)(((Panel)p.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).FindControl("lblMessageTitle"))).Text = "";
        ((Label)(((Panel)p.Master.FindControl("MessagePanel1").FindControl("pnlMessage")).FindControl("lblMessageDetails"))).Text = "";
    }
} // Class