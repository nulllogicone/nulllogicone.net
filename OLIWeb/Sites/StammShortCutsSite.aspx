<%@ Page Language="c#" CodeBehind="StammShortCutsSite.aspx.cs" AutoEventWireup="True"
    MasterPageFile="~/Site.Master" Inherits="OliWeb.Sites.StammShortCutsSite" SmartNavigation="True" %>

<%@ Register TagPrefix="uc1" TagName="StammShortCutsGrid" Src="../Controls/Koerper/ViewGrids/StammShortCutsGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CommandTree" Src="../Controls/Command/CommandTree.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="~/Controls/Koerper/StammKoerper.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <title>OLI-it: Nachrichten von
        <% = MyTitle %>
    </title>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
    <uc1:StammShortCutsGrid ID="StammShortCutsGrid1" runat="server"></uc1:StammShortCutsGrid>
    <asp:Panel ID="HilfePanel" runat="server" CssClass="rechtsobenrund Hilfe">
        <span style="text-align: right"><a title="online dokumentation" href="http://doku.oli-it.com/oberflaeche/stamm.htm#stammpostit"
            target="doku">
            <img style="border-right: 0px; border-top: 0px; border-left: 0px; border-bottom: 0px"
                alt="weitere Hilfe" src="../images/icons/fragezeichen.gif" align="right">
        </a></span>
        <h4>
            meine ShortCuts</h4>
        <p>
            sind Fragmente für die Markierung von Nachrichten.
        </p>
        <p>
            Alle ShortCuts, die als 'automatisch' markiert sind, werden in&nbsp;einem neuen
            Code voreingestellt.</p>
        <p>
            &nbsp;</p>
    </asp:Panel>
</asp:Content>
