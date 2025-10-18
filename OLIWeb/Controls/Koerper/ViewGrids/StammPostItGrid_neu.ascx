<%@ Control Language="c#" AutoEventWireup="false" Codebehind="StammPostItGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.StammPostItGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="True"%>
<%@ Register TagPrefix="uc1" TagName="Legende" Src="../../../Sites/Elemente/Legende.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class="rechtsuntenrund">
	<uc1:closehyperlink id="CloseHyperLink1" runat="server" navigateurl="~/Sites/StammSite.aspx" tooltip="meine Nachrichten schließen"></uc1:closehyperlink>
	<h3><asp:label id="TitleLabel" runat="server"></asp:label></h3>
</div>
<asp:datagrid id="PostItDataGrid" runat="server" pagesize="5" allowpaging="True" datakeyfield="PostItGuid"
	cellspacing="3" gridlines="None" autogeneratecolumns="False" allowsorting="True" width="600px">
	<HeaderStyle Font-Bold="True" CssClass="TableHead"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn>
			<ItemStyle Font-Size="X-Small" Wrap="False"></ItemStyle>
			<ItemTemplate>
				<asp:Label id="Label1" runat="server" Text='<%# "Frist: " + OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Frist").ToString()) %>' CssClass="datum">
				</asp:Label><BR>
				bezahlt:
				<asp:Label id="Label2" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.bezahlt").ToString() ) %>' CssClass="flowKooK">
				</asp:Label><br>
				
				<a runat="server" visible='<%# int.Parse(DataBinder.Eval(Container.DataItem,"AnzS").ToString()) > 1 %>' class="stamm" style="text-align:center; "
						href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + 
						"Sites/PostItStammSite.aspx?pguid=" + 
						DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
					<%# "Urheber: " + DataBinder.Eval(Container, "DataItem.anzS") %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn SortExpression="PostIt" HeaderText="Nachrichten">
			<ItemStyle CssClass="PostIt"></ItemStyle>
			<ItemTemplate>
				<span style="float:right;display:block;font-size:8pt;">
					<asp:Label id="Label4" runat="server" CssClass="Datum" Text='<%# "erstellt: " + OliEngine.OliUtil.MakeDateTimeDiff(DataBinder.Eval(Container, "DataItem.Datum").ToString()) %>'>
					</asp:Label><br>
					Wert:
					<asp:Label id="KooKLabel" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(DataBinder.Eval(Container, "DataItem.KooK").ToString()) %>' CssClass="flowKooK" >
					</asp:Label>
					<br>
					<%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.URL").ToString())) %>
				</span><a href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + "Sites/PostItSite.aspx?pguid=" + DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
					<%# OliEngine.OliUtil.MakeImageHtml(DataBinder.Eval(Container, "DataItem.Datei").ToString(),50) %>
					<b>
						<%# DataBinder.Eval(Container, "DataItem.Titel") %>
					</b>
					<%# OliEngine.OliUtil.FirstXWords (DataBinder.Eval(Container.DataItem, "PostIt").ToString(),30) %>
				</a>
				<div style="BACKGROUND-COLOR: white;clear:both;">
				<a class="angler" style="text-align:center; " runat="server" visible='<%# DataBinder.Eval(Container.DataItem,"AnzA").ToString().Length > 0 %>'
						href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + 
						"Sites/PostItAnglerSite.aspx?pguid=" + 
						DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
						<%# "Empfänger: " + DataBinder.Eval(Container, "DataItem.anzA") %>
					</a><a 	style="text-align:center; " runat="server" visible='<%# DataBinder.Eval(Container.DataItem,"AnzT").ToString().Length > 0 %>'
						href='<%# OliWeb.Klassen.Helper.MakeBaseLink() + 
						"Sites/PostItTopLabSite.aspx?pguid=" + 
						DataBinder.Eval(Container.DataItem, "PostItGuid") %>'>
						<%# "Antworten: " + DataBinder.Eval(Container, "DataItem.anzT") %>
					</a>
				</div>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
	<PagerStyle NextPageText="weiter" PrevPageText="zur&#252;ck" PageButtonCount="20" CssClass="pager"
		Mode="NumericPages"></PagerStyle>
</asp:datagrid>
<uc1:legende id="Legende1" runat="server"></uc1:legende>
