<%@ Page Language="c#" CodeBehind="StammAnglerSite.aspx.cs" AutoEventWireup="True"
    MasterPageFile="~/Site.Master" Inherits="OliWeb.Sites.StammAnglerSite" Description="Filterprofile dieses Stamm. Sie beschreiben in einem Wortraum NKBZ die spiegelsymmetrische Markierung der bunten Punkte" %>

<%@ Register TagPrefix="uc1" TagName="StammAnglerGrid" Src="~/Controls/Koerper/ViewGrids/StammAnglerGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Filterprofile von
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="angler, filter, profil, filterprofil, empfänger, antworter, experte,">
    <meta name="Description" content="Filterprofile von diesem Stamm. Sie beschreiben in einem Wortraum NKBZ die spiegelsymmetrische Markierung der bunten Punkte">
    <link rel="alternate" type="application/rdf+xml" title="RDF Version" href="<% = StammRdfLink %>" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:StammAnglerGrid ID="StammAnglerGrid1" runat="server"></uc1:StammAnglerGrid>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <div>
            <h4>
                <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/oberflaeche/stamm.htm#StammAngler"
                    target="doku">
                    <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                        src="../images/icons/fragezeichen.gif" alt="weitere Hilfe" align="right">
                </a></span>meine Filterprofile,</h4>
            <p>
                die Filterprofile&nbsp;fischen Nachrichten.</p>
            <p>
                Filterprofile können beliebig erstellt, geändert und gelöscht werden.
                <br />
                Die Ansicht zeigt die Anzahl der 'gefundenen Treffer' und&nbsp;ihren Gesamtwert
                an.</p>
            <p>
                Man kann nach den Spaltenüberschriften sortieren und wenn man auf die Treffer Anzahl
                klickt, gelangt man direkt dahin.</p>
        </div>
    </asp:Panel>
</asp:Content>
