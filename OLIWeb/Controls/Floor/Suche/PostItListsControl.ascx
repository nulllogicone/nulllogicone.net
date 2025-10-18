<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PostItListsControl.ascx.cs" Inherits="OliWeb.Controls.Floor.Suche.PostItListsControl"  %>
<H3>
	<asp:Label id="TitelLabel" runat="server"></asp:Label></H3>
<P>
	<asp:DataGrid id="PostItDataGrid" runat="server" AutoGenerateColumns="False" ShowHeader="False">
		<Columns>
			<asp:TemplateColumn>
				<ItemStyle CssClass="PostIt"></ItemStyle>
				<ItemTemplate>
				<a href='<%# "~/P/" + Eval("PostItGuid") + ".aspx" %>'>
										<%# OliEngine.OliUtil.MakeImageHtml(Eval("Datei").ToString(),30) %>
						<b><%# Eval("Titel") %></b>
						<%# Eval("PostIt") %>
					</a>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid></P>
