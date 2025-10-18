<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Repeater.ascx.cs" Inherits="OliWeb.Feed.PostIt.Repeater" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Repeater id="Repeater1" runat="server">
	<ItemTemplate>
	<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "/default.aspx?pguid=" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
	 <b><%# DataBinder.Eval(Container.DataItem, "Titel") %></b><br>
	 <%# DataBinder.Eval(Container.DataItem, "PostIt") %></a>
	 <hr size=1>
	</ItemTemplate>
</asp:Repeater>
