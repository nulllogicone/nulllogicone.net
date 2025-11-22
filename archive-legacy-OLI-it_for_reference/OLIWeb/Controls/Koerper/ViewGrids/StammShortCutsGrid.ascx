<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="StammShortCutsGrid.ascx.cs"
    Inherits="OliWeb.Controls.Koerper.ViewGrids.StammShortCutsGrid" %>
<%@ Register TagPrefix="uc1" TagName="WortraumController" Src="../../Wortraum/WortraumController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CloseHyperLink" Src="../../Command/GetCommand/SpecialCommand/CloseHyperLink.ascx" %>
<div class="rechtsuntenrund">
    <uc1:CloseHyperLink ID="CloseHyperLink1" ToolTip="meine Nachrichten schließen" NavigateUrl="~/Sites/StammSite.aspx"
        runat="server"></uc1:CloseHyperLink>
    <h3>
        <asp:Label ID="TitleLabel" runat="server"></asp:Label></h3>
</div>
<asp:DataGrid ID="ShortCutsDataGrid" runat="server" CellSpacing="2" BackColor="#DEBA84"
    BorderStyle="None" Width="400px" DataKeyField="ShortCutsGuid" CellPadding="3"
    BorderWidth="1px" BorderColor="#DEBA84" AutoGenerateColumns="False" Font-Size="Smaller">
    <FooterStyle ForeColor="#8C4510" BackColor="#F7DFB5"></FooterStyle>
    <SelectedItemStyle Font-Bold="True" BackColor="#738A9C"></SelectedItemStyle>
    <EditItemStyle BackColor="FloralWhite"></EditItemStyle>
    <ItemStyle ForeColor="#8C4510" BackColor="#FFF7E7"></ItemStyle>
    <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#A55129"></HeaderStyle>
    <Columns>
        <asp:TemplateColumn HeaderText="Favoriten Markierungen">
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="True" ForeColor="Sienna"
                    CommandName="select">
					<%# Eval("ShortCut") %>
                </asp:LinkButton>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="ShortCutsTextBox" runat="server" Width="200" Text='<%# Eval("ShortCut") %>'>
                </asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn HeaderText="auto">
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Eval("auto") %>'
                    Enabled="False"></asp:CheckBox>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:CheckBox ID="AutoCheckBox" runat="server" Checked='<%# Eval("auto") %>'>
                </asp:CheckBox>
            </EditItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:LinkButton ID="LinkButton4" runat="server" Text="speichern" CommandName="Update"></asp:LinkButton>&nbsp;
                <asp:LinkButton runat="server" Text="Löschen" CommandName="del" CausesValidation="false"
                    ID="Linkbutton1" name="Linkbutton1"></asp:LinkButton>
            </EditItemTemplate>
        </asp:TemplateColumn>
    </Columns>
    <PagerStyle HorizontalAlign="center" ForeColor="#8C4510" Mode="NumericPages"></PagerStyle>
</asp:DataGrid>
<p>
    <asp:LinkButton ID="NeuShortCutsButton" title="neue ShortCuts" runat="server" CssClass="BigButton"
        Font-Bold="True" OnClick="NeuShortCutsButton_Click">+ ShortCut</asp:LinkButton><br />
    <uc1:WortraumController ID="WortraumController1" runat="server" Visible="False">
    </uc1:WortraumController>
</p>
