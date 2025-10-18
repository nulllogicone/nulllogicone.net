<%@ Page Language="c#" CodeBehind="Journal.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Sites.Elemente.Journal"
    Description="eine chronologische Auflistung der neuesten Einträge in die Datenbank"
    EnableViewState="False" MasterPageFile="~/Site.Master" Title="OLI-it: Journal" %>
<%@ Register Src="~/Controls/Floor/Journal/JournalControl.ascx" TagName="JournalControl"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="alternate" type="application/rss+xml" title="OLI-it Journal" href="http://xml.oli-it.com/RSS/Journal.aspx" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Journal</h1>
    <uc3:JournalControl ID="JournalControl1" runat="server" />

    <br />
    <div style="border: solid 1px #f1f1f1; padding: 10px;">
        <%--RSS Feed--%>
        <asp:HyperLink ID="XmlHyperLink" runat="server" ImageUrl="../../images/xml.gif" NavigateUrl="http://xml.oli-it.com/RSS/Journal.aspx">xml rss news feed</asp:HyperLink>
    </div>
</asp:Content>
