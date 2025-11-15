<%@ Page ValidateRequest="false" Language="c#" CodeBehind="TopLabEdit.aspx.cs" AutoEventWireup="True"
    Inherits="OliWeb.Sites.Edit.TopLabEdit" MasterPageFile="~/Site.Master" %>

<%@ Register TagPrefix="uc1" TagName="BildPicker" Src="../../Controls/Gimicks/BildPicker.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PostItKoerper" Src="../../Controls/Koerper/PostItKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StammKoerper" Src="../../Controls/Koerper/StammKoerper.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TopLabOrgan" Src="../../Controls/Koerper/Organ/TopLabOrgan.ascx" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>TopLabEdit</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<uc1:StammKoerper ID="StammKoerper1" runat="server"></uc1:StammKoerper>
<uc1:PostItKoerper ID="PostItKoerper1" runat="server"></uc1:PostItKoerper>
<div style="clear: both">
    <table id="TopLabEditRumpfTable" cellspacing="1" cellpadding="1" border="0">
        <tr>
            <td valign="top" width="20">
            </td>
            <td valign="top" bgcolor="whitesmoke">
                Titel
            </td>
            <td>
                <asp:TextBox ID="TitelTextBox" runat="server" Width="443px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
            <td valign="top" bgcolor="whitesmoke">
                Antwort
            </td>
            <td>
                <asp:TextBox ID="TopLabTextBox" runat="server" Width="439px" CssClass="TopLab" Height="194px"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
            <td valign="top" bgcolor="whitesmoke">
                Bild
            </td>
            <td>
                <p>
                    <asp:TextBox ID="DateiTextBox" runat="server" Width="442px"></asp:TextBox><br />
                    <a href="http://domain.tld/bild.jpg"><font size="1">entweder verlinken (http://domain.tld/bild.jpg)</font>
                    </a><font size="1"> oder lokal<br />
                                    </font>
                    <asp:Image ID="DateiImage" runat="server" Width="50px" Visible="False"></asp:Image>
                    <asp:LinkButton ID="BildPickerLinkButton" runat="server" Font-Bold="True" OnClick="BildPickerLinkButton_Click">auswählen</asp:LinkButton>
                    <uc1:BildPicker ID="BildPicker1" runat="server"></uc1:BildPicker>
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
            <td valign="top" bgcolor="whitesmoke">
                URL
            </td>
            <td>
                <asp:TextBox ID="URLTextBox" runat="server" Width="443px"></asp:TextBox><br />
                <a href="http://www.domain.tld/"><font size="1">http://www.domain.tld/</font></a>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
            <td valign="top" bgcolor="whitesmoke">
                Typ
            </td>
            <td>
                <asp:DropDownList ID="TypDropDownList" runat="server">
                    <asp:ListItem Value="txt">txt</asp:ListItem>
                    <asp:ListItem Value="htm">htm</asp:ListItem>
                    <asp:ListItem Value="xml">xml</asp:ListItem>
                    <asp:ListItem Value="uri">uri</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
            <td valign="top" bgcolor="whitesmoke">
            </td>
            <td>
                <asp:Image ID="Image1" runat="server" Width="25px" ImageUrl="~/images/icons/ok.gif"
                    BorderStyle="None"></asp:Image>
                <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click"></asp:Button>&nbsp;
                <asp:Image ID="Image2" runat="server" Width="25px" ImageUrl="~/images/icons/cancel.gif"
                    BorderStyle="None"></asp:Image>
                <asp:Button ID="CancelButton" runat="server" Text="cancel" OnClick="CancelButton_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
</div>
</asp:Content>