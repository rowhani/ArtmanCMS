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
using FredCK.FCKeditorV2;

public partial class User_CreateNewPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Form.ID = CreatePage.UniqueID;
        if (!IsPostBack)
        {
            TemplateName.DataBind();
            TemplateName.SelectedIndex = 0;
        }
        LoadData(TemplateName.SelectedValue);
    }

    private void LoadData(string templateName)
    {
        string[][] tags = XmlUtil.GetCmsTags(templateName);

        Table tab = new Table();
        tab.BorderWidth = 0;
        tab.Width = 800;
        tab.Font.Bold = true;

        for (int i = 0; i < tags.Length; i++)
        {
            if (tags[i] == null)
                return;

            Control tb = new TextBox();
            if (tags[i][1] != null && tags[i][1].ToLower() == "textarea")
                ((TextBox)tb).TextMode = TextBoxMode.MultiLine;
            else if (tags[i][1] != null && tags[i][1].ToLower() == "editor")
            {
                tb = new FCKeditor();
                ((FCKeditor)tb).BasePath = ConfigurationManager.AppSettings["FCKEditorBasePath"];
            }
            tb.ID = tags[i][0];

            TableRow row = new TableRow();
            TableCell col = new TableCell();
            col.Text = (tags[i][2] != null ? tags[i][2].ToUpper() : "") + "<br />(" + tags[i][0] + ")";
            row.Cells.Add(col);
            col = new TableCell();
            col.Controls.Add(tb);
            row.Cells.Add(col);
            tab.Rows.Add(row);
            PageContentPlaceHolder.Controls.Add(tab);
        }
    }

    private void SavePageData(string templateName)
    {
        string prefix = "";
        if (!String.IsNullOrEmpty(MasterPageFile))
            prefix = Form.FindControl("MainPanel").FindControl("ContentPlaceHolder1").UniqueID + "$";

        string[][] tags = XmlUtil.GetCmsTags(templateName);
        string[][] contents = new string[tags.Length][];
        for (int i = 0; i < tags.Length; i++)
        {
            contents[i] = new string[2];
            contents[i][0] = tags[i][0];
            contents[i][1] = Request[prefix + tags[i][0]];
        }

        DatabaseUtil.SavePageContentToDB(PageName.Text.Trim(), Request["AUTH_USER"], templateName, contents);
    }

    protected void CreatePage_Click(object sender, EventArgs e)
    {
        SavePageData(TemplateName.SelectedValue);
        PageMultiView.ActiveViewIndex = 1;
    }

    protected void TemplateName_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState.Clear();
    }
}
