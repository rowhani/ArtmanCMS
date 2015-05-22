///Author: Payman Rowhani & Artin Rezaie 
///Copyright (C) Artman Systems Inc. 2004-2009

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.SessionState;

public class Toolkit
{

    public static bool Authenticate(string userName, string password)
    {
        if (FormsAuthentication.Authenticate(userName, password))
            return true;

        string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5.ToString());
        if (DatabaseUtil.CheckForExistance("User", new string[] { "UserName", "Password" }, new string[] { userName, hashedPassword }))
            return true;

        return false;
    }

    public static NameValueCollection GetAllSettings()
    {
        NameValueCollection settings = new NameValueCollection();

        string[] keys = { "CssFilePath", "JsFilePath", "ImageFilePath", "StaticHtmlFilePath" };
        for (int i = 0; i < keys.Length; i++)
            settings.Add(keys[i], ConfigurationManager.AppSettings[keys[i]].Trim());

        string connectionString = ConfigurationManager.ConnectionStrings["CmsDB"].ConnectionString;
        string[] parts = connectionString.Split(';');
        string[] selectedParts = { "Data Source", "AttachDbFilename", "User Id", "Password" };
        for (int i = 0; i < selectedParts.Length; i++)
        {
            for (int j = 0; j < parts.Length; j++)
            {
                if (parts[j].ToLower().StartsWith(selectedParts[i].ToLower()))
                {
                    string val = parts[j].Substring(parts[j].IndexOf('=') + 1);
                    settings.Add(selectedParts[i], val);
                    break;
                }
            }
        }

        return settings;
    }

    public static bool SetAllSetting(HttpRequest request, HttpSessionState session, NameValueCollection settings)
    {
        try
        {
            Configuration objConfig = WebConfigurationManager.OpenWebConfiguration(request.ApplicationPath);

            AppSettingsSection app = (AppSettingsSection)objConfig.GetSection("appSettings");
            string[] keys = { "CssFilePath", "JsFilePath", "ImageFilePath", "StaticHtmlFilePath" };

            for (int i = 0; i < keys.Length; i++)
            {
                app.Settings.Remove(keys[i]);
                app.Settings.Add(keys[i], settings[keys[i]]);
            }

            string connectionString = ConfigurationManager.ConnectionStrings["CmsDB"].ConnectionString;
            string[] parts = connectionString.Split(';');
            string[] selectedParts = { "Data Source", "AttachDbFilename", "User Id", "Password" };
            for (int i = 0; i < selectedParts.Length; i++)
            {
                for (int j = 0; j < parts.Length; j++)
                {
                    if (parts[j].ToLower().StartsWith(selectedParts[i].ToLower()))
                    {
                        parts[j] = selectedParts[i] + "=" + settings[selectedParts[i]];
                        break;
                    }
                }
            }
            connectionString = "";
            for (int i = 0; i < parts.Length; i++)
            {
                connectionString += parts[i];
                if (i != parts.Length - 1)
                    connectionString += ";";
            }
            ConnectionStringsSection cs = (ConnectionStringsSection)objConfig.GetSection("connectionStrings");
            cs.ConnectionStrings.Remove("CmsDB");
            cs.ConnectionStrings.Add(new ConnectionStringSettings("CmsDB", connectionString));

            objConfig.Save();
            session["Admin"] = "true";
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static string GetOutputPathForPage(string pageID)
    {
        string dir = ConfigurationManager.AppSettings["StaticHtmlFilePath"].Substring(
            ConfigurationManager.AppSettings["StaticHtmlFilePath"].IndexOf('/', 1));
        string name = DatabaseUtil.GetValueForProperty("Page", "PageFileName", "PageID",
            pageID, false).ToString().Trim();
        string outputPath = ConfigurationManager.AppSettings["RootDirectory"] + dir + name;
        return outputPath;
    }

    public static void Publish(string pageID)
    {
        string published = DatabaseUtil.GetValueForProperty("Page", "PublishedFlag", "PageID", pageID, false).ToString().Trim();
        if (published == "Yes")
            return;
        string[][] contents = DatabaseUtil.RetrievePageContentsFromDB(pageID);
        string pageName = DatabaseUtil.GetValueForProperty("Page", "PageName", "PageID", pageID, false).ToString().Trim() + pageID + ".html";
        string templateName = (string)DatabaseUtil.GetValueForProperty("Page", "FK_PageTemplate", "PageID", pageID, false);
        string outputFilePath = ConfigurationManager.AppSettings["StaticHtmlFilePath"] + pageName;
        XmlUtil.MergeContentToTemplate(templateName, contents, outputFilePath);
        DatabaseUtil.PublishPage(pageID, pageName);
    }

    public static void Unpublish(string pageID)
    {
        string published = DatabaseUtil.GetValueForProperty("Page", "PublishedFlag", "PageID", pageID, false).ToString().Trim();
        if (published == "No")
            return;
       
        File.Delete(GetOutputPathForPage(pageID));
        DatabaseUtil.UnPublishPage(pageID);
    }

    public static bool IsTemplateValid(string content)
    {
        XmlDocument doc = new XmlDocument();
        Regex re = new Regex(@"<\s*\?.*\?\s*>");
        if (!re.IsMatch(content))
            content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n" + content;
        try
        {
            doc.LoadXml(content);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
