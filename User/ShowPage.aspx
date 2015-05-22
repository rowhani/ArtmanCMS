<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ShowPage.aspx.cs" Inherits="User_ShowPage" Title="Show Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;<br />
    &nbsp;
    <asp:Label ID="HtmlFileLabel" runat="server" Font-Bold="True" Font-Size="16pt"
        ForeColor="#0099FF" Text="Label"></asp:Label><br />
    <asp:Literal ID="PageLiteral" runat="server"></asp:Literal>
</asp:Content>
