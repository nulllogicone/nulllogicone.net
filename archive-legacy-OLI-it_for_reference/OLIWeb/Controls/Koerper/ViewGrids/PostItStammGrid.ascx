<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PostItStammGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItStammGrid" %>
<div class="rechtsuntenrund">
    <div align="right">
        <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/PostItSite.aspx"
            ToolTip="Urheber schließen"></uc1:CloseHyperLink>
    </div>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="PostItDataGrid" runat="server" AutoGenerateColumns="False" AllowSorting="True"
    CellSpacing="3" Width="100%" GridLines="None" DataKeyField="StammGuid" PageSize="5" AllowPaging="True">
    <HeaderStyle CssClass="TableHead"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn SortExpression="Stamm" HeaderText="Q.S">
            <ItemStyle  />
            <ItemTemplate>
                <div class='Stamm <%# myCssClass(Eval("Stamm").ToString(), int.Parse(Eval("StammZust").ToString())) %>'>
                    <%# OliEngine.OliUtil.MakeImageHtml(Eval("Datei").ToString(),50) %>
                    <a runat="server" href='<%#"~/Sites/PostItSite.aspx?sguid=" + Eval("StammGuid") %>'>
                        <%# Eval("Stamm") %>
                    </a>
                </div>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="bezahlt" HeaderText="zahlt">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(Eval("bezahlt").ToString() ) %>'
                    CssClass="flowKooK">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="Frist" HeaderText="Frist">
            <ItemStyle HorizontalAlign="Right"></ItemStyle>
            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" Text='<%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Frist").ToString()) %>'
                    CssClass="datum">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn Visible="False">
            <ItemTemplate>
                <asp:Label runat="server" ID="StammZustLabel" Text='<%# Eval("StammZust") %>'>
                </asp:Label>
                <asp:Label runat="server" ID="ClosedLabel" Text='<%# Eval("closed") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
