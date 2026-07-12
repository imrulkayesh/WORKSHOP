using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

public class ServerSideViewstateAdapter : System.Web.UI.Adapters.PageAdapter 
{
	public ServerSideViewstateAdapter()
	{
	}

    public override PageStatePersister GetStatePersister()
    {
        return new SessionPageStatePersister(this.Page);
    }
}