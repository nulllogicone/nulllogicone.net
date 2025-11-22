<%@ Control Language="c#" AutoEventWireup="false" Codebehind="StammKoerper.ascx.cs" Inherits="OliWeb.Controls.Koerper.StammKoerper" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="StammOrgan" Src="~/Controls/Koerper/Organ/StammOrgan.ascx" %>
<table class="rechtsobenrund" id="StammKoerperTable" cellspacing="1" cellpadding="10" width="600"
	border="0">
	<tr class="linksobenrund">
		<td valign="top">
			<asp:hyperlink id="ExitHyperLink" runat="server" navigateurl="~/default.aspx?cmd=exitS" tooltip="Stamm schliessen und Startseite anzeigen"
				cssclass="ExitButton">&nbsp;</asp:hyperlink>
			<asp:hyperlink id="EditHyperLink" runat="server" tooltip="Stamm editieren" navigateurl="~/Sites/Edit/StammEdit.aspx"
				cssclass="EditButton" visible="False">&nbsp;</asp:hyperlink>
			<div><asp:label id="QLabel" runat="server" tooltip="Sprachabstraktion" cssclass="q" font-size="X-Small"></asp:label>&nbsp;<asp:label id="QQLabel" runat="server" tooltip="Die Sprachabstraktion für diesen Stamm" cssclass="q"
					font-size="X-Small"></asp:label>
			</div>
			<uc1:stammorgan id="StammOrgan1" runat="server"></uc1:stammorgan></td>
	</tr>
</table>
