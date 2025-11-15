<%@ Page Language="c#" CodeBehind="AnglerPostItSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.AnglerPostItSite" SmartNavigation="False" %>

<%@ Register TagPrefix="uc1" TagName="AnglerPostItGrid" Src="../Controls/Koerper/ViewGrids/AnglerPostItGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerKoerper" Src="../Controls/Koerper/AnglerKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="../Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: gefundene Treffer</title>
    <meta name="Description" content="auf das Filterprofil passende Treffer (Nachrichten)">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerKoerper ID="AnglerKoerper1" runat="server"></uc1:AnglerKoerper>
    <uc1:AnglerPostItGrid ID="AnglerPostItGrid1" runat="server"></uc1:AnglerPostItGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Angler.htm"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
        </a></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<h4>
            <asp:HyperLink ID="XmlHyperLink" runat="server" NavigateUrl="http://nulllogic.de/OliXml/"
                ImageUrl="../images/xml.gif">xml rss news feed</asp:HyperLink>
            &nbsp;
            Filterprofil mit Treffern
            <asp:HyperLink ID="RdfHyperLink" runat="server" ImageUrl="~/images/rdf_klein.jpg"
                ToolTip="RDF Metadaten zu diesem Filterprofil">.rdf
            </asp:HyperLink>
        </h4>
        <p>
            Die Treffer sind empfangene Nachrichten.
            <br />
            Man kann nach den Spaltenüberschriften sortieren und so die neuesten oder teuersten
            Nachrichten sehen. Ausserdem kann man die Zahl der Urheber, Empfänger und Antworten
            sehen. Ein Klick auf die Zahlen führt direkt auf die entsprechende Seite.
            <br />
            Um eine Nachricht zu beantworten : einfach draufklicken und dann antworten-Button.
        </p>
    </asp:Panel>
</asp:Content>
