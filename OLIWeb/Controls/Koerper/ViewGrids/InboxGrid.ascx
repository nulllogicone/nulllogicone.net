<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="InboxGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.InboxGrid" %>
<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/StammSite.aspx"
        ToolTip="meine Nachrichten schließen"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="InboxDataGrid" AutoGenerateColumns="False" AllowSorting="True"
    runat="server" DataKeyField="TopLabGuid" CellSpacing="3"  GridLines="None">
    <HeaderStyle CssClass="TableHead"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn SortExpression="datum" HeaderText="Datum">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Datum").ToString()) %>'
                    CssClass="datum">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
            <ItemStyle CssClass="PostIt"></ItemStyle>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton2" runat="server">
					<%# OliEngine.OliUtil.MakeImageHtml(Eval("PDatei").ToString(),30) %>
					<b>
						<%# Eval("PTitel") %>
					</b>
					<%# OliEngine.OliUtil.FirstXWords (Eval("PostIt").ToString(), 30) %>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="TopLab" HeaderText="Q.T">
            <ItemStyle CssClass="TopLab"></ItemStyle>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server">
					<%# OliEngine.OliUtil.MakeImageHtml(Eval("TDatei").ToString(),30) %>
					<b>
						<%# Eval("TTitel") %>
					</b>
					<%# Eval("TopLab") %>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="DurchToll" HeaderText="Toll">
            <ItemTemplate>
                <uc1:TollChart ID="Tollchart2" runat="server" TollWert='<%# Eval("DurchToll").ToString().Length > 0 ? int.Parse(Eval("DurchToll").ToString()) : -1 %>'
                    Breite='50' Hoehe='10'></uc1:TollChart>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn Visible="False" HeaderText="gelesen">
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
            <ItemTemplate>
                <asp:Button ID="Button1" runat="server" Text="x" CommandName="gelesen" CommandArgument='<%# Eval("InboxGuid") %>'>
                </asp:Button>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
