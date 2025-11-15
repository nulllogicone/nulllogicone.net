<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BaumControl.ascx.cs" Inherits="OliWeb.Controls.Wortraum.BaumControl"   enableViewState="True"%>
<div >
<asp:imagebutton id="DateiImageButton" runat="server" width="80px" imagealign="Right" cssclass="Werbung"></asp:imagebutton>
<asp:checkbox id="EditCheckBox" runat="server" autopostback="True" designtimedragdrop="5" visible="False"></asp:checkbox>
<asp:image id="EbeneImage" runat="server" imageurl="../../images/icons/leer11.gif"></asp:image>
    <img src="../../images/icons/Wortraum/BaumT.gif" runat="server" id="BaumTIMG" height="32"
         class="Baum"/>
<asp:label id="BaumLabel" runat="server" forecolor="Green" font-bold="True" font-size="X-Small"></asp:label>
</div>
<asp:panel id="EditPanel" runat="server" visible="False" width="500px" cssclass="EditPanel">
	<table id="BaumControlTable" cellspacing="0" cellpadding="0" width="100%" border="0">
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
				<asp:button id="NeuButton" runat="server" text="neuB"></asp:button>
				<asp:button id="NeuZweigButton" runat="server" text="neuZ"></asp:button></td>
			<td align="right">
				<asp:button id="UpdateButton" runat="server" text="update"></asp:button></td>
		</tr>
	</table>
</asp:panel>
