<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PostItKoerper.ascx.cs" Inherits="OliWeb.Controls.Koerper.PostItKoerper" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="PostItOrgan" Src="~/Controls/Koerper/Organ/PostItOrgan.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../Wortraum/WortraumController.ascx" %>
<table class="rechtsobenrund" id="PostItKoerperTable" cellspacing="1" cellpadding="10"
	width="600" border="0">
	<tr class="linksobenrund">
		<td valign="top">
			<asp:hyperlink id="ExitHyperLink" runat="server" cssclass="ExitButton" navigateurl="~/Sites/StammSite.aspx?cmd=exitP"
				tooltip="Nachricht schliessen">&nbsp;</asp:hyperlink>
			<asp:hyperlink id="EditHyperLink" runat="server" cssclass="EditButton" navigateurl="~/Sites/Edit/PostItMaker.aspx"
				tooltip="Nachricht editieren" visible="False">&nbsp;</asp:hyperlink>
			<asp:panel id="PostItPanel" runat="server">
				<asp:label id="QLabel" tooltip="Sprachabstraktion" cssclass="q" runat="server" font-size="XX-Small"></asp:label>
				<FONT size="1">&nbsp; von:&nbsp; </FONT>
				<asp:Label id="VonLabel" runat="server"></asp:Label>
				<asp:panel id="MeinsPanel" runat="server"><FONT size="2">zahlt:</FONT> 
<asp:label id="BezahltLabel" cssclass="flowKooK" runat="server"></asp:label>&nbsp;<FONT size="1">Frist:
						<asp:label id="FristLabel" cssclass="datum" runat="server"></asp:label><BR>
					</FONT></asp:panel>
				<uc1:postitorgan id="PostItOrgan1" runat="server"></uc1:postitorgan>
			</asp:panel></td>
	</tr>
</table>
