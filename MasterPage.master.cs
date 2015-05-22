///Author: Payman Rowhani & Artin Rezaie 
///Copyright (C) Artman Systems Inc. 2004-2009

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        HomeButton.ImageUrl = ConfigurationManager.AppSettings["ImageFilePath"] + "Home.jpg";
        LogoutButton.ImageUrl = ConfigurationManager.AppSettings["ImageFilePath"] + "Logout.jpg";
        Banner.ImageUrl = ConfigurationManager.AppSettings["ImageFilePath"] + "Banner.jpg";

        if (!String.IsNullOrEmpty(Request["AUTH_USER"]))
        {
            logoutLabel.Visible = false;
            LogoutButton.Visible = true;
            HomeButton.Visible = true;
            LoggedInUserLabel.Visible = true;
            if (Session["Admin"] != null)
                NavigatorMultiView.ActiveViewIndex = 1;
            else
                NavigatorMultiView.ActiveViewIndex = 2;
        }
        else
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Signedout"]))
                logoutLabel.Visible = true;
            else
                logoutLabel.Visible = false;

            LogoutButton.Visible = false;
            HomeButton.Visible = false;
            LoggedInUserLabel.Visible = false;
            NavigatorMultiView.ActiveViewIndex = 0;
        }
    }

    protected void LogoutButton_Click(object sender, ImageClickEventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Clear();
        DatabaseUtil.SetLogoutTime(Request["AUTH_USER"]);
        Response.Redirect(Request.Url.AbsolutePath + "?Signedout=1");
    }

}
