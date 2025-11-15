<%@ Page Language="c#" CodeBehind="AnglerPSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.AnglerPSite" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerKoerper" Src="~/Controls/Koerper/AnglerKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Treffer Nachricht</title>
    <meta name="Description" content="Treffer Nachricht">
    <meta name="Copyright" content="Frederic Luchting">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerKoerper ID="AnglerKoerper1" runat="server"></uc1:AnglerKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/Nachricht.htm#stamminbox"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
        </a></span>
        <h4>
            Filterprofil&nbsp;mit&nbsp;Treffer
            <asp:HyperLink ID="AntwortenHyperLink" runat="server" CssClass="TopLab" Visible="False"
                NavigateUrl="#"></asp:HyperLink></h4>
        <ul>
            <li>man kann die Urheber der Nachricht sehen
                <li>die Markierung studieren
                    <li>sich bei den Empfängern umsehen
                        <li>oder Antworten lesen (und <strong>schreiben</strong>! wenn man eingeloggt ist)<br />
                        </li>
        </ul>
    </asp:Panel>
</asp:Content>
