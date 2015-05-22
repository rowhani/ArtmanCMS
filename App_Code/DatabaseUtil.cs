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
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Web.Configuration;
using System.Xml;
using System.Text.RegularExpressions;

public class DatabaseUtil
{
    private static SqlConnection GetNewConnection()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CmsDB"].ConnectionString;
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        return conn;
    }

    private static DataSet GetExecutedSqlResult(string query, SqlConnection conn)
    {
        if (conn == null || conn.State == ConnectionState.Closed)
            conn = GetNewConnection();

        SqlCommand cmd = new SqlCommand(query, conn);
        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
        SqlCommandBuilder cmdbld = new SqlCommandBuilder(adapt);
        DataSet ds = new DataSet();
        adapt.Fill(ds);
        return ds;
    }

    private static int ExecutedNonQuery(string query, SqlConnection conn)
    {
        if (conn == null || conn.State == ConnectionState.Closed)
            conn = GetNewConnection();

        SqlCommand cmd = new SqlCommand(query, conn);
        return cmd.ExecuteNonQuery();
    }

    public static bool CheckForExistance(string table, string[] property, object[] value)
    {
        SqlConnection conn = GetNewConnection();

        string query = "SELECT * FROM [" + table + "] WHERE ";
        for (int i = 0; i < property.Length; i++)
        {
            query += property[i] + " = ";
            if (value[i] is string)
                query += "'" + value[i] + "'";
            else
                query += value[i];
            if (i != property.Length - 1)
                query += " AND ";
        }

        DataSet result = GetExecutedSqlResult(query, conn);
        conn.Close();

        if (result.Tables.Count == 0)
            return false;
        else if (result.Tables[0].Select().Length == 0)
            return false;
        else
            return true;
    }

    public static object GetValueForProperty(string table, string property, string cond, object condVal, bool needQuote)
    {
        SqlConnection conn = GetNewConnection();

        string query = "SELECT [" + property + "] FROM [" + table + "] WHERE " + cond + " = ";
        if (needQuote)
            query += "'" + condVal + "'";
        else
            query += condVal;

        DataSet res = DatabaseUtil.GetExecutedSqlResult(query, conn);

        conn.Close();
        return (res.Tables[0].Select())[0][property];
    }

    public static void SavePageContentToDB(string name, string author, string templateName, string[][] contents)
    {
        SqlConnection conn = GetNewConnection();

        StringBuilder cont = new StringBuilder();
        for (int i = 0; i < contents.Length; i++)
            cont.Append(contents[i][0] + "-----artmancms-----" +
                contents[i][1] + "*****artmancms*****");

        string insertCommand = "INSERT INTO [Page] ([PageName], [FK_PageAuthor], [FK_PageTemplate], [PageContents], [PageModifiedDate]) " +
            "VALUES ('" + name + "', '" + author + "', '" + templateName + "', '" + cont.ToString() + "', '" + DateTime.Now.ToString() + "')";

        DatabaseUtil.ExecutedNonQuery(insertCommand, conn);
        conn.Close();
    }

    public static string[][] RetrievePageContentsFromDB(string pageID)
    {
        SqlConnection conn = GetNewConnection();
        string query = "SELECT [PageContents] FROM [Page] WHERE [PageID] = " + pageID;

        DataSet res = DatabaseUtil.GetExecutedSqlResult(query, conn);
        string rawContents = (string)(res.Tables[0].Select())[0]["PageContents"];

        string[] parts = rawContents.Split(new string[] { "*****artmancms*****" },
            StringSplitOptions.RemoveEmptyEntries);
        string[][] contents = new string[parts.Length][];
        for (int i = 0; i < parts.Length; i++)
            contents[i] = parts[i].Split(new string[] { "-----artmancms-----" },
                StringSplitOptions.None);

        conn.Close();

        return contents;
    }

    public static void PublishPage(string pageID, string outputFileName)
    {
        SqlConnection conn = GetNewConnection();
        string updateCommand = "UPDATE [Page] SET [PublishedFlag] = 'Yes', [PageFileName] = '" +
            outputFileName + "' , [PageModifiedDate] = '" + DateTime.Now.ToString() +
            "' WHERE PageID = " + pageID;

        DatabaseUtil.ExecutedNonQuery(updateCommand, conn);
        conn.Close();
    }

    public static void UnPublishPage(string pageID)
    { 
        SqlConnection conn = GetNewConnection();
        string updateCommand = "UPDATE [Page] SET [PublishedFlag] = 'No', [PageFileName] = ''" +
            " WHERE PageID = " + pageID;

        DatabaseUtil.ExecutedNonQuery(updateCommand, conn);
        conn.Close();
    }

    public static void SaveTemplateToDB(string name, string content, string author)
    {
        XmlDocument doc = new XmlDocument();
        Regex re = new Regex(@"<\s*\?.*\?\s*>");
        if (!re.IsMatch(content))
            content = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n" + content;
        doc.LoadXml(content);
        SqlConnection conn = GetNewConnection();
        string insertCommand = "INSERT INTO [Template] ([TemplateName], [FK_TemplateAuthor], [TemplateContent], [TemplateModifiedDate]) " +
            "VALUES ('" + name + "', '" + author + "', '" + content + "', '" + DateTime.Now.ToString() + "')";

        DatabaseUtil.ExecutedNonQuery(insertCommand, conn);
        conn.Close();
    }

    public static void SaveUserToDB(string userName, string password, string firstName, string lastName, string email)
    {
        SqlConnection conn = GetNewConnection();
        string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5.ToString());
        string insertCommand = "INSERT INTO [User] ([UserName], [Password], [FirstName], [LastName], [Email]) " +
            "VALUES ('" + userName + "', '" + hashedPassword + "', '" + firstName + "', '" + lastName + "', '" + email + "')";

        DatabaseUtil.ExecutedNonQuery(insertCommand, conn);
        conn.Close();
    }

    public static void SetLogoutTime(string userName)
    {
        if (userName.ToLower() == "admin")
            return;

        SqlConnection conn = GetNewConnection();
        string updateCommand = "UPDATE [User] SET [LastLogoutDate] = '" + DateTime.Now.ToString() + "'" +
            " WHERE [UserName] = '" + userName + "'";

        DatabaseUtil.ExecutedNonQuery(updateCommand, conn);
        conn.Close();
    }

    public static DataRow[] GetListOfNewItems(string userName, bool pages)
    {
        SqlConnection conn = GetNewConnection();
        object date = GetValueForProperty("User", "LastLogoutDate", "UserName", userName, true);
        string query;
        if (pages)
            query = "SELECT * FROM [Page] WHERE [PageModifiedDate] > '" + date.ToString() + "'";
        else
            query = "SELECT * FROM [Template] WHERE [TemplateModifiedDate] > '" + date.ToString() + "'";
        DataSet res = DatabaseUtil.GetExecutedSqlResult(query, conn);
        conn.Close();
        return res.Tables[0].Select();
    }
}
