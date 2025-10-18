<%@ Page Language="c#" CodeBehind="StammInboxSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.StammInboxSite" Description="neue Antworten auf meine Nachrichten" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="InboxGrid" Src="~/Controls/Koerper/ViewGrids/InboxGrid.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: neue Antworten (Inbox) an
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="antwort, ergebnis, feedback">
    <meta name="Description" content="neue Antworten auf meine Nachrichten">
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = StammRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:InboxGrid ID="InboxGrid1" runat="server"></uc1:InboxGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Stamm.htm#stamminbox"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                src="../images/icons/fragezeichen.gif" align="right" alt="weitere Hilfe">
        </a></span>&nbsp;&nbsp;<h4>
            <%--RSS Feed--%>
            <asp:HyperLink ID="XmlHyperLink" runat="server" ImageUrl="../images/xml.gif" NavigateUrl="http://xml.oli-it.com">xml rss news feed</asp:HyperLink>
            meine Inbox
        </h4>
        <p>
            neue Antworten auf meine Nachrichten
            <br />
            Auf die Antwort klicken (dann wird sie als 'gesehen' also nicht mehr fett dargestellt)
            und sie bewerten! Danach werden die Antworten nicht mehr in der Inbox angezeigt
            (x)
        </p>
    </asp:Panel>
</asp:Content>
