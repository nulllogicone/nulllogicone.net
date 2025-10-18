<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TopLabKoerper.ascx.cs" Inherits="OliWeb.Controls.Koerper.TopLabKoerper" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="TopLabTollisGrid" Src="~/Controls/Koerper/ViewGrids/TopLabTollisGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabOrgan" Src="~/Controls/Koerper/Organ/TopLabOrgan.ascx" %>
<table class="rechtsobenrund" id="TopLabKoerperTable" cellspacing="1" cellpadding="10"
	width="600" border="0">
	<tr class="linksobenrund">
		<td valign="top">
			<asp:hyperlink id="ExitHyperLink" runat="server" tooltip="Antwort schließen" navigateurl="~/Sites/PostItSite.aspx?cmd=exitT"
				cssclass="ExitButton">&nbsp;</asp:hyperlink>
			<asp:hyperlink id="EditHyperLink" runat="server" tooltip="Antwort editieren" navigateurl="~/Sites/Edit/TopLabEdit.aspx"
				cssclass="EditButton" visible="False">&nbsp;</asp:hyperlink>
			<div><asp:label id="QLabel" tooltip="Sprachabstraktion" runat="server" cssclass="q" font-size="XX-Small"></asp:label><font size="1">&nbsp;von:
					<asp:hyperlink id="StammHyperLink" runat="server" navigateurl="xxx" cssclass="stamm" font-size="XX-Small"></asp:hyperlink></font>
			</div>
			<span style="FLOAT: left">
				<uc1:toplaborgan id="TopLabOrgan1" runat="server"></uc1:toplaborgan><br>
			</span>
			<br>
		</td>
	</tr>
</table>
<uc1:toplabtollisgrid id="TopLabTollisGrid1" runat="server"></uc1:toplabtollisgrid>
