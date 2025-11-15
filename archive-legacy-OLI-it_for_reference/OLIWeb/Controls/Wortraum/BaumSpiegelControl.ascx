<%@ Control Language="c#" AutoEventWireup="True" Codebehind="BaumControl.ascx.cs" Inherits="OliWeb.Controls.Wortraum.BaumControl"   enableViewState="True"%>
<asp:imagebutton id="DateiImageButton" runat="server" width="80px" imagealign="Left" cssclass="Werbung"></asp:imagebutton>
<div align="right">
	<asp:label id="BaumLabel" runat="server" forecolor="Green" font-bold="True" font-size="X-Small"></asp:label>
    <img src="../../images/icons/Wortraum/Spiegel/BaumT.gif" runat="server" id="BaumTIMG"
         height="32" class="Baum"/>
	<asp:image id="EbeneImage" runat="server" imageurl="../../images/icons/leer11.gif"></asp:image>
	<asp:checkbox id="EditCheckBox" runat="server" autopostback="True" visible="False" oncheckedchanged="EditCheckBox_CheckedChanged"></asp:checkbox>
</div>
<asp:panel id="EditPanel" runat="server" visible="False" width="500px" cssclass="EditPanel">
	<table id="BaumSpiegelTable" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td>Baum</td>
			<td>
				<asp:textbox id="BaumTextBox" width="300px" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>Beschreibung</td>
			<td>
				<asp:textbox id="BeschreibungTextBox" width="300px" runat="server" rows="3" textmode="MultiLine"></asp:textbox></td>
		</tr>
		<tr>
			<td>Datei</td>
			<td>
				<asp:textbox id="DateiTextBox" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>
				<asp:button id="NeuButton" runat="server" text="neuB" onclick="NeuButton_Click"></asp:button>
				<asp:button id="NeuZweigButton" runat="server" text="neuZ" onclick="NeuZweigButton_Click"></asp:button></td>
			<td align="right">
				<asp:button id="UpdateButton" runat="server" text="update" onclick="UpdateButton_Click"></asp:button></td>
		</tr>
	</table>
</asp:panel>
