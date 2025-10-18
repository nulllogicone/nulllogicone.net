<%@ Control Language="c#" AutoEventWireup="false" Codebehind="NetzControl.ascx.cs" Inherits="OliWeb.Controls.Wortraum.NetzControl"   enableViewState="True"%>
<script language="javascript">
	function MouseOver(tr){
		tr.style.backgroundColor = "#f1f1ff";
	}
	function MouseOut(tr){
		tr.style.backgroundColor = "#ffffff";
	}
	function OnClick(tr){
		var cb = tr.getElementsByTagName("EditCheckBox");
		cb[0].checked = !cb[0].checked;
	}
</script>
<!-- <div onmouseover="MouseOver(this)" onmouseout="MouseOut(this)" style="display:none;"> -->
<div>
<asp:imagebutton id="DateiImageButton" width="80px" imagealign="Right" runat="server" cssclass="Werbung"></asp:imagebutton>
<asp:checkbox id="EditCheckBox" runat="server" autopostback="True" visible="False"></asp:checkbox>
<asp:image id="EbeneImage" runat="server" imageurl="../../images/icons/leer11.gif"></asp:image>
    <img id="NetzTIMG" src="../../images/icons/Wortraum/NetzT.gif" runat="server" height="32"
         class="netz" />
<asp:label id="NetzLabel" runat="server" forecolor="Navy" font-bold="True" font-size="X-Small"></asp:label>
<asp:button id="Button1" runat="server" text="Button" visible="False"></asp:button>
</div>
<asp:panel id="EditPanel" runat="server" visible="False" width="500px" cssclass="EditPanel">
	<table id="NetzControlTable" cellspacing="0" cellpadding="0" width="100%" border="0">
		<tr>
			<td>Netz</td>
			<td>
				<asp:textbox id="NetzTextBox" runat="server" width="300px"></asp:textbox></td>
		</tr>
		<tr>
			<td>Beschreibung</td>
			<td>
				<asp:textbox id="BeschreibungTextBox" runat="server" width="300px" rows="3" textmode="MultiLine"></asp:textbox></td>
		</tr>
		<tr>
			<td>Datei</td>
			<td>
				<asp:textbox id="DateiTextBox" runat="server"></asp:textbox></td>
		</tr>
		<tr>
			<td>
				<asp:button id="NeuButton" runat="server" text="neuN"></asp:button>
				<asp:button id="NeuKnotenButton" runat="server" text="neuK"></asp:button></td>
			<td align="right">
				<asp:button id="UpdateButton" runat="server" text="update"></asp:button></td>
		</tr>
	</table>
</asp:panel>
