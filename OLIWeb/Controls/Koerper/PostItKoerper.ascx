<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../Wortraum/WortraumController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItOrgan" Src="~/Controls/Koerper/Organ/PostItOrgan.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PostItKoerper.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.PostItKoerper" %>
<%@ Register TagPrefix="uc1" TagName="PostItStammCommand" Src="../Command/GetCommand/DetailCommand/PostItStammCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItCodeCommand" Src="../Command/GetCommand/DetailCommand/PostItCodeCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItAnglerCommand" Src="../Command/GetCommand/DetailCommand/PostItAnglerCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItTopLabCommand" Src="../Command/GetCommand/DetailCommand/PostItTopLabCommand.ascx" %>
<%@ Register Src="~/Controls/Command/GetCommand/SpecialCommand/AnAbWurzelnCommand.ascx"
    TagName="AnAbWurzelnCommand" TagPrefix="uc2" %>
<div class="rechtsobenrund">
    <asp:HyperLink ID="ExitHyperLink" ToolTip="Nachricht schliessen" NavigateUrl="~/Sites/StammSite.aspx?cmd=exitP"
        CssClass="exitButton" runat="server">&nbsp;</asp:HyperLink>
    <asp:HyperLink ID="EditHyperLink" ToolTip="Nachricht editieren" NavigateUrl="~/Sites/Edit/PostItMaker.aspx"
        CssClass="editButton" runat="server" Visible="False">&nbsp;</asp:HyperLink>
    <div id="PostItHead" >
        <%--AnAbWurzeln--%>
        <uc2:AnAbWurzelnCommand ID="AnAbWurzelnCommand1" runat="server" />
        <asp:Panel ID="PostItPanel" runat="server"  >
            <asp:Label ID="QLabel" runat="server" CssClass="q" ToolTip="Sprachabstraktion" Font-Size="XX-Small"></asp:Label>
              
            <asp:Label ID="VonLabel" runat="server"></asp:Label>
        </asp:Panel>
    </div>
    <uc1:PostItOrgan ID="PostItOrgan1" runat="server"></uc1:PostItOrgan>
    <nav id="PostItButtons" style="min-width:600px;">
        <uc1:PostItStammCommand ID="PostItStammCommand1" runat="server"></uc1:PostItStammCommand>
        <uc1:PostItCodeCommand ID="PostItCodeCommand1" runat="server"></uc1:PostItCodeCommand>
        <uc1:PostItAnglerCommand ID="PostItAnglerCommand1" runat="server"></uc1:PostItAnglerCommand>
        <uc1:PostItTopLabCommand ID="PostItTopLabCommand1" runat="server"></uc1:PostItTopLabCommand>
    </nav>
</div>
