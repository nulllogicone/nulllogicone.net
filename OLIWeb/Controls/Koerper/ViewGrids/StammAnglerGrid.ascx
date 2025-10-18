<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="StammAnglerGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.StammAnglerGrid" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" ToolTip="meine Filterprofile schließen"
        NavigateUrl="~/Sites/StammSite.aspx" runat="server"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<div id="StammAnglerTable">
    <asp:DataGrid ID="AnglerDataGrid" runat="server" BorderWidth="1px" DataKeyField="AnglerGuid"
        GridLines="Horizontal" AutoGenerateColumns="False" AllowSorting="True" Width="100%"
        OnSelectedIndexChanged="AnglerDataGrid_SelectedIndexChanged">
        <HeaderStyle CssClass="TableHead"></HeaderStyle>
        <Columns>
            <asp:TemplateColumn SortExpression="Angler" HeaderText="Q.A">
                <ItemTemplate>
                    <a class="Angler" runat="server" href='<%# "~/A/" + Eval("AnglerGuid") + ".aspx" %>'>
                        <%# Eval("Angler") %>
                        <span class="klein">
                            <%# Eval("Beschreibung") %>
                        </span></a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="AnzP" HeaderText="Treffer">
               <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <a class="PostIt" runat="server" href='<%# "~/Sites/AnglerPostItSite.aspx?aguid=" + Eval("AnglerGuid") %>'>
                        <%# Eval("AnzP") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn SortExpression="SumT" HeaderText="Wert">
                <ItemStyle BackColor="White" HorizontalAlign="Right"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# OliEngine.OliUtil.MakeRedKook(Eval("SumT").ToString() ) %>'
                        CssClass="flowKooK">
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:ButtonColumn Visible="False" Text="del" ButtonType="PushButton" CommandName="Delete">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:ButtonColumn>
        </Columns>
    </asp:DataGrid>
</div>
<br />
