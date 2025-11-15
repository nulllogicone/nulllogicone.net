<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="TopLabKoerper.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.TopLabKoerper" %>
<%@ Register TagPrefix="uc1" TagName="TopLabTollisGrid" Src="~/Controls/Koerper/ViewGrids/TopLabTollisGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabOrgan" Src="~/Controls/Koerper/Organ/TopLabOrgan.ascx" %>
<%@ Register Src="../Command/GetCommand/SpecialCommand/BewertenCommand.ascx" TagName="BewertenCommand"
    TagPrefix="uc2" %>
<%@ Register Src="../Command/GetCommand/SpecialCommand/KommentierenCommand.ascx"
    TagName="KommentierenCommand" TagPrefix="uc3" %>
<div class="rechtsobenrund">
    <%--Exit--%>
    <asp:HyperLink ID="ExitHyperLink" runat="server" ToolTip="Antwort schließen" NavigateUrl="~/Sites/PostItSite.aspx?cmd=exitT"
        CssClass="exitButton">&nbsp;</asp:HyperLink>
    <%--Edit--%>
    <asp:HyperLink ID="EditHyperLink" runat="server" ToolTip="Antwort editieren" NavigateUrl="~/Sites/Edit/TopLabEdit.aspx"
        CssClass="editButton" Visible="False">&nbsp;</asp:HyperLink>
    <div>
        <asp:Label ID="QLabel" ToolTip="Sprachabstraktion" runat="server" CssClass="q" Font-Size="XX-Small"></asp:Label><font
            size="1">&nbsp;von:
            <asp:HyperLink ID="StammHyperLink" runat="server" NavigateUrl="#" CssClass="Stamm"
                Font-Size="XX-Small"></asp:HyperLink></font>
    </div>
    <uc1:TopLabOrgan ID="TopLabOrgan1" runat="server"></uc1:TopLabOrgan>
    <div >
        <uc2:BewertenCommand ID="BewertenCommand1" runat="server" />
        <uc3:KommentierenCommand ID="KommentierenCommand1" runat="server"  />
    </div>
    <div style="clear:both;"><br/>
    <uc1:TopLabTollisGrid ID="TopLabTollisGrid1" runat="server"></uc1:TopLabTollisGrid></div>
</div>
