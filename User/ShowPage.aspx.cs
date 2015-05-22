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
using System.IO;

public partial class User_ShowPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadPage();
    }

    public void LoadPage()
    {
        if (String.IsNullOrEmpty(Request["PageID"]))
        {
            HtmlFileLabel.Text = "No page is requested.";
            return;
        }

        bool exists = DatabaseUtil.CheckForExistance("Page", new string[] { "PageID" }, new string[] { Request["PageID"] });
        if(!exists)
        {
            HtmlFileLabel.Text = "This page is not existing.";
            return;
        }

        string name = DatabaseUtil.GetValueForProperty("Page", "PageFileName", "PageID",
            Request["PageID"], false).ToString().Trim();
        string outputPath = Toolkit.GetOutputPathForPage(Request["PageID"]);

        if (String.IsNullOrEmpty(name) || !File.Exists(outputPath))
        {
            HtmlFileLabel.Text = "This page is not existing or published.";
            return;
        }

        StreamReader reader = new StreamReader(outputPath);
        PageLiteral.Text = reader.ReadToEnd();
        string directLink = ConfigurationManager.AppSettings["StaticHtmlFilePath"] + name;
        PageLiteral.Text += "<hr /><div align='center'><h2><a alt='" + name + "' target='_blank' href='" + directLink + "'>Direct Link</a></h2><br /></div>";
        HtmlFileLabel.Text = "Displaying Page: " + name + "<hr />";
    }
}
