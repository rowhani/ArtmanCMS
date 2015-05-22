<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreateNewTemplate.aspx.cs" Inherits="User_CreateNewTemplate" Title="Create New Template"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="TemplateMultiView" runat="server" ActiveViewIndex="0">
        <asp:View ID="TemplateView" runat="server">
            <div style="background-color: #ccffff">
                &nbsp;<br />
                &nbsp;&nbsp; <span style="font-size: 16pt; color: #6600ff">Create the template according to
                    the following rules:<br />
                </span>
                <ul>
                    <li><strong><span style="color: #996600">Template must be within the &lt;html&gt;&lt;/html&gt;
                        tag.</span></strong></li><li><strong><span style="color: #996600">Template muste be
                            fully comaptible with XHML content rules.</span></strong></li><li><strong><span style="color: #996600">
                                All user defined tags must start with "cms-"</span></strong></li><li><strong><span
                                    style="color: #996600">All user defined tags must contain "type" and "decription"
                                    attributes</span></strong></li><li><strong><span style="color: #996600">"type" attribute
                                        can be "textbox", "textarea", or "editor"</span><br />
                                    </strong>
                                        <br />
                                        <asp:CustomValidator ID="UniqueValidator1" runat="server" ErrorMessage="Template Name is already existing"
                                            Font-Bold="True" OnServerValidate="UniqueValidator1_ServerValidate" ControlToValidate="TemplateName"></asp:CustomValidator><br />
                                        <asp:CustomValidator ID="XMLValidator2" runat="server" ErrorMessage="Template content has some invalid data, plaese apply the above rules"
                                            Font-Bold="True" OnServerValidate="XMLValidator2_ServerValidate" ControlToValidate="TemplateContent"></asp:CustomValidator><br />
                                        <br />
                                        <table border="0" style="font-size: 12pt">
                                            <tr>
                                                <td style="width: 934px">
                                                    <strong><span style="color: #ff33cc">Template Name <span style="color: #ff0066">*</span></span></strong></td>
                                                <td style="width: 483px">
                                                    <asp:TextBox ID="TemplateName" runat="server" Width="430px"></asp:TextBox></td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name cannot be empty"
                                                        Width="141px" ControlToValidate="TemplateName"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 934px">
                                                    <strong><span style="color: #ff33cc">Template Content<span style="color: #ff0066">*</span></span></strong></td>
                                                <td style="width: 483px">
                                                    <asp:TextBox ID="TemplateContent" runat="server" Height="384px" TextMode="MultiLine"
                                                        Width="430px">&lt;html&gt;
    &lt;head&gt;

    &lt;/head&gt;

    &lt;body&gt;

        &lt;cms-user1 type=&quot;textbox&quot; description=&quot;name&quot;&gt;
        &lt;/cms-user1&gt;

    &lt;/body&gt;
&lt;/html&gt;</asp:TextBox></td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Content cannot be empty"
                                                        Width="154px" ControlToValidate="TemplateContent"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr align="right">
                                                <td colspan="2">
                                                    <asp:Button ID="CreateTemplate" runat="server" Font-Bold="True" Text="Create Template"
                                                        OnClick="CreateTemplate_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                </ul>
            </div>
        </asp:View>
        <asp:View ID="SuccessView" runat="server">
            <div align="center">
                <h2>
                    <span style="color: blue"></span>&nbsp;</h2>
                <h2>
                    <span style="font-size: 24pt; color: blue"></span>&nbsp;</h2>
                <h2>
                    <span style="font-size: 24pt; color: blue">Template created successfully</span></h2>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
