<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PostItCodeGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.PostItCodeGrid" %>
<div class="rechtsuntenrund">
    <div align="right">
        <uc1:CloseHyperLink ID="CloseHyperLink1" runat="server" NavigateUrl="~/Sites/PostItSite.aspx"
            ToolTip="Code schließen"></uc1:CloseHyperLink>
    </div>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="CodeDataGrid" runat="server" Font-Size="Smaller" AutoGenerateColumns="False"
    BorderWidth="1px" BackColor="White" CellPadding="0" DataKeyField="CodeGuid" Width="535px"
    GridLines="Horizontal" BorderStyle="None">
    <ItemStyle ForeColor="#4A3C8C" BackColor="White"></ItemStyle>
    <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" BackColor="#4A3C8C"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn HeaderText="Stamm">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" Style="font-size: x-small" Text='<%# OliEngine.OliMiddleTier.DbDirect.GiveStamm(Eval("StammGuid").ToString()) %>'
                    CssClass="Stamm" runat="server">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Code">
            <ItemTemplate>
                <asp:LinkButton ID="Linkbutton2" ToolTip="Code anzeigen" runat="server" Text='<%# Eval("Kommentar") %>'
                    CommandName="code" Font-Bold="true">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="Empf&#228;nger">
            <ItemTemplate>
                <asp:LinkButton ID="Label2" ToolTip="Anzahl der Empfänger" runat="server" CommandName="angler"
                    ForeColor="CornflowerBlue" Text='<%# Eval("AnzA") %>'>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateColumn>
        <asp:ButtonColumn Visible="False" Text="del" ButtonType="PushButton" CommandName="del">
        </asp:ButtonColumn>
    </Columns>
</asp:DataGrid>
