<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="AnglerKoerper.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.AnglerKoerper" %>
<%@ Register TagPrefix="uc1" TagName="AnglerOrgan" Src="Organ/AnglerOrgan.ascx" %>
<%@ Register Src="../Command/GetCommand/DetailCommand/AnglerLoecherCommand.ascx"
    TagName="AnglerLoecherCommand" TagPrefix="uc2" %>
<%@ Register Src="../Command/GetCommand/DetailCommand/AnglerPostItCommand.ascx" TagName="AnglerPostItCommand"
    TagPrefix="uc3" %>
<div class="rechtsobenrund">
    <asp:HyperLink ID="ExitHyperLink" runat="server" NavigateUrl="~/Sites/StammSite.aspx?cmd=exitA"
        ToolTip="Filterprofil schliessen" CssClass="exitButton">&nbsp;</asp:HyperLink>
    <asp:HyperLink ID="EditHyperLink" runat="server" ToolTip="Filterprofil editieren"
        NavigateUrl="~/Sites/Edit/AnglerEdit.aspx" CssClass="editButton" Visible="False">&nbsp;</asp:HyperLink>
    <div>
        <asp:Label ID="QLabel" ToolTip="Sprachabstraktion" runat="server" CssClass="q" Font-Size="XX-Small"></asp:Label>
    </div>

        <uc1:AnglerOrgan ID="AnglerOrgan1" runat="server"></uc1:AnglerOrgan>

    <nav class="PostItButtons" style="">
        <uc2:AnglerLoecherCommand ID="AnglerLoecherCommand1" runat="server" />
        <uc3:AnglerPostItCommand ID="AnglerPostItCommand1" runat="server" />
    </nav>
</div>
