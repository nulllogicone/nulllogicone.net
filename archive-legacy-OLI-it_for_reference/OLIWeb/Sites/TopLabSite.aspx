<%@ Page Language="c#" MasterPageFile="~/Site.Master" CodeBehind="TopLabSite.aspx.cs"
    AutoEventWireup="True" Inherits="OliWeb.Sites.TopLabSite" EnableViewState="False"
    Description="Die Antwort, das Feedback auf eine Nachricht" %>

<%@ Register TagPrefix="uc1" TagName="TopLabKoerper" Src="~/Controls/Koerper/TopLabKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it:[T]
        <% = MyTitle %>
    </title>
    <meta name="keywords" content="antwort, toplab, feedback, reaktion, ergebnis, lösung, support, " />
    <meta name="Description" content="Die Antwort, das Feedback auf eine Nachricht" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <br />
    <uc1:TopLabKoerper ID="TopLabKoerper1" runat="server"></uc1:TopLabKoerper>
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Oberflaeche.htm"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right" />
        </a></span>
        <h4>
            Antwort mit Bewertung
            <asp:HyperLink ID="RdfHyperLink" runat="server" ImageUrl="~/images/rdf_klein.jpg"
                ToolTip="RDF Metadaten zu dieser Nachricht"></asp:HyperLink><span class="id">.rdf</span></h4>
        <p>
            Das ist ein <strong>Feedback </strong>auf die Nachricht.</p>
        <p>
            Wenn Sie eingeloggt sind, können sie eine Bewertung&nbsp;(0 - 100%) abgeben, um
            die&nbsp;Bezahlung der Antworten zu regeln.</p>
        <p>
            Ausserdem können sie weitere Kommentare (also eine Antwort auf diese Antwort) abgeben.</p>
    </asp:Panel>
</asp:Content>
