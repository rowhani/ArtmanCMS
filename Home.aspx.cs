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

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(Request["AUTH_USER"]))   //not logged in
        {
            MultiViewPanels.ActiveViewIndex = 0;
            Form.DefaultButton = Login.UniqueID;
        }
        else if (Session["Admin"] != null)   //already logged in (admin)         
            MultiViewPanels.ActiveViewIndex = 1;
        else      //already logged in (user)                     
        {
            LoadUserData();
            MultiViewPanels.ActiveViewIndex = 2;
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        //See AuthenticateValidator_ServerValidate
    }

    protected void Signout_Click(object sender, EventArgs e)
    {
        //sign out the user (deleting AUTH_USER from the request cache)
        FormsAuthentication.SignOut();
        Session.Clear();
        DatabaseUtil.SetLogoutTime(Request["AUTH_USER"]);
        Response.Redirect(Request.Url.AbsolutePath + "?Signedout=1");
    }

    protected void AuthenticateValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        Session.RemoveAll();
        if (Toolkit.Authenticate(UserName.Text.Trim(), Password.Text.Trim()))
        {
            args.IsValid = true;
            if (UserName.Text.Trim().ToLower() == "admin")
                Session["Admin"] = "true";
            FormsAuthentication.RedirectFromLoginPage(UserName.Text.Trim().ToLower(), Remember.Checked);
        }
        else
            args.IsValid = false;
    }

    public void LoadUserData()
    {
        DataRow[] data = DatabaseUtil.GetListOfNewItems(Request["AUTH_USER"], true);
        string list = "";
        foreach (DataRow row in data)
        {
            if (row["PublishedFlag"].ToString().Trim() == "No")
                list += row["PageName"].ToString() + "<br />\n";
            else
                list += "<a href='User/ShowPage.aspx?PageID=" + row["PageID"].ToString() + "' >" + row["PageName"].ToString() + "</a><br />\n";
        }
        Literal lit = new Literal();
        lit.Text = list;
        PagePanel.Controls.Add(lit);

        data = DatabaseUtil.GetListOfNewItems(Request["AUTH_USER"], false);
        list = "";
        foreach (DataRow row in data)
            list += row["TemplateName"].ToString() + "<br />\n";
        lit = new Literal();
        lit.Text = list;
        TemplatePanel.Controls.Add(lit);
    }
}
