<%@ Register TagPrefix="uc1" TagName="StammPostItCommand" Src="GetCommand/DetailCommand/StammPostItCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammAnglerCommand" Src="GetCommand/DetailCommand/StammAnglerCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItTopLabCommand" Src="GetCommand/DetailCommand/PostItTopLabCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItStammCommand" Src="GetCommand/DetailCommand/PostItStammCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammTopLabCommand" Src="GetCommand/DetailCommand/StammTopLabCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerPostItCommand" Src="GetCommand/DetailCommand/AnglerPostItCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammInboxCommand" Src="GetCommand/DetailCommand/StammInboxCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammNewsCommand" Src="GetCommand/DetailCommand/StammNewsCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammShortCutsCommand" Src="GetCommand/DetailCommand/StammShortCutsCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowAnglerCommand" Src="GetCommand/ShowCommand/ShowAnglerCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammEditCommand" Src="GetCommand/ActionCommand/StammEditCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabNewCommand" Src="GetCommand/ActionCommand/TopLabNewCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowStammCommand" Src="GetCommand/ShowCommand/ShowStammCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CodeNewCommand" Src="GetCommand/ActionCommand/CodeNewCommand.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CommandTree.ascx.cs" Inherits="OliWeb.Controls.Command.CommandTree" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="StatusControl" Src="../Floor/StatusControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowCodeCommand" Src="GetCommand/ShowCommand/ShowCodeCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="KommentierenCommand" Src="GetCommand/SpecialCommand/KommentierenCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnAbWurzelnCommand" Src="GetCommand/SpecialCommand/AnAbWurzelnCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BewertenCommand" Src="GetCommand/SpecialCommand/BewertenCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerNewCommand" Src="GetCommand/ActionCommand/AnglerNewCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItNewCommand" Src="GetCommand/ActionCommand/PostItNewCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerEditCommand" Src="GetCommand/ActionCommand/AnglerEditCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItEditCommand" Src="GetCommand/ActionCommand/PostItEditCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabEditCommand" Src="GetCommand/ActionCommand/TopLabEditCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowPostItCommand" Src="GetCommand/ShowCommand/ShowPostItCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowTopLabCommand" Src="GetCommand/ShowCommand/ShowTopLabCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItAnglerCommand" Src="GetCommand/DetailCommand/PostItAnglerCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItCodeCommand" Src="GetCommand/DetailCommand/PostItCodeCommand.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnglerLoecherCommand" Src="GetCommand/DetailCommand/AnglerLoecherCommand.ascx" %>
<noscript>
	<div style="COLOR:red"><br>
		<FONT size="2">Ihr Browser unterstützt<br>
			kein JavaScript (<strong><a href="<% = MyBaseLink %>NoFeature.aspx">Info</a></strong>)</FONT></div>
</noscript>
<table class="commandtree" id="CommandTable" cellspacing="10" cellpadding="5" border="0"
	runat="server">
	<tbody>
		<tr>
			<td style="FONT-SIZE: smaller" valign="top">
				<p>
					<asp:hyperlink id="HyperLink1" runat="server" cssclass="button" navigateurl="~/Sites/Elemente/Journal.aspx">Journal</asp:hyperlink></p>
			</td>
		</tr>
		<tr>
			<td class="stammorgan" style="FONT-SIZE: smaller" valign="top">
				<asp:hyperlink id="HyperLink3" runat="server" imageurl="../../images/icons/Symbole/Stamm.jpg" navigateurl="../../Sites/StammSite.aspx"
					ToolTip="aktuell angezeigter Stamm ">HyperLink</asp:hyperlink><uc1:showstammcommand id="ShowStammCommand1" runat="server"></uc1:showstammcommand><uc1:stammeditcommand id="StammEditCommand1" runat="server"></uc1:stammeditcommand><uc1:stammnewscommand id="StammNewsCommand1" runat="server"></uc1:stammnewscommand><uc1:stamminboxcommand id="StammInboxCommand1" runat="server"></uc1:stamminboxcommand><uc1:stammshortcutscommand id="StammShortCutsCommand1" runat="server"></uc1:stammshortcutscommand></td>
		</tr>
		<tr>
			<td style="FONT-SIZE: smaller" valign="top"><asp:image id="Image2" runat="server" imageurl="../../images/icons/Symbole/Angler.jpg" tooltip="Die Filterprofile (Angler)"
					alternatetext="Angler"></asp:image>&nbsp;<uc1:showanglercommand id="ShowAnglerCommand1" runat="server" cssclass=""></uc1:showanglercommand><br>
				<uc1:stammanglercommand id="StammAnglerCommand1" runat="server"></uc1:stammanglercommand><uc1:anglereditcommand id="AnglerEditCommand1" runat="server"></uc1:anglereditcommand><uc1:anglernewcommand id="AnglerNewCommand1" runat="server"></uc1:anglernewcommand><uc1:anglerloechercommand id="AnglerLoecherCommand1" runat="server"></uc1:anglerloechercommand><uc1:anglerpostitcommand id="AnglerPostItCommand1" runat="server"></uc1:anglerpostitcommand></td>
		</tr>
		<tr>
			<td style="FONT-SIZE: smaller" valign="top"><asp:image id="Image3" runat="server" imageurl="../../images/icons/Symbole/PostIt.gif" alternatetext="Nachricht"
					ToolTip="Nachricht, Frage, Anzeige"></asp:image>&nbsp;<uc1:showpostitcommand id="ShowPostItCommand1" runat="server"></uc1:showpostitcommand><br>
				<uc1:stammpostitcommand id="StammPostItCommand1" runat="server"></uc1:stammpostitcommand><uc1:postiteditcommand id="PostItEditCommand1" runat="server"></uc1:postiteditcommand><uc1:postitnewcommand id="PostItNewCommand1" runat="server"></uc1:postitnewcommand><uc1:anabwurzelncommand id="AnAbWurzelnCommand1" runat="server"></uc1:anabwurzelncommand><br>
				<uc1:postitstammcommand id="PostItStammCommand1" runat="server"></uc1:postitstammcommand><uc1:postitanglercommand id="PostItAnglerCommand1" runat="server"></uc1:postitanglercommand><uc1:postittoplabcommand id="PostItTopLabCommand1" runat="server"></uc1:postittoplabcommand>
			</td>
		</tr>
		<tr>
			<td style="FONT-SIZE: smaller" valign="top">
				<asp:image id="CodeImage" runat="server" imageurl="../../images/icons/Symbole/Code.gif" alternatetext="code"
					tooltip="die Markierung (Code)" visible="False" borderwidth="0px"></asp:image><strong>
					<uc1:showcodecommand id="ShowCodeCommand1" runat="server"></uc1:showcodecommand></strong>
				<uc1:postitcodecommand id="PostItCodeCommand1" runat="server"></uc1:postitcodecommand>
				<uc1:codenewcommand id="CodeNewCommand1" runat="server"></uc1:codenewcommand></td>
		</tr>
		<tr>
			<td style="FONT-SIZE: smaller" valign="top"><asp:image id="Image4" runat="server" imageurl="../../images/icons/Symbole/toplab.JPG" tooltip="die Antworten, Feedback"
					alternatetext="TopLab"></asp:image><uc1:showtoplabcommand id="ShowTopLabCommand1" runat="server"></uc1:showtoplabcommand><br>
				<uc1:stammtoplabcommand id="StammTopLabCommand1" runat="server"></uc1:stammtoplabcommand><uc1:toplabeditcommand id="TopLabEditCommand1" runat="server"></uc1:toplabeditcommand><uc1:toplabnewcommand id="TopLabNewCommand1" runat="server"></uc1:toplabnewcommand><uc1:bewertencommand id="BewertenCommand1" runat="server"></uc1:bewertencommand><uc1:kommentierencommand id="KommentierenCommand1" runat="server"></uc1:kommentierencommand></td>
		</tr>
	</tbody>
</table>
