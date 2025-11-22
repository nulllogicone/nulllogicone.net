<%@ Page Language="c#" CodeBehind="AnglerPTopLabSite.aspx.cs" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" Inherits="OliWeb.Sites.AnglerPTopLabSite" %>

<%@ Register TagPrefix="uc1" TagName="PostItTopLabGrid" Src="~/Controls/Koerper/ViewGrids/PostItTopLabGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerKoerper" Src="~/Controls/Koerper/AnglerKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="~/Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabKoerper" Src="~/Controls/Koerper/TopLabKoerper.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>OLI-it Antworten auf den Treffer</title>
    <meta name="Description" content="Antworten auf diese Treffer">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:AnglerKoerper ID="AnglerKoerper1" runat="server"></uc1:AnglerKoerper>
    <uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
    <uc1:PostItTopLabGrid ID="PostItTopLabGrid1" runat="server"></uc1:PostItTopLabGrid>
    <br />
    <br />
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/Oberflaeche/antwort.htm"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
        </a></span>
        <h4>
            Antworten
        </h4>
        <p>
            ... auf diese Treffer
        </p>
        <ul>
            <li>Sehen sie sich beim Verfasser der Antwort um (auf Namen klicken)
                <li>und klicken sie eine Antwort an, um die einzelnen Bewertungen zu sehen.<br />
                </li>
        </ul>
    </asp:Panel>
</asp:Content>
