<%@ Page Language="c#" CodeBehind="AnglerPStammSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.AnglerPStammSite" %>

<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerKoerper" Src="~/Controls/Koerper/AnglerKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItStammGrid" Src="~/Controls/Koerper/ViewGrids/PostItStammGrid.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it: Urheber der Nachricht</title>
    <meta name="Description" content="Urheber von diesem Treffer">
    <meta name="Copyright" content="Frederic Luchting">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerKoerper ID="AnglerKoerper1" runat="server"></uc1:AnglerKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:PostItStammGrid ID="PostItStammGrid1" runat="server"></uc1:PostItStammGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/angler.htm"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                src="../images/icons/fragezeichen.gif" align="right" alt="weitere Hilfe">
        </a></span>
        <h4>
            Urheber</h4>
        <p>
            ... dieser Treffer
            <br />
        </p>
    </asp:Panel>
</asp:Content>
