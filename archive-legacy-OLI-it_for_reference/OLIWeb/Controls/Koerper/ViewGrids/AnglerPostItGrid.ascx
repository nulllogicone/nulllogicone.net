<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="AnglerPostItGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.AnglerPostItGrid" %>
<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/AnglerSite.aspx"
        ToolTip="meine Trefferergebnisse schließen"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="AnglerDataGrid" runat="server" AllowPaging="True" PageSize="5"
 DataKeyField="PostItGuid" GridLines="None" CellSpacing="3" AllowSorting="True"
    AutoGenerateColumns="False">
    <HeaderStyle CssClass="TableHead"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn SortExpression="anzS" HeaderText="&lt;img border=0 alt=Urheber width=40 src=../images/icons/Symbole/Stamm.png&gt;">
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
            <ItemTemplate>
                <a class="Stamm" runat="server" href='<%# "~/Sites/PostItStammSite.aspx?pguid=" + 
					Eval("PostItGuid") %>' title="zeigt die Treffer mit Urhebern">
                    <%# Eval("anzS") %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
            <ItemStyle CssClass="PostIt"></ItemStyle>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server">
					<%# OliEngine.OliUtil.MakeImageHtml(Eval("Datei").ToString(),50) %>
					<b>
						<%# Eval("Titel") %>
					</b>
					<span style="font-weight:normal"><%# OliEngine.OliUtil.MakeHtmlLineBreak(OliEngine.OliUtil.FirstXWords(Eval("PostIt").ToString(),40)) %></span>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="Datum" HeaderText="erstellt">
         <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Datum").ToString()) %>'
                    CssClass="datum">
                </asp:Label><br />
                <%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(Eval("URL").ToString())) %>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="KooK" HeaderText="kook">
         <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("KooK", "{0:0.00}" ) %>'
                    CssClass="flowKooK">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="AnzA" HeaderText="&lt;img border=0 alt=Empf&#228;nger width=40 src='../images/icons/Symbole/Angler.png' &gt;" >
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
            <ItemTemplate>
                <a class="Angler" runat="server"  href='<%# "~/Sites/PostItAnglerSite.aspx?pguid=" + 
					Eval("PostItGuid") %>' title="zeigt die Treffer mit Empfängern">
                    <%# Eval("AnzA") %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="AnzT" HeaderText="&lt;img border=0 alt=Antworten width=40 src=../images/icons/Symbole/TopLab.png&gt;">
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
            <ItemTemplate>
            
                <a class="TopLab" runat="server"  href='<%# "~/Sites/PostItTopLabSite.aspx?pguid=" + 
					Eval("PostItGuid") %>' title="zeigt die Treffer mit Antworten">
                    <%# Eval("AnzT") %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn Visible="False">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("CodeGuid") %>'
                    ID="CodeGuidLabel" Visible="False">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn Visible="False" HeaderText="PostItZust">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("PostItZust") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
