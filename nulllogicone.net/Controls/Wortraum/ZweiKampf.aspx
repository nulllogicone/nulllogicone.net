<%@ Page language="c#" Codebehind="ZweiKampf.aspx.cs" AutoEventWireup="True" Inherits="OliWeb.Controls.Wortraum.ZweiKampf" smartNavigation="True" %>
<%--<%@ Register TagPrefix="uc1" TagName="Kopf" Src="~/Controls/Koerper/Kopf.ascx" %>--%>
<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="WortraumController.ascx" %>
<!DOCTYPE HTML>
<html>
	<head>
		<title>ZweiKampf</title>		
		<link href="OliWeb.css" type="text/css" rel="stylesheet">
		<link href="../../OliWeb.css" type="text/css" rel="stylesheet">
	</head>
	<body onload="document.createStyleSheet(href='../../OliWeb.css')">
		<form id="ZweiKampf" method="post" runat="server">
			<%--<uc1:kopf id="Kopf1" runat="server"></uc1:kopf>--%>
			Anm.: hier fehlt das Kopf-control
			<table id="ZweiKampfKopfTable" cellspacing="1" cellpadding="1" width="100%" border="0">
				<tr>
					<td valign="top" width="33%">
						<p>&nbsp;</p>
					</td>
					<td valign="top" align="middle" width="33%">
						<asp:button id="FischenButton" runat="server" text="fischen" onclick="FischenButton_Click"></asp:button><br>
						<asp:linkbutton id="AnzAnglerLinkButton" runat="server" onclick="AnzAnglerLinkButton_Click">Anzahl Empfänger</asp:linkbutton>&nbsp;:
						<asp:linkbutton id="AnzCodeLinkButton" runat="server" onclick="AnzCodeLinkButton_Click">Anzahl Fische</asp:linkbutton><br>
						<asp:button id="CodeXButton" runat="server" text="x" tooltip="Vergleicht den Code gegen alle Angler"
							enabled="False" onclick="CodeXButton_Click"></asp:button>&nbsp;-
						<asp:button id="AnglerXButton" runat="server" text="x" tooltip="Vergleicht den Angler gegen alle Code"
							enabled="False" onclick="AnglerXButton_Click"></asp:button></td>
					<td valign="top" width="33%"></td>
				</tr>
			</table>
			<table id="ZweiKampfRumpfTable" cellspacing="1" cellpadding="1" width="100%" border="0">
				<tr>
					<td width="50%"></td>
					<td align="right"></td>
				</tr>
				<tr>
					<td width="50%"></td>
					<td align="right"></td>
				</tr>
				<tr>
					<td width="50%">
						<asp:dropdownlist id="PostItDropDownList" runat="server" datatextfield="PostIt" datavaluefield="PostItGuid"
							width="400px" autopostback="True" cssclass="PostIt" onselectedindexchanged="PostItDropDownList_SelectedIndexChanged"></asp:dropdownlist></td>
					<td align="right">
						<asp:dropdownlist id="StammAnglerDropDownList" runat="server" autopostback="True" width="400px" datavaluefield="StammGuid"
							datatextfield="Stamm" cssclass="Stamm" visible="False" onselectedindexchanged="StammAnglerDropDownList_SelectedIndexChanged"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td>
						<asp:listbox id="CodeListBox" runat="server" autopostback="True" datavaluefield="CodeGuid" datatextfield="CodeGuid"
							width="400px" onselectedindexchanged="CodeListBox_SelectedIndexChanged"></asp:listbox></td>
					<td align="right">
						<asp:listbox id="AnglerListBox" runat="server" autopostback="True" width="400px" datavaluefield="AnglerGuid"
							datatextfield="Angler" cssclass="Angler" onselectedindexchanged="AnglerListBox_SelectedIndexChanged"></asp:listbox></td>
				</tr>
				<tr>
					<td valign="top" colspan="2">
					</td>
				</tr>
				<tr>
					<td valign="top">
						<uc1:wortraumcontroller id="CodeWortraumController" runat="server" werbefrei="true" markierbar="true"></uc1:wortraumcontroller></td>
					<td valign="top" align="right">
						<uc1:wortraumcontroller id="AnglerWortraumController" runat="server" werbefrei="true" markierbar="true"></uc1:wortraumcontroller></td>
				</tr>
			</table>
		</form>
	</body>
</html>
