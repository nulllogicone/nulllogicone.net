<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Telepolis.ascx.cs" Inherits="OliWeb.Controls.Gimicks.Telepolis"  %>
	<asp:Label id="Label1" runat="server">Label</asp:Label><hr>
	<asp:Repeater id="Repeater1" runat="server">
		<ItemTemplate>
			<a href='<%# Eval("link") %>'>
				<b><%# Eval("title") %></b><br />
				<span style="font-size:8pt; color:black; font-weight:normal"><%# Eval("description") %></span>
			</a><hr>
		</ItemTemplate>
	</asp:Repeater>