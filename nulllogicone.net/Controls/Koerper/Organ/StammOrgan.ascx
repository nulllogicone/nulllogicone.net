<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="~/Controls/Gimicks/BildPicker.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="StammOrgan.ascx.cs" Inherits="OliWeb.Controls.Koerper.Organ.StammOrgan" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table class="stammorgan" id="StammOrganTable" cellspacing="0" cellpadding="1" width="535" border="0">
	<tr>
		<td valign="top">
			<span style="FLOAT: left"><uc1:klickbild id="KlickBild1" runat="server"></uc1:klickbild></span>
				<h1><asp:hyperlink id="StammHyperLink" runat="server" cssclass="stamm" navigateurl="xxx"></asp:hyperlink></h1>
			<asp:label id="BeschreibungLabel" runat="server" cssclass="beschreibung"></asp:label>
		</td>
		<td valign="top" align="right">
			<asp:label id="DatumLabel" runat="server" cssclass="Datum"></asp:label>
			<br>
			<asp:label id="KooKLabel" runat="server" cssclass="boundKooK" tooltip="KooK das Vermögen des Stamm"></asp:label>
			<br>
			<asp:hyperlink id="MailHyperLink" runat="server" navigateurl="~/Sites/Elemente/MailSchreiben.aspx"
				imageurl="~/images/mailme.gif" tooltip="diesem Stamm eine persönliche Nachricht schreiben"
				visible="False">HyperLink</asp:hyperlink>
		</td>
	</tr>
</table>
