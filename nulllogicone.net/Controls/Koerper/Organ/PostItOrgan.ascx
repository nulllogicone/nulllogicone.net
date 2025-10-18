<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PostItOrgan.ascx.cs" Inherits="OliWeb.Controls.Koerper.Organ.PostItOrgan" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="~/Controls/Gimicks/BildPicker.ascx" %>
<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<table class="PostIt" id="PostItOrganTable" cellspacing="0" cellpadding="0" width="535">
	<tr>
		<td><iframe id="UriIframe" runat="server" width="440px"></iframe>
			<h2><span style="FLOAT: left"><uc1:klickbild id="KlickBild1" runat="server"></uc1:klickbild></span>
				
				<asp:label id="TitelLabel" runat="server"></asp:label></h2>
			<asp:hyperlink id="PostItHyperLink" runat="server" navigateurl="PostItSite.aspx"></asp:hyperlink>
			
		</td>
		<td valign="top" align="right">
			
			<asp:label id="DatumLabel" runat="server" cssclass="Datum" tooltip="Nachricht erstellt am"></asp:label><br>
			<asp:label id="KooKLabel" runat="server" cssclass="flowKooK" tooltip="kOOk - Der Wert der Nachricht"></asp:label><br>
			<asp:label id="HitsLabel" cssclass="hits" runat="server"></asp:label><br>
			<asp:literal id="UrlLiteral" runat="server"></asp:literal><br>
			
		</td>
	</tr>
</table>
