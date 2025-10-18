<%@ Register TagPrefix="uc1" TagName="AnglerLoecherCommand" Src="GetCommand/DetailCommand/AnglerLoecherCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowTopLabCommand" Src="GetCommand/ShowCommand/ShowTopLabCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowPostItCommand" Src="GetCommand/ShowCommand/ShowPostItCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItNewCommand" Src="GetCommand/ActionCommand/PostItNewCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerNewCommand" Src="GetCommand/ActionCommand/AnglerNewCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BewertenCommand" Src="GetCommand/SpecialCommand/BewertenCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnAbWurzelnCommand" Src="GetCommand/SpecialCommand/AnAbWurzelnCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="KommentierenCommand" Src="GetCommand/SpecialCommand/KommentierenCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowCodeCommand" Src="GetCommand/ShowCommand/ShowCodeCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StatusControl" Src="../Floor/StatusControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="CommandTree.ascx.cs"
    Inherits="OliWeb.Controls.Command.CommandTree" %>
<%@ Register TagPrefix="uc1" TagName="CodeNewCommand" Src="GetCommand/ActionCommand/CodeNewCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowStammCommand" Src="GetCommand/ShowCommand/ShowStammCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabNewCommand" Src="GetCommand/ActionCommand/TopLabNewCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowAnglerCommand" Src="GetCommand/ShowCommand/ShowAnglerCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammShortCutsCommand" Src="GetCommand/DetailCommand/StammShortCutsCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammNewsCommand" Src="GetCommand/DetailCommand/StammNewsCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammInboxCommand" Src="GetCommand/DetailCommand/StammInboxCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerPostItCommand" Src="GetCommand/DetailCommand/AnglerPostItCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammTopLabCommand" Src="GetCommand/DetailCommand/StammTopLabCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammAnglerCommand" Src="GetCommand/DetailCommand/StammAnglerCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammPostItCommand" Src="GetCommand/DetailCommand/StammPostItCommand.ascx" %>
<%--Stamm--%>
<div>
    <%--STAMM - TODO: ich habe den Link mit Namen unsichtbar gemacht--%>
    <%--<asp:HyperLink  ID="HyperLink3" runat="server" ImageUrl="../../images/icons/Symbole/Stamm.png"
                    NavigateUrl="../../Sites/StammSite.aspx" ToolTip="aktuell angezeigter Stamm ">HyperLink</asp:HyperLink>--%>

    <asp:Image ID="StammImage" ImageUrl="../../images/icons/Symbole/Stamm.png" runat="server" />
    <uc1:ShowStammCommand ID="ShowStammCommand1" runat="server"></uc1:ShowStammCommand>

    <uc1:StammNewsCommand ID="StammNewsCommand1" runat="server"></uc1:StammNewsCommand>
    <uc1:StammInboxCommand ID="StammInboxCommand1" runat="server"></uc1:StammInboxCommand>
    <uc1:StammShortCutsCommand ID="StammShortCutsCommand1" runat="server"></uc1:StammShortCutsCommand>
</div>
<br/>
<%--PostIt--%>
<div  style="clear:left; ">
    <asp:Image ID="Image3" runat="server" ImageUrl="../../images/icons/Symbole/PostIt.gif"
        AlternateText="Nachricht" ToolTip="Nachricht, Frage, Anzeige" Style="float:left;"></asp:Image>
    <uc1:StammPostItCommand ID="StammPostItCommand1" runat="server"></uc1:StammPostItCommand>
    <uc1:ShowPostItCommand ID="ShowPostItCommand1" runat="server" Visible="false"></uc1:ShowPostItCommand>
    <uc1:PostItNewCommand ID="PostItNewCommand1" runat="server"></uc1:PostItNewCommand>
    <uc1:AnAbWurzelnCommand ID="AnAbWurzelnCommand1" runat="server" Visible="false">
    </uc1:AnAbWurzelnCommand>
    <%--Code--%>
   <div >
        <asp:Image ID="ImageCode" runat="server" ImageUrl="../../images/icons/Symbole/Code.gif"
            AlternateText="Code, Markierung" ToolTip="Code, Beschreibung, Markierung, Adressierung">
        </asp:Image>&nbsp;
        <uc1:ShowCodeCommand ID="ShowCodeCommand1" runat="server"></uc1:ShowCodeCommand>
        <uc1:CodeNewCommand ID="CodeNewCommand1" runat="server"></uc1:CodeNewCommand>
    </div>
</div>
<br />
<%--Angler--%>
<br/>
<div  style="clear:left;">
    <asp:Image ID="Image2" runat="server" ImageUrl="../../images/icons/Symbole/Angler.png"
        ToolTip="Die Filterprofile" AlternateText="Angler" style="float:left;margin-right:5px;"></asp:Image>
    <uc1:StammAnglerCommand ID="StammAnglerCommand1" runat="server"></uc1:StammAnglerCommand>
    <uc1:ShowAnglerCommand ID="ShowAnglerCommand1" runat="server" CssClass="" Visible="false"></uc1:ShowAnglerCommand>
    <%--    <uc1:AnglerLoecherCommand ID="AnglerLoecherCommand1" runat="server"></uc1:AnglerLoecherCommand>
    <uc1:AnglerPostItCommand ID="AnglerPostItCommand1" runat="server"></uc1:AnglerPostItCommand>--%>
    <uc1:AnglerNewCommand ID="AnglerNewCommand1" runat="server"></uc1:AnglerNewCommand>
</div>
<br />
<%--TopLab--%>
<br/>
<div  style="clear:left;">
    <asp:Image ID="Image4" runat="server" ImageUrl="../../images/icons/Symbole/TopLab.png"
        ToolTip="die Antworten, Feedback" AlternateText="TopLab" style="float:left;margin-right:5px;"></asp:Image>
    <uc1:StammTopLabCommand ID="StammTopLabCommand1" runat="server"></uc1:StammTopLabCommand>
    <uc1:ShowTopLabCommand ID="ShowTopLabCommand1" runat="server" Visible="false"></uc1:ShowTopLabCommand>
    <uc1:TopLabNewCommand ID="TopLabNewCommand1" runat="server"></uc1:TopLabNewCommand>
    <uc1:BewertenCommand ID="BewertenCommand1" runat="server" Visible="false"></uc1:BewertenCommand>
    <uc1:KommentierenCommand ID="KommentierenCommand1" runat="server" Visible="false">
    </uc1:KommentierenCommand>
</div>
<br />
<%--Journal--%>
<div style="clear:left;">
    <asp:HyperLink ID="JournalHyperLink" runat="server" NavigateUrl="~/Sites/Elemente/Journal.aspx"
        ToolTip="die neuesten Stämme, Filterprofile, Nachrichten und Antworten in der Datenbank"
        Font-Bold="True">Journal</asp:HyperLink></div>
