<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="~/Controls/Gimicks/BildPicker.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TopLabOrgan.ascx.cs" Inherits="OliWeb.Controls.Koerper.Organ.TopLabOrgan" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="TopLabTopLabGrid" Src="~/Controls/Koerper/ViewGrids/TopLabTopLabGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="~/Controls/Gimicks/KlickBild.ascx" %>
<table class="toplab" id="TopLabOrganTable" cellspacing="0" cellpadding="0" border="0"
	width="535">
	<tr>
		<td>
			<h2><asp:imagebutton id="TopImageButton" runat="server" imageurl="~/images/icons/Symbole/toplab.jpg"
					tooltip="Übergeordnete Antwort zeigen"></asp:imagebutton><br>
				<span style="FLOAT: left">
					<uc1:klickbild id="KlickBild1" runat="server"></uc1:klickbild></span>
				<asp:label id="TitelLabel" runat="server" font-bold="True">Label</asp:label></h2>
			<asp:label id="TopLabLabel" runat="server"></asp:label>
			<uc1:toplabtoplabgrid id="TopLabTopLabGrid1" runat="server"></uc1:toplabtoplabgrid></td>
		<td valign="top" align="right" width="10%">
			<asp:label id="DatumLabel" runat="server" cssclass="datum">Label</asp:label>
			<asp:label id="LohnLabel" runat="server" tooltip="Der Lohn für diese Antwort" cssclass="flowKooK"></asp:label>
			<asp:literal id="UrlLiteral" runat="server"></asp:literal></td>
	</tr>
</table>
