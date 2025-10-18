<%@ Page Language="c#" CodeBehind="StammTopLabSite.aspx.cs" AutoEventWireup="True"
    MasterPageFile="~/Site.Master" Inherits="OliWeb.Sites.StammTopLabSite" Description="Antworten, die dieser Stamm geschrieben hat" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammTopLabGrid" Src="~/Controls/Koerper/ViewGrids/StammTopLabGrid.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Antworten von
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="Antwort, feedback, kommentar" />
    <meta name="Description" content="Antworten, die dieser Stamm geschrieben hat" />
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = StammRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:StammTopLabGrid ID="StammTopLabGrid1" runat="server"></uc1:StammTopLabGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <h4>
            <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/oberflaeche/stamm.htm#StammTopLab"
                target="doku">
                <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                    src="../images/icons/fragezeichen.gif" align="right" alt="weitere Hilfe"></a>
                <asp:HyperLink ID="XmlHyperLink" runat="server" ImageUrl="../images/xml.gif" NavigateUrl="http://xml.oli-it.com">xml rss news feed</asp:HyperLink>&nbsp;</span>meine
            Antworten,</h4>
        <p>
            die
            <asp:Label ID="StammLabel" runat="server" CssClass="Stamm">Label</asp:Label>geschrieben
            hat.
        </p>
        <p>
            Man kann nach den Spaltenüberschriften auf- und absteigend sortieren.</p>
    </asp:Panel>
</asp:Content>
