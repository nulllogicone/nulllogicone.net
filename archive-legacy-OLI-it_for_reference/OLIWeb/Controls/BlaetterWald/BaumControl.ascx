<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BaumControl.ascx.cs" Inherits="OliWeb.Controls.BlaetterWald.BaumControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<LINK href="BlaetterWald.css" type="text/css" rel="stylesheet">
<P align="left">
	<asp:Label id="BaumKetteLabel" runat="server" CssClass="Baum" Font-Size="Smaller"></asp:Label></P>
<P><asp:label id="BaumLabel" runat="server" CssClass="baum" Font-Size="Large"></asp:label><BR>
	<asp:Label id="BeschreibungLabel" runat="server" CssClass="kommentar"></asp:Label></P>
<P><asp:repeater id="ZweigeRepeater" runat="server">
		<ItemTemplate>
			<li>
				<%# DataBinder.Eval(Container.DataItem, "Zweig") %>
				<asp:HyperLink runat="server" Text="weiterNetz" NavigateUrl='<%# "BlaetterWald.aspx?nguid=" + DataBinder.Eval(Container, "DataItem.WeiterNetzGuid") %>' visible='<%# DataBinder.Eval(Container, "DataItem.WeiterNetzGuid").ToString().Length > 0 %>' ID="Hyperlink1" NAME="Hyperlink1">
					<img src="../../images/icons/Wortraum/WeiterNetz.gif" border="0">
				</asp:HyperLink>
				<asp:HyperLink id="Hyperlink3" CssClass="hoverWeiterBaum" runat="server" visible='<%# DataBinder.Eval(Container, "DataItem.WeiterBaumGuid").ToString().Length > 0 %>' NavigateUrl='<%# "BlaetterWald.aspx?nguid=" + NetzGuid + "&amp;bguid=" + DataBinder.Eval(Container.DataItem, "WeiterBaumGuid") %>' NAME="Hyperlink2">
					<img src="../../images/icons/Wortraum/WeiterBaum.gif" border="0"></asp:HyperLink>
				<asp:HyperLink id="Hyperlink2" CssClass="hoverWeiterBaum" runat="server" visible='<%# DataBinder.Eval(Container, "DataItem.WeiterBaumGuid").ToString().Length == 0 && DataBinder.Eval(Container, "DataItem.WeiterNetzGuid").ToString().Length == 0 %>' NavigateUrl='<%# "BlaetterWald.aspx?nguid=" + NetzGuid %>' NAME="Hyperlink2">
					<img src="../../images/icons/Wortraum/WeiterNix.gif" border="0"></asp:HyperLink>
			</li>
		</ItemTemplate>
	</asp:repeater></P>
<P align="left">
	<asp:HyperLink id="EditHyperLink" runat="server" Font-Size="XX-Small" Target="_blank" Visible="False">* edit</asp:HyperLink></P>
