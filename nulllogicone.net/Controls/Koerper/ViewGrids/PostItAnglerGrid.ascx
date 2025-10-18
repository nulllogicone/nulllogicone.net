<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PostItAnglerGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItAnglerGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class=rechtsuntenrund>
	<div align=right><uc1:closehyperlink id=CloseHyperLink1 ToolTip="Empfänger schließen" NavigateUrl="~/Sites/PostItSite.aspx"
			runat="server"></uc1:closehyperlink></div>
	<h3><asp:label id=TitleLabel runat="server"></asp:label></h3>
</div>
<asp:datagrid id=PostItDataGrid runat="server" AllowPaging="True" PageSize="5" DataKeyField="StammGuid"
	GridLines="None" CellSpacing="2" AllowSorting="True" AutoGenerateColumns="False" Width="400px"
	BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" BackColor="#DEBA84" CellPadding="3">
	<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#738A9C"></SelectedItemStyle>
	<ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
	<HeaderStyle Font-Bold="True" HorizontalAlign="Center" ForeColor="White" CssClass="TableHead"
		BackColor="White"></HeaderStyle>
	<FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
	<Columns>
		<asp:TemplateColumn SortExpression="Stamm" HeaderText="&lt;img border=0 alt=Stamm width=40 src=../images/icons/Symbole/Stamm.jpg&gt;">
			<ItemTemplate>
				<a class="stamm" href='<%# "http://nulllogicone.net/Stamm/?" + DataBinder.Eval(Container.DataItem, "StammGuid")  %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.SDatei").ToString(),30) %>
					<%# DataBinder.Eval(Container.DataItem, "Stamm") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="Angler" HeaderText="&lt;img border=0 alt=Angler width=40 src=../images/icons/Symbole/Angler.jpg&gt;">
			<ItemStyle CssClass="Angler"></ItemStyle>
			<ItemTemplate>
				<a href='<%# "http://nulllogicone.net/Angler/?" + DataBinder.Eval(Container.DataItem, "AnglerGuid")  %>'>
					<%# DataBinder.Eval(Container.DataItem, "Angler") %>
					<span class="id">
						<%# DataBinder.Eval(Container.DataItem, "Beschreibung") %>
					</span></a>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle HorizontalAlign="Left" ForeColor="#8C4510" BackColor="White" PageButtonCount="20"
		CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
