<%@ Control Language="c#" AutoEventWireup="True" Codebehind="StammListsControl.ascx.cs" Inherits="OliWeb.Controls.Floor.Suche.StammListsControl"  %>
<H3>
	<asp:Label id="TitelLabel" runat="server"></asp:Label></H3>
<P>
	<asp:DataGrid id="StammDataGrid" runat="server" AutoGenerateColumns="False" ShowHeader="False">
		<Columns>
			<asp:TemplateColumn>
				<ItemStyle CssClass="Stamm"></ItemStyle>
				<ItemTemplate>
					<a href='<%# "~/S/" + Eval("StammGuid") + ".aspx" %>'>
						<%# OliEngine.OliUtil.MakeImageHtml(Eval("Datei").ToString(),30) %>
						<%# Eval("Stamm") %>
					</a>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid></P>
