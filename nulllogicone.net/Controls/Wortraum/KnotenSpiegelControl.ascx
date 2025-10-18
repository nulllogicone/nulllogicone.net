<%@ Register TagPrefix="uc1" TagName="BuntePunkte" Src="BuntePunkte.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="KnotenControl.ascx.cs" Inherits="OliWeb.Controls.Wortraum.KnotenControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="True" %>
<div align="right">
	<asp:imagebutton id="ClearImageButton" visible="False" runat="server" imageurl="../../images/x.gif"
		width="10px" height="10px" alternatetext="Clear" imagealign="Middle"></asp:imagebutton>&nbsp;
	<uc1:buntepunkte id="BuntePunkteMark" runat="server"></uc1:buntepunkte>&nbsp;
	<asp:imagebutton id="WeiterNetzImageButton" runat="server" imageurl="../../images/icons/Wortraum/Spiegel/WeiterNetz.gif"
		borderwidth="0px" borderstyle="Outset" width="15" cssclass="Pfeil"></asp:imagebutton>
	<asp:imagebutton id="WeiterBaumImageButton" runat="server" imageurl="../../images/icons/Wortraum/Spiegel/WeiterBaum.gif"
		borderwidth="0px" borderstyle="Outset" width="15" cssclass="Pfeil"></asp:imagebutton>
	<asp:imagebutton id="weiterNixImageButton" runat="server" imageurl="../../images/icons/Wortraum/Spiegel/WeiterNix.gif"
		borderwidth="0px" borderstyle="Outset" width="15px" cssclass="Pfeil"></asp:imagebutton>&nbsp;
	<asp:linkbutton id="KnotenLinkButton" runat="server" font-size="Smaller" cssclass="KnotenLink">LinkButton</asp:linkbutton>&nbsp;
	<asp:imagebutton id="KnotenPunktImageButton" imageurl="../../images/icons/Wortraum/KnotenPunkt.gif"
		runat="server" height="27px" cssclass="Knoten"></asp:imagebutton>
	<asp:image id="EbeneImage" runat="server" imageurl="../../images/icons/leer11.gif"></asp:image>
	<asp:checkbox id="EditCheckBox" visible="False" autopostback="True" runat="server"></asp:checkbox>
</div>
<asp:panel id="EditPanel" visible="False" runat="server" width="500px" cssclass="EditPanel">
	<table id="KnotenSpiegelTable" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td>Knoten</td>
			<td>
				<asp:textbox id="KnotenTextBox" width="300px" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>Beschreibung</td>
			<td>
				<asp:textbox id="BeschreibungTextBox" width="300px" runat="server" rows="3" textmode="MultiLine"></asp:textbox></td>
		</tr>
		<tr>
			<td>Markierung</td>
			<td>
				<table id="KnotenControlTable" cellspacing="0" cellpadding="0" width="300" border="0">
					<tr>
						<td>OLIs</td>
						<td>Get</td>
						<td>ILOs</td>
						<td>Fit</td>
					</tr>
					<tr>
						<td>
							<asp:textbox id="OLIsTextBox" width="30px" runat="server">0</asp:textbox></td>
						<td>
							<asp:textbox id="GetTextBox" width="30px" runat="server">0</asp:textbox></td>
						<td>
							<asp:textbox id="ILOsTextBox" width="30px" runat="server">0</asp:textbox></td>
						<td>
							<asp:textbox id="FitTextBox" width="30px" runat="server">0</asp:textbox></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="1">weiterNetz</td>
			<td>
				<asp:dropdownlist id="WeiterNetzDropDownList" runat="server" datavaluefield="NetzGuid" datatextfield="Netz"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>weiterBaum</td>
			<td>
				<asp:dropdownlist id="WeiterBaumDropDownList" runat="server" datavaluefield="BaumGuid" datatextfield="Baum"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>&nbsp;
				<asp:button id="NeuButton" runat="server" text="neu"></asp:button></td>
			<td align="right">
				<asp:button id="UpdateButton" runat="server" text="update"></asp:button></td>
		</tr>
	</table>
</asp:panel>
