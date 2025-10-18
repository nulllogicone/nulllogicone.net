<%@ Page Language="c#" CodeBehind="AnglerSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.AnglerSite" Description="Filterprofil eines Stammes"
    EnableViewState="False" SmartNavigation="False" %>

<%@ Register TagPrefix="uc1" TagName="AnglerKoerper" Src="~/Controls/Koerper/AnglerKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it:<% = MyTitle %></title>
    <meta content="angler, experte, löcher, filter, antworter, supporter" name="keywords">
    <meta content="Filterprofil eines Stammes" name="Description">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerKoerper ID="AnglerKoerper1" runat="server"></uc1:AnglerKoerper>
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Angler.htm"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
        </a></span>
        <h4>
            Filterprofil
            <asp:HyperLink ID="RdfHyperLink" runat="server" ImageUrl="~/images/rdf_klein.jpg"
                ToolTip="RDF Metadaten zu diesem Filterprofil"></asp:HyperLink></h4>
        <p>
            Das Filterprofil, um Nachrichten zu empfangen.
        </p>
        <p>
            Hier markiert man, welche Nachrichten man erhalten möchte.
        </p>
        <ul>
            <li>Man kann das Filterprofil&nbsp;&nbsp;jederzeit anpassen.
                <li>Neue Profile hinzufügen oder alte löschen.
                    <li>Die RDF Daten dieses Filterprofils anzeigen</li></ul>
        <p>
            &nbsp;</p>
    </asp:Panel>
</asp:Content>
