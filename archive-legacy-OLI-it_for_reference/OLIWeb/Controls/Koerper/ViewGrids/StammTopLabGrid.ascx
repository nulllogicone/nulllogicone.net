<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="StammTopLabGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.StammTopLabGrid" %>
<%@ Import Namespace="OliEngine" %>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="~/Controls/Gimicks/TollChart.ascx" %>
<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/StammSite.aspx"
        ToolTip="meine Antworten schließen"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="TopLabDataGrid" runat="server" AllowSorting="True" AutoGenerateColumns="False"
    CellSpacing="3" GridLines="None" DataKeyField="PostItGuid" PageSize="5" AllowPaging="True">
    <HeaderStyle CssClass="TableHead"></HeaderStyle>
    <ItemStyle VerticalAlign="Top" />
    <Columns>
        <%--PostIt erstellt --%>
        <asp:TemplateColumn SortExpression="PDatum" HeaderText="erstellt" ItemStyle-VerticalAlign="Top">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%#                OliUtil.MakeDateTimeDiff(Eval("PDatum").ToString()) %>' CssClass="datum">
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        <%--PostIt--%>
        <asp:TemplateColumn SortExpression="PostIt" HeaderText="Q.P">
            <ItemStyle CssClass="PostIt"></ItemStyle>
            <ItemTemplate>     <div class="meta"><%# OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(Eval("PURL").ToString())) %></div>
                <a runat="server" href='<%# "~/P/" + Eval("PostItGuid") + ".aspx" %>'>
               
                    <span><%# OliUtil.MakeImageHtml(Eval("PDatei").ToString(), 30) %></span>
                     <b><%#Eval("PTitel") %></b>
                    <br />
                    <%# OliUtil.FirstXWords(Eval("PostIt").ToString(), 30) %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <%--TopLab--%>
        <asp:TemplateColumn SortExpression="TopLab" HeaderText="Q.T">
            <ItemStyle CssClass="TopLab"></ItemStyle>
            <ItemTemplate>
                <div class="meta">
                    <asp:Label ID="Label7" runat="server" Text='<%#                OliUtil.MakeRedKook(Eval("Lohn").ToString()) %>'
                        CssClass="flowKooK">
                    </asp:Label>
                    <%#                OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(Eval("TURL").ToString())) %>
                    <uc1:TollChart ID="Tollchart2" runat="server" TollWert='<%#                Eval("DurchToll").ToString().Length > 0 ? int.Parse(Eval("DurchToll").ToString()) : -1 %>'
                        Breite='50' Hoehe='10'></uc1:TollChart>
                </div>
                <a runat="server" href='<%#                "~/T/" + Eval("TopLabGuid") + ".aspx" %>'>
                    <%#                OliUtil.MakeImageHtml(Eval("TDatei").ToString(), 50) %>
                    <b>
                        <%#Eval("TTitel") %>
                    </b>
                    <br />
                    <%#                OliUtil.FirstXWords(Eval("TopLab").ToString(), 30) %>
                </a>
            </ItemTemplate>
        </asp:TemplateColumn>
        <%--TopLab erstellt--%>
        <asp:TemplateColumn SortExpression="TDatum" HeaderText="erstellt">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%#                OliUtil.MakeDateTimeDiff(Eval("TDatum").ToString()) %>'
                    CssClass="datum">
                </asp:Label>

            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle PageButtonCount="20" CssClass="pager" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
