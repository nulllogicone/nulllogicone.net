<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ZweigControl.ascx.cs" Inherits="OliWeb.Controls.Wortraum.ZweigControl"   enableViewState="True"%>
<%@ Register TagPrefix="uc1" TagName="BuntePunkte" Src="BuntePunkte.ascx" %>
<div align="right">
	<asp:imagebutton id="ClearImageButton" runat="server" imageurl="../../images/x.gif" alternatetext="Clear"
		visible="False" width="10px" height="10px" imagealign="Middle" onclick="ClearImageButton_Click"></asp:imagebutton>&nbsp;
	<uc1:buntepunkte id="BuntePunkteMark" runat="server"></uc1:buntepunkte>&nbsp;
	<asp:imagebutton id="WeiterNetzImageButton" runat="server" imageurl="../../images/icons/Wortraum/Spiegel/WeiterNetz.gif"
		width="15px" borderwidth="0px" borderstyle="Outset" cssclass="Pfeil" onclick="WeiterNetzImageButton_Click"></asp:imagebutton>
	<asp:imagebutton id="WeiterBaumImageButton" runat="server" imageurl="../../images/icons/Wortraum/Spiegel/WeiterBaum.gif"
		width="15px" borderwidth="0px" borderstyle="Outset" cssclass="Pfeil" onclick="WeiterBaumImageButton_Click"></asp:imagebutton>
	<asp:imagebutton id="weiterNixImageButton" runat="server" imageurl="../../images/icons/Wortraum/Spiegel/WeiterNix.gif"
		width="15px" borderwidth="0px" borderstyle="Outset" cssclass="Pfeil" onclick="weiterNixImageButton_Click"></asp:imagebutton>&nbsp;
	<asp:linkbutton id="ZweigLinkButton" runat="server" font-size="Smaller" cssclass="ZweigLink" onclick="ZweigLinkButton_Click">LinkButton</asp:linkbutton>&nbsp;
	<asp:imagebutton id="ZweigStrichImageButton" imageurl="../../images/icons/Wortraum/Spiegel/Zweig.gif"
		runat="server" height="26px" cssclass="Zweig" onclick="ZweigStrichImageButton_Click"></asp:imagebutton>
	<asp:image id="EbeneImage" runat="server" imageurl="../../images/icons/leer11.gif"></asp:image>
	<asp:checkbox id="EditCheckBox" visible="False" autopostback="True" runat="server" oncheckedchanged="EditCheckBox_CheckedChanged"></asp:checkbox>
</div>
<asp:panel id="EditPanel" visible="False" runat="server" width="500px" cssclass="EditPanel">
	<table id="ZweigSpiegelControlTable" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td>Zweig</td>
			<td>
				<asp:textbox id="ZweigTextBox" width="300px" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td></td>
			<td>
				<table id="ZweigSpiegelTable" cellspacing="0" cellpadding="0" width="300" border="0">
					<tr>
						<td>OLIs</td>
						<td>Get</td>
						<td>ILOs</td>
						<td>Fit</td>
					</tr>
					<tr>
						<td>
							<asp:textbox id="OLIsTextBox" width="30px" runat="server"></asp:textbox></td>
						<td>
							<asp:textbox id="GetTextBox" width="30px" runat="server"></asp:textbox></td>
						<td>
							<asp:textbox id="ILOsTextBox" width="30px" runat="server"></asp:textbox></td>
						<td>
							<asp:textbox id="FitTextBox" width="30px" runat="server"></asp:textbox></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>weiterNetz</td>
			<td>
				<asp:dropdownlist id="WeiterNetzDropDownList" runat="server" datavaluefield="NetzGuid" datatextfield="Netz"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>weiterBaum</td>
			<td>
				<asp:dropdownlist id="WeiterBaumDropDownList" runat="server" datavaluefield="BaumGuid" datatextfield="Baum"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td>
				<asp:button id="NeuButton" runat="server" text="neu" onclick="NeuButton_Click"></asp:button></td>
			<td align="right">
				<asp:button id="UpdateButton" runat="server" text="update" onclick="UpdateButton_Click"></asp:button></td>
		</tr>
	</table>
</asp:panel>
