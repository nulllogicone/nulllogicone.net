<%@ Register TagPrefix="uc1" TagName="StammOrgan" Src="~/Controls/Koerper/Organ/StammOrgan.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="StammKoerper.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.StammKoerper" %>
<div class="rechtsobenrund">
    <div>
        <asp:HyperLink ID="ExitHyperLink" runat="server" NavigateUrl="~/default.aspx?cmd=exitS"
            ToolTip="Stamm schliessen und Startseite anzeigen" CssClass="exitButton">&nbsp;</asp:HyperLink>
        <asp:HyperLink ID="EditHyperLink" runat="server" ToolTip="Stamm editieren" NavigateUrl="~/Sites/Edit/StammEdit.aspx"
            CssClass="editButton" Visible="False">&nbsp;</asp:HyperLink></div>
    <div>
        <asp:Label ID="QLabel" runat="server" ToolTip="Sprachabstraktion" CssClass="q" Font-Size="X-Small"></asp:Label>&nbsp;<asp:Label
            ID="QQLabel" runat="server" ToolTip="Die Sprachabstraktion für diesen Stamm"
            CssClass="q" Font-Size="X-Small"></asp:Label>
    </div>
    <uc1:StammOrgan ID="StammOrgan1" runat="server"></uc1:StammOrgan>
</div>
