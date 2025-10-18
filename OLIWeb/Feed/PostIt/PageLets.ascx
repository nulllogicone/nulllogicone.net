<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PageLets.ascx.cs" Inherits="OliWeb.Feed.PostIt.PageLets" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Repeater id="Repeater1" runat="server">
	<ItemTemplate>
		<span class="PageLets"><a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "/default.aspx?pguid=" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
				<SPAN style="FLOAT: left">
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container.DataItem, "Datei").ToString(), 40) %>
				</SPAN><b>
					<%# DataBinder.Eval(Container.DataItem, "Titel") %>
				</b>
				<%# DataBinder.Eval(Container.DataItem, "PostIt") %>
			</a></span>
	</ItemTemplate>
</asp:Repeater>
