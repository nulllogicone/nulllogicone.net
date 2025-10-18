<%@ Page Language="c#" CodeBehind="StammNewsSite.aspx.cs" AutoEventWireup="True"
    MasterPageFile="~/Site.Master" Inherits="OliWeb.Sites.StammNewsSite" Description="neue Nachrichten an meine Filterprofile" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NewsGrid" Src="~/Controls/Koerper/ViewGrids/NewsGrid.ascx" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: neue Nachrichten (News) an
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="postit, nachricht, angler, news,">
    <meta name="Description" content="neue Nachrichten an meine Filterprofile">
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = StammRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:NewsGrid ID="NewsGrid1" runat="server"></uc1:NewsGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Stamm.htm#stammnews"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                src="../images/icons/fragezeichen.gif" align="right" alt="weitere Hilfe">
        </a></span>
        <h4>
            <%--RSS Feed--%>
            <asp:HyperLink ID="XmlHyperLink" runat="server" ImageUrl="../images/xml.gif" NavigateUrl="http://xml.oli-it.com">xml rss news feed</asp:HyperLink>
            meine News
        </h4>
        <p>
            neue Nachrichten an meine Filterprofile.</p>
        <p>
            Man kann nach den Spaltenüberschriften sortieren.
            <br />
            Wenn man nach dem Filterprofil sortiert, lassen sich alle Nachrichten an dieses
            Filterprofil<br />
            auf Einmal&nbsp;löschen (als gelesen markieren).</p>
        <p>
            Beim Anklicken werden Nachrichten als 'gesehen' markiert (also nicht mehr fett),
            <br />
            wenn man sie als 'gelesen' markiert (x), werden sie nicht mehr angezeigt.
        </p>
        <p>
            Um eine Nachricht zu beantworten : einfach draufklicken und dann <strong>neue Antworten</strong>-Button.
        </p>
    </asp:Panel>
</asp:Content>
