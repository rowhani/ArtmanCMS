<%-- 
Author: Payman Rowhani & Artin Rezaie 
Copyright (C) Artman Systems Inc. 2004-2009
--%>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PublishPage.aspx.cs" Inherits="Admin_PublishPage" Title="Publish/Unpublish Page" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    &nbsp;<strong><span style="font-size: 14pt; color: #0000cc"><br />
        &nbsp; Select the intended pages and click on Publish or Unpublish.&nbsp;</span></strong>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<asp:SqlDataSource id="SqlDataSource1" runat="server" SelectCommand="SELECT [PageID], [PageName], [PageModifiedDate], [FK_PageAuthor], [FK_PageTemplate], [PublishedFlag] FROM [Page]" ConnectionString="<%$ ConnectionStrings:CmsDB %>">
            </asp:SqlDataSource> <TABLE border=0><TBODY><TR><TD style="WIDTH: 561px"><STRONG><SPAN style="FONT-SIZE: 14pt; COLOR: #0000cc"><asp:UpdateProgress id="UpdateProgress1" runat="server" __designer:dtid="2814749767106562" DisplayAfter="1" AssociatedUpdatePanelID="UpdatePanel1" __designer:wfdid="w2"><ProgressTemplate __designer:dtid="2814749767106563">
            <strong><span style="font-size: 14pt; color: #00cc99">(Un)Publishing pages...</span></strong>
        
</ProgressTemplate>
</asp:UpdateProgress></SPAN></STRONG></TD><TD align=right>&nbsp;<asp:Button id="Publish" onclick="Publish_Click" runat="server" Font-Bold="True" Text="Publish"></asp:Button> <asp:Button id="Unpublish" onclick="Unpublish_Click" runat="server" Font-Bold="True" Text="Unpublish"></asp:Button></TD></TR><TR><TD colSpan=2><BR /><asp:GridView id="PageTable" runat="server" ForeColor="#333333" Width="559px" DataSourceID="SqlDataSource1" OnRowDataBound="PageTable_RowDataBound" DataKeyNames="PageID" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" GridLines="None" CellPadding="4">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#00FF99" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="SelectCheckBox" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PageID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                    SortExpression="PageID" />
                                <asp:BoundField DataField="PageName" HeaderText="Name" SortExpression="PageName" />
                                <asp:BoundField DataField="PageModifiedDate" HeaderText="Modified Date" SortExpression="PageModifiedDate" />
                                <asp:BoundField DataField="FK_PageAuthor" HeaderText="Author" SortExpression="FK_PageAuthor" />
                                <asp:BoundField DataField="FK_PageTemplate" HeaderText="Template" SortExpression="FK_PageTemplate" />
                                <asp:BoundField DataField="PublishedFlag" HeaderText="Published" SortExpression="PublishedFlag">
                                    <ItemStyle Font-Bold="True" ForeColor="Red" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView> </TD></TR></TBODY></TABLE>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
