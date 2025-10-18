<%@ Register TagPrefix="uc1" TagName="KlickBild" Src="../../Controls/Gimicks/KlickBild.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LogoControl" Src="../Floor/LogoControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EinAusLoggen" Src="../Floor/EinAusLoggen.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Kopf.ascx.cs" Inherits="OliWeb.Controls.Koerper.Kopf" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="CommandBar" Src="../Command/CommandBar.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserToString" Src="../../Controls/Gimicks/UserToString.ascx" %>
<table id="KopfTable" cellSpacing="1" cellPadding="1" width="100%" border="0">
	<TR>
		<TD>
			<uc1:einausloggen id="EinAusLoggen1" runat="server"></uc1:einausloggen>
		</TD>
		<TD vAlign="top"><asp:label id="NachrichtLabel" runat="server" Font-Size="Smaller" ForeColor="#004000" BackColor="LightYellow"></asp:label></TD>
		<TD vAlign="top" align="right"><uc1:logocontrol id="LogoControl1" runat="server"></uc1:logocontrol></TD>
	</TR>
</table>
<uc1:CommandBar id="CommandBar1" runat="server" Visible="False"></uc1:CommandBar>
