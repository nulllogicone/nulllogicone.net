<%@ Register TagPrefix="uc1" TagName="BuntePunkte" Src="BuntePunkte.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="KnotenControl.ascx.cs" Inherits="OliWeb.Controls.Wortraum.KnotenControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="True" %>
<div>
<asp:checkbox id="EditCheckBox" runat="server" autopostback="True" visible="False" oncheckedchanged="EditCheckBox_CheckedChanged"></asp:checkbox><asp:image id="EbeneImage" runat="server" imageurl="../../images/icons/leer11.gif"></asp:image>&nbsp;<asp:imagebutton id="KnotenPunktImageButton" runat="server" imageurl="../../images/icons/Wortraum/KnotenPunkt.gif"
	borderwidth="0px" height="27px" cssclass="Knoten" onclick="KnotenPunktImageButton_Click"></asp:imagebutton>
<asp:linkbutton id="KnotenLinkButton" runat="server" font-size="Smaller" cssclass="KnotenLink" onclick="KnotenLinkButton_Click"></asp:linkbutton>&nbsp;
<asp:imagebutton id="WeiterNetzImageButton" runat="server" imageurl="../../images/icons/Wortraum/WeiterNetz.gif"
	width="15" borderstyle="Outset" borderwidth="0px" cssclass="Pfeil" onclick="WeiterNetzImageButton_Click"></asp:imagebutton>
<asp:imagebutton id="WeiterBaumImageButton" runat="server" imageurl="../../images/icons/Wortraum/WeiterBaum.gif"
	width="15" borderstyle="Outset" borderwidth="0px" cssclass="Pfeil" onclick="WeiterBaumImageButton_Click"></asp:imagebutton>
<asp:imagebutton id="weiterNixImageButton" runat="server" imageurl="../../images/icons/Wortraum/WeiterNix.gif"
	width="15px" borderstyle="Outset" borderwidth="0px" cssclass="Pfeil" onclick="weiterNixImageButton_Click"></asp:imagebutton>&nbsp;
<uc1:buntepunkte id="BuntePunkteMark" runat="server"></uc1:buntepunkte>
&nbsp;&nbsp;
<asp:imagebutton id="ClearImageButton" visible="False" runat="server" imageurl="../../images/x.gif"
	alternatetext="Clear" width="10px" height="10px" imagealign="Middle" onclick="ClearImageButton_Click"></asp:imagebutton>
</div>
<asp:panel id="EditPanel" visible="False" runat="server" width="500px" cssclass="EditPanel">
	<table id="KnotenControlEditTable" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td>Knoten</td>
			<td>
				<asp:textbox id="KnotenTextBox" runat="server" width="300px"></asp:textbox></td>
		</tr>
		<tr>
			<td>Beschreibung</td>
			<td>
				<asp:textbox id="BeschreibungTextBox" runat="server" width="300px" textmode="MultiLine" rows="3"></asp:textbox></td>
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
							<asp:textbox id="OLIsTextBox" runat="server" width="30px">0</asp:textbox></td>
						<td>
							<asp:textbox id="GetTextBox" runat="server" width="30px">0</asp:textbox></td>
						<td>
							<asp:textbox id="ILOsTextBox" runat="server" width="30px">0</asp:textbox></td>
						<td>
							<asp:textbox id="FitTextBox" runat="server" width="30px">0</asp:textbox></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="1">weiterNetz</td>
			<td>
				<asp:dropdownlist id="WeiterNetzDropDownList" runat="server" datatextfield="Netz" datavaluefield="NetzGuid"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>weiterBaum</td>
			<td>
				<asp:dropdownlist id="WeiterBaumDropDownList" runat="server" datatextfield="Baum" datavaluefield="BaumGuid"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>&nbsp;
				<asp:button id="NeuButton" runat="server" text="neu K" onclick="NeuButton_Click"></asp:button></td>
			<td align="right">
				<asp:button id="UpdateButton" runat="server" text="update" onclick="UpdateButton_Click"></asp:button></td>
		</tr>
	</table>
</asp:panel>
