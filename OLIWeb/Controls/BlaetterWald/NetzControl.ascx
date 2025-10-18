<%@ Control Language="c#" AutoEventWireup="false" Codebehind="NetzControl.ascx.cs" Inherits="OliWeb.Controls.BlaetterWald.NetzControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<LINK href="BlaetterWald.css" type="text/css" rel="stylesheet">
<P align="left"><asp:label id="NetzKetteLabel" CssClass="Netz" runat="server" Font-Size="Smaller"></asp:label></P>
<P><asp:label id="NetzLabel" CssClass="Netz" runat="server" Font-Size="Large" Font-Bold="True"></asp:label><BR>
	<asp:label id="BeschreibungLabel" CssClass="kommentar" runat="server"></asp:label></P>
<asp:datagrid id="KnotenDataGrid" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeader="False">
	<Columns>
		<asp:TemplateColumn HeaderText="Knoten / Beschreibung">
			<ItemTemplate>
				<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Knoten") %>'>
				</asp:Label><BR>
				<asp:Label id=Label3 CssClass="kommentar" runat="server" NAME="Label2" Text='<%# DataBinder.Eval(Container, "DataItem.KnotenBeschreibung") %>'>
				</asp:Label><BR>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Vorgabe" ItemStyle-Wrap="false">
			<ItemTemplate>
				<asp:Image id="Image1" runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/OLIs/" + DataBinder.Eval(Container, "DataItem.VgbOlis") + ".gif" %>'>
				</asp:Image>
				<asp:Image id="Image2" runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/OLIs/" + DataBinder.Eval(Container, "DataItem.VgbGet") + ".gif" %>'>
				</asp:Image>
				<asp:Image id="Image3" runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/ILOs/" + DataBinder.Eval(Container, "DataItem.VgbIlos") + ".gif" %>'>
				</asp:Image>
				<asp:Image id="Image4" runat="server" ImageUrl='<%# OliEngine.OliCommon.PunkteOrdner + "/ILOs/" + DataBinder.Eval(Container, "DataItem.VgbFit") + ".gif" %>'>
				</asp:Image>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="weiter">
			<ItemTemplate>
				<asp:HyperLink id="Hyperlink4" CssClass="hoverWeiterNetz" runat="server" visible='<%# DataBinder.Eval(Container, "DataItem.WeiterNetzGuid").ToString().Length > 0 %>' NavigateUrl='<%# "BlaetterWald.aspx?nguid=" + DataBinder.Eval(Container, "DataItem.WeiterNetzGuid") %>' NAME="Hyperlink1">
					<img src="../../images/icons/Wortraum/WeiterNetz.gif" border="0"></asp:HyperLink>
				<asp:HyperLink id="Hyperlink5" CssClass="hoverWeiterBaum" runat="server" visible='<%# DataBinder.Eval(Container, "DataItem.WeiterBaumGuid").ToString().Length > 0 %>' NavigateUrl='<%# "BlaetterWald.aspx?nguid=" + DataBinder.Eval(Container, "DataItem.NetzGuid") + "&amp;bguid=" + DataBinder.Eval(Container.DataItem, "WeiterBaumGuid") %>' NAME="Hyperlink2">
					<img src="../../images/icons/Wortraum/WeiterBaum.gif" border="0"></asp:HyperLink>
				<asp:HyperLink id="Hyperlink6" CssClass="hoverWeiterBaum" runat="server" visible='<%# DataBinder.Eval(Container, "DataItem.WeiterBaumGuid").ToString().Length == 0 && DataBinder.Eval(Container, "DataItem.WeiterNetzGuid").ToString().Length == 0 %>' NavigateUrl='<%# "BlaetterWald.aspx?nguid=" + DataBinder.Eval(Container, "DataItem.NetzGuid") %>' NAME="Hyperlink2">
					<img src="../../images/icons/Wortraum/WeiterNix.gif" border="0"></asp:HyperLink>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</asp:datagrid>
<P align="left"><asp:hyperlink id="EditHyperLink" runat="server" Font-Size="XX-Small" Target="netz">* edit</asp:hyperlink></P>
