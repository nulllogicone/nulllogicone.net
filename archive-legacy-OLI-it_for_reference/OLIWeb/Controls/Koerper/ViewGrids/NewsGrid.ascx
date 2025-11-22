<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="NewsGrid.ascx.cs" Inherits="OliWeb.Controls.Koerper.ViewGrids.NewsGrid" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>

<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/StammSite.aspx"
        ToolTip="meine Nachrichten schließen"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
        {news} - sollten besser sortiert werden ... nach Datum DESC!

</div>
<asp:DataGrid ID="NewsDataGrid" runat="server" AutoGenerateColumns="False" AllowSorting="True"
    DataKeyField="PostItGuid" CellSpacing="3" PageSize="5" AllowPaging="True"
    GridLines="None">
    <HeaderStyle CssClass="TableHead"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn SortExpression="Angler" HeaderText="Q.A">
        <ItemStyle VerticalAlign="Top" />
            <ItemTemplate>
                <asp:Label ID="AnglerGuidLabel" runat="server" Visible="False" Text='<%# Eval("AnglerGuid") %>'>
                </asp:Label>
                <asp:LinkButton ID="AnglerLinkButton" runat="server" Text='<%# Eval("Angler") %>'
                    CommandName="AnglerLinkButton" CssClass="Angler">
                </asp:LinkButton><br />
                <asp:Button ID="AlleNewsGelesenButton" runat="server" Visible="False" Text="alles gelesen"
                    CommandName="AlleNewsGelesen" Font-Size="8"></asp:Button>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="Datum" HeaderText="gefischt">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Datum").ToString()) %>'
                    CssClass="datum">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
            <ItemStyle CssClass="PostIt"></ItemStyle>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server">
					<%# OliEngine.OliUtil.MakeImageHtml(Eval("Datei").ToString(),40) %>
					<b>
						<%# Eval("Titel") %>
					</b>
					<%# OliEngine.OliUtil.MakeHtmlLineBreak(OliEngine.OliUtil.FirstXWords(Eval("PostIt").ToString(),30)) %>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="KooK" HeaderText="KooK">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(Eval("KooK").ToString()) %>'
                    CssClass="flowKooK">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="AnzT" HeaderText="&lt;img border=0 alt=Antworten width=40 src=../images/icons/Symbole/TopLab.png&gt;">
            <ItemStyle HorizontalAlign="Center" CssClass="TopLab"></ItemStyle>
            <ItemTemplate>
                <asp:LinkButton CommandName="zeigT" runat="server" Text='<%# Eval("AnzT") %>'>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="PDatum" HeaderText="erstellt">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("PDatum").ToString()) %>'
                    CssClass="datum">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn Visible="False" HeaderText="gelesen">
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
            <ItemTemplate>
                <asp:Button ID="Button1" runat="server" Text="x" CommandName="gelesen" CommandArgument='<%# Eval("NewsGuid") %>'>
                </asp:Button>
                <asp:Label runat="server" ID="NewsGuidLabel" Visible="False" Text='<%# Eval("NewsGuid") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
