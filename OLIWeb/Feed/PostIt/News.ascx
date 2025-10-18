<%@ Control Language="c#" AutoEventWireup="false" Codebehind="News.ascx.cs" Inherits="OliWeb.Feed.PostIt.News" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<P>
	<asp:DataList id="PostItDataList" runat="server">
		<ItemTemplate>
			<div class="news">
				<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "/default.aspx?pguid=" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
					<SPAN style="FLOAT: left;   text-align:left;">
						<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container.DataItem, "Datei").ToString(), 50) %>
					</SPAN><b>
						<%# DataBinder.Eval(Container.DataItem, "Titel") %>
					</b>
					<%# DataBinder.Eval(Container.DataItem, "PostIt") %>
				</a>
			</div>
		</ItemTemplate>
	</asp:DataList></P>
<P>
	<asp:Xml id="Xml1" runat="server"></asp:Xml></P>
