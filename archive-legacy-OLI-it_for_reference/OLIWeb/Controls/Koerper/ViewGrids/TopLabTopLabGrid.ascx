<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="TopLabTopLabGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.TopLabTopLabGrid" %>
<%@ Register TagPrefix="uc1" TagName="TollChart" Src="../../../Controls/Gimicks/TollChart.ascx" %>
<asp:DataGrid ID="TopLabDataGrid" ShowHeader="False" PageSize="5" GridLines="None"
    CellSpacing="3" AllowSorting="True" AutoGenerateColumns="False" runat="server">
    <HeaderStyle Font-Bold="True" BackColor="WhiteSmoke"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn HeaderText="Kommentar"></asp:TemplateColumn>
        <asp:TemplateColumn SortExpression="TopLab" HeaderText="Q.T">
            <ItemTemplate>
                <div class="q">
                    Kommentar: 
                </div>
                <div class="TopLab" style="float: left;">
                    <div class="meta">
                        <%# OliEngine.OliUtil.MakeDateTimeDiff(Eval("Datum")) %><br />
                        <asp:Label ID="Label4" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(Eval("Lohn").ToString() ) %>'
                            CssClass="flowKooK">
                        </asp:Label>
                        <%# OliEngine.OliUtil.MakeLinkHtml(HttpUtility.HtmlDecode(Eval("TURL").ToString())) %><br />
                        <uc1:TollChart ID="Tollchart2" runat="server" TollWert='<%# Eval("DurchToll").ToString().Length > 0 ? int.Parse(Eval("DurchToll").ToString()) : -1 %>'
                            Breite='50' Hoehe='10'></uc1:TollChart>
                    </div>
                    <a id="A1" runat="server" class="Stamm" style="font-size: 8pt" href='<%# "~/Sites/TopLabSite.aspx?sguid=" + Eval("StammGuid").ToString() %>'>
                        <%# Eval("Stamm") %>
                    </a>
                    <a runat="server" href='<%# "~/Sites/TopLabSite.aspx?tguid=" + Eval("TopLabGuid").ToString() %>'>
                        <%# OliEngine.OliUtil.MakeImageHtml(Eval("tdatei").ToString(),50) %>
                        <strong>
                            <%# OliEngine.OliUtil.MakeHtmlLineBreak(Eval("Titel")) %></strong><br />
                        <%# OliEngine.OliUtil.MakeHtmlLineBreak(Eval("TopLab")) %>
                    </a>
                </div>
            </ItemTemplate>
        </asp:TemplateColumn>
        <%--Zwei unsichtbare Spalten--%>
        <asp:TemplateColumn Visible="False">
            <ItemTemplate>
                <asp:Label ID="StammGuidLabel" runat="server" Text='<%# Eval("StammGuid") %>'>
                </asp:Label>
                <asp:Label ID="TopLabGuidLabel" runat="server" Text='<%# Eval("TopLabGuid") %>'>
                </asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle Position="TopAndBottom" PageButtonCount="20" CssClass="pager" Mode="NumericPages">
    </PagerStyle>
</asp:DataGrid>
