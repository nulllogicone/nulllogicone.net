<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabTopLabGrid" Src="TopLabTopLabGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PostItTopLabGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItTopLabGrid" %>
<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/PostItSite.aspx"
        ToolTip="Antworten schließen"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="PostItDataGrid" runat="server" AutoGenerateColumns="False" AllowSorting="True"
    CellSpacing="3" GridLines="None" DataKeyField="TopLabGuid" PageSize="5" AllowPaging="True" Width="100%">
    <HeaderStyle CssClass="TableHead"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn SortExpression="Stamm" HeaderText="Q.S">
            <ItemStyle VerticalAlign="Top" />
            <ItemTemplate>
                <a class="Stamm" runat="server" href='<%# "~/S/" + Eval("StammGuid") + ".aspx" %>'>
                    <%# OliEngine.OliUtil.MakeImageHtml(Eval("SDatei").ToString(),30) %>
                    <%# Eval("Stamm") %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="Datum" HeaderText="Q.T">
            <ItemStyle CssClass="TopLab" />
            <ItemTemplate>
                <div class="meta">
                    <div class="datum"><%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Datum").ToString()) %> </div>
                    <%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(Eval("TURL").ToString())) %></div>
                <a runat="server" href='<%# "~/Sites/TopLabSite.aspx?tguid=" + Eval("TopLabGuid")  %>'>
                    <b><%# Eval("Titel") %></b>
                    <br />
                    <%# OliEngine.OliUtil.MakeHtmlLineBreak(OliEngine.OliUtil.FirstXWords (Eval("TopLab").ToString(),50)) %>
                </a>
                <uc1:TopLabTopLabGrid ID="TopLabTopLabGrid1" runat="server" ParentTopLabGuidString='<%# Eval("TopLabGuid") %>'></uc1:TopLabTopLabGrid>
            </ItemTemplate>
        </asp:TemplateColumn>
        <%--Lohn--%>
        <asp:TemplateColumn SortExpression="Lohn" HeaderText="Lohn">
            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(Eval("Lohn").ToString() ) %>'
                    CssClass="flowKooK">
                </asp:Label>

            </ItemTemplate>
            <ItemStyle VerticalAlign="Top"></ItemStyle>
        </asp:TemplateColumn>
        <%--Toll--%>
        <asp:TemplateColumn SortExpression="DurchToll" HeaderText="Toll">
            <ItemTemplate>
                <uc1:TollChart ID="Tollchart2" runat="server" TollWert='<%# Eval("DurchToll").ToString().Length > 0 ? int.Parse(Eval("DurchToll").ToString()) : -1 %>'
                    Breite='50' Hoehe='10'></uc1:TollChart>
            </ItemTemplate>
            <ItemStyle VerticalAlign="Top"></ItemStyle>
        </asp:TemplateColumn>
        <asp:TemplateColumn Visible="False">
            <ItemTemplate>
                <asp:Label ID="StammGuidLabel" runat="server" Text='<%# Eval("StammGuid") %>'>
                </asp:Label>
                <asp:Label ID="TopLabGuidLabel" runat="server" Text='<%# Eval("TopLabGuid") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
