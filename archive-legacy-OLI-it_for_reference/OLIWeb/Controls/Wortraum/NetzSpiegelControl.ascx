<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NetzControl.ascx.cs" Inherits="OliWeb.Controls.Wortraum.NetzControl"   enableViewState="True"%>
<asp:imagebutton id="DateiImageButton" runat="server" width="80px" visible="False" imagealign="Left"
	cssclass="Werbung"></asp:imagebutton>
<div align="right">
	<asp:label id="NetzLabel" runat="server" font-size="X-Small" font-bold="True" forecolor="Navy"></asp:label>
    <img id="NetzTIMG" height="32" src="../../images/icons/Wortraum/NetzT.gif" runat="server"
         class="Netz"/>
	<asp:image id="EbeneImage" runat="server" imageurl="../../images/icons/leer11.gif"></asp:image>
	<asp:checkbox id="EditCheckBox" runat="server" visible="False" autopostback="True" oncheckedchanged="EditCheckBox_CheckedChanged"></asp:checkbox>
</div>
<asp:panel id="EditPanel" runat="server" visible="False" width="500px" cssclass="EditPanel">
	<table id="NetzSpiegelControlTable" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td>Netz</td>
			<td>
				<asp:textbox id="NetzTextBox" width="300px" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>Beschreibung</td>
			<td>
				<asp:textbox id="BeschreibungTextBox" width="300px" runat="server" textmode="MultiLine" rows="3"></asp:textbox></td>
		</tr>
		<tr>
			<td>Datei</td>
			<td>
				<asp:textbox id="DateiTextBox" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>
				<asp:button id="NeuButton" runat="server" text="neuN" onclick="NeuButton_Click"></asp:button>
				<asp:button id="NeuKnotenButton" runat="server" text="neuK" onclick="NeuKnotenButton_Click"></asp:button></td>
			<td align="right">
				<asp:button id="UpdateButton" runat="server" text="update" onclick="UpdateButton_Click"></asp:button></td>
		</tr>
	</table>
</asp:panel>
