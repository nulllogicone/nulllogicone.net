<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Banner.ascx.cs" Inherits="OliWeb.Feed.PostIt.Banner" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Repeater id="Repeater1" runat="server">
	<ItemTemplate>
		<div class="banner">
			<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "/default.aspx?pguid=" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
				<SPAN style="FLOAT: left">
				<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container.DataItem, "Datei").ToString(), 40) %></span> 
				<b>
					<%# DataBinder.Eval(Container.DataItem, "Titel") %>
				</b>
				<%# DataBinder.Eval(Container.DataItem, "PostIt") %>
			</a>
		</div>
	</ItemTemplate>
</asp:Repeater>
