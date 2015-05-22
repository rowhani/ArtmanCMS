<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        Session.Timeout = 10;
    }

    void Session_End(object sender, EventArgs e)
    {
        DatabaseUtil.SetLogoutTime(Request["AUTH_USER"]);
    }
       
</script>

