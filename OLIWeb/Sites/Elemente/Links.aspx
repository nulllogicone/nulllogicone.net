<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Links.aspx.cs" Inherits="OliWeb.Sites.Elemente.Links" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>
        <asp:Literal ID="LiteralLinks" Text="Links" runat="server" meta:resourcekey="LiteralLinksResource1" /></h1>
    <h3>domains</h3>
    <ul>
        <li><a href="https://www.oli-it.com" target="_blank">www.oli-it.com</a> [GUI, web app]</li>

        <li><a href="http://service.oli-it.com" target="_blank">service.oli-it.com</a> &lt;xml&gt;</li>
        <li><a href="http://client.oli-it.com" target="_blank">client.oli-it.com</a> (win)</li>
        <li><a href="https://nulllogicone.net" target="_blank">nulllogicone.net</a> {API, rdf}</li>
    </ul>
    <h3>papers</h3>
    <ul>
        <li>
            <div>
                <asp:HyperLink runat="server" ID="HyperLink4" NavigateUrl="http://iswc2011.semanticweb.org/fileadmin/iswc/Papers/outrageous/iswc2011outrageousid_submission_13.pdf"
                    Text="<b>soulmate</b>, ISWC 2011, outgrageous ideas" Target="oli"></asp:HyperLink>
                (4 pages pdf)
            </div>
        </li>
        <li>
            <div>
                <asp:HyperLink runat="server" ID="HyperLinkBachelor" NavigateUrl="~/content/2006 Bachelorarbeit kathrin dentler NachrichtenVermittlung RDF OLI-it.pdf"
                    meta:resourcekey="HyperLinkBachelorResource1" Text="
                    Vergleich von Ansätzen zur Nachrichtenvermittlung im Internet und 
                    Implementierung eines Systems in RDF"
                    Target="oli"></asp:HyperLink>
                <asp:Literal ID="LiteralBachelorSubtitle" Text="Bachelorarbeit von Kathrin Dentler, 2006"
                    runat="server" meta:resourcekey="LiteralBachelorSubtitleResource1" />
            </div>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink6" NavigateUrl="~/content/Script 2-spaltig.pdf" runat="server"
                Text="Script" Target="oli" /> (2004) </li>
        <li>
            Application for Startup Chile (2014)
            <asp:HyperLink runat="server" NavigateUrl="https://www.youtube.com/watch?v=jAM1duehs18">https://www.youtube.com/watch?v=jAM1duehs18</asp:HyperLink>
        </li>

    </ul>
<%--    <h3>sites</h3>
    <ul>--%>
   <%--     <li>
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="http://www.unisonus.net"
                meta:resourcekey="HyperLink3Resource1" Text="unisonus"></asp:HyperLink>
        </li>--%>
    <%--    <li>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="http://www.metaweb-project.net/"
                meta:resourcekey="HyperLink1Resource1" Text="metaweb-project"></asp:HyperLink>
        </li>--%>
        <%--        <li>
            <asp:HyperLink ID="DokumentationHyperLink" runat="server" NavigateUrl="http://doku.oli-it.com/Einleitung/Einleitung.htm"
                ToolTip="Dokumentation von OLI-it" meta:resourcekey="DokumentationHyperLinkResource1"
                Text="documentation"></asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="http://nulllogic.de/OliRezepte"
                meta:resourcekey="HyperLink2Resource1" Text="Rezepte"></asp:HyperLink>
        </li>--%>
     <%--   <li>
            <asp:HyperLink ID="GuideHyperLink" ToolTip="Eine beispielhaft geführte Anleitung für OLI-it"
                NavigateUrl="http://www.oli-it.com/oliitguide/" runat="server" meta:resourcekey="GuideHyperLinkResource1"
                Text="tour"></asp:HyperLink></li>--%>
<%--    </ul>--%>
</asp:Content>
