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

public partial class User_CreateNewTemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.ID = CreateTemplate.UniqueID;
    }

    public bool ValidateTemplateName()
    {
        bool exists = DatabaseUtil.CheckForExistance("Template", new string[] { "TemplateName" }, new string[] { TemplateName.Text.Trim() });
        return !exists;
    }

    private void SaveTemplateData()
    {
        DatabaseUtil.SaveTemplateToDB(TemplateName.Text.Trim(), TemplateContent.Text.Trim(),
                           Request["AUTH_USER"]);
    }
    protected void UniqueValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (ValidateTemplateName())
        {
            args.IsValid = true;
            Session.Remove("UniqueValidator1_ServerValidate");
        }
        else
        {
            args.IsValid = false;
            Session["UniqueValidator1_ServerValidate"] = "true";
        }
    }
    protected void XMLValidator2_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (Toolkit.IsTemplateValid(TemplateContent.Text))
        {
            args.IsValid = true;
            Session.Remove("XMLValidator2_ServerValidate");
        }
        else
        {
            args.IsValid = false;
            Session["XMLValidator2_ServerValidate"] = "true";
        }
    }

    protected void CreateTemplate_Click(object sender, EventArgs e)
    {
        if (Session["XMLValidator2_ServerValidate"] == null &&
            Session["UniqueValidator1_ServerValidate"] == null)
        {
            SaveTemplateData();
            TemplateMultiView.ActiveViewIndex = 1;
        }
    }
}
