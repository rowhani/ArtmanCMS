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
using System.Collections.Specialized;

public partial class EditGlobalSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.DefaultButton = SubmitChanges.UniqueID;
        FillForm();
    }

    private void FillForm()
    {
        NameValueCollection setting = Toolkit.GetAllSettings();
        foreach (Control cont in SettingView.Controls)
        {
            if (cont is TextBox)
            {
                TextBox tb = (TextBox)cont;
                if (setting[tb.ID] != null)
                    tb.Text = setting[tb.ID];
                else if (setting[tb.ID.Replace("_", " ")] != null)
                    tb.Text = setting[tb.ID.Replace("_", " ")];
            }
        }
    }

    private void SaveSetting()
    {
        string prefix = "";
        if (!String.IsNullOrEmpty(MasterPageFile))
            prefix = Form.FindControl("MainPanel").FindControl("ContentPlaceHolder1").UniqueID + "$";

        NameValueCollection setting = new NameValueCollection();
        string[] keys = {"CssFilePath", "JsFilePath","ImageFilePath",
            "StaticHtmlFilePath","Data_Source","AttachDbFilename","User_Id","Password"};
        for (int i = 0; i < keys.Length; i++)
            setting.Add(keys[i].Replace("_", " "), Request[prefix + keys[i]]);
        Toolkit.SetAllSetting(Request, Session, setting);
    }

    protected void SubmitChanges_Click(object sender, EventArgs e)
    {
        SaveSetting();
        SettingMultiView.ActiveViewIndex = 1;
    }
}
