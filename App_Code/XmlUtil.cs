///Author: Payman Rowhani & Artin Rezaie 
///Copyright (C) Artman Systems Inc. 2004-2009

using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

public class XmlUtil
{
    //Return a 2D array of cms tags containing their tag name, type, and description
    public static string[][] GetCmsTags(string templateName)
    {
        string template = (string)DatabaseUtil.GetValueForProperty("Template", "TemplateContent", "TemplateName", templateName, true);
        ArrayList tagList = new ArrayList();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(template);

        XmlNodeReader reader = new XmlNodeReader(doc);
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name.ToLower().StartsWith("cms"))
                tagList.Add(new string[] { reader.Name, reader.GetAttribute("type"), reader.GetAttribute("description") });
        }

        return (string[][])tagList.ToArray(typeof(string[]));
    }

    public static void MergeContentToTemplate(string templateName, string[][] htmlContentsList, string outputFilePath)
    {
        string template = (string)DatabaseUtil.GetValueForProperty("Template", "TemplateContent", "TemplateName", templateName, true);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(template);

        for (int i = 0; i < htmlContentsList.Length; i++)
        {
            if (htmlContentsList[i] != null)
            {
                string tagID = htmlContentsList[i][0];
                XmlNodeList tags = doc.GetElementsByTagName(tagID);
                //Add items with the same tag to the contents list
                ArrayList contents = new ArrayList();
                for (int j = i; j < htmlContentsList.Length; j++)
                {
                    if (htmlContentsList[j] != null && htmlContentsList[j][0] == tagID)
                    {
                        contents.Add(htmlContentsList[j][1]);
                        htmlContentsList[j] = null;
                    }
                }
                for (int k = 0; k < tags.Count; k++)
                    tags[k].InnerXml = (string)contents[k];
            }
        }
        string tempFileName = System.Environment.GetEnvironmentVariable("temp") +
            "\\amcms" + new Random(DateTime.Now.Millisecond).Next(Int32.MaxValue) +
            ".xml";

        XmlTextWriter xmlWriter = new XmlTextWriter(tempFileName, System.Text.Encoding.UTF8);
        doc.WriteTo(xmlWriter);
        xmlWriter.Flush();
        xmlWriter.Close();

        StreamReader reader = new StreamReader(tempFileName);
        StreamWriter writer = new StreamWriter(outputFilePath);

        //Remove xml cms tags       
        string line = reader.ReadLine();
        Regex re = new Regex(@"<\s*\?.*\?\s*>");
        line = re.Replace(line, "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");

        re = new Regex(@"<\s*/*cms[^<]*\s*>", RegexOptions.IgnoreCase);
        do
        {
            writer.WriteLine(re.Replace(line, ""));
        } while ((line = reader.ReadLine()) != null);

        reader.Close();
        writer.Flush();
        writer.Close();
        File.Delete(tempFileName);
    }
}

