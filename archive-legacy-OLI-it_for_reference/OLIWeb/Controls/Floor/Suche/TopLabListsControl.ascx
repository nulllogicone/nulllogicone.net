<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TopLabListsControl.ascx.cs" Inherits="OliWeb.Controls.Floor.Suche.TopLabListsControl"  %>
<H3>
	<asp:Label id="TitelLabel" runat="server"></asp:Label></H3>
<P>
	<asp:DataGrid id="TopLabDataGrid" runat="server" AutoGenerateColumns="False" ShowHeader="False">
		<Columns>
			<asp:TemplateColumn>
				<ItemStyle CssClass="TopLab"></ItemStyle>
				<ItemTemplate>
									<a href='<%# "~/T/" + Eval("TopLabGuid") + ".aspx" %>'>
						<%# OliEngine.OliUtil.MakeImageHtml(Eval("Datei").ToString(),30) %>
						<b><%# Eval("Titel") %></b>
						<%# Eval("TopLab") %>
					</a>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid></P>
