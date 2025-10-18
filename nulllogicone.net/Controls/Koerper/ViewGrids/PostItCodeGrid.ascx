<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PostItCodeGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItCodeGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="rechtsuntenrund">
	<div align="right">
		<uc1:closehyperlink id="CloseHyperLink1" runat="server" navigateurl="~/Sites/PostItSite.aspx" tooltip="Code schließen"></uc1:closehyperlink>
	</div>
	<h3><asp:label id="TitleLabel" runat="server"></asp:label></h3>
</div>
<asp:datagrid id="CodeDataGrid" runat="server" font-size="Smaller" autogeneratecolumns="False"
	borderwidth="1px" backcolor="White" cellpadding="0" datakeyfield="CodeGuid" width="535px"
	gridlines="Horizontal" borderstyle="None">
	<ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
	<HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
	<Columns>
		<asp:TemplateColumn HeaderText="Stamm">
			<ItemTemplate>
				<asp:LinkButton id="LinkButton1" style="FONT-SIZE: x-small" Text='<%# OliEngine.OliMiddleTier.DbDirect.GiveStamm(DataBinder.Eval(Container, "DataItem.StammGuid").ToString()) %>' CssClass="stamm" Runat="server">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Code">
			<ItemTemplate>
				<asp:LinkButton id="Linkbutton2" ToolTip="Code anzeigen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Kommentar") %>' CommandName="code" Font-Bold="true">
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Empf&#228;nger">
			<ItemTemplate>
				<asp:LinkButton id="Label2" ToolTip="Anzahl der Empfänger" runat="server" CommandName="angler" ForeColor="CornflowerBlue" Text='<%# DataBinder.Eval(Container, "DataItem.AnzA") %>'>
				</asp:LinkButton>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:ButtonColumn Visible="False" Text="del" ButtonType="PushButton" CommandName="del"></asp:ButtonColumn>
	</Columns>
</asp:datagrid>
