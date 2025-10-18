<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TopLabTopLabGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.TopLabTopLabGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="../../../Controls/Gimicks/TollChart.ascx" %>
<asp:datagrid id="TopLabDataGrid" ShowHeader="False" PageSize="5" GridLines="None" CellSpacing="3"
	AllowSorting="True" AutoGenerateColumns="False" runat="server">
	<HeaderStyle Font-Bold="True" BackColor="WhiteSmoke"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="Stamm" HeaderText="Q.S">
			<ItemTemplate>
				<a class="stamm" style="font-size:8pt" href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "Sites/TopLabSite.aspx?sguid=" + DataBinder.Eval(Container.DataItem, "StammGuid").ToString() %>'>
					<%# DataBinder.Eval(Container, "DataItem.Stamm") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="TopLab" HeaderText="Q.T">
			<ItemStyle CssClass="topLab"></ItemStyle>
			<ItemTemplate>
				<a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "Sites/TopLabSite.aspx?tguid=" + DataBinder.Eval(Container.DataItem, "TopLabGuid").ToString() %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.tdatei").ToString(),50) %>
					<%# OliEngine.OliUtil.MakeHtmlLineBreak(DataBinder.Eval(Container, "DataItem.TopLab")) %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Lohn" HeaderText="Lohn">
			<ItemTemplate>
				<asp:Label id=Label4 runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.Lohn").ToString() ) %>' CssClass="flowKooK">
				</asp:Label><br>
				<%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.TURL").ToString())) %>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="DurchToll" HeaderText="Toll">
			<ItemTemplate>
				<uc1:TollChart id="Tollchart2" runat="server" TollWert='<%# DataBinder.Eval(Container.DataItem,"DurchToll").ToString().Length > 0 ? int.Parse(DataBinder.Eval(Container.DataItem,"DurchToll").ToString()) : -1 %>' Breite='50' Hoehe='10'>
				</uc1:TollChart>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn Visible="False">
			<ItemTemplate>
				<asp:Label ID="StammGuidLabel" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StammGuid") %>'>
				</asp:Label>
				<asp:Label ID="TopLabGuidLabel" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TopLabGuid") %>'>
				</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle Position="TopAndBottom" PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
